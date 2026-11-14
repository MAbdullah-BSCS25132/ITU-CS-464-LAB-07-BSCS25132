using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private float pressedScale = 0.94f;

    private Tween tween;

    public void OnPointerDown(PointerEventData eventData)
    {
        ScaleTo(pressedScale, 0.08f, Ease.OutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ScaleTo(1f, 0.30f, Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScaleTo(1f, 0.20f, Ease.OutQuad);
    }

    private void ScaleTo(float scale, float time, Ease ease)
    {
        tween?.Kill();
        tween = transform.DOScale(scale, time).SetEase(ease).SetUpdate(true);
    }

    private void OnDisable()
    {
        tween?.Kill();
        transform.localScale = Vector3.one;
    }
}