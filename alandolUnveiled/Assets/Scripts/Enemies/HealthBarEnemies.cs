using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemies : MonoBehaviour
{
    public Slider healthBar;
    

    public void UpdateHealthBar(float currentValue, float maxValue) 
    {
        healthBar.value = currentValue / maxValue;
        Debug.Log(currentValue);
    }



}
