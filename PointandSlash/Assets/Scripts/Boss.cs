using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Enemy enemy;
    private Health health;
    private Rigidbody2D rb;
    private PolygonCollider2D boxcol;
    private GameObject player;

    public float tellTime;

    //charge attack
    public int minCharge, maxCharge;
    public float chargeAttackWait;
    public float chargeSpeed;
    public int chargeCount;
    public bool isChargeAttack;
    public bool facePlayer = false;
    private Vector2 direction;

    //ProjectileAttack
    public GameObject projectile;
    public Transform[] projSpawn;
    public int spawnAmount;
    public float spawnRate;

    //Retreat
    public Transform retreatPoint;
    public float moveSpeed;

    //Spawning Enemies
    public Transform[] enemySpawnPoints;
    public GameObject[] enemies;
    private List<GameObject> aliveEnemies = new List<GameObject>();
    //public int enemySpawnAmount;
    public float enemySpawnRate;
    //private bool hasSpawnedEnemies = false;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        boxcol = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        //StartCycle();
    }

    private void Update()
    {
        /*
        if(facePlayer == true)
        {
            FacePlayer();
        }
        */

        if (isChargeAttack)
        {
            rb.velocity = direction.normalized * chargeSpeed * Time.fixedDeltaTime;
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

    /*
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
    */

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

            direction = player.transform.position - transform.position;

            facePlayer = false;
            isChargeAttack = true;

            yield break;
        }
        else if(chargeCount <= 0)
        {
            if (health.health <= health.maxHealth / 2)
            {
                int chance = Random.Range(0, 2);
                if(chance == 1)
                {
                    StartCoroutine("SpawnEnemies");
                }
                else
                {
                    StartCoroutine("RainAttack");
                }
            }
            else
            {
                StartCoroutine("RainAttack");
            }
        }
    }

    IEnumerator RainAttack()
    {
        //Debug.Log("Retreat");
        boxcol.enabled = false;
        transform.rotation = Quaternion.identity;

        while (transform.position != retreatPoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, retreatPoint.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        Debug.Log("rain");
        for (int i = 0; i < spawnAmount; i++)
        {
            int spawn = Random.Range(0, projSpawn.Length);

            Instantiate(projectile, projSpawn[spawn].position, projSpawn[spawn].rotation);

            yield return new WaitForSeconds(spawnRate);
        }

        //Debug.Log("Backtostage");
        while (transform.position != transform.parent.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        boxcol.enabled = true;

        chargeCount = Random.Range(minCharge, maxCharge + 1);

        if (health.health <= health.maxHealth / 2)
        {
            int chance = Random.Range(0, 3);
            if (chance == 1)
            {
                StartCoroutine("SpawnEnemies");
            }
            else
            {
                StartCoroutine("ChargeAttack");
            }
        }
        else
        {
            StartCoroutine("ChargeAttack");
        }
    }

    IEnumerator SpawnEnemies()
    {
        Debug.Log("spawn enemies");

        //Retreats to the top
        boxcol.enabled = false;
        transform.rotation = Quaternion.identity;

        while (transform.position != retreatPoint.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, retreatPoint.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        //spawns the enemies

        int spawn = 0;

        for (int i = 0; i < enemySpawnPoints.Length; i++)
        {
            GameObject e = Instantiate(enemies[Random.Range(0, enemies.Length)], enemySpawnPoints[spawn].position, Quaternion.identity, transform.parent);

            aliveEnemies.Add(e);

            if(spawn < enemySpawnPoints.Length)
            {
                Debug.Log(spawn);
                spawn += 1;
            }

            if(spawn >= enemySpawnPoints.Length)
            {
                Debug.Log("reset");
                spawn = 0;
            }

            yield return new WaitForSeconds(enemySpawnRate);
        }

        while(aliveEnemies.Count > 0)
        {
            foreach (GameObject en in aliveEnemies)
            {
                if (en.activeSelf == false)
                {
                    aliveEnemies.Remove(en);
                    break;
                }
            }
            yield return null;
        }

        //Debug.Log("Backtostage");
        while (transform.position != transform.parent.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        boxcol.enabled = true;

        int chance = Random.Range(0, 3);
        if (chance == 1)
        {
            StartCoroutine("RainAttack");
        }
        else
        {
            chargeCount = Random.Range(minCharge, maxCharge + 1);
            StartCoroutine("ChargeAttack");
        }
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
