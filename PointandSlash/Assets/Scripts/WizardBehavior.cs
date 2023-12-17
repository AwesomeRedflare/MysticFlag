using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBehavior : MonoBehaviour
{
    private Enemy enemy;
    private GameObject player;
    public Transform rotator;
    public GameObject evilBall;

    public float speed;

    private bool inRange = false;
    private bool inCloseRange = false;
    public float range;
    public float closeRange;

    private Animator anim;

    //Attackin
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    [SerializeField]

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeBtwAttack = startTimeBtwAttack;
    }

    private void Update()
    {
        //checks to see if inrange

        if (Vector2.Distance(transform.position, player.transform.position) <= range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= closeRange)
        {
            inCloseRange = true;
        }
        else
        {
            inCloseRange = false;
            anim.SetBool("moving", false);
        }

        //when player is in range
        if (inRange == true)
        {
            FacePlayer();

            //checks to see if enemy can attack yet
            if (timeBtwAttack <= 0)
            {
                enemy.TellOff();
                Vector2 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                Instantiate(evilBall, rotator.transform.GetChild(0).transform.position, rotator.rotation);

                timeBtwAttack = startTimeBtwAttack;
            }
            else if (timeBtwAttack >= 0)
            {
                if(inCloseRange == true)
                {
                    if(timeBtwAttack >= startTimeBtwAttack / 2)
                    {
                        anim.SetBool("moving", true);
                        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
                    }
                    else
                    {
                        anim.SetBool("moving", false);
                    }
                }

                if (timeBtwAttack <= startTimeBtwAttack / 4 && enemy.tell.activeSelf == false)
                {
                    enemy.TellOn();
                }
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    void FacePlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        rotator.rotation = rotation;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (col.gameObject.CompareTag("Player"))
        {
            Player pla = col.gameObject.GetComponent<Player>();

            pla.SetInvicibility((int)enemy.attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, closeRange);
    }
}
