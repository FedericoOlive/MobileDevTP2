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
}