using DG.Tweening;
using UnityEngine;

public class LoopMotion : MonoBehaviour
{
    public enum LoopType2 { Pulse, Spin }

    [SerializeField] private LoopType2 mode = LoopType2.Pulse;
    [SerializeField] private float pulseScale = 1.08f;
    [SerializeField] private float pulseTime = 0.8f;
    [SerializeField] private float spinTime = 8f;

    private Tween tween;

    private void OnEnable()
    {
        tween?.Kill();

        if (mode == LoopType2.Pulse)
        {
            transform.localScale = Vector3.one;
            tween = transform.DOScale(pulseScale, pulseTime)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true);
        }
        else
        {
            transform.localRotation = Quaternion.identity;
            tween = transform.DORotate(new Vector3(0f, 0f, -360f), spinTime, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(true);
        }
    }

    private void OnDisable()
    {
        tween?.Kill();
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }
}