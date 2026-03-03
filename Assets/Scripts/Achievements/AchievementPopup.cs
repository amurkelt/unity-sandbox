using UnityEngine;
using TMPro;
using DG.Tweening;

public class AchievementPopup : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descText;

    public void Show(string title, string desc)
    {
        titleText.text = title;
        descText.text = desc;

        canvasGroup.alpha = 0;
        panel.anchoredPosition = new Vector2(0, -200);

        canvasGroup.DOFade(1, 0.3f);
        panel.DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack);

        DOVirtual.DelayedCall(3f, Hide);
    }

    private void Hide()
    {
        canvasGroup.DOFade(0, 0.3f);
        panel.DOAnchorPosY(-200, 0.4f);
    }
}