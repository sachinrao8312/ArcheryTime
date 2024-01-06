using System.Collections;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float spawnDelayTime;
    private static bool canSpawnArrow = true;

    // Singleton instance
    public static ArrowSpawner Instance { get; private set; }

    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        Instance = this;
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
        return ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)));
    }

    private IEnumerator SpawnArrowWithDelay()
    {
        if (arrowPrefab != null)
        {
            SpawnArrow();
            yield return new WaitForSeconds(spawnDelayTime);
            canSpawnArrow = true;
        }
        else
        {
            Debug.LogError("ArrowPrefab is null. Assign a prefab to it in the Inspector.");
        }
    }

    private void SpawnArrow()
    {
        GameObject newArrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        canSpawnArrow = false;
    }

    // Method to auto-release an arrow
    public void AutoReleaseArrow()
    {
        StartCoroutine(SpawnArrowWithDelay());
    }

    // Reset the singleton instance
    private void OnDestroy()
    {
        Instance = null;
    }
}
