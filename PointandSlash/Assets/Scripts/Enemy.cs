using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject gold;
    public GameObject heart;

    public float attackDamage;
    public Vector2 goldDrop;
    public int experienceDrop;
    public int healthDrop;

    public Transform healthBar;
    public float offSet;

    private void Awake()
    {
        offSet = healthBar.position.y;
    }

    private void Update()
    {
        healthBar.rotation = Quaternion.identity;
        healthBar.position = new Vector2(transform.position.x, transform.position.y + offSet);
    }

    public void EnemyDeath()
    {
        float goldAmount = Random.Range(goldDrop.x, goldDrop.y);
        for (int i = 0; i < goldAmount; i++)
        {
            Vector2 spawn = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(gold, spawn, Quaternion.identity);
        }

        float heartChance = Random.Range(1, healthDrop);
        if(heartChance == 0)
        {
            //drop a herat here
            Debug.Log("a heart spawns");
        }
        Destroy(gameObject);
    }
}
