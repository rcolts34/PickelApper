using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public static Main Instance;

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(float X, float Y);
    static private Main S;
    static private Dictionary<eWeaponType, WeaponDefinition> WEAP_DICT;


    [Header("Inscribed")]
    public GameObject[] prefabEnemies;
    public int maxEnemies = 20; 
    public float enemySpawnRate = 0.5f;
    public int enemiesPerSpawn = 1;
    public bool spawnEnemies = true;
    public float enemyInsetDefault = 1.5f;
    public float camWidth;
    public float camHeight;
    public float gameRestartDelay = 2;
    public GameObject prefabPowerUp;
    public WeaponDefinition[] weaponDefinitions;

    public eWeaponType[] powerUpFrequency = new eWeaponType[]
    {
        eWeaponType.thrapple,
        eWeaponType.grapple, 
        eWeaponType.rapple, 
        eWeaponType.snapple,
        eWeaponType.sunLight
    };
    

    private BoundsCheck bndCheck;

    void Awake()
    {
        // Set bndCheck to reference BoundsCheck component on this GameObject
        S = this;

        bndCheck = GetComponent<BoundsCheck>();


        WEAP_DICT = new Dictionary<eWeaponType, WeaponDefinition>();
        foreach(WeaponDefinition def in weaponDefinitions)
            {
                WEAP_DICT[def.type] = def;
            }

        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

        }

    }
    public void SpawnEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemies)
        {
            return;
        }

        if (!spawnEnemies) return;

        for (int i = 0; i < enemiesPerSpawn; i++)
        {
            // Pick a random enemy prefab to instantiate
            int ndx = Random.Range(0, prefabEnemies.Length);
            GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

            //// Postion enemy above the screen with a random x position
            float enemyInset = enemyInsetDefault;

            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-bndCheck.camWidth + bndCheck.radius, bndCheck.camWidth - bndCheck.radius);
            pos.y = bndCheck.camHeight - bndCheck.radius;
            go.transform.position = pos;


        }

        //Debug.Log($"Spawn Rate Set to: {enemySpawnRate} , Enemies per spawn set to: {enemiesPerSpawn}");
        Debug.Log($"SpawnEnemy called. Active Invokes: {enemySpawnRate}, Enemies On Screen: {GameObject.FindGameObjectsWithTag("Enemy").Length}");
    }

    public void SpawnFalse()
    {
        spawnEnemies = false;
    }

    public void SpawnTrue()
    {
        spawnEnemies = true;
    }

    public void SetSpawnRate(float rate)
    {

        // Ensure the new spawn rate is set
        enemySpawnRate = rate;

        // Stop all existing invokes of SpawnEnemy
        CancelInvoke(nameof(SpawnEnemy));

        // Start a repeating invocation of SpawnEnemy with the updated rate
        InvokeRepeating(nameof(SpawnEnemy), 0f, enemySpawnRate);


    }

    public void UpdateEnemyCount(int count)
    {
        enemiesPerSpawn = count;
    }

    void DelayedRestart()
    {
        // Invoke Restart method in gameRetartDelay seconds
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void Restart()
    {
        // Reload Scene_0 to restart the game
        Debug.Log("Game Over. Scene Reloaded.");
        SceneManager.LoadScene("__Scene_0");
    }
    static public void PLAYER_DIED()
    {
        S.DelayedRestart();
    }

    static public WeaponDefinition GET_WEAPON_DEFINITION(eWeaponType wt)
    {
        if (WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }
        return (new WeaponDefinition());
    }
    static public void SHIP_DESTROYED( Enemy e)
    {
        if(Random.value <= e.powerUpDropChance)
        {
            int ndx = Random.Range(0, S.powerUpFrequency.Length);
            eWeaponType pUpType = S.powerUpFrequency[ndx];
            GameObject go = Instantiate<GameObject>(S.prefabPowerUp);
            PowerUp pUp = go.GetComponent<PowerUp>();
            pUp.SetType(pUpType);
            pUp.transform.position = e.transform.position;

        }
    }
}
