using System.Collections.Generic;
using UnityEngine;
public class Obstacle : MonoBehaviour
{
    public bool inUse;
    public Hitted hitted;
    [HideInInspector] public int chanceApearReward = 40;
    public List<SpriteRenderer> columns = new List<SpriteRenderer>();
    public List<SpriteRenderer> bases = new List<SpriteRenderer>();

    private void Start()
    {
        hitted.onHitted += OnHitted;
    }
    public void OnHitted()
    {
        inUse = false;
    }
    public void UpdateSprites(Sprite spriteColumn, Sprite spriteBases)
    {
        for (int i = 0; i < columns.Count; i++)
        {
            columns[i].sprite = spriteColumn;
        }
        for (int i = 0; i < bases.Count; i++)
        {
            bases[i].sprite = spriteBases;
        }
    }
}