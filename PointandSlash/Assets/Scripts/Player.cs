using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject chatBox;
    private Health health;
    private Magic magic;
    private Dash dash;
    private Camera cam;
    public Rigidbody2D rb;
    public Transform pivot;

    //base variables
    public float speed = 5f;
    public Transform target;
    private float angle;
    private Vector2 direction;

    //dash variables;
    public bool isDashing = false;

    //invicibility frames
    public bool canBeHurt = true;
    public float damageTime;
    public float damageTimeStart;

    //Respawning
    public Transform spawnPoint;

    //HeartPickup
    public int heartValue;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
        health = GetComponent<Health>();
        magic = GetComponent<Magic>();
        dash = GetComponent<Dash>();
        target.position = transform.position;
    }

    void Update()
    {
        // Code to move player
        if(isDashing == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.zero;
        }

        // Code to face player to mouse
        direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        pivot.rotation = rotation;

        //Dashing
        if (isDashing == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * dash.speedMultiplier * Time.deltaTime);
        }

        //Invibility logic or something
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


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Heart"))
        {
            health.HealHealth(heartValue);
            Destroy(col.gameObject);
        }

        if (col.transform.CompareTag("Wall"))
        {
            target.position = transform.position;
        }
    }


    public void SetInvicibility(int damage)
    {
        if(canBeHurt == true && isDashing == false)
        {
            health.TakeDamage(damage);
        }
        rb.velocity = Vector2.zero;

        if(damageTime <= 0)
        {
            canBeHurt = false;
            damageTime = damageTimeStart;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("NPC") == true)
        {
            chatBox.SetActive(true);
            chatBox.GetComponent<Animator>().SetTrigger("chat");
            col.GetComponent<NPC>().Speak();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("NPC") == true)
        {
            //col.GetComponent<NPC>().StopSpeak();
            //chatBox.SetActive(false);
            chatBox.GetComponent<Animator>().SetTrigger("out");
        }
    }
}
