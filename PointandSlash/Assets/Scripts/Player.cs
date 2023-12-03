using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //base variables
    private Rigidbody2D rb;
    public float speed = 5f;
    public Transform target;
    public Camera cam;
    private float angle;
    private Vector2 direction;

    //dash variables;
    public bool canDash = false;
    public bool isDashing = false;
    public float speedMultiplier;
    private float timeBtwDash;
    public float dashTime;
    public float dashLenght;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cam = FindObjectOfType<Camera>();
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

    }

    void StopDash()
    {
        isDashing = false;
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
