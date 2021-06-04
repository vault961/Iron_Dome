using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
}

public static class Invoker
{
    public static void SafeInvoke(Action action)
    {
        if (null == action) return;

        try { action.Invoke(); }
        catch (Exception e) { Debug.Assert(false, $"Except: {e.Message}\n{e.StackTrace}"); }
    }

    public static void SafeInvoke<T>(Action<T> action, T value)
    {
        if (null == action) return;

        try { action.Invoke(value); }
        catch (Exception e) { Debug.Assert(false, $"Except: {e.Message}\n{e.StackTrace}"); }
    }
}

public enum Layer
{
    Missile,
}