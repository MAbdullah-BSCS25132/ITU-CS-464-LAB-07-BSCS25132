using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private string itemId = "NOVA";
    [SerializeField] private int price = 300;
    [SerializeField] private bool oneTimePurchase = true;
    [SerializeField] private Button buyButton;
    [SerializeField] private Image buyImage;
    [SerializeField] private TMP_Text buyLabel;
    [SerializeField] private CoinLabel coinLabel;
    [SerializeField] private Color normalColor = new Color32(0xFF, 0x9F, 0x1C, 255);
    [SerializeField] private Color ownedColor = new Color32(0x3D, 0xDC, 0x97, 255);
    [SerializeField] private Color failColor = new Color32(0xFF, 0x4D, 0x6D, 255);

    private void Awake()
    {
        buyButton.onClick.AddListener(OnBuy);
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void Refresh()
    {
        bool owned = oneTimePurchase && GameState.IsOwned(itemId);
        buyImage.color = owned ? ownedColor : normalColor;
        buyLabel.text = owned ? "OWNED" : price.ToString();
    }

    private void OnBuy()
    {
        if (oneTimePurchase && GameState.IsOwned(itemId)) return;

        if (!GameState.TrySpend(price))
        {
            RectTransform rt = (RectTransform)buyButton.transform;
            rt.DOKill(true);
            rt.DOShakeAnchorPos(0.35f, new Vector2(20f, 0f), 20, 90f, false, true).SetUpdate(true);
            buyImage.DOKill();
            buyImage.color = failColor;
            buyImage.DOColor(normalColor, 0.4f).SetUpdate(true);
            return;
        }

        if (oneTimePurchase) GameState.Own(itemId);

        if (coinLabel != null) coinLabel.Animate();

        buyButton.transform.DOKill(true);
        buyButton.transform.DOPunchScale(Vector3.one * 0.12f, 0.3f, 6, 0.5f).SetUpdate(true);

        Refresh();
    }
}