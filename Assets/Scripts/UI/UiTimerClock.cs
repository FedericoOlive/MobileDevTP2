using UnityEngine;
public class UiTimerClock : MonoBehaviour
{
    [SerializeField] private Transform uiClock;
    void Update()
    {
        ConvertTimerToString(GamePlayManager.Get().GetPlayerStats().gamePlayTime);
    }
    string ConvertTimerToString(float timer)
    {
        int seconds = (int)timer;
        int minutes = 0;
        while (seconds > 59)
        {
            seconds -= 60;
            minutes++;
        }
        float newAngle = (360 * seconds) / 60.0f;
        uiClock.rotation = Quaternion.Euler(0, 0, -newAngle);
        string text = (minutes < 10) ? "0" + minutes : minutes.ToString();
        text += ":";
        text += (seconds < 10) ? "0" + seconds : seconds.ToString();
        return text;
    }
}
