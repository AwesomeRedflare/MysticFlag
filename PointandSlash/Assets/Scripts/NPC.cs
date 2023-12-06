using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [TextArea(3, 10)]
    public string dialogue;

    private TextMeshProUGUI chat;
    private TextMeshProUGUI chatName;

    private void Start()
    {

    }

    public void Speak()
    {
        if(chat == null)
        {
            chat = GameObject.FindGameObjectWithTag("ChatBox").GetComponent<TextMeshProUGUI>();
        }
        chat.text = dialogue;

        if (chatName == null)
        {
            chatName = GameObject.FindGameObjectWithTag("Name").GetComponent<TextMeshProUGUI>();
        }
        chatName.text = name;
    }

    public void StopSpeak(GameObject box)
    {

    }

    public void NPCDeath()
    {
        Debug.Log("AAAAAAAAAAAAAA");
        Destroy(gameObject);
    }
}
