using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FoeController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent = null;
    [Space]
    [SerializeField] float moveSpeed = 6.5f;
    //[SerializeField] float turnSpeed = 10f;
    [SerializeField] float senseRadius = 15f;
    [SerializeField] float patrolTime = 1.5f;
    [SerializeField] float patrolWaitTime = 1.5f;
    [SerializeField] float minDistance = 3f;
    [SerializeField] FoeState state = default;


    Transform player = null;
    Coroutine coroutinePatrol = null;
    int playerLayer = 0;
    int layerMask = 0;




    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerLayer = LayerMask.NameToLayer("Player");
        layerMask = 1 << playerLayer;
    }

    void Update()
    {
        switch (state)
        {
            case FoeState.Patrol:   
                if (!SensePlayer())
                    DoPatrol();
                break;

            case FoeState.Chase:
                DoChase();
                break;
        }
    }

    bool SensePlayer()
    {
        var colls = Physics.OverlapSphere(transform.position, senseRadius, layerMask);
        if (0 < colls.Length)
        {
            Debug.Log($"Foe 플레이어 감지");
            state = FoeState.Chase;
            player = colls[0].transform;
            return true;
        }
        return false;
    }

    void DoPatrol()
    {
        if (null != coroutinePatrol) return;
        coroutinePatrol = StartCoroutine(CoroutinePatrol());
    }

    IEnumerator CoroutinePatrol()
    {
        Debug.Log($"Foe 순찰 시작");
        
        var dir = Vector3.zero;
        var randomX = Random.Range(-1f, 1f);
        var randomZ = Random.Range(-1f, 1f);
        dir.x = randomX;
        dir.z = randomZ;

        var duration = 0f;
        while (duration <= patrolTime)
        {
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            duration += Time.deltaTime;
            yield return null;
        }

        duration = 0f;
        while (duration <= patrolWaitTime)
        {
            duration += Time.deltaTime;
            yield return null;
        }
        
        coroutinePatrol = null;
    }

    void DoChase()
    {
        if (null == agent) return;
        
        if (null != coroutinePatrol) 
            StopCoroutine(coroutinePatrol);

        if (agent.isPathStale)
            return;

        agent.speed = moveSpeed;
        agent.stoppingDistance = minDistance;
        agent.destination = player.position;
        agent.isStopped = false;

        var dir = (player.position - transform.position).normalized;
    }

    bool CheckMoveable()
    {
        return true;
    }

    void CheckObstacle()
    {
    }

    void DoJump()
    {

    }


    
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"triggerEnter: {other.gameObject.name} tag: {other.gameObject.tag}");
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log($"collisionEnter: {other.gameObject.name}");
    }

    enum FoeState
    {
        Patrol,
        Chase,
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, senseRadius);
    }
#endif
}