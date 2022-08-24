using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase3 : StateMachineBehaviour
{
    private bool returnWeaponPos = false;
    private GameObject weapon;
    private float destination;
    private Vector2 destinationPoint;
    public float horizontalSpeed;

    public GameObject bulletPrefab;
    private Transform firePoint;
    public float bulletForce;
    private float shootTimer = 0.5f;

    private Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;
    public float offset;
    private int ammo = 1;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        weapon = GameObject.FindGameObjectWithTag("BossWeapon");
        firePoint = weapon.transform;
        destination = Random.Range(-2.0f, 2.0f);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (stateInfo.normalizedTime > 1)
        {
            bossWeapon();
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private void bossWeapon()
    {
        if (target != null && returnWeaponPos == false)
        {
            if (ammo == 0 && GameObject.FindGameObjectWithTag("EnemyBullet") == null)
            {
                returnWeaponPos = true;
                destination = 0f;
            }

            destinationPoint = new Vector2(destination, weapon.transform.position.y);
            weapon.transform.position = Vector2.MoveTowards(weapon.transform.position, destinationPoint, horizontalSpeed * Time.deltaTime);

            if (weapon.transform.position.x == destination)
                destination = Random.Range(-2.0f, 2.0f);

            targetPos = target.position;
            thisPos = weapon.transform.position;
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
                shootTimer = Random.Range(3.0f, 6.0f);
                Quaternion rot = Quaternion.Euler(0, 0, firePoint.eulerAngles.z);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rot);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                bullet.name = (firePoint.eulerAngles.z).ToString();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                Destroy(bullet, 3f);
                ammo -= 1;
            }
        }
        else
        {
            //destinationPoint = new Vector2(0, 0.1244f);
            weapon.transform.position = Vector2.MoveTowards(weapon.transform.position, destinationPoint, horizontalSpeed * Time.deltaTime);
            targetPos = target.position;
            thisPos = weapon.transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle + offset));

            if (weapon.transform.position.x == destinationPoint.x) 
                GameObject.FindGameObjectWithTag("BossHolder").GetComponent<Animator>().SetTrigger("BackToIdle");

        }
        
    }
}
