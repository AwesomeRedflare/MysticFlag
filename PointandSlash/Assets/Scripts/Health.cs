using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Health : MonoBehaviour
{
    public GameObject damageText;

    public float maxHealth = 5f;
    public float health;

    public UnityEvent Death;


    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        GameObject text = Instantiate(damageText, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
        text.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damage.ToString());
        Destroy(text, 1f);

        health -= damage;
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
