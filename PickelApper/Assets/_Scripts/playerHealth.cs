using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;
    //public GameObject HealthBar;
    public float damagePerHit = 10;

    void Start()
    {
        maxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        Debug.Log("Player hit by: " + go.tag);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by Enemy!");
            //TakeDamage(damagePerHit);
            health -= damagePerHit;
            Destroy(other.gameObject);
        }
    }


    void Die()
    {
        Debug.Log("Player is dead!");
        {
            if (gameObject.CompareTag("Player"))  // Add logic for player death
            {   
                Destroy(gameObject);
                Main.PLAYER_DIED();
                //healthBar.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }
}
