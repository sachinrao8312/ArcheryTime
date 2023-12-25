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
    public TMP_Text noOfHeartsText;

    public float timerDuration = 15f; // 15 seconds for testing purposes
    private float timer;
    private int currentHearts; // Add this line to declare the variable

    private int firstFullHeartIndex = -1; // Track the index of the first full heart

    public void Start()
    {
        // Initialize currentHearts to some initial value
        currentHearts = 5; // Change this value based on your requirements
        firstFullHeartIndex = FindFirstFullHeartIndex(); // Initialize the first full heart index
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
        // Find the first full heart index
        int fullHeartIndex = FindFirstFullHeartIndex();

        // If a full heart is found, switch its sprite
        if (fullHeartIndex != -1)
        {
            SetHeartSprite(heartImages[fullHeartIndex], false);
        }

        // If there was a previous full heart, revert its sprite
        if (firstFullHeartIndex != -1)
        {
            SetHeartSprite(heartImages[firstFullHeartIndex], true);
        }

        // Update the firstFullHeartIndex for the next iteration
        firstFullHeartIndex = fullHeartIndex;
    }

    private int FindFirstFullHeartIndex()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i].sprite == fullHeartSprite)
            {
                return i;
            }
        }

        return -1; // Return -1 if no full heart is found
    }

    public void ResetTimer() => timer = timerDuration;

    public void UpdateUI()
    {
        UpdateRemainingTimeUI();
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
        noOfHeartsText.text = $"{currentHearts}";
    }

    public void SetHeartSprite(Image heartImage, bool isFull) => heartImage.sprite = isFull ? emptyHeartSprite : fullHeartSprite;
}
