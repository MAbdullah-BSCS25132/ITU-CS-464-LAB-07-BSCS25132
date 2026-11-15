using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.onValueChanged.AddListener(OnChanged);
    }

    private void OnEnable()
    {
        slider.SetValueWithoutNotify(SettingsData.Volume);
        Draw(SettingsData.Volume);
    }

    private void OnChanged(float value)
    {
        SettingsData.Volume = value;
        AudioListener.volume = value;
        Draw(value);
    }

    private void Draw(float value)
    {
        valueText.text = Mathf.RoundToInt(value * 100f) + "%";
    }
}