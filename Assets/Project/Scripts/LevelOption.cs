using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelOption : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private bool unlocked = true;
    [SerializeField] private TMP_Text numberText;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] private Image background;
    [SerializeField] private Color unlockedColor = new Color32(0x1A, 0x21, 0x45, 255);
    [SerializeField] private Color lockedColor = new Color32(0x12, 0x17, 0x33, 255);

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        Refresh();
    }

    private void Refresh()
    {
        numberText.text = level.ToString();
        labelText.text = unlocked ? "LEVEL " + level : "LOCKED";
        background.color = unlocked ? unlockedColor : lockedColor;
        numberText.alpha = unlocked ? 1f : 0.3f;
        labelText.alpha = unlocked ? 1f : 0.5f;
    }

    private void OnClick()
    {
        if (!unlocked)
        {
            ((RectTransform)transform).DOShakeAnchorPos(0.35f, new Vector2(24f, 0f), 20, 90f, false, true).SetUpdate(true);
            return;
        }

        GameState.SelectedLevel = level;

        if (ScreenManager.Instance != null)
            ScreenManager.Instance.GoTo("Loadout");
    }
}