using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket_1 : Enemy
{
    [Header("Basket_1 Inscribed Fields")]

    public float waveFrequency = 2;  // Sinewave (# of seconds)
    public float waveWidth = 4;      // Sinewave (width in meters)
    private float x0;                // Initial x value of pos
    private float birthTime;
    public float waveRotY = 45;

    void Start()
    {
        x0 = pos.x;
        birthTime = Time.time;
    }

    public override void Move()     // Override enemy Move function
    {
        Vector3 tempPos = pos;
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth - sin;
        pos = tempPos;

        // Y rotation
        Vector3 rot = new Vector3(0, sin *waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        base.Move();
    }
}
