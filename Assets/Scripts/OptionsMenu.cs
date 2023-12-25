using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Music controller
    public Toggle musicToggle;

    // Arrow options
    public Button arrowButton;
    public ScrollRect arrowScrollRect;

    // Bow options
    public Button bowButton;
    public ScrollRect bowScrollRect;

    // Background options
    public Button backgroundButton;
    public ScrollRect backgroundScrollRect;

    void Start()
    {
        // Add listeners for button clicks
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        arrowButton.onClick.AddListener(ShowArrowOptions);
        bowButton.onClick.AddListener(ShowBowOptions);
        backgroundButton.onClick.AddListener(ShowBackgroundOptions);

        // Hide the scrollable lists initially
        HideScrollLists();
    }

    void ToggleMusic(bool isOn)
    {
        // Implement logic to toggle music based on 'isOn' value
        Debug.Log("Music toggled: " + isOn);
    }

    void ShowArrowOptions()
    {
        // Show the arrow options scroll list
        HideScrollLists();
        arrowScrollRect.gameObject.SetActive(true);
    }

    void ShowBowOptions()
    {
        // Show the bow options scroll list
        HideScrollLists();
        bowScrollRect.gameObject.SetActive(true);
    }

    void ShowBackgroundOptions()
    {
        // Show the background options scroll list
        HideScrollLists();
        backgroundScrollRect.gameObject.SetActive(true);
    }

    void HideScrollLists()
    {
        // Hide all scrollable lists
        arrowScrollRect.gameObject.SetActive(false);
        bowScrollRect.gameObject.SetActive(false);
        backgroundScrollRect.gameObject.SetActive(false);
    }

    
}

