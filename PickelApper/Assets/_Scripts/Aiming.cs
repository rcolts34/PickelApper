using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation based on input
    public float smoothingTime = 0.5f; // Duration of the smoothing effect
    private float targetRotation = 0f; // The rotation we aim for
    private float currentRotation = 0f; // The current smooth rotation
    private float rotationVelocity = 0f; // Helper for SmoothDamp
    private Quaternion baseRotation; // Initial rotation

    void Start()
    {
        // Capture initial rotation of the player
        baseRotation = transform.rotation;
    }



    // Update is called once per frame
    void Update()
    {
        // Get mouse input
        float mouseInput = Input.GetAxis("Mouse X");

        // Calculate the target rotation change
        float rotationChange = mouseInput * rotationSpeed * Time.deltaTime;

        // Update target rotation within limits
        targetRotation = Mathf.Clamp(targetRotation + rotationChange, -55f, 55f);

        // Smoothly interpolate towards the target rotation
        currentRotation = Mathf.SmoothDamp(currentRotation, targetRotation, ref rotationVelocity, smoothingTime);

        // Apply the smoothed rotation to the player
        transform.rotation = baseRotation * Quaternion.Euler(currentRotation,0f, 0f);
    }
}
