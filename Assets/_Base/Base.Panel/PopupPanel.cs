using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Base.UI.Panel
{
    [ExecuteAlways]
    public class PopupPanel : Panel
    {
        [Title("Background (named correctly)")]
        public bool hasBackground = true;
        [ShowIf("@hasBackground")] public Image background;
        [ShowIf("@hasBackground")] public float targetAlpha = 0.5f;

        [Title("Popup (named correctly)")]
        public CanvasGroup popupCanvas;
        public Transform popupTransform;

        protected virtual void OnValidate()
        {
            if (hasBackground && background != null)
                background.Fade(targetAlpha);
        }

        protected override void Reset()
        {
            base.Reset();
            background = transform.Find("Background").GetComponent<Image>();
            popupCanvas = transform.Find("Popup").GetComponent<CanvasGroup>();
            popupTransform = popupCanvas.transform;
        }

        public override void Open()
        {
            gameObject.SetActive(true);

            if (hasBackground)
            {
                background.Fade(0f);
                background.DOFade(targetAlpha, openAnimationDuration);
            }

            popupCanvas.alpha = 0;
            popupCanvas.interactable = false;
            popupCanvas.DOFade(1f, openAnimationDuration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => popupCanvas.interactable = true);

            popupTransform.localScale = Vector3.one * 0.5f;
            popupTransform.DOScale(1f, openAnimationDuration)
                .SetEase(Ease.OutBack);
        }

        public override void Close()
        {
            if (hasBackground)
            {
                background.DOKill();
                background.DOFade(0f, closeAnimationDuration)
                    .SetEase(Ease.OutCubic);
            }

            popupCanvas.interactable = false;
            popupCanvas.DOKill();
            popupCanvas.DOFade(0f, closeAnimationDuration)
                .SetEase(Ease.OutCubic);

            popupTransform.DOKill();
            popupTransform.DOScale(0.5f, closeAnimationDuration)
                .SetEase(Ease.InBack)
                .OnComplete(OnCloseCompleted);
        }
    }
}