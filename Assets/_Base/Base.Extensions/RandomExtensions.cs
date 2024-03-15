using System.Collections.Generic;
using UnityEngine;

public static class RandomExtensions
{
    public static int Rand(int min, int max)
    {
        return Random.Range(min, max + 1);
    }
    
    public static float Rand(float min, float max)
    {
        return Random.Range(min, max);
    }

    public static T Rand<T>(this IList<T> list, T currentValue = default)
    {
        if (list.Count == 0)
        {
            Debug.LogError("<color=red> IList is empty </color>");
        }

        // random giá trị mới cho tới khi nào khác current value
        // khi không truyền vào currentValue, mặc định là null
        while (true)
        {
            var newValue = list[Random.Range(0, list.Count)];
            if (!newValue.Equals(currentValue))
            {
                return newValue;
            }
        }
    }

    public static T Rand<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}