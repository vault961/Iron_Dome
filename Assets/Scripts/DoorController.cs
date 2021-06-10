using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Transform hinge = null;
    float senseRadius = 3f;
    bool isOpen = false;

    int playerLayer = 0;
    int foeLayer = 0;
    int layerMask = 0;


    void Awake()
    {
        playerLayer = LayerMask.NameToLayer(EnumHelper.Layer.Player.ToString());
        foeLayer = LayerMask.NameToLayer(EnumHelper.Layer.Foe.ToString());
        layerMask = 1 << playerLayer;
    }

    void Update()
    {
        SenseCharacter();
    }

    void SenseCharacter()
    {
        var colls = Physics.OverlapSphere(transform.position, senseRadius, layerMask);
        if (0 < colls.Length)
        {
            if (!isOpen)
                DoorOpen(colls[0].transform.position);
        }
        else if (isOpen)
            DoorClose();
    }

    void DoorOpen(Vector3 _characterPos)
    {
        isOpen = true;
        hinge.localEulerAngles = new Vector3(0f, 90f, 0f);

        var dir = _characterPos - transform.position;
        var dot = Vector3.Dot(transform.forward, dir);
        if (0 <= dot)
            hinge.localEulerAngles = new Vector3(0f, -90f, 0f);
        else
            hinge.localEulerAngles = new Vector3(0f, 90f, 0f);
    }

    void DoorClose()
    {
        isOpen = false;
        hinge.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, senseRadius);
    }
    #endif
}
