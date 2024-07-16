using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject objectToPlace; // The object to place
    public float placementRange = 5f; // Maximum range around the character to place the object
    private Camera mainCam;
    private bool placingMode = false; // Flag to indicate if we are in placing mode
    private Vector3 startPosition; // Starting position for placing the object
    public GameObject bulletSpawnPointPrefab; // Prefab for the bullet spawn 
    public Rigidbody2D rb;

    public Animator animator;
    public GameObject shooting;
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("isSummoning", true);
            StartPlacingMode();
        }

        if (placingMode)
        {
            UpdatePlacementPosition();
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

            if (Input.GetMouseButtonDown(0)) 
            {
                rb.constraints = RigidbodyConstraints2D.None;
                animator.SetBool("isSummoning", false);
                PlaceObject();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                rb.constraints = RigidbodyConstraints2D.None;
                animator.SetBool("isSummoning", false);
                Invoke("enableShooting", 0.1f);
                placingMode = false;
            }
        }
    }

    void StartPlacingMode()
    {
        placingMode = true;
        startPosition = transform.position; // Set starting position around the character
    }

    void UpdatePlacementPosition()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = mousePos - startPosition;
        direction = direction.normalized;

        // Limit the placement within the specified range around the character
        float distanceToCharacter = Vector3.Distance(startPosition, mousePos);
        if (distanceToCharacter <= placementRange)
        {
            objectToPlace.SetActive(true);
            objectToPlace.transform.position = mousePos;
        }
        else
        {
            objectToPlace.SetActive(true);
            Vector3 targetPos = startPosition + direction * placementRange;
            objectToPlace.transform.position = targetPos;
        }
    }

    void PlaceObject()
    {
        if (objectToPlace.activeSelf) // Only place the object if it's currently active (within range)
        {
            GameObject placedObject = Instantiate(objectToPlace, objectToPlace.transform.position, objectToPlace.transform.rotation);
            objectToPlace.SetActive(false); 
            placingMode = false; 

            // Create and attach bullet spawn point
            GameObject bulletSpawnPoint = Instantiate(bulletSpawnPointPrefab, placedObject.transform);
            bulletSpawnPoint.transform.localPosition = Vector3.zero; // Adjust as needed
            placedObject.GetComponent<petBehavior>().bulletSpawnPoint = bulletSpawnPoint.transform;
        }
    }
}
