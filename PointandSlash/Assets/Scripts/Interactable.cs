using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private GameObject player;
    private GameObject text;

    public bool canInteractAgain;
    private bool hasInteracted;

    public UnityEvent action;

    private void Start()
    {
        text = transform.GetChild(0).gameObject;
        text.SetActive(false);
    }

    private void Update()
    {
        if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && hasInteracted == false)
            {
                action.Invoke();

                if (canInteractAgain == false)
                {
                    hasInteracted = true;
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && hasInteracted == false)
        {
            text.SetActive(true);
            player = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            text.SetActive(false);
            player = null;
        }
    }

    public void FindSword()
    {
        player.GetComponent<PlayerAttack>().enabled = true;
        Debug.Log("you found a sword");
    }
}
