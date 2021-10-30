using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class UiShakeOnTime : MonoBehaviour
{
    private Vector2 initialPos;
    private RectTransform rt;
    [SerializeField] private float shakeTime = 1f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private Vector2 dispersion;
    private float randomStartTime;
    private float onTime;

    enum Status
    {
        None,
        Wait,
        Shake
    }
    private Status status = Status.None;
    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        randomStartTime = Random.Range(0, 1);
    }
    private void Start()
    {
        initialPos = rt.anchoredPosition;
    }
    private void Update()
    {
        onTime += Time.deltaTime;
        switch (status)
        {
            case Status.None:
                if (onTime > randomStartTime)
                {
                    onTime = 0;
                    status = Status.Shake;
                }
                break;
            case Status.Shake:
                float posx = Random.Range(-dispersion.x, dispersion.x);
                float posy = Random.Range(-dispersion.y, dispersion.y);
                rt.anchoredPosition = new Vector2(posx, posy);

                if (onTime > shakeTime)
                {
                    onTime = 0;
                    status = Status.Wait;
                    rt.anchoredPosition = initialPos;
                }
                break;
            case Status.Wait:
                if (onTime > waitTime)
                {
                    onTime = 0;
                    status = Status.Shake;
                }
                break;

        }

    }
    private void Reset()
    {
        initialPos = Vector2.zero;
        rt = GetComponent<RectTransform>();
        shakeTime = 1f;
        waitTime = 2f;
        dispersion = Vector2.one;
    }
}