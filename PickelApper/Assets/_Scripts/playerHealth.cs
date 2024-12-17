using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public float pHealth;
    public float maxHealth;
    public UnityEngine.UI.Image healthBar;

    public float damagePerHit = 10;
    private bool isInvulnerable = false;
    private bool isDead = false;

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

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        isInvulnerable = false;
    }

    void Start()
    {
        maxHealth = pHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable) return;
        
        isInvulnerable = true;
        StartCoroutine(DamageCooldown());

        pHealth -= damagePerHit;
        Debug.Log($"Player took {damage}. Current Health {pHealth}");
        if (pHealth <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        DebugUtils.LogDamage(other.gameObject);
        //Debug.Log("Player Health Script - Player hit by: " + go.tag);

        if (other.CompareTag("Enemy"))
        {
            TakeDamage(damagePerHit);
            Destroy(other.gameObject);
        }
    }
    void Die()
    {
        if (isDead) return;
        isDead = true;

        Destroy(gameObject);
        Main.PLAYER_DIED();



        if (pHealth <= 0)
        {
            Destroy(gameObject);
            Main.PLAYER_DIED();
            GameManager.Instance.ShowGameOver(Score.Instance.GetScore(), Score.Instance.GetHighScore());
            Debug.Log("Player is dead!");
        }
    }
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(pHealth / maxHealth, 0, 1);
    }
}
