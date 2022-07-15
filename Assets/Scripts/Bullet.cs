using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public Sprite[] bulletSprites;
    private float bulletForce;
    private int bounceCount = 2;

    private void Start()
    {
        spriteRenderer.sprite = bulletSprites[Random.Range(0, bulletSprites.Length)];
        rb = GetComponent<Rigidbody2D>();
        bulletForce = GameObject.FindGameObjectWithTag("Indicator").GetComponent<ShipIndicator>().bulletForce;

    }

    private float degree;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = Vector2.zero;
            degree = float.Parse(this.gameObject.name);
            degree += degree;
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, this.transform.eulerAngles.z - degree);
            rb.AddForce(gameObject.transform.up * bulletForce, ForceMode2D.Impulse);
            this.gameObject.name = (this.transform.eulerAngles.z).ToString();
            bounceCount--;
        }
        else
            Destroy(this.gameObject);

        if(bounceCount==0)
            Destroy(this.gameObject);
    }
}
