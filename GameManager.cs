using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public int playerMaxHp;
    public int playerCurrentHp;
    public int submarineMaxCapacity;
    public int submarineMaxFuel;
    public int submarineCurrentCargo;
    public float submarineCurrentFuel;
    public int playerMoney;

    public TextMeshProUGUI moneyInfoText;
    public Slider fuelSlider;
    public Slider cargoSlider;
    public TextMeshProUGUI infoBox;
    bool shownFuelInfo;

    List<ItemToBuy> shop;

    public List<Collectibles> collectiblesOnBoard;

    CharacterControl player;

    void Start()
    {
        shownFuelInfo = false;
        player = FindObjectOfType<CharacterControl>();
        playerCurrentHp = playerMaxHp = 3;
        playerMoney = 0;
        submarineCurrentCargo = 0;
        submarineMaxCapacity = 10;
        submarineMaxFuel = 25;
        submarineCurrentFuel = submarineMaxFuel;

        infoBox.gameObject.SetActive(false);

        shop = new List<ItemToBuy>();
        foreach(var obj in FindObjectsOfType<ItemToBuy>())
            shop.Add(obj);

        collectiblesOnBoard = new List<Collectibles>();

        fuelSlider.minValue = 0;
        fuelSlider.maxValue = submarineMaxFuel;
        fuelSlider.value = submarineCurrentFuel;

        cargoSlider.minValue = 0;
        cargoSlider.maxValue = submarineMaxCapacity;
        cargoSlider.value = submarineCurrentCargo;
    }

    private void Update()
    {
        if(!player.docked)
            UseFuel();
        if (submarineCurrentFuel < 0)
        {
            player.canMove = false;
            player.docked = true;
            infoBox.text = "You run out of fuel. You will stay underwater forever. Press R if you want to try again";
            infoBox.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(0);
        }
    }

    private void UseFuel()
    {
        submarineCurrentFuel -= Time.deltaTime;
        fuelSlider.value = submarineCurrentFuel;
        if(submarineCurrentFuel < submarineMaxFuel / 5 && !shownFuelInfo)
        {
            infoBox.text = "LOW FUEL LEVEL";
            infoBox.gameObject.SetActive(true);
            StartCoroutine(turnOfInfoBox());
            shownFuelInfo = true;
        }
    }

    public void UpdateShopButtons()
    {
        moneyInfoText.text = playerMoney + "$";
        foreach(var item in shop)
        {
            item.CheckIfPlayerHaveEnoughtMoney();
        }
    }

    public void SellCollectibles()
    {
        int tmp = 0;
        foreach (var item in collectiblesOnBoard)
        {
            tmp += item.value;
        }
        playerMoney += tmp;
        submarineCurrentCargo = 0;
        collectiblesOnBoard.Clear();
        moneyInfoText.text = playerMoney + "$";
        cargoSlider.value = submarineCurrentCargo;
        shownFuelInfo = false;
        UpdateShopButtons();
    }

    public IEnumerator turnOfInfoBox()
    {
        infoBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        infoBox.gameObject.SetActive(false);
    }
}
