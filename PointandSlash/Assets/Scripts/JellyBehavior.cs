using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBehavior : MonoBehaviour
{
    private Enemy enemy;
    private GameObject player;
    private Rigidbody2D rb;

    public float speed;

    private bool inRange = false;
    public float range;

    //Attackin
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    [SerializeField]
    private bool isAttacking;

    private float attackTimer;
    public float attackLenght;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeBtwAttack = startTimeBtwAttack;
    }

    private void Update()
    {
        //checks to see if inrange
        if(inRange == false)
        {
            if(Vector2.Distance(transform.position, player.transform.position) <= range)
            {
                inRange = true;
            }
        }

        //when player is in range
        if(inRange== true)
        {
            //faces player when in range
            if(isAttacking == false)
            {
                FacePlayer();
            }

            //checks to see if enemy can attack yet
            if(timeBtwAttack <=0 && isAttacking == false)
            {
                FacePlayer();
                isAttacking = true;
                rb.velocity = Vector2.zero;
                timeBtwAttack = startTimeBtwAttack;
                attackTimer = attackLenght;
            }
            else if (timeBtwAttack >= 0 && isAttacking == false)
            {
                timeBtwAttack -= Time.deltaTime;
            }

            //attack which is basically a projectile
            if(isAttacking == true)
            {
                if(attackTimer >= 0)
                {
                    rb.velocity = transform.up * speed * Time.fixedDeltaTime;
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    isAttacking = false;
                }
            }
        }
    }

    void FacePlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isAttacking = false;
        rb.velocity = Vector2.zero;

        if (col.gameObject.CompareTag("Player"))
        {
            Player pla = col.gameObject.GetComponent<Player>();

            if(pla.canBeHurt == true)
            {
                col.gameObject.GetComponent<Health>().TakeDamage((int)enemy.attackDamage);
            }
            pla.rb.velocity = Vector2.zero;

            pla.SetInvicibility();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
