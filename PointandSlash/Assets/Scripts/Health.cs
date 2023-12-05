using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth = 5f;
    public float health;

    public UnityEvent Death;

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
            Death.Invoke();
        }
    }

    public void HealHealth(int heal)
    {
        health += heal;
        Debug.Log("Health healed" + health);

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
