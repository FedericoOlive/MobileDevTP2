using System;
using UnityEngine;
public class Item : MonoBehaviour
{
    public bool inUse;
    public enum TypeReward
    {
        Life,
        Score,
        Money
    }
    public TypeReward typeReward = TypeReward.Score;
    public int amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Global.Get().LayerEqualDeSpawner(other.gameObject.layer))
        {
            inUse = false;
        }
    }
}