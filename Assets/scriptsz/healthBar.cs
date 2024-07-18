using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public float maxHealth, health;
    public Image bar;

    public void UpdateHealth(float maxHealth, float health)
    {
        //uses images in canvas to display health, this dictates how much green is in the bar
        bar.fillAmount = health / maxHealth;
    }
}
