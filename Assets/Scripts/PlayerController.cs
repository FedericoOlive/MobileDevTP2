using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    private delegate void Method();
    private Method CustomUpdate;
    private Method CustomFixedUpdate;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Action onDie;
    public Action<int> onCollect;
    
    private int jumps;
    private int score;
    private float horizontal = 1;
    private float speed = 10;
    private float jumpForce = 5;
    private float gamePlayTime = 5;
    private Vector2 initialPosition;
    public float GetSpeed() => speed;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        initialPosition = transform.position;
        CustomUpdate = PauseUpdate;
        CustomFixedUpdate = GamePlayFixedUpdate;
    }
    void GamePlayUpdate()
    {
        gamePlayTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            Jump();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CustomUpdate = PauseUpdate;
            Time.timeScale = 0;
        }
    }
    void GamePlayFixedUpdate()
    {
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
    }
    void PauseUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CustomUpdate = PauseUpdate;
            Time.timeScale = 1;
        }
    }
    void Update()
    {
        CustomUpdate();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        LayerMask otheLayerMask = other.gameObject.layer;
        if (Global.Get().LayerEqualKiller(otheLayerMask))
        {
            Die();
        }
        else if (Global.Get().LayerEqualReward(otheLayerMask))
        {
            Item item = other.gameObject.GetComponent<Item>();
            if (item)
            {
                CollectPoints(item.GetReward());
            }
        }
    }
    void Jump()
    {
        jumps++;
        rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        rb.AddForce((Vector2.up * jumpForce), ForceMode2D.Impulse);
    }
    public void Die()
    {
        onDie?.Invoke();
    }
    public void Restart()
    {
        score = 0;
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
    }
    public void CollectPoints(int points)
    {
        score += points;
        onCollect?.Invoke(score);
    }
}