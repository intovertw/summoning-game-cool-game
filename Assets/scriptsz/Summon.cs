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

            StartPlacingMode();
        }

        if (placingMode)
        {
            UpdatePlacementPosition();

            if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
            {
                PlaceObject();
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
        mousePos.z = 0; // Ensure the z position is 0 to place the object in 2D space
        Vector3 direction = mousePos - startPosition;
        direction = direction.normalized;

        // Limit the placement within the specified range around the character
        float distanceToCharacter = Vector3.Distance(startPosition, mousePos);
        if (distanceToCharacter <= placementRange)
        {
            objectToPlace.SetActive(true); // Ensure the object is active to show during placement
            objectToPlace.transform.position = mousePos;
        }
        else
        {
            objectToPlace.SetActive(true); // Hide the object if it's out of range
            Vector3 targetPos = startPosition + direction * placementRange;
            objectToPlace.transform.position = targetPos;
        }
    }

    void PlaceObject()
    {
        if (objectToPlace.activeSelf) // Only place the object if it's currently active (within range)
        {
            Instantiate(objectToPlace, objectToPlace.transform.position, objectToPlace.transform.rotation);
            objectToPlace.SetActive(false); // Deactivate the object after placing
            placingMode = false; // Exit placing mode
        }
    }
}

