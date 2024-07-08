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
<<<<<<< Updated upstream
    public Rigidbody2D rb;

    public Animator animator;
=======
    public GameObject bulletSpawnPointPrefab; // Prefab for the bullet spawn point
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
<<<<<<< Updated upstream
            animator.SetBool("isSummoning", true);
=======
>>>>>>> Stashed changes
            StartPlacingMode();
        }

        if (placingMode)
        {
            UpdatePlacementPosition();
<<<<<<< Updated upstream
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;

            if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
            {
                rb.constraints = RigidbodyConstraints2D.None;
                animator.SetBool("isSummoning", false);
                PlaceObject();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                rb.constraints = RigidbodyConstraints2D.None;
                animator.SetBool("isSummoning", false);
                placingMode = false;
            }
        }
    }
=======

            if (Input.GetMouseButtonDown(0)) 
            {
                PlaceObject();
            }
        }
    }

>>>>>>> Stashed changes
    void StartPlacingMode()
    {
        placingMode = true;
        startPosition = transform.position; // Set starting position around the character
    }

    void UpdatePlacementPosition()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
<<<<<<< Updated upstream
        mousePos.z = 0; // Ensure the z position is 0 to place the object in 2D space
=======
        mousePos.z = 0;
>>>>>>> Stashed changes
        Vector3 direction = mousePos - startPosition;
        direction = direction.normalized;

        // Limit the placement within the specified range around the character
        float distanceToCharacter = Vector3.Distance(startPosition, mousePos);
        if (distanceToCharacter <= placementRange)
        {
<<<<<<< Updated upstream
            objectToPlace.SetActive(true); // Ensure the object is active to show during placement
=======
            objectToPlace.SetActive(true);
>>>>>>> Stashed changes
            objectToPlace.transform.position = mousePos;
        }
        else
        {
<<<<<<< Updated upstream
            objectToPlace.SetActive(true); // Hide the object if it's out of range
=======
            objectToPlace.SetActive(true);
>>>>>>> Stashed changes
            Vector3 targetPos = startPosition + direction * placementRange;
            objectToPlace.transform.position = targetPos;
        }
    }

    void PlaceObject()
    {
        if (objectToPlace.activeSelf) // Only place the object if it's currently active (within range)
        {
<<<<<<< Updated upstream
            Instantiate(objectToPlace, objectToPlace.transform.position, objectToPlace.transform.rotation);
            objectToPlace.SetActive(false); // Deactivate the object after placing
            placingMode = false; // Exit placing mode
        }
    }
}

=======
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
>>>>>>> Stashed changes
