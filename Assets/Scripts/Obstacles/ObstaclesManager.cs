using System.Collections.Generic;
using UnityEngine;
public class ObstaclesManager : MonoBehaviour
{
    public RewardsManager rewardsManager;
    public Transform cam;
    public List<Obstacle> obstaclesList = new List<Obstacle>();
    private List<int> indexList = new List<int>();

    public Vector2 rangeMinMax = new Vector2();
    [SerializeField] private float distanceSpawn = 4;
    public GameObject spawner;
    public GameObject deSpawner;
    private float lastPosCam;

    private void Awake()
    {
        if(!cam)
            cam = Camera.main.transform;
    }
    private void Update()
    {
        if (cam.position.x > lastPosCam + distanceSpawn)
        {
            SpawnObstacle();
            lastPosCam = cam.position.x;
        }
    }
    public Obstacle GetAvailableObstacle()
    {
        SetAvailableList();
        int index = indexList[Random.Range(0, indexList.Count)];
        return obstaclesList[index];
    }
    public void SetAvailableList()
    {
        indexList.Clear();
        for (int i = 0; i < obstaclesList.Count; i++)
        {
            if (!obstaclesList[i].inUse)
            {
                indexList.Add(i);
            }
        }
    }
    void SpawnObstacle()
    {
        Obstacle obstacle = GetAvailableObstacle();
        obstacle.inUse = true;
        Vector2 pos = spawner.transform.position;
        pos.y = Random.Range(rangeMinMax.x, rangeMinMax.y);
        obstacle.transform.position = pos;

        int random = Random.Range(0, 100);
        if (random < obstacle.chanceApearReward)
        {
            GameObject reward = rewardsManager.InstantiateReward();
            float x = obstacle.transform.position.x - distanceSpawn / 2;
            float y = 0;
            float z = 0;
            reward.transform.position = new Vector3(x, y, z);
        }
    }
}