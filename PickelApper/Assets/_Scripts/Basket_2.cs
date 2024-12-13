using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public class Basket_2 : Enemy
{
    private float randomFloat;

    [Header("Basket_2 Inscribed Fields")]

    public float lerpTime = 10;
    public float sinEccentricity = 0.6f;
    public AnimationCurve rotCurve;

    [Header("Enemy_2 Private Fields")]

    [SerializeField]
    private float birthTime;    // Interpolation start points
    private Quaternion baseRotation;
    private Vector3 p0, p1;    // Lerp Points

    void Start()
    {
        randomFloat = Random.Range(0.0f , 1.0f);

        // Point on left side of screen
        p0 = Vector3.zero;
        p0.x = Random.Range(-bndCheck.camWidth * randomFloat, 0);
        p0.y = bndCheck.camHeight - bndCheck.radius;

        // Point on right side of screen
        p1 = Vector3.zero;

        p1.x = Random.Range(0, bndCheck.camWidth * randomFloat);
        p1.y = -bndCheck.camHeight + (2 * bndCheck.radius);

        // Randomly select left or right
        if (Random.value > 0.5f)
        {
            p0.x *= -1;
            p1.x *= -1;
        }

        // birthTime = now
        birthTime = Time.time;

        // Initial rotation
        transform.position = p0;
        transform.LookAt(p1, Vector3.back);
        baseRotation = transform.rotation;
    }

    public override void Move()
    {
        // Linear interpoloation works on u value between 0 and 1
        float u = (Time.time - birthTime) / lerpTime;

        if(u >1)
            {
                Destroy(this.gameObject);
                return;
            }

        // AnimationCurve sets Y rotation
        float enemyRot = rotCurve.Evaluate(u) * 90;
        transform.rotation = baseRotation * Quaternion.Euler(enemyRot, 0, 0);
        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));
        pos = (1 - u) * p0 + u * p1;

    }
}
