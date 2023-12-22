using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    public int goldAmount;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Gold"))
        {
            //FindObjectOfType<AudioManager>().Play("pickup1");
            Destroy(col.gameObject);
            AddGold();
        }
    }

    public void AddGold()
    {
        goldAmount = goldAmount + 1;
        goldText.text = "Gold x " + goldAmount;
    }

    public void SpendGold(int price)
    {
        goldAmount = goldAmount - price;
        goldText.text = "Gold x " + goldAmount;
    }
}
