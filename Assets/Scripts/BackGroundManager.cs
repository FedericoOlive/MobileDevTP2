using System;
using System.Collections.Generic;
using UnityEngine;
public class BackGroundManager : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private List<Transform> backGrounds = new List<Transform>();
    private float widthImage = 5.7f;
    private float distance;

    private void Start()
    {
        for (int i = 0; i < backGrounds.Count; i++)
        {
            distance += widthImage;
        }
    }

    public float GetDistance() => distance;
}