using UnityEngine;
using System.Text;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] CharacterController controller = null;
    [SerializeField] MissileLauncher launcher = null;

    [Space]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float turnSpeed = 10f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float senseRadius = 15f;
    [SerializeField] float rayDistance = 1.1f;
    [SerializeField] Transform target = null;

    [Space]
    [SerializeField] Vector3 moveDir = default;

    int foeLayer = 0;
    int layerMask = 0;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        launcher = GetComponentInChildren<MissileLauncher>();

        foeLayer = LayerMask.NameToLayer("Foe");
        layerMask = 1 << foeLayer;
    }

    void Update()
    {
        InputControl();
    }

    void InputControl()
    {
        if (CheckMove()) OnMove();
        if (Input.GetButtonDown("Fire1")) OnFire1();
    }

    bool CheckMove() => 0 != Input.GetAxis("Horizontal") || 0 != Input.GetAxis("Vertical");

    void OnMove()
    {
        if (IsGrounded())
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            moveDir = new Vector3(h, 0f, v);
            moveDir *= moveSpeed;

            if (Input.GetButtonDown("Jump"))
                moveDir.y = jumpPower;
        }
        moveDir.y -= gravity * Time.deltaTime;
        if (Vector3.zero == moveDir) return;
        controller.Move(moveDir * Time.deltaTime);

        var lookDir = new Vector3(moveDir.x, 0f, moveDir.z);
        if (Vector3.zero == lookDir) return;
        var qua = Quaternion.LookRotation(lookDir.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, qua, turnSpeed * Time.deltaTime);
    }

    void OnFire1()
    {
        if (SenseFoe())
            LaunchMissile();
        else
        {
            Debug.Log($"There is no Foe");
        }
    }


    bool IsGrounded()
    {
        if (controller.isGrounded) return true;
        var rayOrigin = transform.position + Vector3.up * 0.1f;
        var ray = new Ray(rayOrigin, Vector3.down);
        Debug.DrawRay(rayOrigin, Vector3.down * rayDistance, Color.red);

        return Physics.Raycast(ray, rayDistance);
    }

    bool SenseFoe()
    {
        target = null;
        var colls = Physics.OverlapSphere(transform.position, senseRadius, layerMask);
        if (0 < colls.Length)   
        {
            var sb = new StringBuilder();
            sb.AppendLine($"collCount: {colls.Length}");
            for (int i = 0; i < colls.Length; ++i)
            {
                var coll = colls[i];
                sb.AppendLine($"{i}: name:{coll.name} tag:{coll.gameObject.tag}");
            }
            Debug.Log(sb.ToString());

            target = colls[0].transform;
            return true;
        }

        return false;
    }

    void LaunchMissile()
    {
        launcher.LaunchMissile(target);
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        var rayOrigin = transform.position + Vector3.up * 0.1f;
        Debug.DrawRay(rayOrigin, Vector3.down * rayDistance, Color.red);
        // Gizmos.DrawWireSphere(transform.position, senseRadius);
    }
#endif
}