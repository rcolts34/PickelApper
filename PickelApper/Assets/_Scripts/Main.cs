using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main : MonoBehaviour
{
    static private Main S;

    [Header("Inscribed")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f;
    private float camWidth;
    private float camHeight;

    private BoundsCheck bndCheck;

    void Awake()
    {
        // Set bndCheck to reference BoundsCheck component on this GameObject
        S = this;
        bndCheck = GetComponent<BoundsCheck>();

        // Invoke SpawnEnemy() (every 2 seconds based on 0.5f)
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    //void SpawnEnemy()
    //{
    //    // Calculate random X-position within screen bounds
    //    //float xPosition = Random.Range(-camWidth, camWidth);

    //    // Set spawn position at the top of the screen


    //    // Instantiate the enemy
    //    //Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

    //}

    public void SpawnEnemy()
    {
        // Pick a random enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        // Postion enemy above the screen with a random x position
        float enemyInset = enemyInsetDefault;
        //if (go.GetComponent<BoundsCheck>() != null)
        //{
        //    enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        //}

        // Set the initial position for the spawned enemy
        //float xMin = -bndCheck.camWidth + enemyInset;
        //float xMax = bndCheck.camWidth - enemyInset;
        //pos.x = Random.Range(xMin, xMax);
        //pos.y = bndCheck.camHeight = enemyInset;
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(-camWidth + -enemyInset, camWidth + enemyInset);
        pos.y = camHeight + enemyInset;
        go.transform.position = pos;

        //Invoke SpawnEnemy() again
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }
}
