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
        if(ShipCount > counter)
        {
            EnemyShips[counter].transform.parent.gameObject.GetComponent<Animator>().enabled = true;
            counter++;
        }
        else
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().backgroundColor = Color.green;

        
    }
}
