using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] private SettingKey key = SettingKey.Music;
    [SerializeField] private RectTransform knob;
    [SerializeField] private Image track;
    [SerializeField] private TMP_Text stateText;
    [SerializeField] private float travel = 50f;
    [SerializeField] private Color onColor = new Color32(0x22, 0xD3, 0xEE, 255);
    [SerializeField] private Color offColor = new Color32(0x3A, 0x43, 0x75, 255);

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        Apply(true);
    }

    private void OnDisable()
    {
        knob.DOKill();
        track.DOKill();
    }

    private void OnClick()
    {
        SettingsData.Toggle(key);
        Apply(false);
    }

    private void Apply(bool instant)
    {
        bool on = SettingsData.Get(key);
        float x = on ? travel : -travel;
        Color c = on ? onColor : offColor;

        knob.DOKill();
        track.DOKill();

        if (stateText != null) stateText.text = on ? "ON" : "OFF";

        if (instant)
        {
            knob.anchoredPosition = new Vector2(x, 0f);
            track.color = c;
        }
        else
        {
            knob.DOAnchorPosX(x, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
            track.DOColor(c, 0.25f).SetEase(Ease.OutCubic).SetUpdate(true);
        }
    }
}