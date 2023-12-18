using System.Collections;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab; // Renamed from arrowController for clarity
    private static bool canSpawnArrow = true;

    // Singleton instance
    public static ArrowSpawner Instance { get; private set; }

    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        Instance = this; // Set the instance reference in Awake to ensure it's set before Start methods
    }

    private void Start()
    {
        // Add null check for GameManager
        if (gameManager == null)
        {
            Debug.LogError("GameManager not assigned to ArrowSpawner in the Inspector.");
        }
    }

    private void Update()
    {
        if (ShouldSpawnArrow() && canSpawnArrow)
        {
            StartCoroutine(SpawnArrowWithDelay());
        }
    }

    private bool ShouldSpawnArrow()
    {
        return (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            && GameManager.Instance.arrowCount < GameManager.Instance.maxArrowCount;
    }


    private IEnumerator SpawnArrowWithDelay()
    {
        // Add null check for arrowPrefab
        if (arrowPrefab != null)
        {
            GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
            canSpawnArrow = false;
            yield return new WaitForSeconds(0.8f);
            canSpawnArrow = true;
        }
        else
        {
            Debug.LogError("ArrowPrefab is null. Assign a prefab to it in the Inspector.");
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
