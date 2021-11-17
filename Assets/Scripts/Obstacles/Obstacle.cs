using UnityEngine;
public class Obstacle : MonoBehaviour
{
    public bool inUse;
    public Hitted hitted;
    [HideInInspector] public int chanceApearReward = 40;

    private void Start()
    {
        hitted.onHitted += OnHitted;
    }
    public void OnHitted()
    {
        inUse = false;
    }
}