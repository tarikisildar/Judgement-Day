
using System;
using System.Collections.Generic;
using System.Linq;

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
}
