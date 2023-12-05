using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    public Camera cam;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = cam.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            transform.position = target;
        }

    }
}
