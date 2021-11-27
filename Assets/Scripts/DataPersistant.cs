using System;
using System.Collections.Generic;
using UnityEngine;
public class DataPersistant : MonoBehaviourSingleton<DataPersistant>
{
    public static Action<string> onSendLog;
    public bool activateDebugLog;
    public bool activatePlugin;
    private PluginLogger plugin;
    public PlayerStats playerHistory = new PlayerStats();
    
    public List<Sprite> playerSprites = new List<Sprite>();
    public List<Sprite> backGroundSprites = new List<Sprite>();
    public List<Sprite> obstaclesSprites = new List<Sprite>();
    public List<Sprite> obstaclesBaseSprites = new List<Sprite>();
    public ObjectsBuyed objectsBuyed = new ObjectsBuyed();

    void Start()
    {
        plugin = new PluginLogger();
        plugin.Init();
        
    }
    public void InitPlayerLevel(PlayerController player)
    {
        player.GetComponent<SpriteRenderer>().sprite = playerSprites[objectsBuyed.playerUsage];
    }
    public void InitBackGroundLevel(BackGroundManager backGroundManager)
    {
        foreach (var background in backGroundManager.backGrounds)
        {
            background.GetComponent<SpriteRenderer>().sprite = backGroundSprites[objectsBuyed.backGroundUsage];
        }
    }
    public void InitObstablesrLevel(ObstaclesManager obstaclesManager)
    {
        foreach (Obstacle obstacle in obstaclesManager.obstaclesList)
        {
            obstacle.UpdateSprites(obstaclesSprites[objectsBuyed.obstaclesUsage], obstaclesBaseSprites[objectsBuyed.obstaclesUsage]);
        }
    }

    //--------------------- PLUGIN ---------------------
    public double PluginGetElapsedTime()
    {
        if (activatePlugin)
            return plugin.GetElapsedTime();
        return 0;
    }
    public void PluginSendLog(string msj)
    {
        onSendLog?.Invoke(msj);
        Debug.Log("Plugin: " + msj);
        plugin.SendLog(msj);
    }
    public string PluginReadFile()
    {
        return plugin.ReadLogs();
    }
    public void PluginClearLogs()
    {
        plugin.ClearLogs();
    }
}
//[Serializable] 
public class ObjectsBuyed
{
    public int playerUsage = 0;
    public int backGroundUsage = 0;
    public int obstaclesUsage = 0;

    public List<int> buyedBackGrounds = new List<int>();
    public List<int> buyedObstacles = new List<int>();
    public List<int> buyedPlayers = new List<int>();

    public ObjectsBuyed()
    {
        buyedBackGrounds.Add(0);
        buyedBackGrounds.Add(150);
        buyedBackGrounds.Add(200);
        buyedBackGrounds.Add(0);
        buyedBackGrounds.Add(300);
        buyedBackGrounds.Add(400);

        buyedObstacles.Add(0);
        buyedObstacles.Add(50);
        buyedObstacles.Add(75);
        buyedObstacles.Add(100);

        buyedPlayers.Add(0);
        buyedPlayers.Add(100);
        buyedPlayers.Add(150);
        buyedPlayers.Add(200);
        buyedPlayers.Add(500);
    }
}