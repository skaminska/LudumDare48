using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    CharacterControl player;
    bool moveToPosition;
    bool turnOnInfo;

    private void Start()
    {
        player = FindObjectOfType<CharacterControl>();
        turnOnInfo = false;
    }

    private void Update()
    {
        if (!player.canMove && Input.GetKeyDown(KeyCode.Space))
        {
            player.canMove = true;
            player.docked = false;
            moveToPosition = false;
            shopUI.SetActive(false);
            GameManager.Instance.infoBox.gameObject.SetActive(false);
        }
        if(moveToPosition)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(6.5f, -3.5f), 2 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shopUI.SetActive(true);
        player.canMove = false;
        player.docked = true;
        moveToPosition = true;

        GameManager.Instance.submarineCurrentFuel = GameManager.Instance.submarineMaxFuel;
        GameManager.Instance.fuelSlider.value = GameManager.Instance.submarineCurrentFuel;
        GameManager.Instance.SellCollectibles();

        if (turnOnInfo)
        {
            GameManager.Instance.infoBox.text = "Press 'Space' to leave shop and start swim";
            GameManager.Instance.infoBox.gameObject.SetActive(true);
        }
        turnOnInfo = true;
    }

}
