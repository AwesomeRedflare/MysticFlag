using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 5f;
    public float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage Taken" + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
