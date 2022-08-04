using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public Animator screenAnim;
    public Animator backgroundAnim;
    //public GameObject hoverObject;
    private int currentLevel;
    private GameObject[] levelButtons;

    public GameObject continueButton;
    public GameObject continueText;
    void Start()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            continueButton.GetComponent<Button>().enabled = true;
            continueButton.GetComponent<Animator>().enabled = true;
            continueButton.GetComponent<Image>().color = new Color32(255, 255, 255, 220);
            continueText.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        }
        GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().Load();

        Time.timeScale = 1;
        levelButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject item in levelButtons)
        {
            CheckButton(item);
        }

        foreach (GameObject item in GameObject.FindGameObjectsWithTag("LevelButtonText"))
        {
            CheckButtonTexts(item);
        }

        
    }


    public void ButtonSelected(GameObject button)
    {
        button.GetComponent<Animator>().SetTrigger("Selected");
        FindObjectOfType<AudioManager>().Play("Button");
        //hoverObject.SetActive(true);
        //hoverObject.GetComponent<RectTransform>().position = button.GetComponent<RectTransform>().position;
    }

    public void ButtonUnselected(GameObject button)
    {

        //hoverObject.SetActive(false);
        
    }


    public void ToSettingsScreen()
    {
        screenAnim.SetTrigger("toSettings");
        backgroundAnim.SetTrigger("toSettings");
        ButtonClickedSound();



    }

    public void BackToMenu()
    {
        screenAnim.SetTrigger("toMenu");
        backgroundAnim.SetTrigger("toMenu");
        ButtonClickedSound();
    }

    public void ToLevelsScreen()
    {
        screenAnim.SetTrigger("toLevels");
        backgroundAnim.SetTrigger("toLevels");
        ButtonClickedSound();
    }
    public void ToMoreLevelsScreen()
    {
        screenAnim.SetTrigger("toMoreLevels");
        backgroundAnim.SetTrigger("toMoreLevels");
        ButtonClickedSound();
    }

    public void LevelButtonSelected(GameObject button)
    {
        button.GetComponent<Animator>().SetBool("isCurserOnButton",true);

        if (button.GetComponent<Animator>().enabled == true)
            FindObjectOfType<AudioManager>().Play("Button");
    }

    public void LevelButtonUnelected(GameObject button)
    {
        button.GetComponent<Animator>().SetBool("isCurserOnButton", false);
    }

    public void LevelSelect(GameObject button)
    {
        ButtonClickedSound();
        SceneManager.LoadScene(button.name);
    }

    public void NewGame()
    {
        ButtonClickedSound();
        GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().CreateNewGame();
        SceneManager.LoadScene("Tutorial");
    }
    public void Continue()
    {
        ButtonClickedSound();
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
    public void CheckButtonTexts(GameObject buttonText)
    {
        currentLevel = GameObject.FindGameObjectWithTag("SaveSystem").GetComponent<SaveSystem>().FindCurrentLevel();
        if (currentLevel < int.Parse(buttonText.GetComponent<Text>().text))
        {
            buttonText.GetComponent<Text>().color = new Color32(140, 140, 140, 255);
        }
    }

    private void ButtonClickedSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void SliderValueChanged()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().ValueUpdate();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
