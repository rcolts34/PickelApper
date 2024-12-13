using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public float pHealth;
    public float maxHealth;
    public UnityEngine.UI.Image healthBar;

    public float damagePerHit = 10;

   void Awake()
    {

        GameObject hBarObject = GameObject.Find("hBar");
        if (hBarObject != null)
        {
            healthBar = hBarObject.GetComponent<UnityEngine.UI.Image>();
        }
        else
        {
            Debug.LogError("Health Bar (hBar) not found.");
        }

    }
    void Start()
    {
        maxHealth = pHealth;
    }

    public void TakeDamage(float damage)
    {
        pHealth -= damagePerHit;
        //Debug.Log($"Player took {damage}. Current Health {pHealth}");
        if (pHealth <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //DebugUtils.LogDamage(other.gameObject);
        //Debug.Log("Player Health Script - Player hit by: " + go.tag);

        if (other.CompareTag("Enemy"))
        {
            TakeDamage(damagePerHit);
            Destroy(other.gameObject);
        }
    }
    void Die()
    {
        if (pHealth <= 0)
        {
            Destroy(gameObject);
            Main.PLAYER_DIED();
            Debug.Log("Player is dead!");
        }
    }
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(pHealth / maxHealth, 0, 1);
    }
}
