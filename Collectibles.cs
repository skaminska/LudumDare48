using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public int value;
    public string itemName;
    public Sprite image;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.submarineCurrentCargo < GameManager.Instance.submarineMaxCapacity)
        {
            GameManager.Instance.submarineCurrentCargo++;
            GameManager.Instance.collectiblesOnBoard.Add(this);
            GameManager.Instance.cargoSlider.value++;
            Destroy(this.gameObject);
        }
        else
        {
            GameManager.Instance.infoBox.text = "You don't have enough space to pick it up";
            StartCoroutine(GameManager.Instance.turnOfInfoBox());
        }    
    }
}
