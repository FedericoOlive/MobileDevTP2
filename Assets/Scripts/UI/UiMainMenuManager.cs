using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UiMainMenuManager : MonoBehaviourSingleton<UiMainMenuManager>
{
    [SerializeField] private List<CanvasGroup> menues = new List<CanvasGroup>();
    public enum Menues { Main, Credits, GameOver, Logs }
    public Menues menuActual = Menues.Main;
    [SerializeField] private float timeTransition = 0.5f;
    private float onTime;
    public TextMeshProUGUI textVersion;
    public TextMeshProUGUI textLog;

    private void Start()
    {
        if (textVersion)
            textVersion.text = Application.version;
        ResetMenu();
        if (textLog)
        {
            DataPersistant.onSendLog += SendLogMsj;
        }
    }
    private void Reset()
    {
        if (menues.Count == 0)
        {
            menues.Add(GameObject.Find("PanelMain_Reff01").GetComponent<CanvasGroup>());
            menues.Add(GameObject.Find("PanelCredits_Reff02").GetComponent<CanvasGroup>());
        }
        ResetMenu();
        menuActual = Menues.Main;
        Time.timeScale = 1;
        onTime = 0;
        timeTransition = 0.5f;
    }
    private void EnablePanelMenuesOff(bool enable, int i)
    {
        menues[i].alpha = enable ? 1 : 0;
        menues[i].blocksRaycasts = enable;
        menues[i].interactable = enable;
    }
    private void ResetMenu()
    {
        menues[0].alpha = 1;
        menues[0].blocksRaycasts = true;
        menues[0].interactable = true;

        for (int i = 1; i < menues.Count; i++)
        {
            EnablePanelMenuesOff(false, i);
        }
    }
    public void SwitchPanel(int otherMenu)
    {
        menues[(int)menuActual].blocksRaycasts = false;
        menues[(int)menuActual].interactable = false;
        StartCoroutine(SwitchPanel(timeTransition, otherMenu, (int)menuActual));
    }
    private IEnumerator SwitchPanel(float maxTime, int onMenu, int offMenu)
    {
        CanvasGroup on = menues[onMenu];
        CanvasGroup off = menues[offMenu];

        while (onTime < maxTime)
        {
            onTime += Time.unscaledDeltaTime;
            float fade = onTime / maxTime;
            on.alpha = fade;
            off.alpha = 1 - fade;
            yield return null;
        }
        on.blocksRaycasts = true;
        on.interactable = true;
        onTime = 0;

        menuActual = (Menues)onMenu;
    }
    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    private void SendLogMsj(string msj)
    {
        textLog.text += msj;
    }
    public void ClearLogMsj()
    {
        DataPersistant.Get().PluginClearLogs();
        textLog.text = "";
    }
    public void ReadLogMsj()
    {
        textLog.text= DataPersistant.Get().PluginReadFile();
    }
}