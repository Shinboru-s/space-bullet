using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] EnemyShips;
    private int ShipCount = 0;
 
    void Start()
    {
        EnemyShips = GameObject.FindGameObjectsWithTag("Enemy");
        ShipCount = EnemyShips.Length;

        EnemySpawnerMethod();
    }

    void Update()
    {
        
    }

    private int counter = 0;
    public void EnemySpawnerMethod()
    {
        if (ShipCount > counter && GameObject.FindGameObjectWithTag("Player") != null)
        {
            EnemyShips[counter].transform.parent.gameObject.GetComponent<Animator>().enabled = true;
            counter++;
        }
        else if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Animator>().SetTrigger("levelEnded");
            GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerShip>().isGameEnd = true;

        }
            


    }
}
