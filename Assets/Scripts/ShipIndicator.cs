using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIndicator : MonoBehaviour
{   
    //public float rotationSpeed;
    //public float setRotationReverseTime;
    //private float rotationReverseTime;

    public Transform firePoint;
    public Transform testPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;


    void Start()
    {
        //rotationReverseTime = setRotationReverseTime / 2;
    }
    private bool canShoot = true;
    public int ammo;
    void Update()
    {
        if (ammo == 0 && canShoot == true)
        {
            canShoot = false;
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyShip>().DestroyPlayer();
        }


        //if (rotationReverseTime <= 0)
        //{
        //    rotationSpeed *= -1f;
        //    rotationReverseTime = setRotationReverseTime;
        //}
        //else
        //    rotationReverseTime -= Time.deltaTime;

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotationSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1") && canShoot == true) {

            Quaternion rot = Quaternion.Euler(0, 0, transform.eulerAngles.z);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.name = (transform.eulerAngles.z).ToString();
            rb.AddForce(testPoint.up * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, 3f);

            ammo -= 1;
        }
    }
}
