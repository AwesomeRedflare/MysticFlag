using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float damage;
    public string attackee;

    private void Start()
    {
        //FindObjectOfType<AudioManager>().Play("shoot");
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(attackee))
        {
            if(attackee == "Player")
            {
                col.gameObject.GetComponent<Player>().SetInvicibility((int)damage);
            }
            else
            {
                col.gameObject.GetComponent<Health>().TakeDamage((int)damage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
