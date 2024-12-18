using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{

    public float moveSpeed = 2f; // Speed at which the ConveyorBelt moves
    public bool isPaused = false; // Pause state for boss fights

    void Update()
    {
        // Only move the ConveyorBelt if it is not paused
        if (!isPaused)        {
            MoveConveyorBelt();
        }
    }

    private void MoveConveyorBelt()
    {
        // Move the ConveyorBelt downward
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    // Pause the ConveyorBelt (e.g., for boss fights)
    public void PauseConveyorBelt()
    {
        isPaused = true;
    }

    // Resume the ConveyorBelt
    public void ResumeConveyorBelt()
    {
        isPaused = false;
    }

}
