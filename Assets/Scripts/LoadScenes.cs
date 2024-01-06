using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Canvas loadingCanvas; // Assign loading canvas object
    [SerializeField] private Slider loadingSlider; // Assign Slider UI element

    void Awake()
    {
        // Disable loading canvas on awake
        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(false);
        }
    }

    public async void LoadSceneWithSlider(string sceneName)
    {
        
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is null or empty. Please provide a valid scene name.");
            return;
        }

        // Start loading scene asynchronously
        var scene = SceneManager.LoadSceneAsync(sceneName);
        if (scene == null)
        {
            Debug.LogError("Failed to load the scene asynchronously.");
            return;
        }
        scene.allowSceneActivation = false;

        // Activate loading canvas if it's not null
        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Loading canvas is null. Please assign a valid canvas.");
            return;
        }

        do
        {
            await Task.Delay(100);
            if (loadingSlider != null)
            {
                UpdateSliderValue(scene.progress);
            }
            else
            {
                Debug.LogError("Loading slider is null. Please assign a valid slider.");
                return;
            }
        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;

        // Deactivate loading canvas if it's not null
        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Loading canvas is null. Please assign a valid canvas.");
        }
    }

    private void UpdateSliderValue(float value)
    {
        // Ensure the value is within the valid range of the slider (0 to 1)
        value = Mathf.Clamp01(value);

        // Update the slider value if the loadingSlider is not null
        if (loadingSlider != null)
        {
            loadingSlider.value = value;
        }
        else
        {
            Debug.LogError("Loading slider is null. Please assign a valid slider.");
        }
    }

  
}
