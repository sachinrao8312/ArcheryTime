using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string nextSceneName; // Name of the next scene
    public float transitionTime = 1.0f; // Duration of the transition

    private bool isTransitioning = false; // Flag to prevent multiple scene loads

    private void Start()
    {
        // You can also register the button click event programmatically here
    }

    // Method to be called when the button is clicked (set in the Unity Inspector)
    public void OnButtonClick()
    {
        // Check if a transition is already in progress
        if (!isTransitioning)
        {
            // Set the flag to true to prevent multiple scene loads
            isTransitioning = true;

            // Use a coroutine for a smooth transition
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        // Trigger any transition animations or effects here

        // Wait for the transitionTime before unloading the current scene
        yield return new WaitForSeconds(transitionTime);

        // Get the active scene index before unloading
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Unload the current scene asynchronously
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(activeSceneIndex);

        // Wait until the scene is unloaded
        while (unloadOperation != null && !unloadOperation.isDone)
        {
            yield return null;
        }

        // Optionally, destroy any additional GameObjects or assets from the previous scene
        // For example, you can destroy the GameObject that has this script attached to it
        Destroy(gameObject);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
