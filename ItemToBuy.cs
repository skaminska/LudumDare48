using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToBuy : MonoBehaviour
{
    public string itemName;
    public int[] upgradeCost = new int[]{100, 300, 600, 900, 1200 };
    public List<Image> images = new List<Image>();
    public Sprite itemImage;
    public Image itemImageHolder;
    public int unlockedLevels;
    public Sprite unlocedLevelSprite;
    public TextMeshProUGUI buyButton;

    private void Start()
    {
        itemImageHolder.sprite = itemImage;
        buyButton.text = "Buy for " + upgradeCost[unlockedLevels] + "$";
        
    }

    public void BuyUpgrade()
    {
        if (GameManager.Instance.playerMoney >= upgradeCost[unlockedLevels])
        {
            GameManager.Instance.playerMoney -= upgradeCost[unlockedLevels];
            unlockedLevels++;
            GameManager.Instance.UpdateShopButtons();

            switch (itemName)
            {
                case "GasTank":
                    GameManager.Instance.submarineMaxFuel += 20;
                    GameManager.Instance.fuelSlider.maxValue = GameManager.Instance.submarineCurrentFuel = GameManager.Instance.submarineMaxFuel;
                    GameManager.Instance.fuelSlider.value = GameManager.Instance.submarineMaxFuel;
                    GameManager.Instance.infoBox.text = "Fuel tank capacity increased";
                    StartCoroutine(GameManager.Instance.turnOfInfoBox());
                    break;
                case "SubmarineCapacity":
                    GameManager.Instance.submarineMaxCapacity += 5;
                    GameManager.Instance.cargoSlider.maxValue = GameManager.Instance.submarineMaxCapacity;
                    GameManager.Instance.cargoSlider.value = 0;
                    GameManager.Instance.infoBox.text = "The capacity of the submarine has been increased";
                    StartCoroutine(GameManager.Instance.turnOfInfoBox());
                    break;
            }

            if (unlockedLevels <= 4)
            {
                for(int i = 0; i < unlockedLevels; i++)
                {
                    images[i].sprite = unlocedLevelSprite;
                }
                buyButton.text = "Buy for " + upgradeCost[unlockedLevels] + "$";
            
            }
            if(unlockedLevels == 5)
            {
                for (int i = 0; i < unlockedLevels; i++)
                {
                    images[i].sprite = unlocedLevelSprite;
                }
                buyButton.text = "Max level";
                buyButton.GetComponentInParent<Button>().interactable = false;
            }
        }
    }

    public void CheckIfPlayerHaveEnoughtMoney()
    {
        if (!(GameManager.Instance.playerMoney >= upgradeCost[unlockedLevels]))
            buyButton.GetComponentInParent<Button>().interactable = false;
        else
            buyButton.GetComponentInParent<Button>().interactable = true;
    }
}
