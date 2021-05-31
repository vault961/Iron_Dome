using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float missileSpeed = 10f;

    [Space]
    [SerializeField] FoeMissile foeMissilePrefab = null;
    [SerializeField] Transform targetPos = null;
    [SerializeField] IronDomeLauncher ironDome = null;

    FoeMissileManager foeMissileManager = null;

    
    void Start()
    {
        // foeMissileManager = new FoeMissileManager(foeMissilePrefab, targetPos);
    }

    void Update()
    {
        InputControl();
    }

    void InputControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ironDome.LaunchMissile(missileSpeed, targetPos);
            //foeMissileManager.LaunchMissile(missileSpeed);
    }
}

public static class Invoker
{
    public static void SafeInvoke(Action action)
    {
        if (null == action) return;

        try { action.Invoke(); }
        catch (Exception e) { Debug.Assert(false, $"Except: {e.Message}\n{e.StackTrace}"); }
    }
}
