using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ThemesSO", fileName = "ThemesSO")]
public class ThemeSO : ScriptableObject
{
    [Header("Primary Color")]
    public Color primary_bg;
    public Color primary_text;

    [Header("Secondary Color")]
    public Color secondary_bg;
    public Color secondary_text;

    [Header("Tertiary Color")]
    public Color tertiary_bg;
    public Color tertiary_text;

    [Header("Other")]
    public Color disable;
}
