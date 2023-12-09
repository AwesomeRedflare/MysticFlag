using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    private Player player;

    public bool isDashing = false;
    public float speedMultiplier;
    public float timeBtwDash;
    public float dashTime;
    public float dashLenght;

    //UI stuff
    private float targetStamina;
    public Image dashCircle;
    public float circleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (timeBtwDash <= 0)
        {
            dashCircle.fillAmount = 1;
            if (Input.GetKeyDown(KeyCode.C))
            {
                dashCircle.fillAmount = 0;
                timeBtwDash = dashTime;
                player.isDashing = true;
                Invoke("StopDash", dashLenght);
            }
        }
        else
        {
            dashCircle.fillAmount = 1- targetStamina / dashTime;
            targetStamina = Mathf.Lerp(targetStamina, timeBtwDash, circleSpeed * Time.deltaTime);
            timeBtwDash -= Time.deltaTime;
        }
    }

    void StopDash()
    {
        player.isDashing = false;
    }
}
