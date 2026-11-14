using DG.Tweening;
using UnityEngine;

public class ScreenEntrance : MonoBehaviour
{
    [SerializeField] private RectTransform[] items;
    [SerializeField] private float startDelay = 0.15f;
    [SerializeField] private float stagger = 0.08f;
    [SerializeField] private float duration = 0.45f;
    [SerializeField] private float rise = 60f;

    private Vector2[] targets;
    private CanvasGroup[] groups;

    private void Awake()
    {
        targets = new Vector2[items.Length];
        groups = new CanvasGroup[items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            targets[i] = items[i].anchoredPosition;
            groups[i] = items[i].GetComponent<CanvasGroup>();
            if (groups[i] == null) groups[i] = items[i].gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        if (targets == null) return;

        for (int i = 0; i < items.Length; i++)
        {
            items[i].DOKill();
            groups[i].DOKill();

            groups[i].alpha = 0f;
            items[i].anchoredPosition = targets[i] + Vector2.down * rise;

            float delay = startDelay + stagger * i;
            groups[i].DOFade(1f, duration).SetDelay(delay).SetEase(Ease.OutCubic).SetUpdate(true);
            items[i].DOAnchorPos(targets[i], duration).SetDelay(delay).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }

    private void OnDisable()
    {
        if (targets == null) return;

        for (int i = 0; i < items.Length; i++)
        {
            items[i].DOKill();
            groups[i].DOKill();
            items[i].anchoredPosition = targets[i];
            groups[i].alpha = 1f;
        }
    }
}