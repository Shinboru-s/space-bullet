using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    private float bulletForce;
    private int bounceCount = 1;

    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        bulletForce = GameObject.FindGameObjectWithTag("Indicator").GetComponent<ShipIndicator>().bulletForce;

    }

    private float timeBtwSpawns;
    public float setTimeBtwSpawns;
    public GameObject trail;
    public bool enemyBullet;

    private void Update()
    {
        if (enemyBullet == false)
        {
            if (timeBtwSpawns <= 0)
            {
                GameObject trailObject = Instantiate(trail, transform.position, Quaternion.identity);
                timeBtwSpawns = setTimeBtwSpawns;
                Destroy(trailObject, 0.5f);
            }
            else
                timeBtwSpawns -= Time.deltaTime;
        }
        
    }

    private float degree;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounceCount == 0)
            Destroy(this.gameObject);


            

        if (collision.gameObject.tag == "Wall")
        {

            rb.velocity = Vector2.zero;
            degree = float.Parse(this.gameObject.name);
            degree += degree;
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z - degree);
            rb.AddForce(gameObject.transform.up * bulletForce, ForceMode2D.Impulse);
            this.gameObject.name = (this.transform.eulerAngles.z).ToString();
            bounceCount--;
            FindObjectOfType<AudioManager>().Play("Bounce");
        }

        else if (collision.gameObject.tag == "NotBounce")
        {
            FindObjectOfType<AudioManager>().Play("Bounce");
            Destroy(this.gameObject);
        }

        else
            Destroy(this.gameObject);



    }
}
