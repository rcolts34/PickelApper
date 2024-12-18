using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DebugUtils : MonoBehaviour
{
    public static void LogAndDestroy(GameObject obj, [CallerMemberName] string caller = null)
    {
        if (obj != null)
        {
            Debug.Log($"Destroying {obj.name} - Called from: {caller}");
            Object.Destroy(obj);
        }
        else
        {
            Debug.LogWarning($"Attempted to destroy a null object - Called from: {caller}");
        }
    }

    public static void LogEnemy(GameObject obj, [CallerMemberName] string caller = null)
    {
        if (obj != null)
        {
            Debug.Log($"{obj.name} Out of bounds.  Destroying {obj.name} - Called from: {caller}");
            Object.Destroy(obj);
        }
        else
        {
            Debug.LogWarning($"Attempted to destroy a null object - Called from: {caller}");
        }
    }

    public static void LogDamage(GameObject obj, [CallerMemberName] string caller = null)
    {
        if (obj != null)
        {
            Debug.Log($"Player damaged by {obj.name}.  Destroying {obj.name} - Called from: {caller}");
            Object.Destroy(obj);
        }
        else
        {
            Debug.LogWarning($"Attempted to destroy a null object - Called from: {caller}");
        }
    }
}

