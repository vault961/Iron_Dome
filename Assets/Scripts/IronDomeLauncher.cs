using UnityEngine;

public class IronDomeLauncher : MonoBehaviour
{
    [SerializeField] IronDomeMissile missile = null;

    public void LaunchMissile(float _speed, Transform _target)
    {
        if (null != missile)
        {
            var missile = Instantiate(this.missile, transform.position, transform.rotation);
            missile.Launch(_speed, _target);
        }
    }
}