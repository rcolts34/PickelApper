using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoundsCheck))]
public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public PlayerHealth pHealth;

    [Header("Inscribed")]
    public float speed = 10f; // movement speed (10f = 10m/s)
    public float eHealth = 10; // Damage needed to destroy enemy
    public int score = 10; // Points earned for destroying enemy
    public float powerUpDropChance = 1f;
    protected bool calledShipDestroyed = false;
    public float damagePerHit = 10;
    public bool isOffScreen;

    protected BoundsCheck bndCheck;
    private bool hasDamagedPlayer = false;

    private void HandleOffScreen(BoundsCheck bndCheck)
    {
        if (hasDamagedPlayer) return;
        if (pHealth == null)
        {
            pHealth = GameObject.FindWithTag("Player")?.GetComponent<PlayerHealth>();
        }

    if (pHealth != null && pHealth.pHealth > 0)
    {
            pHealth.TakeDamage(damagePerHit);
            hasDamagedPlayer = true;
    }

        Destroy(gameObject);
    }

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


    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        Projectile p = otherGO.GetComponent<Projectile>();
        if (p != null)
        {
            if(bndCheck.isOnScreen)
            {
                eHealth -= Main.GET_WEAPON_DEFINITION(p.type).damageOnHit;
            }
            if (eHealth <= 0)
            {
                if(!calledShipDestroyed)
                {
                    calledShipDestroyed = true;
                    Main.SHIP_DESTROYED(this);
                }
                Destroy(this.gameObject);
            }
        }

        Destroy(otherGO);
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

        if (eHealth <= 0)
        {
            if (!calledShipDestroyed)
            {
                calledShipDestroyed = true;

                // Notify the ScoreManager
                Score.Instance.AddScore(score);

                Main.SHIP_DESTROYED(this); // Existing logic
            }
            Destroy(this.gameObject);
        }
    }

    public void DestroyEnemies()
    {
        GameObject.FindWithTag("Enemy");
        Destroy(this.gameObject);
    }
        
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
}
