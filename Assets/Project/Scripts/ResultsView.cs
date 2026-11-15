using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsView : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private RectTransform[] orbits;
    [SerializeField] private Color earnedColor = new Color32(0xFF, 0x9F, 0x1C, 255);
    [SerializeField] private Color emptyColor = new Color32(0x2B, 0x34, 0x63, 255);

    private Sequence seq;

    private void OnEnable()
    {
        seq?.Kill();

        scoreText.text = "0";
        coinsText.text = "+" + RunResult.Coins + " COINS";
        coinsText.alpha = 0f;

        int score = 0;

        seq = DOTween.Sequence().SetUpdate(true);
        seq.AppendInterval(0.4f);
        seq.Append(DOTween.To(() => score, x =>
        {
            score = x;
            scoreText.text = x.ToString("N0");
        }, RunResult.Score, 1.0f).SetEase(Ease.OutCubic));

        for (int i = 0; i < orbits.Length; i++)
        {
            Image img = orbits[i].GetComponent<Image>();
            img.color = i < RunResult.Stars ? earnedColor : emptyColor;
            orbits[i].DOKill();
            orbits[i].localScale = Vector3.zero;
            seq.Append(orbits[i].DOScale(1f, 0.4f).SetEase(Ease.OutBack));
        }

        seq.Append(coinsText.DOFade(1f, 0.3f).SetEase(Ease.OutCubic));
    }

    private void OnDisable()
    {
        seq?.Kill();

        for (int i = 0; i < orbits.Length; i++)
        {
            orbits[i].DOKill();
            orbits[i].localScale = Vector3.one;
        }
    }
}