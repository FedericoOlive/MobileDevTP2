using System;
using TMPro;
using UnityEngine.UI;
public class UiGamePlayManager : MonoBehaviourSingleton<UiGamePlayManager>
{
    public PlayerController player;
    public GameOverContent gameOverContent;
    private PlayerStats playerStats;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textLifes;
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textTime;
    public Image imgLifes;
    public UiLifes uiLifes;

    private float onTime;

    private void Start()
    {
        player = GamePlayManager.Get().player;
        playerStats = GamePlayManager.Get().GetPlayerStats();
        ScoreUpdated();
        LifesUpdated();
        MoneyUpdated();
    }
    private void Update()
    {
        TimeUpdated();
    }
    public void Pause(bool onPause)
    {

    }
    public void ScoreUpdated()
    {
        textScore.text = playerStats.score.ToString("F0");
    }
    public void LifesUpdated()
    {
        uiLifes.UpdateLifes();
        textLifes.text = playerStats.lifes.ToString();
    }
    public void MoneyUpdated()
    {
        textMoney.text = playerStats.money.ToString();
    }
    public void TimeUpdated()
    {
        textTime.text = playerStats.gamePlayTime.ToString("F2");
        ScoreUpdated();
    }
    public void UpdateGameOver()
    {
        gameOverContent.textGamePlayTime.text = playerStats.gamePlayTime.ToString("F2");
        gameOverContent.textScore.text = playerStats.score.ToString("F0");
        gameOverContent.textMoney.text = playerStats.money.ToString();
    }
}
[Serializable] public class GameOverContent
{
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textGamePlayTime;
    public TextMeshProUGUI textMoney;
}