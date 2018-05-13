using System;
using System.Collections.Generic;
using UnityEngine;

public class Vector3EqualityComparer : IEqualityComparer<Vector3>
{
    float ep = Single.Epsilon;
    public bool Equals(Vector3 b1, Vector3 b2)
    {
        if (b2 == null && b1 == null)
            return true;
        else if (b1 == null | b2 == null)
            return false;
        else if (nearlyEqual(b1.x, b1.x, ep) && nearlyEqual(b1.y, b1.y, ep) && nearlyEqual(b1.z, b1.z, ep))
            return true;
        else
            return false;
    }
    public int GetHashCode(Vector3 bx)
    {
        int p1 = 104729;
        int p2 = 100057;
        int p3 = 101627;
        int hCode = (int)Mathf.Floor(bx.x * p1) ^ (int)Mathf.Floor(bx.y * p2) ^ (int)Mathf.Floor(bx.z * p3);
        Debug.Log(hCode);
        return hCode.GetHashCode();
    }
    public static bool nearlyEqual(float a, float b, float epsilon)
    {
        float absA = Mathf.Abs(a);
        float absB = Mathf.Abs(b);
        float diff = Mathf.Abs(a - b);

        if (a == b)
        { // shortcut, handles infinities
            return true;
        }
        else if (a == 0 || b == 0 || diff < Single.Epsilon)
        {
            // a or b is zero or both are extremely close to it
            // relative error is less meaningful here
            return diff < (epsilon * Single.Epsilon);
        }
        else
        { // use relative error
            return diff / (absA + absB) < epsilon;
        }
    }
}