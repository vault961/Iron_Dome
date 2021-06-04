using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform player = null;
    [SerializeField] float distance = 30f;
    

    void Awake()
    {
        if (null == player) { Debug.Log($"Player Transform null");}
    }

    void Update()
    {
        if (null == player) return;

        var playerPos = player.transform.position;
        var targetPos = new Vector3(playerPos.x, playerPos.y + distance, playerPos.z - distance);
        transform.position = targetPos;
    }
}