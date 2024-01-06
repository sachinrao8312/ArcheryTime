using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class ImageOutline : MonoBehaviour
{
    public Color color = Color.white;

    private Image _image;
    private Outline _outline;

    [SerializeField]
    private float _outlineSize = 1;

    private bool _destroyOutline;

    void OnEnable()
    {
        _image = GetComponent<Image>();
        _outline = gameObject.GetComponent<Outline>();

        if (_outline == null)
        {
            _outline = gameObject.AddComponent<Outline>();
            _destroyOutline = true;
        }

        UpdateOutline(_outlineSize);

        // Delay the destruction of the Outline component by one frame
        StartCoroutine(DelayedDestroy());
    }

    void UpdateOutline(float outline)
    {
        if (_outline != null)
        {
            _outline.effectColor = color;
            _outline.effectDistance = new Vector2(outline, -outline);
        }
    }

    IEnumerator DelayedDestroy()
    {
        yield return null; // Wait for one frame
        if (_destroyOutline)
        {
            Destroy(_outline);
        }
    }

    void OnDisable()
    {
        // Ensure the Coroutine is stopped if the component is disabled
        StopAllCoroutines();
    }

    void OnValidate()
    {
        if (enabled)
        {
            UpdateOutline(_outlineSize);
        }
    }
}
