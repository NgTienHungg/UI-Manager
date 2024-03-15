public static class StringExtensions
{
    public static string Color(this string str, string color)
    {
        return $"<color={color}>{str}</color>";
    }

    public static string Size(this string str, int size)
    {
        return $"<size={size}>{str}</size>";
    }

    public static string Bold(this string str)
    {
        return $"<b>{str}</b>";
    }

    public static string Italic(this string str)
    {
        return $"<i>{str}</i>";
    }
}