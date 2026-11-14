using DG.Tweening;
using UnityEngine;

public class StaggerPop : MonoBehaviour
{
    [SerializeField] private RectTransform[] items;
    [SerializeField] private float startDelay = 0.2f;
    [SerializeField] private float stagger = 0.06f;
    [SerializeField] private float duration = 0.4f;

    private CanvasGroup[] groups;

    private void Awake()
    {
        groups = new CanvasGroup[items.Length];

        for (int i = 0; i < items.Length; i++)
        {
            groups[i] = items[i].GetComponent<CanvasGroup>();
            if (groups[i] == null) groups[i] = items[i].gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        if (groups == null) return;

        for (int i = 0; i < items.Length; i++)
        {
            items[i].DOKill();
            groups[i].DOKill();

            groups[i].alpha = 0f;
            items[i].localScale = Vector3.one * 0.8f;

            float delay = startDelay + stagger * i;
            groups[i].DOFade(1f, duration).SetDelay(delay).SetEase(Ease.OutCubic).SetUpdate(true);
            items[i].DOScale(1f, duration).SetDelay(delay).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }

    private void OnDisable()
    {
        if (groups == null) return;

        for (int i = 0; i < items.Length; i++)
        {
            items[i].DOKill();
            groups[i].DOKill();
            groups[i].alpha = 1f;
            items[i].localScale = Vector3.one;
        }
    }
}