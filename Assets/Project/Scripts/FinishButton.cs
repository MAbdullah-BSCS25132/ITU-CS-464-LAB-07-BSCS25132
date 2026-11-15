using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FinishButton : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        int level = GameState.SelectedLevel;

        RunResult.Score = Random.Range(900, 2500) + level * 300;
        RunResult.Stars = RunResult.Score > 2600 ? 3 : (RunResult.Score > 1900 ? 2 : 1);
        RunResult.Coins = 50 + RunResult.Stars * 40;
        GameState.Coins += RunResult.Coins;

        if (ScreenManager.Instance != null)
            ScreenManager.Instance.GoTo("Results");
    }
}