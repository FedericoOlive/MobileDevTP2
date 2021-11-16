using System.Collections.Generic;
using UnityEngine;
public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private List<Transform> backGrounds = new List<Transform>();
    private float distance = 28;
    public float GetDistance() => distance;
}