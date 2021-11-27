﻿using UnityEngine;
public class PluginLogger
{
    private const string PackName = "com.olivefederico.megaflappyfleylogger";
    private const string LoggerClassName = "FOLogger";

    private AndroidJavaClass unityPlayer;
    private AndroidJavaObject activity;
    private AndroidJavaObject context;

    private static AndroidJavaClass loggerClass;
    private static AndroidJavaObject loggerInstance;
    public static bool isFileCreated;

    public void Init()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            loggerClass = new AndroidJavaClass(PackName + "." + LoggerClassName);
            loggerInstance = loggerClass.CallStatic<AndroidJavaObject>("GetInstance");

            unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = activity.Call<AndroidJavaObject>("getApplicationContext");
            
            loggerInstance.Call("CreateDirectory", context);
            DataPersistant.Get().PluginSendLog("Juego Comenzado");
        }
    }
    public void SendLog(string msg)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            loggerInstance.Call("SendLog", msg);
            loggerInstance.Call("WriteFile", msg, context);
        }
    }
    public double GetElapsedTime()
    {
        if (Application.platform == RuntimePlatform.Android)
            return loggerInstance.Call<double>("GetPlayTime");
        return 0;
    }
    public string ReadLogs()
    {
        if (Application.platform == RuntimePlatform.Android)
            return loggerInstance.Call<string>("ReadFile", context);
        return "Not Android";
    }
    public void ClearLogs()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            loggerInstance.Call("ClearLogs", context);
        }
    }
}