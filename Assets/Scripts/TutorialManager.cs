using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialTextHolder;
    private int counter = -1;
    private bool tutorialStart = false;

    public GameObject tapToNext;
    private float tapToNextTimer;
    public float setTapToNextTimer;


    

    private void Start()
    {

    }

    void Update()
    {
        //if (GameObject.FindGameObjectWithTag("PlayerHolder").transform.position.y == -6 && tutorialStart == false)
        //{
        //    tutorialStart = true;
        //    NextText();
        //}
        if (tutorialStart == true)
        {
            if (tapToNextTimer <= 0)
            {
                tapToNext.GetComponent<Animator>().enabled = true;
            }
            else
                tapToNextTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
            NextText();
    }

    public void NextText()
    {
        tutorialStart = true;
        tapToNext.GetComponent<Text>().color = new Color32(255, 255, 255, 0);
        tapToNext.GetComponent<Animator>().enabled = false;
        tapToNextTimer = setTapToNextTimer;
        counter ++ ;

        if (counter == tutorialTextHolder.transform.childCount)
            SceneManager.LoadScene("Level-1");

        else
        {
            if (counter > 0) 
                tutorialTextHolder.transform.GetChild(counter - 1).gameObject.SetActive(false);

            tutorialTextHolder.transform.GetChild(counter).gameObject.SetActive(true);
        }

        if (counter == 4)
            GameObject.FindGameObjectWithTag("Indicator").GetComponent<ShipIndicator>().Fire();

    }
}
