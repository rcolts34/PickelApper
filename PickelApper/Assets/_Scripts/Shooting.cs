using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    //private Camera mainCam;
    //private Vector3 mousePos;
    //private Vector3 shootPos;
    //public GameObject Apple; // Projectile Prefab
    public GameObject Projectile; // Projectile Prefab
    public Transform appleTransform; // Fire Point
    //public Transform rotatePoint;
    public bool canFire; // Can player shoot
    private float timer =0f;
    public float timeBetweenFiring = 0.5f; // Cooldown between shots
    private Quaternion baseRotation; // Initial rotation


    void Start()
    {
        baseRotation = transform.rotation;
    }

    void Update()
    {
        // Apply the smoothed rotation to the player
        //transform.rotation = baseRotation * Quaternion.Euler(currentRotation, 0f, 0f);

        //mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 rotation = shootPos - transform.position;

        //float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotZ);
        //shootPos = appleTransform.transform.position;

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
            //Instantiate(Apple, appleTransform.position, Quaternion.identity);
            //Instantiate(Apple, appleTransform.position, transform.rotation);
            Instantiate(Projectile, appleTransform.position, transform.rotation);

            // Correct projectile initial rotation
            //Apple.transform.rotation = appleTransform.rotation;
            Projectile.transform.rotation = appleTransform.rotation;
        }
    }
}
