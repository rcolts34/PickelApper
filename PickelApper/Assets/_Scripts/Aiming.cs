using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation based on input
    public float smoothingTime = 0.1f; // Duration of the smoothing effect
    public float rotationVelocity = 0f; // Helper for SmoothDamp
    private float targetRotation = 0f; // The rotation we aim for
    private float currentRotation = 0f; // The current smooth rotation
    private Quaternion baseRotation; // Initial rotation

    [Header("Shooting Settings")]
    public bool canFire; // Can player shoot
    private float timer = 0f;
    public float timeBetweenFiring = 0.5f; // Cooldown between shots
    public GameObject Projectile; // Projectile Prefab
    public Transform appleTransform; // Fire Point

    void Start()
    {
        baseRotation = transform.rotation;  // Capture initial rotation of the player
    }

    void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    void HandleAiming()
    {
        // Get mouse input
        float mouseInput = Input.GetAxis("Mouse X");

        // Calculate the target rotation change
        float rotationChange = mouseInput * rotationSpeed * Time.deltaTime;

        // Update target rotation within limits
        targetRotation = Mathf.Clamp(targetRotation + rotationChange, -65f, 65f);

        // Smoothly interpolate towards the target rotation
        currentRotation = Mathf.SmoothDamp(currentRotation, targetRotation, ref rotationVelocity, smoothingTime);

        // Apply the smoothed rotation to the player
        transform.rotation = baseRotation * Quaternion.Euler(currentRotation, 0f, 0f);
    }

    void HandleShooting()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;

            //GameObject projectile = Instantiate(Apple, appleTransform.position, appleTransform.rotation);
            GameObject projectile = Instantiate(Projectile, appleTransform.position, appleTransform.rotation);

            //AdjustProjectileRotation(projectile);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Use the fire point's up direction for movement
                rb.velocity = appleTransform.up * 20f;
            }
        }
    }
}
