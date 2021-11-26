using UnityEngine;
public class GamePlayManager : MonoBehaviourSingleton<GamePlayManager>
{
    public Transform cam;
    private float gameSpeed = 5;

    public PlayerController player;
    [SerializeField] private PlayerStats playerStats = new PlayerStats();

    void Start()
    {
        //#if UNITY_ANDROID && !UNITY_EDITOR
        if (Social.localUser.authenticated)
            PlayGamesAchievement.Get().AchievementPlayFirtsTime();
        //#endif
        if (!cam)
            if (!(Camera.main is null))
                cam = Camera.main.transform;
        player.onCollect += CollectPoints;
        player.onImpactObstacle += PlayerImpact;
        player.onJump += PlayerJump;
        player.onAvoidObstacle += PlayerAvoidObstacle;
        player.onDie += PlayerInstaDie;
    }
    void Update()
    {
        float time = Time.deltaTime;
        playerStats.gamePlayTime += time;
        playerStats.score += (time * 10);
    }
    private void FixedUpdate()
    {
        var position = cam.position;
        Vector3 advance = new Vector3(position.x + gameSpeed * Time.deltaTime, 0, position.z);
        position = advance;
        cam.position = position;
    }
    public void CollectPoints(Item item)
    {
        Debug.Log("RewardType: " + item.typeReward);
        switch (item.typeReward)
        {
            case Item.TypeReward.Life:
                if (playerStats.lifes < 5)
                {
                    playerStats.lifes += item.amount;
                    UiGamePlayManager.Get().LifesUpdated();
                }
                else
                {
                    playerStats.score += 100;
                    UiGamePlayManager.Get().ScoreUpdated();
                }
                break;
            case Item.TypeReward.Money:
                playerStats.money += item.amount;
                UiGamePlayManager.Get().MoneyUpdated();
                break;
            case Item.TypeReward.Score:
                playerStats.score += item.amount;
                UiGamePlayManager.Get().ScoreUpdated();
                break;
        }
        playerStats.recolected++;
        item.transform.position = Vector3.zero;
        item.inUse = false;
    }
    public void PlayerImpact()
    {
        playerStats.lifes--;
        if (playerStats.lifes <= 0)
        {
            playerStats.lifes = 0;
            PlayerDie();
        }
        UiGamePlayManager.Get().LifesUpdated();
    }

    void PlayerInstaDie()
    {
        for (int i = playerStats.lifes; i > 0; i--)
        {
            Debug.Log("InstaKiller.");
            PlayerImpact();
        }
    }
    void PlayerDie()
    {
//#if UNITY_ANDROID && !UNITY_EDITOR
        if (Social.localUser.authenticated)
        {
            double timeplayed = DataPersistant.Get().PluginGetElapsedTime();
            DataPersistant.Get().PluginSendLog("Player Die: " + timeplayed);
            Handheld.Vibrate();

            if (playerStats.score >= 200)
                PlayGamesAchievement.Get().AchievementWin200Points();
            if (playerStats.score >= 500)
                PlayGamesAchievement.Get().AchievementWin500Points();
            if (playerStats.score >= 1000)
                PlayGamesAchievement.Get().AchievementWin1000Points();
            if (playerStats.score >= 1500)
                PlayGamesAchievement.Get().AchievementWin1500Points();

            if (playerStats.money >= 200)
                PlayGamesAchievement.Get().AchievementEarn200Coins();
            if (playerStats.money >= 500)
                PlayGamesAchievement.Get().AchievementEarn500Coins();
            if (playerStats.money >= 850)
                PlayGamesAchievement.Get().AchievementEarn850Coins();
            if (playerStats.money >= 1000)
                PlayGamesAchievement.Get().AchievementEarn1000Coins();

            PlayGamesAchievement.Get().SendScore((int) playerStats.score);
        }
//#endif

        Debug.Log("Player Die.");
        UiMainMenuManager.Get().SwitchPanel(2);
        player.Die();
        Time.timeScale = 0;
        SaveStats();
        UiGamePlayManager.Get().UpdateGameOver();
    }
    public void PlayerJump()
    {
        playerStats.jumps++;
    }
    public void PlayerAvoidObstacle()
    {
        playerStats.obstaclesAvoided++;
    }
    public void Pause(bool onPause)
    {
        if (onPause)
        {
            Time.timeScale = 0;
            player.EjectPause(true);
            UiGamePlayManager.Get().Pause(true);
            UiMainMenuManager.Get().SwitchPanel(1);
        }
        else
        {
            player.GoToWaitingInput();
            UiGamePlayManager.Get().Pause(false);
            UiMainMenuManager.Get().SwitchPanel(0);
        }
    }
    void SaveStats()
    {
        DataPersistant.Get().playerHistory.money += playerStats.money;
        DataPersistant.Get().playerHistory.score += playerStats.score;
        DataPersistant.Get().playerHistory.gamePlayTime += playerStats.gamePlayTime;
        DataPersistant.Get().playerHistory.jumps += playerStats.jumps;
        DataPersistant.Get().playerHistory.recolected += playerStats.recolected;
        DataPersistant.Get().playerHistory.obstaclesAvoided += playerStats.obstaclesAvoided;

        DataPersistant.Get().PluginSendLog("----------Stats----------");
        DataPersistant.Get().PluginSendLog("Money:" + playerStats.money);
        DataPersistant.Get().PluginSendLog("Score:" + playerStats.score);
        DataPersistant.Get().PluginSendLog("GamePlayTime:" + playerStats.gamePlayTime);
        DataPersistant.Get().PluginSendLog("Jumps:" + playerStats.jumps);
        DataPersistant.Get().PluginSendLog("Recolected:" + playerStats.recolected);
        DataPersistant.Get().PluginSendLog("ObstaclesAvoided:" + playerStats.obstaclesAvoided);
        DataPersistant.Get().PluginSendLog("----------Stats----------");
    }
    // ---------------------------------
    public PlayerStats GetPlayerStats() => playerStats;
}