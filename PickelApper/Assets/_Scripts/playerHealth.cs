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

    private void Awake()
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
        if (pHealth <= 0)
        {
            Die();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        Debug.Log("Player Health Script - Player hit by: " + go.tag);

        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("Player hit by Enemy! (Player Health Script)");
            //TakeDamage(damagePerHit);
            pHealth -= damagePerHit;
            Destroy(other.gameObject);
        }
    }


    void Die()
    {
        if (pHealth <= 0)
        {
            if (gameObject.CompareTag("Player"))  // Add logic for player death
            {
                Destroy(gameObject);
                Main.PLAYER_DIED();
                Debug.Log("Player is dead!");
                //healthBar.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(pHealth / maxHealth, 0, 1);
    }
}
