using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIndicator : MonoBehaviour
{   

    public Transform firePoint;
    public Transform testPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    private bool canShoot = true;
    public int ammo;
    public GameObject[] AmmoUI;

    void Start()
    {

    }
    

    void Update()
    {
        if (ammo == 0 && canShoot == true)
        {
            canShoot = false;
            gameObject.GetComponent<Animator>().enabled = false;
            for (int i = 0; i < 5; i++)
            {
                this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            //GameObject.FindGameObjectsWithTag("Enemy"); GetComponent<EnemyShip>().DestroyPlayer();
            //foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
            //{
            //    if (item.GetComponent<EnemyShip>().enabled == true)
            //    {
            //        item.GetComponent<EnemyShip>().DestroyPlayer();
            //        Debug.Log(item.name);
            //        break;
            //    }
                    

            //}
        }


       
        
    }

    public void Fire()
    {
        if (canShoot == true)
        {
            FindObjectOfType<AudioManager>().Play("Shoot");
            Quaternion rot = Quaternion.Euler(0, 0, transform.eulerAngles.z);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.name = (transform.eulerAngles.z).ToString();
            rb.AddForce(testPoint.up * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, 3f);
            AmmoUI[ammo - 1].SetActive(false);
            ammo -= 1;
        }
    }
}
