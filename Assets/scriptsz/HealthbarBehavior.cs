using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    private Camera mainCamera;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        // Set the camera to the main camera by default so that it doesnt have to be set manually each spawns.
        mainCamera = Camera.main;
    }

    //healthbar stats
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value= currentValue/maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //automatic set to camera to main camera & fixed position of lifebar to the parent
        if (mainCamera != null)
        {
            transform.rotation = mainCamera.transform.rotation;
            transform.position = target.position + offset;
        }


    }

    
}
