using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSprite : MonoBehaviour
{

    public CharacterDatabase characterDatabase;
    public SpriteRenderer spriteRenderer;

    private int currentCharacterIndex = 0;

    void Start()
    {
        if (!PlayerPrefs.HasKey("CharacterIndex"))
        {
            PlayerPrefs.SetInt("CharacterIndex", 0);
        }
        else
        {
            Load();
        }

        UpdateCharacter(currentCharacterIndex);
    }

    private void UpdateCharacter(int selectedCharacterIndex)
    {
        Character character = characterDatabase.GetCharacter(selectedCharacterIndex);
        spriteRenderer.sprite = character.characterSprite;
    }

    private void Load()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("CharacterIndex");
        UpdateCharacter(currentCharacterIndex);
    }
}
