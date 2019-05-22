using System;
using UnityEngine;
using TMPro;

public class FramesPerSecond : MonoBehaviour
{
    TextMeshProUGUI GUI_text;
    private float alpha = 0.02f;
    private float EMA_Value = 50;

    private void Start()
    {
        GUI_text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        float framerate = 1.0f / Time.deltaTime;
        EMA_Value = EMA(framerate);

        GUI_text.text = Convert.ToInt16(EMA_Value).ToString() + " fps";
    }
   
   float EMA(float input)
    {
        return (alpha * input) + (1 - alpha) * EMA_Value;
    }
}

