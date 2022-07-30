using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseObjects;
    public GameObject shootArea;
    private bool isPaused = false;
    public GameObject Level;

    public GameObject[] inGameObjects;

    public string nextLevelName;

    private bool isGameStarted = false;
    private bool loadLevel = false;

    public GameObject gameOver;
    private bool isGameOver = false;
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

        if (isGameStarted == true && isGameOver == false)
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
        shootArea.SetActive(true);

        foreach (GameObject item in inGameObjects)
        {
            item.SetActive(true);
        }
    }

    public void PauseGame()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ButtonSelected(GameObject button)
    {
        button.GetComponent<Animator>().SetTrigger("Selected");
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
            Debug.Log(item.name);
            item.GetComponent<EnemyShip>().enabled = false;
        }
        //Time.timeScale = 0;

    }
}
