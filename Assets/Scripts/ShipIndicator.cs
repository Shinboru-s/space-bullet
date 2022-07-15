using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIndicator : MonoBehaviour
{   public float rotationSpeed;

    public Transform firePoint;
    public Transform testPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;


    void Start()
    {
        
    }
    private bool canShoot = true;
    public int ammo = 5;
    void Update()
    {
        if (ammo == 0)
        {
            canShoot = false;
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyShip>().DestroyPlayer();
        }
            

        if (transform.eulerAngles.z >= 170 || transform.eulerAngles.z <= 10)
            rotationSpeed *= -1f;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + rotationSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1") && canShoot == true) {

            Quaternion rot = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.name = (transform.eulerAngles.z - 90).ToString();
            rb.AddForce(testPoint.right * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, 3f);

            ammo -= 1;
        }
    }
}
