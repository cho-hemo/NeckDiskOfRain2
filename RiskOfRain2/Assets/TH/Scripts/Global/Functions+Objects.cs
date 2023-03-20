using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Functions
{
    public static float GetSqrDistance(Vector3 pointA, Vector3 pointB)
    {
        return (pointB - pointA).sqrMagnitude;
    }
}