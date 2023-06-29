using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void UpdateHealth(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}
