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




        //// Determine which direction to rotate towards
        //Vector3 mouseDirection = mouse.position - transform.position;

        //// Step size is equal to speed times frame time
        //float singleStep = speed - Time.deltaTime;

        //// Roate the forward vector towards the target direction by one step    
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, mouseDirection, singleStep, 0.0f);

        //// Draw a ray pointing at the target
        //Debug.DrawRay(transform.position, newDirection, Color.red);

        //// Calculate a roatation a step closer to the target and applies rotation to the object
        //transform.rotation = Quaternion.LookRotation(newDirection);

        //Vector3 direction = mousePosition - transform.position;
        //Quaternion desiredRotation = LookRotation(direction);

        //transform.rotation = Slerp(transform.rotation, desiredRotation, rotationMultiplier * Time.deltaTime);

    }
}
