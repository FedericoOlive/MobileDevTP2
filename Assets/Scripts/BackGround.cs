using UnityEngine;
public class BackGround : MonoBehaviour
{
    public BackGroundManager backGroundManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Global.Get().LayerEqualDeSpawner(other.gameObject.layer))
        {
            Vector3 pos = transform.position;
            pos.x += backGroundManager.GetDistance();
            transform.position = pos;
        }
    }
}