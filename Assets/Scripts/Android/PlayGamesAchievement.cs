using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using System.Collections;

public class PlayGamesAchievement : MonoBehaviourSingleton<PlayGamesAchievement>
{
    public TextMeshProUGUI textLog;
//#if UNITY_ANDROID && !UNITY_EDITOR
    public static PlayGamesPlatform platform;
    
    public void Initialize()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }
        
        Social.Active.localUser.Authenticate(success =>
        {
            string text = success ? "Logged in successfully" : "Login Failed";
            textLog.text += text + "\n";
            Debug.Log(text);
        });
    }



    public void SendScore(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_high_scores, success => { });
        }
    }
    public void ShowRanking()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_high_scores);
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
            Social.ReportProgress(GPGSIds.achievement_first_time, 100, success => { });
        }
    }
    public void AchievementWin200Points()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_win_200_points, 100, success => { });
        }
    }
    public void AchievementWin500Points()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_win_500_points, 100, success => { });
        }
    }
    public void AchievementWin1000Points()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_win_1000_points, 100, success => { });
        }
    }
    public void AchievementWin1500Points()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_win_1500_points, 100, success => { });
        }
    }
    public void AchievementEarn200Coins()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_earn_200_coins, 100, success => { });
        }
    }
    public void AchievementEarn500Coins()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_earn_500_coins, 100, success => { });
        }
    }
    public void AchievementEarn850Coins()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_earn_850_coins, 100, success => { });
        }
    }
    public void AchievementEarn1000Coins()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress(GPGSIds.achievement_earn_1000_coins, 100, success => { });
        }
    }
//#endif
}