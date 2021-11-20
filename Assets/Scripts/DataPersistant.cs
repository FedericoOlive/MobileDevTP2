using UnityEngine;
public class DataPersistant : MonoBehaviourSingleton<DataPersistant>
{
    [HideInInspector] public PluginLogger plugin;
    private PlayerStats playerHistory;

    void Start()
    {
        plugin = new PluginLogger();
        plugin.Init();
        plugin.SendLog("Juego Comenzado" + Time.time);
    }
    void Update()
    {
        
    }
}