using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiLifes : MonoBehaviour
{
    [SerializeField] private List<Image> lifes = new List<Image>();
    private bool[] lifeEnable = new[] {true, true, true, false, false};
    private int currentLifes = 3;

    private void Start()
    {
        for (int i = 0; i < lifes.Count; i++)
        {
            lifes[i].color = lifeEnable[i] ? Color.white : Color.clear;
        }
    }
    public void UpdateLifes()
    {
        int playerLifes = GamePlayManager.Get().GetPlayerStats().lifes;
        if (currentLifes < playerLifes)
        {
            AddLife();
        }
        if (currentLifes > playerLifes)
        {
            SubtractLife();
        }
        currentLifes = playerLifes;
    }
    private void AddLife()
    {
        for (int i = 0; i < lifes.Count; i++)
        {
            if(lifeEnable[i])
                continue;
            lifeEnable[i] = true;
            lifes[i].color = Color.white;
            break;
        }
    }
    private void SubtractLife()
    {
        for (int i = lifes.Count - 1; i >= 0; i--)
        {
            if (!lifeEnable[i])
                continue;
            lifeEnable[i] = false;
            lifes[i].color = Color.clear;
            break;
        }
    }
}