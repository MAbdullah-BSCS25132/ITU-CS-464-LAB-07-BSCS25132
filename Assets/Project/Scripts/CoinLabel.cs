using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CoinLabel : MonoBehaviour
{
    private TMP_Text text;
    private int shown;
    private Tween tween;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        shown = GameState.Coins;
        Draw(shown);
    }

    public void Animate()
    {
        tween?.Kill();
        float v = shown;

        tween = DOTween.To(() => v, x =>
        {
            v = x;
            shown = Mathf.RoundToInt(x);
            Draw(shown);
        }, GameState.Coins, 0.5f).SetEase(Ease.OutCubic).SetUpdate(true);

        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOPunchScale(Vector3.one * 0.15f, 0.3f, 6, 0.5f).SetUpdate(true);
    }

    private void Draw(int value)
    {
        text.text = value.ToString();
    }

    private void OnDisable()
    {
        tween?.Kill();
        transform.DOKill();
        transform.localScale = Vector3.one;
    }
}