using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSprite : MonoBehaviour
{
    public CharacterDatabase characterDatabase;
    public SpriteRenderer spriteRenderer;

    public string CurrentCharacterNameKey;
    private int currentCharacterIndex = 0;

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

    private void UpdateCharacter(int selectedCharacterIndex)
    {
        Debug.Log(selectedCharacterIndex);
        
        // Check if the index is within bounds
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterDatabase.character.Length)
        {
            Character character = characterDatabase.GetCharacter(selectedCharacterIndex);
            spriteRenderer.sprite = character.characterSprite;
        }
        else
        {
            Debug.LogError("Invalid character index: " + selectedCharacterIndex);
        }
    }

    private void Load()
    {
        currentCharacterIndex = PlayerPrefs.GetInt(CurrentCharacterNameKey);
        UpdateCharacter(currentCharacterIndex);
    }
}
