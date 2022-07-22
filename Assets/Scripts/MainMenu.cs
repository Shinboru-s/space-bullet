using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator screenAnim;
    public GameObject hoverObject;
    void Start()
    {
        
    }


    void Update()
    {

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

}
