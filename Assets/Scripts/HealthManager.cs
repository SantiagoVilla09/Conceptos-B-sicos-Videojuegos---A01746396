using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private Slider slider;

    //void Start()
    //{
    //    slider = GetComponent<Slider>();
    //}

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ChangeCurrentHealth(float healthLevel)
    {
        slider.value = healthLevel;
    }

    public void InitialiseHealthBar(float healthLevel)
    {
        ChangeMaxHealth(healthLevel);
        ChangeCurrentHealth(healthLevel);
    }
}
