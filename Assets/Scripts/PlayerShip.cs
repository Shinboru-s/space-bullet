using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public bool isGameEnd = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet") && isGameEnd == false)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GameOverScreen();
            
        }

    }
}
