using TMPro;
using UnityEngine;

public static class NumberExtensions
{
    public static int Millisecond(this float second)
    {
        return Mathf.FloorToInt(second * 1000);
    }

    public static int Round(this int a, int b)
    {
        return Mathf.Round(1f * a / b).Int();
    }

    public static int Int(this float x)
    {
        return Mathf.RoundToInt(x);
    }

    public static int Int(this TextMeshProUGUI text)
    {
        return float.Parse(text.text).Int();
    }

    public static int Percent(this float x)
    {
        return (100 * x).Int();
    }

    public static bool InRange(this int value, int min, int max)
    {
        return value >= min && value <= max;
    }

    public static bool InRange(this float value, float min, float max)
    {
        return value >= min && value <= max;
    }

    public static bool Equal(this float a, float b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a - b) <= tolerance;
    }
}