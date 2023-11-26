using UnityEngine;

public class RotateRectTransform : MonoBehaviour
{
    public enum ScreenOrientationType
    {
        Portrait,
        Landscape
    }

    [Header("Portrait Configuration")]
    public Vector2 portraitPosition = Vector2.zero;
    public Vector3 portraitScale = Vector3.one;
    public Vector3 portraitRotation = Vector3.zero;

    [Header("Landscape Configuration")]
    public Vector2 landscapePosition = Vector2.zero;
    public Vector3 landscapeScale = Vector3.one;
    public Vector3 landscapeRotation = new Vector3(0, 0, 90);

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SetObjectPropertiesBasedOnCurrentOrientation();
    }

    private void Update()
    {
        // You can add additional logic here if needed
    }

    private void SetObjectPropertiesBasedOnCurrentOrientation()
    {
        ScreenOrientationType currentOrientation = GetCurrentOrientation();
        SetObjectProperties(currentOrientation);
    }

    private void SetObjectProperties(ScreenOrientationType orientation)
    {
        switch (orientation)
        {
            case ScreenOrientationType.Landscape:
                SetValues(landscapePosition, landscapeScale, landscapeRotation);
                break;
            case ScreenOrientationType.Portrait:
                SetValues(portraitPosition, portraitScale, portraitRotation);
                break;
        }
    }

    private void SetValues(Vector2 position, Vector3 scale, Vector3 rotation)
    {
        rectTransform.anchoredPosition = position;
        rectTransform.localScale = scale;
        rectTransform.localRotation = Quaternion.Euler(rotation);
    }

    private ScreenOrientationType GetCurrentOrientation()
    {
        ScreenOrientation currentScreenOrientation = Screen.orientation;

        if (currentScreenOrientation == ScreenOrientation.LandscapeLeft || currentScreenOrientation == ScreenOrientation.LandscapeRight)
        {
            return ScreenOrientationType.Landscape;
        }
        else
        {
            return ScreenOrientationType.Portrait;
        }
    }
}
