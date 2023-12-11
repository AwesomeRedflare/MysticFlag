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
                    text.GetComponent<Animator>().SetTrigger("out");
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && hasInteracted == false)
        {
            text.SetActive(true);
            text.GetComponent<Animator>().SetTrigger("in");
            player = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            text.GetComponent<Animator>().SetTrigger("out");
            player = null;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
