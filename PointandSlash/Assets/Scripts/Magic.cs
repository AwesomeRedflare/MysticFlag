using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Magic : MonoBehaviour
{
    public GameObject fireBall;
    public Transform spawnPoint;

    public float maxMana;
    public float manaAmount;
    public float manaCost;
    public float manaRegen;
    public float attackAmount;

    public Image manaBar;
    public TextMeshProUGUI manaText;
    private float targetMana;
    public float speed;

    private void OnEnable()
    {
        manaAmount = maxMana;
    }

    private void Update()
    {
        if(manaAmount <= maxMana)
        {
            manaAmount = manaAmount + manaRegen * Time.deltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.N)) && manaAmount >= manaCost)
        {
            manaAmount = manaAmount - manaCost;

            GameObject projectile = Instantiate(fireBall, spawnPoint.position, GetComponent<Player>().pivot.rotation).gameObject;
            projectile.GetComponent<FireBall>().damage = attackAmount;

        }

        manaBar.fillAmount = targetMana / maxMana;
        targetMana = Mathf.Lerp(targetMana, manaAmount, speed * Time.deltaTime);

        manaText.text = Mathf.FloorToInt(manaAmount).ToString();
    }
}
