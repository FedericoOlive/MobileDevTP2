using UnityEngine;
public class Obstacle : MonoBehaviour
{
    public bool inUse;
    public Hitted hitted;

    private void Start()
    {
        hitted.OnHitted += OnHitted;
    }
    public void OnHitted()
    {
        inUse = false;
    }
}