using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiStatsMainMenu : MonoBehaviour
{
    public UiShop uiShop;
    public TextMeshProUGUI textMoney;
    public Image imgMoney;
    public TextMeshProUGUI textScore;
    public Image imgScore;
    void Start()
    {
        uiShop.onUpdateStats += UpdateStatsUi;
        UpdateStatsUi();
    }
    public void UpdateStatsUi()
    {
        textMoney.text = DataPersistant.Get().playerHistory.money.ToString();
        textScore.text = DataPersistant.Get().playerHistory.score.ToString("F0");
    }
}