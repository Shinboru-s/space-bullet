using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator screenAnim;
    public GameObject hoverObject;
    private int currentLevel;
    private GameObject[] levelButtons;
    void Start()
    {
        Time.timeScale = 1;
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject item in levelButtons)
        {
            CheckButton(item);
        }
    }


    void Awake()
    {
        GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().Load();
    }

    public void ButtonSelected(GameObject button)
    {
        button.GetComponent<Animator>().SetTrigger("Selected");
        hoverObject.SetActive(true);
        hoverObject.GetComponent<RectTransform>().position = button.GetComponent<RectTransform>().position;
    }

    public void ButtonUnselected(GameObject button)
    {

        hoverObject.SetActive(false);
        
    }


    public void ToSettingsScreen()
    {
        screenAnim.SetTrigger("toSettings");
    }

    public void BackToMenu()
    {
        screenAnim.SetTrigger("toMenu");
    }

    public void ToLevelsScreen()
    {
        screenAnim.SetTrigger("toLevels");
    }

    public void LevelButtonSelected(GameObject button)
    {
        button.GetComponent<Animator>().SetBool("isCurserOnButton",true);
    }

    public void LevelButtonUnelected(GameObject button)
    {
        button.GetComponent<Animator>().SetBool("isCurserOnButton", false);
    }

    public void LevelSelect(GameObject button)
    {
        SceneManager.LoadScene(button.name);
    }

    public void NewGame()
    {
        GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().CreateNewGame();
        SceneManager.LoadScene("Level-1");
    }
    public void Continue()
    {
        SceneManager.LoadScene("Level-"+ (GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().FindCurrentLevel()));
    }

    public void CheckButton(GameObject button)
    {
        currentLevel = GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().FindCurrentLevel();
        if(currentLevel < int.Parse(button.name))
        {
            button.GetComponent<Button>().enabled = false;
            button.GetComponent<Animator>().enabled = false;
            button.GetComponent<Image>().color = new Color32(140, 140, 140, 255);
        }
    }
}
