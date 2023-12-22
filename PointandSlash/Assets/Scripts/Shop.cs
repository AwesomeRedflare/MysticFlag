using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Shop : MonoBehaviour
{
    public int price;

    private Interactable interactable;

    public TextMeshPro priceText;

    public UnityEvent itemEffect;

    private void Start()
    {
        priceText.text = price + " Gold";
        interactable = GetComponent<Interactable>();
    }

    public void Buy()
    {
        if(interactable.player.GetComponent<GoldManager>().goldAmount >= price)
        {
            GoldManager gold = interactable.player.GetComponent<GoldManager>();
            gold.goldAmount -= price;
            gold.goldText.text = "Gold x " + gold.goldAmount;
            itemEffect.Invoke();
            Destroy(gameObject);
        }
    }

    public void HeartsWorthDouble()
    {
        interactable.player.GetComponent<Player>().heartValue *= 2;
    }

    public void ExtraHealth()
    {
        interactable.player.GetComponent<Health>().maxHealth += 50;
        interactable.player.GetComponent<Health>().HealHealth(50);
    }

    public void SpeedBoost()
    {
        interactable.player.GetComponent<Player>().speed += 3;
    }

    public void ManaRegenIncrease()
    {
        interactable.player.GetComponent<Magic>().manaRegen += 2;
    }
    public void DashMultiplier()
    {
        interactable.player.GetComponent<Dash>().dashLenght += 1;
    }

}
