using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] Missile missile = null;
    [SerializeField] Transform firePos = null;


    void Awake()
    {
        Debug.Assert(null != missile, $"missile prefab is null");
        Debug.Assert(null != firePos, $"firePos is null");
    }

    public void LaunchMissile(Transform _target)
    {
        if (null == missile) return;
        if (null == firePos) return;

        var _missile = Instantiate(missile, firePos.position, firePos.rotation);
        _missile.Launch(_target);
    }
}