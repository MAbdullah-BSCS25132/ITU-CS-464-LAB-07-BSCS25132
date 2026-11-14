using System.Collections.Generic;

public static class GameState
{
    public static string SelectedMode = "CLASSIC";
    public static int SelectedLevel = 1;
    public static int Coins = 500;
    public static string SelectedShip = "FALCON";

    private static readonly HashSet<string> owned = new HashSet<string> { "FALCON" };

    public static bool IsOwned(string id)
    {
        return owned.Contains(id);
    }

    public static void Own(string id)
    {
        owned.Add(id);
    }

    public static bool TrySpend(int amount)
    {
        if (Coins < amount) return false;
        Coins -= amount;
        return true;
    }
}