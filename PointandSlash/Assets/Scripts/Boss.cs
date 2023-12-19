using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Enemy enemy;
    private Health health;
    private Rigidbody2D rb;
    private BoxCollider2D boxcol;
    private GameObject player;

    public float tellTime;

    //charge attack
    public int minCharge, maxCharge;
    public float chargeAttackWait;
    public float chargeSpeed;
    public int chargeCount;
    public bool isChargeAttack;
    public bool facePlayer = false;

    //ProjectileAttack
    public GameObject projectile;
    public Transform[] projSpawn;
    public int spawnAmount;
    public float spawnRate;

    //Retreat
    public Transform retreatPoint;
    public float moveSpeed;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        boxcol = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        StartCycle();
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

    public void StartCycle()
    {
        chargeCount = Random.Range(minCharge, maxCharge + 1);
        StartCoroutine("ChargeAttack");
    }

    IEnumerator Retreat()
    {
        Debug.Log("Retreat");
        boxcol.enabled = false;
        transform.rotation = Quaternion.identity;

        while(transform.position != retreatPoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, retreatPoint.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        StartCoroutine("RainAttack");
    }

    IEnumerator BackToStage()
    {
        Debug.Log("Backtostage");
        while (transform.position != transform.parent.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        boxcol.enabled = true;
        StartCycle();
    }

    IEnumerator ChargeAttack()
    {
        Debug.Log("charge");
        if (chargeCount > 0)
        {
            chargeCount -= 1;

            facePlayer = true;

            yield return new WaitForSeconds(chargeAttackWait - tellTime);

            enemy.TellOn();

            yield return new WaitForSeconds(tellTime);

            enemy.TellOff();

            facePlayer = false;
            isChargeAttack = true;

            yield break;
        }
        else if(chargeCount <= 0)
        {
            StartCoroutine("Retreat");
        }
    }

    IEnumerator RainAttack()
    {
        Debug.Log("rain");
        for (int i = 0; i < spawnAmount; i++)
        {
            int spawn = Random.Range(0, projSpawn.Length);

            Instantiate(projectile, projSpawn[spawn].position, projSpawn[spawn].rotation);

            yield return new WaitForSeconds(spawnRate);
        }

        StartCoroutine("BackToStage");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player pla = col.gameObject.GetComponent<Player>();

            pla.SetInvicibility((int)enemy.attackDamage);
        }

        if (col.gameObject.CompareTag("Wall") && isChargeAttack == true)
        {
            isChargeAttack = false;
            rb.velocity = Vector2.zero;
            StartCoroutine("ChargeAttack");
        }

    }
}
