using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RewardsManager : MonoBehaviour
{
    [SerializeField] private List<Item> allItems = new List<Item>();
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject deSpawner;
    [Range(0, 100)] private float chanceScore = 55;
    [Range(0, 100)] private float chanceMoney = 30;
    [Range(0, 100)] private float chanceLife = 15;

    private List<int> indexList = new List<int>();
    
    public GameObject InstantiateReward()
    {
        int random = Random.Range(0, 100);

        if (random < chanceScore)
            return InstantiateRewardScore();
        else if (random < chanceScore + chanceMoney)
            return InstantiateRewardMoney();
        else if (random < chanceScore + chanceMoney + chanceLife)
            return InstantiateRewardLife();

        return null;
    }
    private GameObject InstantiateRewardScore()
    {
        return FindObjectFree(Item.TypeReward.Score).gameObject;
    }
    private GameObject InstantiateRewardMoney()
    {
        return FindObjectFree(Item.TypeReward.Money).gameObject;
    }
    private GameObject InstantiateRewardLife()
    {
        return FindObjectFree(Item.TypeReward.Life).gameObject;
    }
    private Item FindObjectFree(Item.TypeReward typeReward)
    {
        SetAvailableList();
        int maxItems = allItems.Count;

        foreach (int index in indexList)
        {
            if (!allItems[index].inUse && allItems[index].typeReward == typeReward)
            {
                allItems[index].inUse = true;
                return allItems[index];
            }
        }

        Debug.LogWarning("NO SE ENCONTRARON ITEMS DISPONIBLES DEL TIPO: " + typeReward);
        return null;
    }
    private void SetAvailableList()
    {
        indexList.Clear();
        for (int i = 0; i < allItems.Count; i++)
        {
            if (!allItems[i].inUse)
            {
                indexList.Add(i);
            }
        }
    }
}