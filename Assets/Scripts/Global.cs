using UnityEngine;
public class Global : MonoBehaviourSingleton<Global>
{
    [Header("Layers")]
    [SerializeField] private LayerMask layerMaskPlayer;
    [SerializeField] private LayerMask layerMaskPlatform;
    [SerializeField] private LayerMask layerMaskReward;
    [SerializeField] private LayerMask layerMaskKiller;
    [SerializeField] private LayerMask layerMaskDeSpawner;
    [SerializeField] private LayerMask layerMaskAvoidedObstacle;

    public bool LayerEqualPlayer(int layer)
    {
        return layerMaskPlayer == (layerMaskPlayer | (1 << layer));
    }
    public bool LayerEqualPlatform(int layer)
    {
        return layerMaskPlatform == (layerMaskPlatform | (1 << layer));
    }
    public bool LayerEqualReward(int layer)
    {
        return layerMaskReward == (layerMaskReward | (1 << layer));
    }
    public bool LayerEqualKiller(int layer)
    {
        return layerMaskKiller == (layerMaskKiller | (1 << layer));
    }
    public bool LayerEqualDeSpawner(int layer)
    {
        return layerMaskDeSpawner == (layerMaskDeSpawner | (1 << layer));
    }
    public bool LayerEqualAvoidedObstacle(int layer)
    {
        return layerMaskAvoidedObstacle == (layerMaskAvoidedObstacle | (1 << layer));

    }
}