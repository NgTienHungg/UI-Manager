using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class FadeExtensions
{
    public static Color Fade(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static void Fade(this CanvasGroup canvasGroup, float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public static void Fade(this Image image, float alpha)
    {
        image.color = image.color.Fade(alpha);
    }

    public static void Fade(this TextMeshProUGUI text, float alpha)
    {
        text.color = text.color.Fade(alpha);
    }

    public static void Fade(this SpriteRenderer renderer, float alpha)
    {
        renderer.color = renderer.color.Fade(alpha);
    }

    public static void FadeAll(this GameObject obj, float alpha)
    {
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();

        if (spriteRenderer)
            spriteRenderer.color = spriteRenderer.color.Fade(alpha);

        foreach (Transform child in obj.transform)
            FadeAll(child.gameObject, alpha);
    }

    public static void DOFade(this GameObject obj, float alpha, float duration = 0f, float delay = 0f)
    {
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.DOKill();
            spriteRenderer.DOFade(alpha, duration).SetDelay(delay);
        }

        foreach (Transform child in obj.transform)
            DOFade(child.gameObject, alpha, duration, delay);
    }
}