using UnityEngine;
public class GamePlayManager : MonoBehaviour
{
    public Transform cam;
    private float gameSpeed = 5;

    private void Awake()
    {
        if (!cam)
            cam = Camera.main.transform;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector3 advance = new Vector3(cam.position.x + gameSpeed * Time.deltaTime, 0, cam.position.z);
        cam.position = advance;
    }
}