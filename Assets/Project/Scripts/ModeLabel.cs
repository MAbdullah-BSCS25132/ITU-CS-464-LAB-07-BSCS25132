using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ModeLabel : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TMP_Text>().text = GameState.SelectedMode + " MODE";
    }
}