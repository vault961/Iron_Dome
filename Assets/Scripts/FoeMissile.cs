using System.Collections;
using UnityEngine;

public class FoeMissile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float range = 30f;
    //[SerializeField] float height = 10f;
    [SerializeField] float deadEnd = 3000f;
    //[SerializeField] float gravity = 9.8f;


    public void Launch(float _speed)
    {
        speed = _speed;
        StartCoroutine(CoroutineLaunch());
    }

    IEnumerator CoroutineLaunch()
    {
        var startPos = transform.position;
        var targetPos = new Vector3(0f, 0f, range);

        while (DetectEnd())
        {
            var distance = Vector3.Distance(transform.position, targetPos);
            
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);


            yield return null;
        }
        
        Destroy(this);
    }

    bool DetectEnd() => 0 <= transform.localPosition.x && transform.localPosition.x <= deadEnd && 0 <= transform.localPosition.y && transform.localPosition.y <= deadEnd && 0 <= transform.localPosition.z && transform.localPosition.z <= deadEnd;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    Vector3 GetPointOnBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var u = 1f - t;
        var u2 = u * u;
        var u3 = u2 * u;
        var t2 = t * t;
        var t3 = t2 * t;

        var result = u3 * p0
                    + (3f * u2 * t) * p1
                    + (3f * u * t2) * p2
                    + t3 * p3;

        Debug.Log($"result: {result}");

        return result;
    }
}
