using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronDomeMissile : MonoBehaviour
{
    public float waitTime = 0.5f;

    public void Launch(float _speed, Transform _target)
    {
        StartCoroutine(CoroutineLaunch(_speed, _target));
    }

    IEnumerator CoroutineLaunch(float _speed, Transform _target)
    {
        var startTime = 0f;

        Debug.Log($"미사일 발사");
        while (startTime <= waitTime)
        {

            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            startTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log($"유도 시작");
        while (true)
        {
            var targetPos = _target.position;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);

            var targetDir = targetPos - transform.position;
            var qua = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, qua, Time.deltaTime * 2f);

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"충돌");
        StopAllCoroutines();
        Destroy(this);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
