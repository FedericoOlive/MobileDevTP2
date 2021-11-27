package com.olivefederico.megaflappyfleylogger;
import android.app.Activity;
import android.content.Context;
import android.os.Environment;
import android.util.Log;
import android.widget.Toast;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;

public class FOLogger extends Activity {
    public static final FOLogger _instance = new FOLogger();
    private static final String logTag = "OliveLogger: ";     // Unity|megaflappyfley
    private static long startTime;
    private static OutputStreamWriter outputStreamWriter;
    private static String fileName= "Logs.txt";

    public static FOLogger GetInstance() {
        android.util.Log.d(logTag, "Get Instance Plugin");
        startTime = System.currentTimeMillis();
        return _instance;
    }

    public void SendLog(String msg) {
        Log.d(logTag, msg);
    }

    public double GetPlayTime() {
        return (System.currentTimeMillis() - startTime) / 1000.0f;
    }

    private boolean IsFileCreated(Context context) {
        try {
            InputStream inputStream = context.openFileInput(fileName);
            outputStreamWriter = new OutputStreamWriter(context.openFileOutput(fileName, Context.MODE_APPEND));
            inputStream.close();
            return true;
        } catch (FileNotFoundException e) {
            SendLog("File not found");
        } catch (IOException e) {
            e.printStackTrace();
            SendLog("File can't open.");
        }
        return false;
    }
    public void CreateDirectory(Context context) {
        if(!IsFileCreated(context)) {
            CreateFile(context);
            CloseFile();
        }
    }
    private void CreateFile(Context context) {
        SendLog("Creating File...");
        try {
            outputStreamWriter = new OutputStreamWriter(context.openFileOutput(fileName, Context.MODE_APPEND));
            SendLog("File Successfully Created");
        } catch (IOException e) {
            SendLog("File ERROR Created");
        }
    }
    public void WriteFile(String msg, Context context) {
        try {
            OutputStreamWriter streamWriter = new OutputStreamWriter(context.openFileOutput(fileName, Context.MODE_APPEND));
            streamWriter.write(msg + "\n");
            streamWriter.close();
        } catch (IOException e) {
            SendLog("File can't Written: " + e);
        }
    }
    public void ClearLogs(Context context)
    {
        try {
            OutputStreamWriter streamWriter = new OutputStreamWriter(context.openFileOutput(fileName, Context.MODE_PRIVATE));
            streamWriter.write(" ");
            streamWriter.close();
        } catch (IOException e) {
            SendLog("File can't Clean: " + e);
        }
    }
    public String ReadFile(Context context) {
        String text = "";
        SendLog("Start Reading.");
        try {
            InputStream inputStream = context.openFileInput(fileName);
            SendLog("File Opened.");
            if (inputStream != null) {
                InputStreamReader inputStreamReader = new InputStreamReader(inputStream);
                BufferedReader bufferedReader = new BufferedReader(inputStreamReader);
                String receiveString = "";
                StringBuilder stringBuilder = new StringBuilder();

                while ((receiveString = bufferedReader.readLine()) != null) {
                    stringBuilder.append(receiveString).append("\n");
                }

                inputStream.close();
                text = stringBuilder.toString();
                SendLog("File Content: " + text);
            }
        } catch (FileNotFoundException e) {
            SendLog("File doesn't exist.");
        } catch (IOException e) {
            SendLog("File can't Read");
        }
        return text;
    }
    private void CloseFile() {
        try {
            outputStreamWriter.close();
        } catch (IOException e) {
            SendLog("File can't Close.");
        }
    }
}