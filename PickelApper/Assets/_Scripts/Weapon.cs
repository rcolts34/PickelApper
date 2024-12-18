using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWeaponType
{
    none,       // Default Apple
    thrapple,     // tri-shot
    grapple,     // green apples - piercing shots
    rapple,     // rotten apples - explode on impact
    snapple,     // homing apples
    sunLight    // Health Powerup
}

[System.Serializable]

public class WeaponDefinition
{
    public eWeaponType      type = eWeaponType.none;
    public string           letter;
    public Color            powerUpColor = Color.white;
    public GameObject       weaponModelPrefab;
    public GameObject       projectilePrefab;
    public Color            projectileColor = Color.white;
    public float            damageOnHit = 0;
    public float            damagePerSec = 0;
    public float            delayBetweenShots = 0;
    public float            velocity = 50;

}
public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;
    [Header("Dynamic")]

    [SerializeField]

    private eWeaponType _type = eWeaponType.none;
    public WeaponDefinition def;
    public float nextShotTime;
    private GameObject weaponModel;
    private Transform shotPointTrans;
    void Start()
    {
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }

        shotPointTrans = transform.GetChild(0);
        SetType(_type);
        AppleTree appleTree = GetComponentInParent<AppleTree>();
        if (appleTree != null) appleTree.fireEvent += Fire;
    }

    public eWeaponType type
    {
        get { return (_type); }
        set { SetType(value); }

    }

    public void SetType(eWeaponType wt)
    {
        _type = wt;
        if (type == eWeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        def = Main.GET_WEAPON_DEFINITION(_type);
        if (weaponModel != null) Destroy(weaponModel);
        weaponModel = Instantiate<GameObject>(def.weaponModelPrefab, transform);
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localScale = Vector3.one;
        nextShotTime = 0; 
    }

    private void Fire()
    {
        if (!gameObject.activeInHierarchy) return;
        Projectile p;
        Vector3 vel = Vector3.up * def.velocity;

        switch (type)
        {
            case eWeaponType.none:
                p = MakeProjectile();
                p.vel = vel;
                break;

            case eWeaponType.grapple:
                p = MakeProjectile();
                p.vel = vel;
                break;

            case eWeaponType.rapple:
                p = MakeProjectile();
                p.vel = vel;
                break;

            case eWeaponType.snapple:
                p = MakeProjectile();
                p.vel = vel;
                break;

            case eWeaponType.thrapple:
                p = MakeProjectile();
                p.vel = vel;
                p = MakeProjectile();
                p.vel = vel;
                p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                p = MakeProjectile();
                p.vel = vel;
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.vel = p.transform.rotation * vel;
                break;
        }
    }

    private Projectile MakeProjectile()
    {
        GameObject go;
        go = Instantiate<GameObject>(def.projectilePrefab, PROJECTILE_ANCHOR);
        Projectile p = go.GetComponent<Projectile>();
        Vector3 pos = shotPointTrans.position;
        pos.z = 0;
        p.transform.position = pos;
        p.type = type;
        nextShotTime = Time.time + def.delayBetweenShots;
        return (p);
    }

}
