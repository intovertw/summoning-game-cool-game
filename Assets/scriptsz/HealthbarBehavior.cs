using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Transform target;
    [SerializeField] private Camera camera;
    [SerializeField] private Vector3 offset;

    void Start()
    {
      
        /*if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
        }

        // Ensure slider is not null
        if (slider != null)
        {
            slider.fillRect.GetComponentInChildren<Image>().color = high;
        }*/
     
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value= currentValue/maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
            transform.rotation = camera.transform.rotation;
            transform.position = target.position + offset;
       
       
    }

    
}
