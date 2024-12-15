using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class Projectile : MonoBehaviour
{
    public float speed = 20f; // Speed of projectile
    public float lifeTime = 5f; // How long projectile lives
    public Rigidbody rb;
    private BoundsCheck bndCheck;
    private Renderer rend;
    public GameObject projectilePrefab;

    [Header("Dynamic")]
    public Rigidbody rigid;

    [SerializeField]
    private eWeaponType _type;
    public eWeaponType type
    {
        get { return _type; }
        set { SetType(value); }
    }

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy after lifetime expires
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("Hit Enemy!");
            Destroy(gameObject); // Destroy projectile
            Destroy(other.gameObject); // Destroy enemy
        }
    }
    void Update()
    {
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp))
        {
            Destroy(gameObject);
        }

        // Move forward based on its roation
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetType(eWeaponType eType)
    {
        _type = eType;
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION(_type);
        rend.material.color = def.projectileColor;
    }

    public Vector3 vel
    {
        get { return rigid.velocity; }
        set { rigid.velocity = value; } 
    }
}

