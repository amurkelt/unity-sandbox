using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class AchievementPopup : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descText;

    private Queue<(string title, string desc)> queue = new();
    private bool isShowing = false;

    public void Show(string title, string desc)
    {
        queue.Enqueue((title, desc));

        if (!isShowing)
        {
            ShowNext();
        }
    }

    private void ShowNext()
    {
        if (queue.Count == 0)
        {
            isShowing = false;
            return;
        }

        isShowing = true;
        var item = queue.Dequeue();

        titleText.text = item.title;
        descText.text = item.desc;

        canvasGroup.alpha = 0;
        panel.anchoredPosition = new Vector2(0, -200);

        canvasGroup.DOFade(1, 0.3f);
        panel.DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack);

        DOVirtual.DelayedCall(1f, HideCurrent); // adjust popup time here
    }

    private void HideCurrent()
    {
        canvasGroup.DOFade(0, 0.3f);
        panel.DOAnchorPosY(-200, 0.4f).SetEase(Ease.InBack).OnComplete(() =>
        {
            ShowNext();
        });
    }
}