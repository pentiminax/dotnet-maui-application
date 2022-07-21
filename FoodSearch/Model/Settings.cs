namespace FoodSearch.Model;

public class Settings
{
    public static string NutriScore
    {
        get => Preferences.Get(nameof(NutriScore), "ALL");
        set => Preferences.Set(nameof(NutriScore), value);
    }
}