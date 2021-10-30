using UnityEngine;
public class Obstacle : MonoBehaviour
{
    public bool inUse;
    public Hitted hitted;

    private void Start()
    {
        hitted.onHitted += OnHitted;
    }
    public void OnHitted()
    {
        inUse = false;
    }
}