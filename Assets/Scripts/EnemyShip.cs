using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public float destroySpeed = 4f;

    [Header("Horizontal Movement")]
    public bool horizontalMove = false;
    public bool randomMove = false;
    private float destination = 2.3f;
    public float horizontalSpeed = 1f;
    private Vector2 destinationPoint;


    [Header("Vertical Movement")]
    public bool verticalMove = false;
    private Vector2 playerPosition;
    public float verticalSpeed = 1f;

    [Header("Shield")]
    public bool shield = false;
    public Transform shieldObject;
    private float shieldRotationSpeed = 150f;

    [Header("Invincible")]
    public bool invincible = false;
    public Sprite invincibleSprite;
    public Sprite originalSprite;
    private bool isInvincible = false;
    public float setInvincibleTimer;
    private float invincibleTimer;
    public float setCooldownTimer;
    private float cooldownTimer;
    private SpriteRenderer spriteRenderer;

    void Update()
    {
        if (horizontalMove == true)
        {
            destinationPoint = new Vector2(destination, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, destinationPoint, horizontalSpeed * Time.deltaTime);

            if (transform.position.x == destination)
            {
                if (randomMove == true)
                    destination = Random.Range(-2.0f, 2.0f);

                else
                    destination *= -1;
            }
        }


        if (verticalMove == true)
        {
            playerPosition= new Vector2(transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, playerPosition, verticalSpeed * Time.deltaTime);

            
        }

        if (shield == true)
        {
            shieldObject.eulerAngles = new Vector3(shieldObject.eulerAngles.x, shieldObject.eulerAngles.y, shieldObject.eulerAngles.z + shieldRotationSpeed * Time.deltaTime);
            
        }

        if (invincible == true)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else if (cooldownTimer <= 0)
            {
                spriteRenderer.sprite = invincibleSprite;
                isInvincible = true;
                if(invincibleTimer <= 0)
                {
                    setCooldownTimer = Random.Range(2.0f, 5.0f);
                    invincibleTimer = setInvincibleTimer;
                    cooldownTimer = setCooldownTimer;
                    isInvincible = false;
                    spriteRenderer.sprite = originalSprite;
                }
                invincibleTimer -= Time.deltaTime;
            }
        }
    }

    
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (verticalMove == true)
            destination = 1;

        if (shield == true)
            shieldObject.gameObject.SetActive(true);

        if (randomMove == true)
            destination = Random.Range(0.5f, 2.3f);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && isInvincible == false)
        {
            Destroy(this.gameObject);
        }
    }

    
    public void DestroyPlayer()
    {
        verticalMove = false;
        horizontalMove = false;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, destroySpeed * Time.deltaTime);
    }
}
