using System.Collections;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    // Prefab for the arrow
    public GameObject arrowController;

    // Flag to control arrow spawning
    public static bool canSpawnArrow = true;

    // Singleton instance
    public static ArrowSpawner Instance { get; private set; }

    // Reference to your GameManager
    public GameManager gameManager;

    void Start()
    {
        Instance = this; // Set the instance reference
    }

    void Update()
    {
        if (ShouldSpawnArrow() && canSpawnArrow)
        {
            StartCoroutine(SpawnArrowWithDelay());
        }
    }

    private bool ShouldSpawnArrow()
    {
        return Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
    }

    IEnumerator SpawnArrowWithDelay()
{
    if (arrowController != null)
    {
        GameObject newArrow = Instantiate(arrowController, transform.position, transform.rotation);
        canSpawnArrow = false;
        yield return new WaitForSeconds(0.8f);
        canSpawnArrow = true;
    }
    else
    {
        Debug.LogError("ArrowController is null. Assign a prefab to it in the Inspector.");
    }
}

    // Method to auto-release an arrow
    public void AutoReleaseArrow()
    {
        if (canSpawnArrow)
        {
            StartCoroutine(SpawnArrowWithDelay());
        }
    }
}
