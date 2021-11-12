using UnityEngine;
public class Item : MonoBehaviour
{
    public enum TypeReward
    {
        Life,
        Score,
        Money
    }
    public TypeReward typeReward = TypeReward.Score;
    public int amount;
}