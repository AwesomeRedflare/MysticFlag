using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;

    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    private Vector3 target;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = transform.position;
    }

    void Update()
    {
        // Code to move to mouse position
        if (Input.GetMouseButtonDown(0))
        {
            target = cam.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        // Code to face mouse
        Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    private void FixedUpdate()
    {

    }
}
