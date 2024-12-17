using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BoundsCheck;

/// <summary>
/// Keep GameObject on screen
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    [System.Flags]
    public enum eScreenLocs
    {
        onScreen = 0,
        offRight = 1,
        offLeft = 2,
        offUp = 4,
        offDown = 8
    }
    public enum eType { center, inset, outset };
    public PlayerHealth pHealth;



    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;


    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    public float camWidth;
    public float camHeight;


    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    public void Update()
    {
        UpdateScreenLocs();
    }

    void UpdateScreenLocs()
    {
        float checkRadius = 0;

        // Determine the check radius for center, inset, or outset bounds
        if (boundsType == eType.inset) checkRadius = -radius;
        if (boundsType == eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;

        // Check X position against camWidth
        if (pos.x > camWidth + checkRadius)
        {
            screenLocs |= eScreenLocs.offRight;
        }
        else if (pos.x < -camWidth - checkRadius)
        {
            screenLocs |= eScreenLocs.offLeft;
        }

        // Check Y position against camHeight
        if (pos.y > camHeight + checkRadius)
        {
            screenLocs |= eScreenLocs.offUp;
        }
        else if (pos.y < -camHeight - checkRadius)
        {
            screenLocs |= eScreenLocs.offDown;
        }

        // Handle clamping if needed
        if (keepOnScreen && !isOnScreen)
        {
            pos.x = Mathf.Clamp(pos.x, -camWidth - checkRadius, camWidth + checkRadius);
            pos.y = Mathf.Clamp(pos.y, -camHeight - checkRadius, camHeight + checkRadius);
            transform.position = pos;

            // Reset screenLocs to onScreen after clamping
            screenLocs = eScreenLocs.onScreen;
        }

    }

    public bool isOnScreen
    {
        get{return (screenLocs == eScreenLocs.onScreen);}

    }

    public bool isOffScreen
    {
        get{return (screenLocs == eScreenLocs.offDown);}
    }

    public bool LocIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen)
        {
            return isOnScreen; // Return true or false for onScreen status


        }

        if (checkLoc == eScreenLocs.offDown)
        {
            return isOffScreen; // Return true or false for offDown status
        }


        // For other locations, perform bitwise comparison
        return (screenLocs & checkLoc) == checkLoc;

    }
    private void HandleOffScreen()
    {
        // Logic for when the enemy moves off the bottom of the screen

        if (pHealth == null)
        {
            pHealth = GameObject.FindWithTag("Player")?.GetComponent<PlayerHealth>();
        }

        if (pHealth != null && pHealth.pHealth > 0) // Check if health is above 0
        {   

        }

        else if (pHealth != null && pHealth.pHealth <= 0) // Check if health is zero or below
        {
            Debug.Log("Player is dead, no further damage applied.");
        }

        Destroy(gameObject);
    }

}

