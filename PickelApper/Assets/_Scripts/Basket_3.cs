using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket_3 : Enemy
{
    [Header("Basket_3 Inscribed Fields")]
    public float lerpTime = 5;
    public Vector2 midpointYRange = new Vector2(1.5f, 3);

    public bool drawDebugInfo = true;

    [Header("Basket_3 Private Fields")]

    [SerializeField]
    private Vector3[] points;
    [SerializeField]
    private float birthTime;
    private float randomFloat;

    void Start()
    {
        randomFloat = Random.Range(0.0f, 1.0f);
        points = new Vector3[3];
        points[0] = pos;

        // Set xMin and xMax the same way as Main.SpawnEnemy()
        //float xMin = bndCheck.camWidth + bndCheck.radius;
        //float xMax = bndCheck.camWidth - bndCheck.radius;
        float xMin = Random.Range(-bndCheck.camWidth * randomFloat, 0);
        float xMax = Random.Range(0, bndCheck.camWidth * randomFloat);

        // Random middle position - bottom half of screen
        points[1] = Vector3.zero;
        points[1].x = Random.Range(xMin, xMax);
        float midYMult = Random.Range(midpointYRange[0], midpointYRange[1]);
        //points[1].y = -bndCheck.camHeight * midYMult;
        points[1].y = (-bndCheck.camHeight - bndCheck.radius) * midYMult;

        // Random final position above top of screen
        points[2] = Vector3.zero;
        points[2].y = pos.y;
        points[2].x = Random.Range(xMin, xMax);

        birthTime = Time.time;

        if (drawDebugInfo) DrawDebug();
    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lerpTime;

        if(u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.rotation = Quaternion.Euler(u * 180, 0, 0);

        pos = Utils.Bezier(u, points);
    }

    void DrawDebug()
    {
        Debug.DrawLine(points[0], points[1], Color.cyan, lerpTime);
        Debug.DrawLine(points[1], points[2], Color.yellow, lerpTime);

        float numSections = 20;
        Vector3 prevPoint = points[0];

        Color col;
        Vector3 pt;
        for(int i = 1; i < numSections; i++)
        {
            float u = i / numSections;
            pt = Utils.Bezier(u, points);
            col = Color.Lerp(Color.cyan, Color.yellow, u);
            Debug.DrawLine(prevPoint, pt, col, lerpTime);
            prevPoint = pt;
        }
    }
}
