using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 5f;

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
