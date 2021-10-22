using System;
using UnityEngine;
public class Hitted : MonoBehaviour
{
    public Action OnHitted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        LayerMask otheLayerMask = other.gameObject.layer;
        if (Global.Get().LayerEqualDeSpawner(otheLayerMask))
        {
            OnHitted?.Invoke();
        }
    }
}