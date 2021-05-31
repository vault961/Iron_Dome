using UnityEngine;

public class FoeMissileManager : MonoBehaviour
{
    FoeMissile missilePrefab = null;
    Transform pos = null;

    
    public FoeMissileManager(FoeMissile _missile, Transform _pos)
    {
        missilePrefab = _missile;
        pos = _pos;
    }

    public void LaunchMissile(float _speed)
    {
        if (null != missilePrefab)
        {
            var missile = Instantiate(missilePrefab, pos.localPosition, pos.localRotation);
            missile.Launch(_speed);
        }
    }
}