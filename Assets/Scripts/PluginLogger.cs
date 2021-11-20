using UnityEngine;
public class PluginLogger
{
    private const string PackName = "com.OliveFederico.FlappyFleyLogger";
    private const string LoggerClassName = "FOLogger";
    private static AndroidJavaClass loggerClass = null;
    private static AndroidJavaObject loggerInstance = null;

    public void Init()
    {
        loggerClass = new AndroidJavaClass(PackName + "." + LoggerClassName);
        loggerInstance = loggerClass.CallStatic<AndroidJavaObject>("GetInstance");
    }
    public void SendLog(string msg)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (loggerInstance == null)
                Init();
            loggerInstance.Call("SendLog", msg);
        }
    }
    public double GetElapsedTime()
    {
        if (Application.platform == RuntimePlatform.Android)
            return loggerInstance.Call<double>("GetPlayTime");
        return 0;
    }
}