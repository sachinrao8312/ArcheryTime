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

    public float timerDuration = 15f;
    private float timer;
    private int currentHearts;

    private int firstFullHeartIndex = -1;

    void Start()
    {
        timerDuration  *= 60; 
        currentHearts = 5; // Set the initial number of hearts
        ResetTimer();
        UpdateUI();
    }

    void Update()
    {
        UpdateTimer();
        UpdateUI();
    }

    void UpdateTimer()
    {
        if ((timer -= Time.deltaTime) <= 0)
        {
            SwitchHeartSprite();
            ResetTimer();
        }
    }

    void SwitchHeartSprite()
    {
        if (firstFullHeartIndex > -1) // Use cached index if valid
        {
            SetHeartSprite(heartImages[firstFullHeartIndex], true);
        }

        int nextEmptyIndex = FindNextEmptyHeartIndex(firstFullHeartIndex + 1); // Optimized search starting from next position
        if (nextEmptyIndex > -1)
        {
            SetHeartSprite(heartImages[nextEmptyIndex], false);
            firstFullHeartIndex = nextEmptyIndex; // Update cached index
        }
    }

    int FindNextEmptyHeartIndex(int start)
    {
        for (int i = start; i < heartImages.Length; i++)
        {
            if (heartImages[i].sprite == emptyHeartSprite)
            {
                return i;
            }
        }
        return -1; // No empty hearts found
    }

    void ResetTimer() => timer = timerDuration;

    void UpdateUI()
    {
        UpdateRemainingTimeUI();
        noOfHeartsText.text = $"{currentHearts}";
    }

    int GetCurrentHearts() => currentHearts;

    void UseHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--;
            UpdateUI(); // Update heart UI after using a heart
        }
    }

    void UpdateRemainingTimeUI()
    {
        TimeSpan remainingTime = TimeSpan.FromSeconds(timer);
        remainingTimeText.text = $"Next heart in: {remainingTime:mm\\:ss}";
    }

    void SetHeartSprite(Image heartImage, bool isFull) => heartImage.sprite = isFull ? fullHeartSprite : emptyHeartSprite;
}
