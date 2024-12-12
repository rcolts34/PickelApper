using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerHealth pHealth;
    public PlayerHealth damagePerHit;
    public float damage;

    void Start()
    {
        
    }

    void Update()
    {
        //if (pHealth = null)
        //{
        //    Destroy();
        //}
    }
    void OnTriggerEnter(Collider other)
    {
        //Transform rootT = other.gameObject.transform.root;
        //GameObject go = rootT.gameObject;
        //Debug.Log("Player hit by: " + go.tag);

        //if (other.CompareTag("Player"))
        //{
        //    Debug.Log("Player hit by Enemy!");
        //    pHealth(takedamage);
        //}
    }
}

