﻿using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UiGamePlayManager : MonoBehaviourSingleton<UiGamePlayManager>
{
    public PlayerController player;

    private PlayerStats playerStats;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textLifes;
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textTime;
    public Image imgLifes;

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
        textScore.text = playerStats.score.ToString();
    }
    public void LifesUpdated()
    {
        textLifes.text = playerStats.lifes.ToString();
    }
    public void MoneyUpdated()
    {
        textMoney.text = playerStats.money.ToString();
    }
    public void TimeUpdated()
    {
        textTime.text = playerStats.gamePlayTime.ToString("F2");
    }
}