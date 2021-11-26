using UnityEngine;
public class UiShopBuyAndUsage : MonoBehaviour
{
    public enum TypeObject
    {
        BackGround,
        Obstacle,
        Player
    }
    public UiShop uiShop;
    public TypeObject typeObject = TypeObject.BackGround;
    public int index;

    public void SetButton(TypeObject type, int i, UiShop uiShopInstance)
    {
        uiShop = uiShopInstance;
        typeObject = type;
        index = i;
    }
    public void ClickButtonBuy()
    {
        if (!uiShop) uiShop = FindObjectOfType<UiShop>();
        int moneySpend = 0;
        switch (typeObject)
        {
            case TypeObject.BackGround:
                moneySpend = DataPersistant.Get().objectsBuyed.buyedBackGrounds[index];
                DataPersistant.Get().objectsBuyed.buyedBackGrounds[index] = 0;
                break;
            case TypeObject.Obstacle:
                moneySpend = DataPersistant.Get().objectsBuyed.buyedObstacles[index];
                DataPersistant.Get().objectsBuyed.buyedObstacles[index] = 0;
                break;
            case TypeObject.Player:
                moneySpend = DataPersistant.Get().objectsBuyed.buyedPlayers[index];
                DataPersistant.Get().objectsBuyed.buyedPlayers[index] = 0;
                break;
            default:
                break;
        }
        DataPersistant.Get().playerHistory.money -= moneySpend;

        uiShop.UpdateBuyButtons();
        uiShop.UpdateUseButtons();
        Debug.Log("Buy Object: " + index + ". Spend: " + moneySpend);
    }
    public void ClickButtonUsage()
    {
        if (!uiShop) uiShop = FindObjectOfType<UiShop>();
        switch (typeObject)
        {
            case TypeObject.BackGround:
                DataPersistant.Get().objectsBuyed.backGroundUsage = index;
                break;
            case TypeObject.Obstacle:
                DataPersistant.Get().objectsBuyed.obstaclesUsage = index;
                break;
            case TypeObject.Player:
                DataPersistant.Get().objectsBuyed.playerUsage = index;
                break;
            default:
                break;
        }
        uiShop.UpdateUseButtons();
        Debug.Log("Change Use to: " + index);
    }
}