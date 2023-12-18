using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HeartManager : MonoBehaviour
{
    public Image[] heartImages;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    public TMP_Text remainingTimeText;

    public float timerDuration = 15f; // 15 seconds for testing purposes
    private float timer;
    private int currentHearts; // Add this line to declare the variable

    public void Start()
    {
        // Initialize currentHearts to some initial value
        currentHearts = 5; // Change this value based on your requirements
        ResetTimer();
        UpdateUI();
    }

    public void Update()
    {
        UpdateTimer();
        UpdateUI();
    }

    public void UpdateTimer()
    {
        if ((timer -= Time.deltaTime) <= 0)
        {
            // Timer has reached zero, switch heart sprite from full to empty and reset the timer
            SwitchHeartSprite();
            ResetTimer();
        }
    }

    public void SwitchHeartSprite()
    {
        // Find the first full heart from left to right and switch its sprite to empty
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i].sprite == fullHeartSprite)
            {
                SetHeartSprite(heartImages[i], false);
                break; // Stop after changing the first full heart
            }
        }
    }

    public void ResetTimer() => timer = timerDuration;

    public void UpdateUI()
    {
        UpdateRemainingTimeUI();

        // Check if the timer has reached zero to update hearts
        if (timer <= 0)
        {
            SwitchHeartSprite();
        }
    }

    public int GetCurrentHearts()
    {
        // Return the current number of hearts
        return currentHearts;
    }

    public void UseHeart()
    {
        // Use a heart (decrement the count, handle regeneration logic, etc.)
        if (currentHearts > 0)
        {
            currentHearts--;
            // Optionally, trigger regeneration logic or update UI
        }
    }

    public void UpdateRemainingTimeUI()
    {
        TimeSpan remainingTime = TimeSpan.FromSeconds(timer);

        // Display the remaining time in the UI Text component
        remainingTimeText.text = $"Next heart in: {remainingTime:mm\\:ss}";
    }

    public void SetHeartSprite(Image heartImage, bool isFull) => heartImage.sprite = isFull ? emptyHeartSprite : fullHeartSprite;
}
