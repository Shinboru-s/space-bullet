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
    void Start()
    {
        pauseObjects.SetActive(false);
        shootArea.SetActive(true);
        isPaused = false;
        Time.timeScale = 1;
        Level.GetComponent<Text>().text = SceneManager.GetActiveScene().name;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();

        if (Input.GetKeyDown(KeyCode.R))
            Restart();
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
}
