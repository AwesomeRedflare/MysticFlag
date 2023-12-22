using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Enemy enemy;

    public float speed;

    public Transform rotatePoint;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        transform.RotateAround(rotatePoint.position, Vector3.forward, speed *Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        speed *= -1;

        if (col.gameObject.CompareTag("Player"))
        {
            Player pla = col.gameObject.GetComponent<Player>();

            pla.SetInvicibility((int)enemy.attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(rotatePoint.position, Mathf.Abs(rotatePoint.localPosition.y));
    }

}
