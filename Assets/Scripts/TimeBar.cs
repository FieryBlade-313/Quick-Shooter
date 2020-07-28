using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxTime(float maxTime)
    {
        slider.maxValue = maxTime;
        slider.value = maxTime;
    }
    public void SetTimeVal(float time)
    {
        slider.value = time;
    }
}
