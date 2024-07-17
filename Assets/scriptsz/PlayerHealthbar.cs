using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    //[SerializeField] private Transform target;
    [SerializeField] private Camera camera;
    [SerializeField] private Vector3 offset;

    void Start()
    {

    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    // Update is called once per frame
    void Update()
    {

        /*transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;*/


    }


}
