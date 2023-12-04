using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magic : MonoBehaviour
{
    public GameObject fireBall;
    public Transform spawnPoint;

    public float maxMana;
    public float manaAmount;
    public float manaCost;

    public Image manaBar;
    private float targetMana;
    public float speed;

    private void OnEnable()
    {
        manaAmount = maxMana;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && manaAmount >= manaCost)
        {
            manaAmount = manaAmount - manaCost;

            Instantiate(fireBall, spawnPoint.position, transform.rotation);
        }

        manaBar.fillAmount = targetMana / maxMana;
        targetMana = Mathf.Lerp(targetMana, manaAmount, speed * Time.deltaTime);
    }
}
