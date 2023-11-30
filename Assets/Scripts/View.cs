using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    // view
    public ViewSO viewData;
    // gameObjects
    public GameObject componentTop;
    public GameObject componentCenter;
    public GameObject componentBottom;
    // Images
    private Image imageTop;
    private Image imageCenter;
    private Image imageBottom;
    // Vertical Layout Group
    private VerticalLayoutGroup verticalLayoutGroup;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        Setup();
        Configure();
    }

    public void Setup()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        imageTop = GetComponent<Image>();
        imageCenter = GetComponent<Image>();
        imageBottom = GetComponent<Image>();
    }

    public void Configure()
    {
        if (viewData == null)
        {
            Debug.LogError("View data is null. Assign a valid ViewSO instance.");
            return;
        }

        if (viewData.theme == null)
        {
            Debug.LogError("Theme in view data is null. Assign a valid ThemeSO instance.");
            return;
        }

        if (imageTop == null || imageCenter == null || imageBottom == null)
        {
            Debug.LogError("Image components are not properly initialized.");
            return;
        }

        verticalLayoutGroup.padding = viewData.padding;
        verticalLayoutGroup.spacing = viewData.spacing;

        imageTop.color = viewData.theme.primary_bg;
        imageCenter.color = viewData.theme.secondary_bg;
        imageBottom.color = viewData.theme.tertiary_bg;
    }


    private void OnValidate()
    {
        Init();
    }
}
