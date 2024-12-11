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
        pos.x = Random.Range(-camWidth + -enemyInset, camWidth + enemyInset);
        pos.y = camHeight + enemyInset;
        go.transform.position = pos;

        //Invoke SpawnEnemy() again
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    void DelayedRestart()
    {
        // Invoke Restart method in gameRetartDelay seconds
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void Restart()
    {
        // Reload Scene_0 to restart the game
        SceneManager.LoadScene("__Scene_0");
    }

    static public void PLAYER_DIED()
    {
        S.DelayedRestart();
    }
}
