using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShipOption : MonoBehaviour
{
    public static event Action Changed;

    [SerializeField] private string shipId = "FALCON";
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Outline outline;

    private Button button;
    private CanvasGroup group;

    private void Awake()
    {
        button = GetComponent<Button>();
        group = GetComponent<CanvasGroup>();
        if (group == null) group = gameObject.AddComponent<CanvasGroup>();
        button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        Changed += OnChanged;
        Apply(true);
    }

    private void OnDisable()
    {
        Changed -= OnChanged;
        outline.DOKill();
        transform.DOKill();
        transform.localScale = Vector3.one;
    }

    private void OnChanged()
    {
        Apply(false);
    }

    private void Apply(bool instant)
    {
        bool owned = GameState.IsOwned(shipId);
        bool selected = owned && GameState.SelectedShip == shipId;

        statusText.text = !owned ? "LOCKED" : (selected ? "SELECTED" : "TAP TO SELECT");
        group.alpha = owned ? 1f : 0.5f;

        float targetAlpha = selected ? 1f : 0f;
        float targetScale = selected ? 1.03f : 1f;

        outline.DOKill();
        transform.DOKill();

        if (instant)
        {
            Color c = outline.effectColor;
            c.a = targetAlpha;
            outline.effectColor = c;
            transform.localScale = Vector3.one * targetScale;
        }
        else
        {
            DOTween.To(() => outline.effectColor.a,
                a =>
                {
                    Color c = outline.effectColor;
                    c.a = a;
                    outline.effectColor = c;
                },
                targetAlpha, 0.25f).SetEase(Ease.OutCubic).SetUpdate(true).SetTarget(outline);

            transform.DOScale(targetScale, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }

    private void OnClick()
    {
        if (!GameState.IsOwned(shipId))
        {
            ((RectTransform)transform).DOShakeAnchorPos(0.35f, new Vector2(24f, 0f), 20, 90f, false, true).SetUpdate(true);
            return;
        }

        if (GameState.SelectedShip == shipId) return;

        GameState.SelectedShip = shipId;
        Changed?.Invoke();
    }
}