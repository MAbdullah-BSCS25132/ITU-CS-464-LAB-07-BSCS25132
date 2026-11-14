using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum ScreenId
{
    Title,
    MainMenu,
    ModeSelect,
    LevelSelect,
    Loadout,
    Gameplay,
    Pause,
    Results,
    Shop,
    Settings,
    Credits
}

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [Serializable]
    public class ScreenEntry
    {
        public ScreenId id;
        public CanvasGroup group;
    }

    [Header("Screens")]
    [SerializeField] private List<ScreenEntry> screens = new List<ScreenEntry>();
    [SerializeField] private ScreenId startScreen = ScreenId.Title;

    [Header("Transition")]
    [SerializeField] private float duration = 0.35f;
    [SerializeField] private float slideDistance = 120f;

    private readonly Dictionary<ScreenId, ScreenEntry> lookup = new Dictionary<ScreenId, ScreenEntry>();
    private readonly Stack<ScreenId> history = new Stack<ScreenId>();
    private ScreenId current;
    private bool hasCurrent;
    private bool busy;

    private void Awake()
    {
        Instance = this;

        foreach (ScreenEntry entry in screens)
        {
            if (entry.group == null) continue;
            lookup[entry.id] = entry;
            entry.group.alpha = 0f;
            entry.group.interactable = false;
            entry.group.blocksRaycasts = false;
            entry.group.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        Transition(startScreen, true);
    }

    // Button OnClick ke liye: screen ka naam string mein likho, e.g. "MainMenu"
    public void GoTo(string screenName)
    {
        if (!Enum.TryParse(screenName, out ScreenId target))
        {
            Debug.LogWarning("ScreenManager: unknown screen name " + screenName);
            return;
        }

        if (busy || !lookup.ContainsKey(target)) return;

        if (hasCurrent)
        {
            if (target == ScreenId.MainMenu || target == ScreenId.Title)
                history.Clear();
            else
                history.Push(current);
        }

        Transition(target, true);
    }

    // Back button ke liye
    public void Back()
    {
        if (busy || history.Count == 0) return;
        Transition(history.Pop(), false);
    }

    private void Transition(ScreenId target, bool forward)
    {
        if (!lookup.ContainsKey(target)) return;

        busy = true;
        float dir = forward ? 1f : -1f;

        CanvasGroup next = lookup[target].group;
        RectTransform nextRt = (RectTransform)next.transform;

        next.gameObject.SetActive(true);
        next.alpha = 0f;
        next.interactable = false;
        next.blocksRaycasts = false;
        nextRt.anchoredPosition = new Vector2(slideDistance * dir, 0f);

        Sequence seq = DOTween.Sequence().SetUpdate(true);

        if (hasCurrent)
        {
            CanvasGroup outgoing = lookup[current].group;
            RectTransform outRt = (RectTransform)outgoing.transform;
            outgoing.interactable = false;
            outgoing.blocksRaycasts = false;

            seq.Join(outgoing.DOFade(0f, duration * 0.8f).SetEase(Ease.InCubic));
            seq.Join(outRt.DOAnchorPosX(-slideDistance * dir, duration).SetEase(Ease.InCubic));

            seq.OnComplete(() => outgoing.gameObject.SetActive(false));
        }

        seq.Join(next.DOFade(1f, duration).SetEase(Ease.OutCubic));
        seq.Join(nextRt.DOAnchorPosX(0f, duration).SetEase(Ease.OutCubic));

        seq.OnComplete(() =>
        {
            if (hasCurrent && current != target)
            {
                CanvasGroup old = lookup[current].group;
                if (old != next) old.gameObject.SetActive(false);
            }

            next.interactable = true;
            next.blocksRaycasts = true;
            current = target;
            hasCurrent = true;
            busy = false;
        });
    }
}