using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Health health;
    private Magic magic;
    private Camera cam;
    public Rigidbody2D rb;

    //base variables
    public float speed = 5f;
    public Transform target;
    private float angle;
    private Vector2 direction;

    //dash variables;
    public bool canDash = false;
    public bool isDashing = false;
    public float speedMultiplier;
    private float timeBtwDash;
    public float dashTime;
    public float dashLenght;

    //invicibility frames
    public bool canBeHurt = true;
    public float damageTime;
    public float damageTimeStart;

    //Respawning
    public Transform spawnPoint;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
        health = GetComponent<Health>();
        magic = GetComponent<Magic>();
        target.position = transform.position;
    }

    void Update()
    {
        // Code to move player
        if(isDashing == false || canDash == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        // Code to face player to mouse
        direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;

        //Dashing
        if (isDashing == true && canDash == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * speedMultiplier * Time.deltaTime);
        }

        if(timeBtwDash <= 0 && canDash == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                timeBtwDash = dashTime;
                Debug.Log("check speed");
                isDashing = true;
                Invoke("StopDash", dashLenght);
            }
        }
        else
        {
            timeBtwDash -= Time.deltaTime;
        }

        if(canBeHurt == false)
        {
            damageTime -= Time.deltaTime;

            if(damageTime <= 0)
            {
                canBeHurt = true;
            }
        }

    }

    void StopDash()
    {
        isDashing = false;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        health.health = health.maxHealth;
        magic.manaAmount = magic.maxMana;
        transform.position = spawnPoint.position;
        target.position = spawnPoint.position;
    }

    /*
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && canBeHurt == true)
        {
            health.TakeDamage(20);

            canBeHurt = false;
            damageTime = damageTimeStart;
        }
    }
    */

    public void SetInvicibility()
    {
        canBeHurt = false;
        damageTime = damageTimeStart;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("NPC") == true)
        {
            col.GetComponent<NPC>().Speak();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("NPC") == true)
        {
            col.GetComponent<NPC>().StopSpeak();
        }
    }
}
