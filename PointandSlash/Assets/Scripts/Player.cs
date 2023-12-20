using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public SpriteRenderer sp;

    //Respawning
    public Transform spawnPoint;

    //HeartPickup
    public int heartValue;

    //health text
    public TextMeshProUGUI healthText;

    //Animation
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1f);
                canBeHurt = true;
            }
        }

        //update health value on health bar
        healthText.text = health.health.ToString();

        //Animation
        if(Vector2.Distance(transform.position, target.position) != 0)
        {
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }

    void StopDash()
    {
        isDashing = false;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        RestoreStats();
        transform.position = spawnPoint.position;
        target.position = spawnPoint.position;
    }

    void RestoreStats()
    {
        health.health = health.maxHealth;
        magic.manaAmount = magic.maxMana;
    }

    public void Teleport(Transform point)
    {
        RestoreStats();
        transform.position = point.position;
        target.position = point.position;
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
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.75f);
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
