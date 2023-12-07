using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Player player;

    public bool isDashing = false;
    public float speedMultiplier;
    private float timeBtwDash;
    public float dashTime;
    public float dashLenght;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (timeBtwDash <= 0)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                timeBtwDash = dashTime;
                player.isDashing = true;
                Invoke("StopDash", dashLenght);
            }
        }
        else
        {
            timeBtwDash -= Time.deltaTime;
        }
    }

    void StopDash()
    {
        isDashing = false;
    }
}
