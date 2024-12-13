using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(float X, float Y);
    static private Main S;

    [Header("Inscribed")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f;
    public float camWidth;
    public float camHeight;
    public float gameRestartDelay = 2;
    public bool spawnEnemies = true;
    public WeaponDefinition[] weaponDefinitions;

    private BoundsCheck bndCheck;

    void Awake()
    {
        // Set bndCheck to reference BoundsCheck component on this GameObject
        S = this;
        bndCheck = GetComponent<BoundsCheck>();

        // Invoke SpawnEnemy() (every 2 seconds based on 0.5f)
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);

    }

    void Start()
    {
        //camHeight = Camera.main.orthographicSize;
        //camWidth = camHeight * Camera.main.aspect;

        //SetCursorPos(camHeight, camWidth);  // Center the Cursor
    }
    public void SpawnEnemy()
    {
        // Pick a random enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        // Postion enemy above the screen with a random x position
        float enemyInset = enemyInsetDefault;

        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(-bndCheck.camWidth + bndCheck.radius, bndCheck.camWidth - bndCheck.radius);
        pos.y = bndCheck.camHeight - bndCheck.radius;
        go.transform.position = pos;

        //Invoke SpawnEnemy() again
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);

        if (!spawnEnemies)
        {
            Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
            return;
        }

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
}
