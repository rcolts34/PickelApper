using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    static public AppleTree S { get; private set; }

    [Header("Dynamic")][Range(0, 3)] [SerializeField]
    //private float _shieldLevel = 1;
    public float _healthLevel = 1;
    private GameObject lastTriggerGo = null;
    public delegate void WeaponFireDelegate();
    public event WeaponFireDelegate fireEvent;
    public Weapon[] weapons;


    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == eWeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }
    

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        if (go == lastTriggerGo) return;
        lastTriggerGo = go;
        Enemy enemy = go.GetComponent<Enemy>();
        PowerUp pUp = go.GetComponent<PowerUp>();
        if (enemy != null)
        {
            healthLevel--;
            Destroy(go);
        }
        else if (pUp != null)
        {

            AbsorbPowerUp(pUp);

        }
    }

    public void AbsorbPowerUp(PowerUp pUp)
    {
        switch (pUp.type) 
        {
            case eWeaponType.sunLight:
                healthLevel++;
                break;

            default:
                if (pUp.type == weapons[0].type)
                {
                    Weapon weap = GetEmptyWeaponSlot();
                    if (weap != null)
                    {
                        weap.SetType(pUp.type);
                    }
                }
                else
                {
                    ClearWeapons();
                    weapons[0].SetType(pUp.type);
                }
                break;
        }
        pUp.AbsorbedBy(this.gameObject);
    }
    public float healthLevel
    {
        get { return ( _healthLevel); } 
        private set
        {
            _healthLevel = Mathf.Min(value, 3);
            if (value <0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
        w.SetType(eWeaponType.none);
        }
    }



void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("AppleTree.Awake() - Attempted to assign second AppleTree.S!");
        }
        //fireEvent += TempFire;

        ClearWeapons();
        weapons[0].SetType(eWeaponType.none);
    }

    void Update()
    {
        if (Input.GetAxis("Fire1") == 1 && fireEvent != null)
            fireEvent();


    }




    //void TempFire()
    //{
    //    GameObject projGO = Instantiate<GameObject>(projectilePrefab);
    //    projGO.transform.position = transform.position;
    //    Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

    //    Projectile proj = projGO.GetComponent<Projectile>();
    //    proj.type = eWeaponType.thrapple;
    //    float tSpeed = Main.GET_WEAPON_DEFINITION(proj.type).velocity;
    //    rigidB.velocity = Vector3.up * tSpeed;

    //}




}
