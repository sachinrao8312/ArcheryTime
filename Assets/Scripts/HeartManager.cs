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

    private void Start()
    {
        ResetTimer();
        UpdateUI();
    }

    private void Update()
    {
        UpdateTimer();
        UpdateUI();
    }

    private void UpdateTimer()
    {
        if ((timer -= Time.deltaTime) <= 0)
        {
            // Timer has reached zero, switch heart sprite from left to right and reset the timer
            SwitchHeartSpriteSequentially();
            ResetTimer();
        }
    }

    private void ResetTimer() => timer = timerDuration;

    private void UpdateUI()
    {
        UpdateRemainingTimeUI();
    }

    private void UpdateRemainingTimeUI()
    {
        TimeSpan remainingTime = TimeSpan.FromSeconds(timer);

        // Display the remaining time in the UI Text component
        remainingTimeText.text = $"Next heart in: {remainingTime:mm\\:ss}";
    }

    private void SwitchHeartSpriteSequentially()
    {
        // Find the first empty heart from left to right and switch its sprite to full
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i].sprite == emptyHeartSprite)
            {
                SetHeartSprite(heartImages[i], true);
                break; // Stop after changing the first empty heart
            }
        }
    }

    private void SetHeartSprite(Image heartImage, bool isFull) => heartImage.sprite = isFull ? fullHeartSprite : emptyHeartSprite;
}
