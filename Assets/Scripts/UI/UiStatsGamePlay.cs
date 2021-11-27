using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UiStatsGamePlay : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI textTime;
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textScore;
    private void Start()
    {
        playerController.onDie += UpdateStatsUi;
    }
    public void UpdateStatsUi()
    {
        //textMoney.text = GamePlayManager.Get().GetPlayerStats().money.ToString();
        //textScore.text = GamePlayManager.Get().GetPlayerStats().score.ToString("F0");
        //textTime.text = GamePlayManager.Get().GetPlayerStats().gamePlayTime.ToString("F0");
    }
}