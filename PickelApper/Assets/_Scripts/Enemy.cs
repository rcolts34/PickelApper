using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f; // movement speed (10f = 10m/s)
    public float eHealth = 10; // Damage needed to destroy enemy
    public int score = 100; // Points earned for destroying enemy
    public float damageToPlayer = 10;

    protected BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }
    public Vector3 pos
    {
        get
        {
            return this.transform.position;
        }

        set
        {
            this.transform.position = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // Check where Enemy has gone off the bottom of the screen
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown))
        {
            {
                PlayerHealth pHealth = FindObjectOfType<PlayerHealth>();
                if (pHealth != null)
                {
                    Debug.Log("Damage: Enemy reached goal! (Enemy Script)");
                    pHealth.TakeDamage(damageToPlayer);
                }
            }

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.GetComponent<Projectile>() != null)
        {
            Debug.Log("Enemy Hit!");
            Destroy(otherGO);
            Destroy(gameObject);
        }
            else
            {
            Debug.Log("Enemy hit by non-Projectile: " + otherGO.name);
            }
    }
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
