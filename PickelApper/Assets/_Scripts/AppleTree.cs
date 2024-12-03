using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    static public AppleTree S { get; private set; }

    [Header("Dynamic")] [Range(0, 3)]
    public float healthLevel = 3;



    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("AppleTree.Awake() - Attempted to assign second AppleTree.S!");
        }

    }

    void Update()
    {

    }
}
