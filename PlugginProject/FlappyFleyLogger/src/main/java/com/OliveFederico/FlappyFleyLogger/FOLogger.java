package com.OliveFederico.FlappyFleyLogger;
import android.util.Log;

public class FOLogger {
    public static final FOLogger _instance = new FOLogger();
    private static final String logTag = "OliveLogger: ";
    private static long startTime;

    public static FOLogger GetInstance() {
        android.util.Log.d(logTag, "Get Instance Plugin");
        startTime = System.currentTimeMillis();
        return _instance;
    }

    public void SendLog(String msg) {
        Log.d(logTag, "Send Log del Plugin");
        Log.d(logTag, msg);
    }

    public double GetPlayTime() {
        return (System.currentTimeMillis() - startTime) / 1000.0f;
    }
}
