using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseObjects;
    public GameObject shootArea;
    private bool shootAreaActive = false;
    private bool isPaused = false;
    public GameObject Level;

    public GameObject[] inGameObjects;

    public string nextLevelName;

    private bool isGameStarted = false;
    private bool loadLevel = false;

    public GameObject gameOver;
    private bool isGameOver = false;

    public bool isTutorial;
    void Start()
    {
        isGameOver = false;
        gameOver.SetActive(false);

        foreach (GameObject item in inGameObjects)
        {
            item.SetActive(false);
        }
        pauseObjects.SetActive(false);
        shootArea.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        Level.GetComponent<Text>().text = SceneManager.GetActiveScene().name;
    }


    void Update()
    {
        if (Level.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && isGameStarted == false)
        {
            StartGame();
            isGameStarted = true;
        }

        if (isGameStarted == true && shootAreaActive == false && GameObject.FindGameObjectWithTag("PlayerHolder").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1) 
        {
            shootAreaActive = true;
            shootArea.SetActive(true);
        }

        if (isGameStarted == true && isGameOver == false && isTutorial == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseGame();

            if (Input.GetKeyDown(KeyCode.R))
                Restart();

            if (GameObject.FindGameObjectWithTag("PlayerHolder").transform.position.y == 7 && loadLevel == false)
            {
                loadLevel = true;
                NextLevel();

            }
        }

       
    }


    private void StartGame()
    {


        foreach (GameObject item in inGameObjects)
        {
            item.SetActive(true);
        }

        if (isTutorial == true)
        {
            GameObject.FindGameObjectWithTag("TutorialManagerTag").GetComponent<TutorialManager>().NextText();
            shootArea.SetActive(false);
        }
            
    }

    public void PauseGame()
    {
        ButtonClickedSound();
        if (isPaused == false)
        {
            pauseObjects.SetActive(true);
            shootArea.SetActive(false);
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            pauseObjects.SetActive(false);
            shootArea.SetActive(true);
            isPaused = false;
            Time.timeScale = 1;
        } 
    }

    public void Restart()
    {
        ButtonClickedSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        ButtonClickedSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonSelected(GameObject button)
    {
        button.GetComponent<Animator>().SetTrigger("Selected");
        FindObjectOfType<AudioManager>().Play("Button");
    }

    public void NextLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int level = int.Parse(sceneName.Substring(sceneName.IndexOf("-")+1));
        level++;
        GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().CheckCurrentLevel(level);
        SceneManager.LoadScene(nextLevelName);
    }

    private GameObject[] EnemyShips;
    public void GameOverScreen()
    {
        FindObjectOfType<AudioManager>().Play("Destroy");
        foreach (var item in GameObject.FindGameObjectsWithTag("DeathParticals"))
        {
            item.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            item.GetComponent<ParticleSystem>().Play();
        }
        
        Destroy(GameObject.FindGameObjectWithTag("PlayerHolder"));

        gameOver.SetActive(true);
        isGameOver = true;

        EnemyShips = GameObject.FindGameObjectsWithTag("Enemy");

        
        foreach (GameObject item in EnemyShips)
        {
            if (item.GetComponent<EnemyShip>() != null)
                item.GetComponent<EnemyShip>().enabled = false;
        }
        //Time.timeScale = 0;

    }

    private void ButtonClickedSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
