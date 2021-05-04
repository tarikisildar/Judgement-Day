
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public static class Utils
{
    public static Enum GetRandomEnumValue(this Type t)
    {
        return Enum.GetValues(t) // get values from Type provided
            .OfType<Enum>() // casts to Enum
            .OrderBy(e => Guid.NewGuid()) // mess with order of results
            .FirstOrDefault(); // take first item in result
    }
    
    private static Random rng = new Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
    
    public static float Angle360(Vector2 p1, Vector2 p2, Vector2 o = default(Vector2))
    {
        Vector2 v1, v2;
        if (o == default(Vector2))
        {
            v1 = p1.normalized;
            v2 = p2.normalized;
        }
        else
        {
            v1 = (p1 - o).normalized;
            v2 = (p2 - o).normalized;
        }
        float angle = Vector2.Angle(v1, v2);
        return Mathf.Sign(Vector3.Cross(v1, v2).z) < 0 ? (360 - angle) % 360 : angle;
    }
    
    public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)       
        => self.Select((item, index) => (item, index));

    
}
