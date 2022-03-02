using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    private Slider slider;
    private static  float MaxHealth;
    [HideInInspector] public bool settingMaxHealth = true;
    private float sliderValue;
    public float speedOfHealthChange = 1f;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

   public void setHealth(float currentHealth)
    {
        if (settingMaxHealth) { 
        MaxHealth = currentHealth;
            settingMaxHealth = false;
    }
        sliderValue =  currentHealth / MaxHealth*100;
        sliderValue = Mathf.RoundToInt(sliderValue);
        print(sliderValue);
        InvokeRepeating("ChangeSliderValue", 0.000001f, 0.01f);
    }
    void ChangeSliderValue()
    {
        if (slider.value > sliderValue) { slider.value -= speedOfHealthChange; }
        else if (slider.value < sliderValue) { slider.value += speedOfHealthChange; }
        else { CancelInvoke(); }
    }
}
