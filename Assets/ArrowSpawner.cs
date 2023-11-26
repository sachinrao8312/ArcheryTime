// ArrowSpawner.cs

using System.Collections;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowController;
    // public AudioSource arrowReleased;

    public static bool canSpawnArrow = true;

    public static ArrowSpawner Instance { get; private set; }

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
            // arrowReleased.Play();

            canSpawnArrow = false;
            yield return new WaitForSeconds(0.8f);
            canSpawnArrow = true;
        }
        else
        {
            Debug.LogError("ArrowController is null. Assign a prefab to it in the Inspector.");
        }
    }

    public void AutoReleaseArrow()
    {
        // This method is called when the arrow should be automatically released
        if (canSpawnArrow)
        {
            StartCoroutine(SpawnArrowWithDelay());
        }
    }
}
