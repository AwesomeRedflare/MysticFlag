using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Enemy enemy;
    private Health health;
    private Rigidbody2D rb;
    private GameObject player;

    public float chargeAttackWait;
    public float chargeSpeed;
    public float tellTime;

    public bool facePlayer = false;
    public bool isChargeAttack;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine("ChargeAttack");
    }

    private void Update()
    {
        if(facePlayer == true)
        {
            FacePlayer();
        }

        if (isChargeAttack)
        {
            rb.velocity = transform.up * chargeSpeed * Time.fixedDeltaTime;
        }
    }

    void FacePlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    public void StartBoss()
    {
        StartCoroutine("ChargeAttack");
    }

    IEnumerator ChargeAttack()
    {
        facePlayer = true;

        for (int i = 0; i < Random.Range(3f, 5f); i++)
        {
            yield return new WaitForSeconds(chargeAttackWait - tellTime);

            facePlayer = true;
            enemy.TellOn();

            yield return new WaitForSeconds(tellTime);

            enemy.TellOff();

            facePlayer = false;
            isChargeAttack = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player pla = col.gameObject.GetComponent<Player>();

            pla.SetInvicibility((int)enemy.attackDamage);
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            isChargeAttack = false;
            rb.velocity = Vector2.zero;
        }

    }
}
