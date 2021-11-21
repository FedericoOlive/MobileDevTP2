using UnityEngine;
public class GamePlayManager : MonoBehaviourSingleton<GamePlayManager>
{
    public Transform cam;
    private float gameSpeed = 5;

    public PlayerController player;
    [SerializeField] private PlayerStats playerStats = new PlayerStats();

    void Start()
    {
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
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            double timeplayed = DataPersistant.Get().plugin.GetElapsedTime();
            DataPersistant.Get().plugin.SendLog("Player Die: " + timeplayed);
            Handheld.Vibrate();
        }
        playerStats.score
#endif

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
        Debug.Log("----------Stats----------");
        Debug.Log("Jumps:" + playerStats.jumps);
        Debug.Log("Lifes:" + playerStats.lifes);
        Debug.Log("Score:" + playerStats.score);
        Debug.Log("GamePlayTime:" + playerStats.gamePlayTime);
        Debug.Log("Money:" + playerStats.money);
        Debug.Log("Recolected:" + playerStats.recolected);
        Debug.Log("ObstaclesAvoided:" + playerStats.obstaclesAvoided);
        Debug.Log("----------Stats----------");
    }
    // ---------------------------------
    public PlayerStats GetPlayerStats() => playerStats;
}