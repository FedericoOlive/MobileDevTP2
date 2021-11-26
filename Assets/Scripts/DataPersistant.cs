using System.Collections.Generic;
using UnityEngine;
public class DataPersistant : MonoBehaviourSingleton<DataPersistant>
{
    [HideInInspector] public PluginLogger plugin;
    private PlayerStats playerHistory;
    private int playerUsage = 1;
    private int backGroundUsage = 1;
    private int obstaclesUsage = 1;
    public List<Sprite> playerSprites = new List<Sprite>();
    public List<Sprite> backGroundSprites = new List<Sprite>();
    public List<Sprite> obstaclesSprites = new List<Sprite>();

    void Start()
    {
        plugin = new PluginLogger();
        plugin.Init();
        plugin.SendLog("Juego Comenzado" + Time.time);
    }
    public void InitPlayerLevel(PlayerController player)
    {
        player.GetComponent<SpriteRenderer>().sprite = playerSprites[playerUsage];
    }
    public void InitBackGroundLevel(BackGroundManager backGroundManager)
    {
        foreach (var background in backGroundManager.backGrounds)
        {
            background.GetComponent<SpriteRenderer>().sprite = backGroundSprites[backGroundUsage];
        }
    }
    public void InitObstablesrLevel(ObstaclesManager obstaclesManager)
    {
        for (int i = 0; i < obstaclesManager.obstaclesList.Count; i++)
        {

        }
    }
    void Update()
    {
        
    }
}