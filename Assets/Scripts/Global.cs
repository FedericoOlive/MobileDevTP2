using UnityEngine;
public class Global : MonoBehaviour
{
    private static Global _funcs;
    public static Global Get() => _funcs;

    [Header("Layers")]
    [SerializeField] private LayerMask layerMaskPlayer;
    [SerializeField] private LayerMask layerMaskPlatform;
    [SerializeField] private LayerMask layerMaskReward;
    [SerializeField] private LayerMask layerMaskKiller;

    private void Awake()
    {
        _funcs = this;
    }
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
}