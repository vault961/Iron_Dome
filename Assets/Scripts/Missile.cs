using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] Transform body = null;

    float speed = 30f;
    float rotateSpeed = 500f;
    float waitTime = 0.5f;
    float turnSpeed = 10f;
    float viewAngle = 10f;

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    public void Launch(Transform _target)
    {
        StartCoroutine(CoroutineLaunch(_target));
    }

    IEnumerator CoroutineLaunch(Transform _target)
    {
        var startTime = 0f;

        while (startTime <= waitTime)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            startTime += Time.deltaTime;
            yield return null;
        }

        while (true)
        {
            var targetPos = _target.position;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            var dir = (targetPos - transform.position).normalized;
            var qua = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, qua, turnSpeed * Time.deltaTime);

            body.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter: {other.gameObject.name} tag: {other.gameObject.tag}");
        
        StopAllCoroutines();
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log($"OnCollisionEnter: {other.gameObject.name} tag: {other.gameObject.tag}");
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
