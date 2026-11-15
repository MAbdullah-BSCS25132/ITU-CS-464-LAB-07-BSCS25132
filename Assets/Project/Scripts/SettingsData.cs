public enum SettingKey { Music, Sound, Vibration }

public static class SettingsData
{
    public static bool Music = true;
    public static bool Sound = true;
    public static bool Vibration = true;
    public static float Volume = 0.8f;

    public static bool Get(SettingKey key)
    {
        switch (key)
        {
            case SettingKey.Music: return Music;
            case SettingKey.Sound: return Sound;
            default: return Vibration;
        }
    }

    public static void Toggle(SettingKey key)
    {
        switch (key)
        {
            case SettingKey.Music: Music = !Music; break;
            case SettingKey.Sound: Sound = !Sound; break;
            default: Vibration = !Vibration; break;
        }
    }
}