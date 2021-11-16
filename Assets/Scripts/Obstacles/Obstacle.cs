using UnityEngine;
public class Obstacle : MonoBehaviour
{
    public bool inUse;
    public Hitted hitted;
    public int chanceApearReward = 100;

    private void Start()
    {
        hitted.onHitted += OnHitted;
    }
    public void OnHitted()
    {
        inUse = false;
    }
}