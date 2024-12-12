using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket_2 : Enemy
{
    [Header("Basket_2 Inscribed Fields")]

    public float lifeTime = 10;
    public float sinEccentricity = 0.6f;
    public AnimationCurve rotCurve;

    [Header("Enemy_2 Private Fields")]

    [SerializeField] private float birthTime;    // Interpolation start points
    [SerializeField] private Vector3 p0, p1;    // Lerp Points

    private void Start()
    {
        // Point on left side of screen
        p0 = Vector3.zero;
        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p0.y = Random.Range(bndCheck.camHeight - (bndCheck.camHeight / 2), bndCheck.camHeight);

        // Point on right side of screen
        p1 = Vector3.zero;
        p1.x = bndCheck.camWidth + bndCheck.radius;
        p1.y = Random.Range(bndCheck.camHeight - (bndCheck.camHeight / 2), bndCheck.camHeight);

        // Randomly select left or right
        if(Random.value > 0.5f)
        {
            p0.x *= -1;
            p0.y *= -1;
        }

        // birthTime = now
        birthTime = Time.time;
    }

    public override void Move()
    {
        // Linear interpoloation works on u value between 0 and 1
        float u = (Time.time - birthTime) / lifeTime;

        if(u >1)
            {
                Destroy(this.gameObject);
                return;
            }

        // AnimationCurve sets Y rotation
        float shipRot = rotCurve.Evaluate(u) * 360;
        if (p0.x > p1.x) shipRot = -shipRot;
        transform.rotation = Quaternion.Euler(0, shipRot, 0);

        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));

        pos = (1 - u) * p0 + u * p1;

        base.Move();
    }



}
