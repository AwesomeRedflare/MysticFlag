using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExpManager : MonoBehaviour
{
    private Player player;
    private PlayerAttack attack;
    private Health health;
    private Magic magic;
    private Dash dash;

    public int expAmount;
    public int expToNext;
    private int level;

    public GameObject levelUpText;
    public GameObject statBox;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI AtkText;
    //public TextMeshProUGUI spdText;
    public TextMeshProUGUI dshText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI magText;
    //public TextMeshProUGUI rgnText;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI nextExpText;

    //other things I have to unlock
    public GameObject dashCircle;
    public GameObject manaBar;

    private void Start()
    {
        player = GetComponent<Player>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<Health>();
        magic = GetComponent<Magic>();
        dash = GetComponent<Dash>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(statBox.activeSelf == false)
            {
                statBox.SetActive(true);
                SetStats();
                return;
            }
            else
            {
                statBox.SetActive(false);
            }
        }
    }

    void SetStats()
    {
        hpText.text = health.maxHealth.ToString();
        AtkText.text = attack.damage.ToString();
        //spdText.text = player.speed.ToString();
        dshText.text = dash.dashTime.ToString();
        mpText.text = magic.maxMana.ToString();
        magText.text = magic.attackAmount.ToString();
        //rgnText.text = magic.manaRegen.ToString();
        levelText.text = level.ToString();
        expText.text = expAmount.ToString();
        nextExpText.text = expToNext.ToString();
    }

    public void GetExp(int exp)
    {
        expAmount += exp;
        if(expAmount >= expToNext)
        {
            Debug.Log("You Leveled Up");
            LevelUp();
        }
    }

    public void LevelUp()
    {
        GameObject text = Instantiate(levelUpText, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
        Destroy(text, 1f);

        level++;
        health.maxHealth += 10;
        attack.damage += 5;
        //player.speed += 1;
        if(dash.enabled == true)
        {
            if(dash.dashTime >= 3)
            {
                //dash.dashTime *= 0.95f;
                dash.dashTime = Mathf.Round(dash.dashTime * 100f) / 100f;
            }
        }
        if (magic.enabled == true)
        {
            magic.maxMana += 5;
            magic.attackAmount += 5;
            //magic.manaRegen += 1;
        }

        expToNext = expToNext + 50 + level * 10;
    }

    //Unlocks

    public void FindSword()
    {
        GetComponent<PlayerAttack>().enabled = true;
        player.pivot.gameObject.SetActive(true);
    }

    public void UnlockDash()
    {
        GetComponent<Dash>().enabled = true;
        dshText.gameObject.SetActive(true);
        dashCircle.SetActive(true);
    }

    public void UnlockMagic()
    {
        GetComponent<Magic>().enabled = true;
        manaBar.SetActive(true);
        mpText.gameObject.SetActive(true);
        magText.gameObject.SetActive(true);
        //rgnText.gameObject.SetActive(true);
    }
}
