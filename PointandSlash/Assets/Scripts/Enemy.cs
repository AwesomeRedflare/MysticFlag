using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Health health;

    public GameObject gold;
    public GameObject heart;

    public float attackDamage;
    public Vector2 goldDrop;
    public int experienceDrop;
    public int healthDrop;

    public Transform healthBar;
    public float offset;

    private Vector2 startPos;

    private void Start()
    {
        health = GetComponent<Health>();
        startPos = transform.position;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        healthBar.rotation = Quaternion.identity;
        healthBar.position = new Vector2(transform.position.x, transform.position.y + offset);
    }

    public void EnemyDeath()
    {
        //gold 
        float goldAmount = Random.Range(goldDrop.x, goldDrop.y);
        for (int i = 0; i < goldAmount; i++)
        {
            Vector2 spawn = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(gold, spawn, Quaternion.identity);
        }

        //heart
        int heartChance = Random.Range(0, healthDrop);
        if (heartChance == 0)
        {
            Vector2 spawn = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(heart, spawn, Quaternion.identity);
        }

        //experience
        GameObject.FindGameObjectWithTag("Player").GetComponent<ExpManager>().GetExp(experienceDrop);

        gameObject.SetActive(false);
        transform.position = startPos;
    }

    public void Respawn()
    {
        if (gameObject.activeSelf == false)
        {
            health.health = health.maxHealth;
            gameObject.SetActive(true);
        }
    }
}
