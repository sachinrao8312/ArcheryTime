using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowController; // Assign the arrow prefab in the Inspector
    private bool canSpawnArrow = true;

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
            Instantiate(arrowController, transform.position, transform.rotation);
            canSpawnArrow = false;
            yield return new WaitForSeconds(0.1f); // Adjust the delay duration as needed
            canSpawnArrow = true;
        }
        else
        {
            Debug.LogError("ArrowController is null. Assign a prefab to it in the Inspector.");
        }
    }
}
