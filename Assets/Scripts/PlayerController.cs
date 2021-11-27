using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
// ReSharper disable InconsistentNaming
    private delegate void Method();
    private Method CustomUpdate;
    private Method PlayerInteract;
// ReSharper restore InconsistentNaming

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Action onImpactObstacle;
    public Action onJump;
    public Action onAvoidObstacle;
    public Action onDie;
    public Action<Item> onCollect;

    private float jumpForce = 5;
    private Vector2 initialPosition;
    public bool die;

    private float minAngle = -50;
    private float maxAngle = 50;
    private float minVel = -6;
    private float maxVel = 4;

    private void Awake()
    {
        Time.timeScale = 0;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        DataPersistant.Get().InitPlayerLevel(this);
        PlayerInteract = PlayerStartGame;
        initialPosition = transform.position;
        CustomUpdate = UpdateWaitingInput;
        rb.velocity = Vector3.zero;
    }
    void Update()
    {
        CustomUpdate();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        LayerMask otheLayerMask = other.gameObject.layer;
        if (Global.Get().LayerEqualKiller(otheLayerMask))
        {
            DataPersistant.Get().PluginSendLog("Player choca contra: " + other.name);
            //Debug.Log("Player choca contra: " + other.name, gameObject);
            ImpactObstacle();
        }
        else if (Global.Get().LayerEqualReward(otheLayerMask))
        {
            Item item = other.gameObject.GetComponent<Item>();
            if (item)
            {
                Debug.Log("Player recolecta contra: " + other.name, gameObject);
                onCollect?.Invoke(item);
            }
        }
        else if (Global.Get().LayerEqualAvoidedObstacle(otheLayerMask))
        {
            onAvoidObstacle?.Invoke();
        }
        else if (Global.Get().LayerEqualInstaKiller(otheLayerMask))
        {
            onDie?.Invoke();
        }
    }
    public void GoToWaitingInput()
    {
        CustomUpdate = UpdateWaitingInput;
        PlayerInteract = PlayerStartGame;
    }
    void UpdateWaitingInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerStartGame();
        }
    }
    void UpdateGamePlay()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        float currentAngle = transform.eulerAngles.z;
        float currentVel = rb.velocity.y;
        if (currentVel > 0)
        {
            float percentVel = currentVel / maxVel;
            currentAngle = Mathf.Abs(maxAngle * percentVel);
        }
        else
        {
            float percentVel = currentVel / minVel;
            currentAngle = -Mathf.Abs(minAngle * percentVel);
        }

        if (currentAngle > maxAngle) currentAngle = maxAngle;
        if (currentAngle < minAngle) currentAngle = minAngle;
        
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
    void UpdatePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CustomUpdate = UpdateGamePlay;
            Time.timeScale = 1;
        }
    }
    void UpdateDie()
    {

    }
    void Jump()
    {
        onJump?.Invoke();
        rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        rb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
    }
    void PlayerStartGame()
    {
        PlayerInteract = Jump;
        CustomUpdate = UpdateGamePlay;
        Time.timeScale = 1;
    }
    public void PlayerInteractable()
    {
        if (!die)
        {
            PlayerInteract();
        }
    }
    public void EjectPause(bool onPause)
    {
        if (onPause)
            CustomUpdate = UpdatePause;
        else
            CustomUpdate = UpdateGamePlay;
    }
    public void ImpactObstacle()
    {
        onImpactObstacle?.Invoke();
    }
    public void Die()
    {
        die = true;
        CustomUpdate = UpdateDie;
    }
    public void Restart()
    {
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
    }
}