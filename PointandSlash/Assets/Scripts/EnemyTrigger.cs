using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public Enemy[] enemies;

    private void Start()
    {
        foreach (Enemy e in enemies)
        {
            e.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foreach (Enemy e in enemies)
            {
                e.Respawn();
            }
        }
    }
}
