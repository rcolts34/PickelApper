using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public float speed = 20f; // Speed of projectile
    public float lifetime = 5f; // How long projectile lives

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after lifetime expires
    }

    void Update()
    {
        // Move forward based on its roation
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
