using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    

    public bool Phase1 = false;


    public bool Phase2 = false;

    [Header("Phase 3")]
    public bool Phase3 = false;

    private bool returnWeaponPos = false;
    public GameObject weapon;
    private float destination;
    private Vector2 destinationPoint;
    public float horizontalSpeed;
    
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce;
    private float shootTimer = 0.5f;

    private Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    public float offset;
    private int ammo = 4;



    private Animator anim;
    void Start()
    {
        anim = transform.parent.gameObject.GetComponent<Animator>();
        destination = Random.Range(-2.0f, 2.0f);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetTrigger("Phase3");
        }

        while (returnWeaponPos == true)
        {
            weapon.transform.position = Vector2.MoveTowards(weapon.transform.position, destinationPoint, horizontalSpeed * Time.deltaTime);
        }

        




        //if (invincible == true)
        //{
        //    if (cooldownTimer > 0)
        //    {
        //        cooldownTimer -= Time.deltaTime;
        //    }
        //    else if (cooldownTimer <= 0)
        //    {
        //        spriteRenderer.sprite = invincibleSprite;
        //        isInvincible = true;
        //        if (invincibleTimer <= 0)
        //        {
        //            setCooldownTimer = Random.Range(1.0f, 3.0f);
        //            invincibleTimer = setInvincibleTimer;
        //            cooldownTimer = setCooldownTimer;
        //            isInvincible = false;
        //            spriteRenderer.sprite = originalSprite;
        //        }
        //        invincibleTimer -= Time.deltaTime;
        //    }
        //}

    }
}
