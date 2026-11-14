using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NavButton : MonoBehaviour
{
    public enum NavAction { GoTo, Back, None }

    [SerializeField] private NavAction action = NavAction.GoTo;
    [SerializeField] private ScreenId target = ScreenId.MainMenu;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (ScreenManager.Instance == null) return;
        if (action == NavAction.None) return;

        if (action == NavAction.Back)
            ScreenManager.Instance.Back();
        else
            ScreenManager.Instance.GoTo(target.ToString());
    }
}