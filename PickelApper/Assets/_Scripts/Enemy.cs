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
    public float damagePerHit = 10;
    public PlayerHealth pHealth;
    public bool isOffScreen;

    protected BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();

        if (bndCheck == null)
        {
            Debug.LogError("BoundsCheck not found on Enemy!");
        }
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

        // Update and check bndCheck status
        if (bndCheck != null && bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown))
        {
            //DebugUtils.LogEnemy(gameObject);
            HandleOffScreen(GetBndCheck());
        }

    }

    private BoundsCheck GetBndCheck()
    {
        return bndCheck;
    }

    private void HandleOffScreen(BoundsCheck bndCheck)
    {
        // Logic for when the enemy moves off the bottom of the screen

        if (pHealth == null)
        {
            pHealth = GameObject.FindWithTag("Player")?.GetComponent<PlayerHealth>();
        }

        if (pHealth != null && pHealth.pHealth > 0) // Check if health is above 0
        {   

            //Debug.Log("Enemy reached the bottom! Damaging player.");
            pHealth.TakeDamage(damagePerHit);
        }

        else if (pHealth != null && pHealth.pHealth <= 0) // Check if health is zero or below
        {
            Debug.Log("Player is dead, no further damage applied.");
        }

        //DebugUtils.LogAndDestroy(gameObject);
        Destroy(gameObject);

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
