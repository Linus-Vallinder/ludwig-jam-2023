using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public static void GetBezierCurve(Vector3 A, Vector3 B, Vector3 C, Vector3 D, List<Vector3> sections)
    {
        //The resolution of the line
        //Make sure the resolution is adding up to 1, so 0.3 will give a gap at the end, but 0.2 will work
        float resolution = 0.1f;

        sections.Clear();

        float t = 0;

        while (t <= 1f)
        {
            Vector3 newPosition = Algorithm(A, B, C, D, t);

            sections.Add(newPosition);

            t += resolution;
        }

        sections.Add(D);
    }

    //The De Casteljau's Algorithm
    static Vector3 Algorithm(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
    {
        float oneMinusT = 1f - t;

        Vector3 Q = oneMinusT * A + t * B;
        Vector3 R = oneMinusT * B + t * C;
        Vector3 S = oneMinusT * C + t * D;

        Vector3 P = oneMinusT * Q + t * R;
        Vector3 T = oneMinusT * R + t * S;

        Vector3 U = oneMinusT * P + t * T;

        return U;
    }
}
