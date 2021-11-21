using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine.SocialPlatforms;
public class PlayGamesAchievement : MonoBehaviourSingleton<PlayGamesAchievement>
{
#if UNITY_ANDROID
    private void Start()
    {
        try
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) => { });
        }
        catch (Exception e)
        {
            Debug.Log("Error al cargar PlayGames: " + e);
            Console.WriteLine(e);
            throw;
        }
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