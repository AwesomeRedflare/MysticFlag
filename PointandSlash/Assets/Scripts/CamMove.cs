using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform player;
    private Camera cam;

    public float zoomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }

    public void ChangeSize(int size)
    {
        StartCoroutine("CameraZoom", size);
    }

    IEnumerator CameraZoom(int s)
    {
        while(cam.orthographicSize != s)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, s, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
