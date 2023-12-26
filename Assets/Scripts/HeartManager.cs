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
        currentHearts = 5; // Set the initial number of hearts
        firstFullHeartIndex = FindFirstFullHeartIndex();
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
        int fullHeartIndex = FindFirstFullHeartIndex();

        if (fullHeartIndex != -1)
        {
            SetHeartSprite(heartImages[fullHeartIndex], false);
        }

        if (firstFullHeartIndex != -1)
        {
            SetHeartSprite(heartImages[firstFullHeartIndex], true);
        }

        firstFullHeartIndex = fullHeartIndex;
    }

    int FindFirstFullHeartIndex()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i].sprite == fullHeartSprite)
            {
                return i;
            }
        }

        return -1;
    }

    void ResetTimer() => timer = timerDuration;

    void UpdateUI()
    {
        UpdateRemainingTimeUI();
    }

    int GetCurrentHearts() => currentHearts;

    void UseHeart()
    {
        if (currentHearts > 0)
        {
            currentHearts--;
        }
    }

    void UpdateRemainingTimeUI()
    {
        TimeSpan remainingTime = TimeSpan.FromSeconds(timer);
        remainingTimeText.text = $"Next heart in: {remainingTime:mm\\:ss}";
        noOfHeartsText.text = $"{currentHearts}";
    }

    void SetHeartSprite(Image heartImage, bool isFull) => heartImage.sprite = isFull ? emptyHeartSprite : fullHeartSprite;
}
