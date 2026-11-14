using UnityEngine;
using UnityEngine.EventSystems;

public class ModeOption : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string modeName = "CLASSIC";

    public void OnPointerDown(PointerEventData eventData)
    {
        GameState.SelectedMode = modeName;
    }
}