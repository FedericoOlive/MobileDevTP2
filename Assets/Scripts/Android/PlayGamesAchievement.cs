using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
public class PlayGamesAchievement : MonoBehaviourSingleton<PlayGamesAchievement>
{
    public TextMeshProUGUI textLog;
#if UNITY_ANDROID //&& !UNITY_EDITOR
    public static PlayGamesPlatform platform; 
    
    public void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        platform = PlayGamesPlatform.Activate();

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                textLog.text += "Logged in successfully\n";
                DataPersistant.Get().plugin.SendLog("Logged in successfully");
            }
            else
            {
                textLog.text += "Login Failed\n";
                DataPersistant.Get().plugin.SendLog("Login Failed");
            }
        });
    }
    public void SendScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, "CgkIrIWusoIMEAIQCg", success => { });
        }
    }
    public void ShowRanking()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIrIWusoIMEAIQCg");
        }
    }
    public void ShowAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
    }
    public void AchievementPlayFirtsTime()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQAA", 100, success => { });
        }
    }
    public void AchievementWin200Points()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQAQ", 100, success => { });
        }
    }
    public void AchievementWin300Points()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQAg", 100, success => { });
        }
    }
    public void AchievementWin400Points()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQAw", 100, success => { });
        }
    }
    public void AchievementWin600Points()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQBA", 100, success => { });
        }
    }
    public void AchievementEarn200Coins()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQBQ", 100, success => { });
        }
    }
    public void AchievementEarn300Coins()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQBg", 100, success => { });
        }
    }
    public void AchievementEarn400Coins()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQBw", 100, success => { });
        }
    }
    public void AchievementEarn600Coins()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIrIWusoIMEAIQCA", 100, success => { });
        }
    }
#endif
}