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
    private GameObject player;

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

    [Header("Shooter")]
    public bool shooter = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce;
    private float shootTimer = 0.5f;

    private Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    public float offset;

    

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
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
                player = GameObject.FindGameObjectWithTag("Player");
                if (Mathf.Abs(player.transform.position.y - transform.position.y) > 2)
                    playerPosition = new Vector2(transform.position.x, player.transform.position.y);
                else
                    playerPosition = player.transform.position;

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
                    if (invincibleTimer <= 0)
                    {
                        setCooldownTimer = Random.Range(1.0f, 3.0f);
                        invincibleTimer = setInvincibleTimer;
                        cooldownTimer = setCooldownTimer;
                        isInvincible = false;
                        spriteRenderer.sprite = originalSprite;
                    }
                    invincibleTimer -= Time.deltaTime;
                }
            }

            if (shooter == true)
            {
                targetPos = target.position;
                thisPos = transform.position;
                targetPos.x = targetPos.x - thisPos.x;
                targetPos.y = targetPos.y - thisPos.y;
                angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
                firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

                if (shootTimer > 0)
                {
                    shootTimer -= Time.deltaTime;
                }
                else if (shootTimer <= 0)
                {
                    shootTimer = Random.Range(2.0f, 4.0f);
                    //FindObjectOfType<AudioManager>().Play("Shoot");
                    Quaternion rot = Quaternion.Euler(0, 0, firePoint.eulerAngles.z);
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    bullet.name = (transform.eulerAngles.z).ToString();
                    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, 3f);
                    //AmmoUI[ammo - 1].SetActive(false);
                    //ammo -= 1;
                }
            }


            if (GameObject.FindGameObjectWithTag("Indicator").GetComponent<ShipIndicator>().ammo == 0 && GameObject.FindGameObjectWithTag("Bullet") == null)
            {
                //verticalMove = false;
                //canDestroyPlayer = false;
                //invincible = false;
                //shooter = false;
                DestroyPlayer();
            }
        }
    }
    
    
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            transform.parent.GetComponent<Animator>().enabled = false;

            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if (verticalMove == true)
                destination = 1;

            if (shield == true)
                shieldObject.gameObject.SetActive(true);

            if (randomMove == true)
                destination = Random.Range(0.5f, 2.3f);
            if (invincible == true)
                invincibleTimer = setInvincibleTimer;

            cameraShake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
        }
    }
    private CameraShake cameraShake;
    private GameObject[] deathParticals;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && isInvincible == false)
        {
            FindObjectOfType<AudioManager>().Play("Destroy");
            deathParticals = GameObject.FindGameObjectsWithTag("DeathParticals");
            cameraShake.CamShake();
            //StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
            foreach (var item in deathParticals)
            {
                item.transform.position = transform.position;
                item.GetComponent<ParticleSystem>().Play();
            }
            GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().EnemySpawnerMethod();
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Bullet" && isInvincible == true)
            FindObjectOfType<AudioManager>().Play("Bounce");
    }

    
    public void DestroyPlayer()
    {
        if (shield == true)
        {
            shieldObject.gameObject.SetActive(false);

        }

        verticalMove = false;
        horizontalMove = false;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        this.transform.position = Vector2.MoveTowards(this.transform.position, playerPosition, destroySpeed * Time.deltaTime);
    }
}
