using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
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

    public void NextCharacter()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex >= characterDatabase.CharacterCount)
        {
            currentCharacterIndex = 0;
        }
        UpdateCharacter(currentCharacterIndex);
        Save();
    }

    public void PreviousCharacter()
    {
        currentCharacterIndex--;
        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = characterDatabase.CharacterCount - 1;
        }

        UpdateCharacter(currentCharacterIndex);
        Save();
    }

    private void UpdateCharacter(int selectedCharacterIndex)
    {
        Character character = characterDatabase.GetCharacter(selectedCharacterIndex);
        spriteRenderer.sprite = character.characterSprite;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("CharacterIndex", currentCharacterIndex);
    }

    private void Load()
    {
        currentCharacterIndex = PlayerPrefs.GetInt("CharacterIndex");
        UpdateCharacter(currentCharacterIndex);
    }
}
