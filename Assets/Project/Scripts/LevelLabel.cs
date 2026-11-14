using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LevelLabel : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TMP_Text>().text = "LEVEL " + GameState.SelectedLevel + " - " + GameState.SelectedMode;
    }
}