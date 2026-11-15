using UnityEngine;
using UnityEngine.UI;

public class StarField : MonoBehaviour
{
    [SerializeField] private Sprite starSprite;
    [SerializeField] private int count = 40;
    [SerializeField] private Vector2 sizeRange = new Vector2(4f, 10f);
    [SerializeField] private Vector2 speedRange = new Vector2(15f, 60f);
    [SerializeField] private Color starColor = new Color32(0xF4, 0xF6, 0xFF, 255);

    private RectTransform area;
    private RectTransform[] stars;
    private float[] speeds;

    private void Start()
    {
        area = (RectTransform)transform;
        Canvas.ForceUpdateCanvases();

        stars = new RectTransform[count];
        speeds = new float[count];

        float w = area.rect.width;
        float h = area.rect.height;

        for (int i = 0; i < count; i++)
        {
            GameObject go = new GameObject("Star", typeof(RectTransform), typeof(Image));
            go.transform.SetParent(area, false);

            RectTransform rt = (RectTransform)go.transform;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            rt.pivot = new Vector2(0.5f, 0.5f);

            float t = Random.value;
            float size = Mathf.Lerp(sizeRange.x, sizeRange.y, t);
            rt.sizeDelta = new Vector2(size, size);
            rt.anchoredPosition = new Vector2(Random.Range(0f, w), Random.Range(0f, h));

            Image img = go.GetComponent<Image>();
            img.sprite = starSprite;
            img.raycastTarget = false;
            Color c = starColor;
            c.a = Mathf.Lerp(0.2f, 0.8f, t);
            img.color = c;

            stars[i] = rt;
            speeds[i] = Mathf.Lerp(speedRange.x, speedRange.y, t);
        }
    }

    private void Update()
    {
        if (stars == null) return;

        float w = area.rect.width;
        float h = area.rect.height;

        for (int i = 0; i < stars.Length; i++)
        {
            Vector2 p = stars[i].anchoredPosition;
            p.y -= speeds[i] * Time.unscaledDeltaTime;

            if (p.y < -20f)
            {
                p.y = h + 20f;
                p.x = Random.Range(0f, w);
            }

            stars[i].anchoredPosition = p;
        }
    }
}