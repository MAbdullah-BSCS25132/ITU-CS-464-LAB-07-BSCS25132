using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip click;
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioListener.volume = SettingsData.Volume;

        if (music != null)
        {
            musicSource.clip = music;
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }

    private void Update()
    {
        musicSource.mute = !SettingsData.Music;
        sfxSource.mute = !SettingsData.Sound;
    }

    public void PlayClick()
    {
        if (click == null || !SettingsData.Sound) return;
        sfxSource.PlayOneShot(click);
    }
}