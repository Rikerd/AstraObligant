using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierTimerSlider : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = GameManager.gm.setScoreMultiplierTimer;
        slider.minValue = 0;
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = GameManager.gm.getCurrentMultiplierTimer();

        if (slider.value < 0)
        {
            slider.value = 0;
        }
    }
}
