using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile: MonoBehaviour
{
    public float speed = 20f; // Speed of projectile
    public float lifetime = 5f; // How long projectile lives
    public Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy after lifetime expires
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy!");
            Destroy(gameObject); // Destroy projectile
            Destroy(other.gameObject); // Destroy enemy
        }
    }



    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Hit Enemy! (2D)");
    //        Destroy(gameObject);
    //    }
    //}


    void Update()
    {
        // Move forward based on its roation
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
