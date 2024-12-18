using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

    static public Vector3 Bezier(float u, params Vector3[]points)
    {
        // Setup Array
        Vector3[,] vArr = new Vector3[points.Length, points.Length];

        // Fill last row of array with elments of vList
        int r = points.Length - 1;
        for (int c = 0; c < points.Length; c++)
        {
            vArr[r, c] = points[c];
        }

        // Iterate over all other rows and interpolate points at each one
        for (r--; r>=0; r--)
        {
            for (int c = 0; c <= r ; c++)
            {
                vArr[r,c] = Vector3.LerpUnclamped(vArr[r+1,c], vArr[r+1,c+1], u);
            }

        }
        
        // When complete, array at 0,0 holds the final interpolated value
        return vArr[0,0];
    }

}
