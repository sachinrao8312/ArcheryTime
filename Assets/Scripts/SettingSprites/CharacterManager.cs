using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDatabase;
    public SpriteRenderer spriteRenderer;

    private int currentCharacterIndex = 0;

    public string CurrentCharacterNameKey;
    void Start()
    {
        if (!PlayerPrefs.HasKey(CurrentCharacterNameKey))
        {
            PlayerPrefs.SetInt(CurrentCharacterNameKey, 0);
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
        // Ensure selectedCharacterIndex is within bounds
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterDatabase.CharacterCount)
        {
            Character character = characterDatabase.GetCharacter(selectedCharacterIndex);
            spriteRenderer.sprite = character.characterSprite;
        }
        else
        {
            Debug.LogError("Invalid character index: " + selectedCharacterIndex);
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt(CurrentCharacterNameKey, currentCharacterIndex);
    }

    private void Load()
    {
        currentCharacterIndex = PlayerPrefs.GetInt(CurrentCharacterNameKey);
        UpdateCharacter(currentCharacterIndex);
    }
}
