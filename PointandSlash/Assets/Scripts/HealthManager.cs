using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private Health health;
    private float targetHealth;

    public Image healthBar;

    public float speed;

    private void Start()
    {
        health = GetComponent<Health>();
        targetHealth = health.health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            health.TakeDamage(20);
        }

        healthBar.fillAmount = targetHealth / health.maxHealth;
        targetHealth = Mathf.Lerp(targetHealth, health.health, speed * Time.deltaTime);
    }
}
