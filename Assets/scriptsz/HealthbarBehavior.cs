using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset; 

    void Start()
    {
      
        if (slider == null)
        {
            slider = GetComponentInChildren<Slider>();
        }

        // Ensure slider is not null
        if (slider != null)
        {
            slider.fillRect.GetComponentInChildren<Image>().color = high;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
        {
            // Update the slider position based on the enemy's position and the offset
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        }
       
    }

    public void SetHealth(float health, float maxHealth)
    {
        if (slider != null)
        {
            slider.gameObject.SetActive(health < maxHealth);
            slider.value = health;
            slider.maxValue = maxHealth;
            slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
        }
        
    }
}
