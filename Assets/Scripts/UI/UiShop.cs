using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiShop : MonoBehaviour
{
    public Action onUpdateStats;

    [SerializeField] private Sprite uiButtonBuyOn;
    [SerializeField] private Sprite uiButtonBuyOff;
    [SerializeField] private Sprite uiButtonUseOn;
    [SerializeField] private Sprite uiButtonUseOff;

    public List<UiShopPanelPerObject> shopPerBackGround = new List<UiShopPanelPerObject>();
    public List<UiShopPanelPerObject> shopPerObstacle = new List<UiShopPanelPerObject>();
    public List<UiShopPanelPerObject> shopPerPlayer = new List<UiShopPanelPerObject>();

    void Start()
    {
        SetIconsShop();
        UpdateBuyButtons();
        UpdateUseButtons();
    }
    public void UpdateBuyButtons()
    {
        UpdateBuy(shopPerBackGround, DataPersistant.Get().objectsBuyed.buyedBackGrounds);
        UpdateBuy(shopPerObstacle, DataPersistant.Get().objectsBuyed.buyedObstacles);
        UpdateBuy(shopPerPlayer, DataPersistant.Get().objectsBuyed.buyedPlayers);
    }
    public void UpdateUseButtons()
    {
        UpdateUse(shopPerBackGround, DataPersistant.Get().objectsBuyed.buyedBackGrounds, DataPersistant.Get().objectsBuyed.backGroundUsage);
        UpdateUse(shopPerObstacle, DataPersistant.Get().objectsBuyed.buyedObstacles, DataPersistant.Get().objectsBuyed.obstaclesUsage);
        UpdateUse(shopPerPlayer, DataPersistant.Get().objectsBuyed.buyedPlayers, DataPersistant.Get().objectsBuyed.playerUsage);
        onUpdateStats?.Invoke();
    }
    void UpdateBuy(List<UiShopPanelPerObject> shopPerObject, List<int> objectsBuyedPrice)
    {
        for (int i = 0; i < shopPerObject.Count; i++)
        {
            if (objectsBuyedPrice[i] == 0)
            {
                shopPerObject[i].imageBuy.gameObject.SetActive(false);
                shopPerObject[i].imagePrice.gameObject.SetActive(false);
                shopPerObject[i].textPrice.gameObject.SetActive(false);
            }
            else
            {
                shopPerObject[i].textPrice.text = "Price: " + objectsBuyedPrice[i];
                bool canBuy = DataPersistant.Get().playerHistory.money >= objectsBuyedPrice[i];
                shopPerObject[i].imageBuy.sprite = canBuy ? uiButtonBuyOn : uiButtonBuyOff;
                shopPerObject[i].imageBuy.GetComponent<Button>().interactable = canBuy;
            }
        }
    }
    void UpdateUse(List<UiShopPanelPerObject> shopPerObject, List<int> objectsBuyedPrice, int indexUse)
    {
        for (int i = 0; i < shopPerObject.Count; i++)
        {
            if (objectsBuyedPrice[i] == 0)
            {
                shopPerObject[i].imageUse.gameObject.SetActive(true);

                bool isInUse = indexUse == i;
                shopPerObject[i].imageUse.sprite = isInUse ? uiButtonUseOff : uiButtonUseOn;
                shopPerObject[i].imageUse.GetComponent<Button>().interactable = !isInUse;
            }
            else
            {
                shopPerObject[i].imageUse.gameObject.SetActive(false);
            }
        }
    }
    void SetIconsShop()
    {
        for (int i = 0; i < shopPerBackGround.Count; i++)
        {
            shopPerBackGround[i].imageObject.sprite = DataPersistant.Get().backGroundSprites[i];
            shopPerBackGround[i].imageBuy.GetComponent<UiShopBuyAndUsage>().SetButton(UiShopBuyAndUsage.TypeObject.BackGround, i, this);
            shopPerBackGround[i].imageUse.GetComponent<UiShopBuyAndUsage>().SetButton(UiShopBuyAndUsage.TypeObject.BackGround, i, this);
        }

        for (int i = 0; i < shopPerObstacle.Count; i++)
        {
            shopPerObstacle[i].imageObject.sprite = DataPersistant.Get().obstaclesSprites[i];
            shopPerObstacle[i].imageBuy.GetComponent<UiShopBuyAndUsage>().SetButton(UiShopBuyAndUsage.TypeObject.Obstacle, i, this);
            shopPerObstacle[i].imageUse.GetComponent<UiShopBuyAndUsage>().SetButton(UiShopBuyAndUsage.TypeObject.Obstacle, i, this);
        }

        for (int i = 0; i < shopPerPlayer.Count; i++)
        {
            shopPerPlayer[i].imageObject.sprite = DataPersistant.Get().playerSprites[i];
            shopPerPlayer[i].imageBuy.GetComponent<UiShopBuyAndUsage>().SetButton(UiShopBuyAndUsage.TypeObject.Player, i, this);
            shopPerPlayer[i].imageUse.GetComponent<UiShopBuyAndUsage>().SetButton(UiShopBuyAndUsage.TypeObject.Player, i, this);
        }
    }
}