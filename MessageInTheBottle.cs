using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageInTheBottle : MonoBehaviour
{
    public GameObject messageContent;
    CharacterControl player;
    public string URL;

    private void Start()
    {
        player = FindObjectOfType<CharacterControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.docked = true;
        messageContent.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(messageContent.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            player.docked = false;
            messageContent.gameObject.SetActive(false);
        }
    }

}
