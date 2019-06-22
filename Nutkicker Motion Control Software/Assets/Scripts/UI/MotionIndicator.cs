﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[ExecuteInEditMode]
public class MotionIndicator : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI TMP_Text;

    [SerializeField] private Color Color_Motion;
    [SerializeField] private Color Color_Pause;
    [SerializeField] private Color Color_Park;
    [SerializeField] private Color Color_Transit;

    [SerializeField] private StartStopLogic startstoplogic;

    private void Start()
    {
        image = GetComponent<Image>();
    }
    void FixedUpdate()
    {
        switch (startstoplogic.SwitchStatus)
        {
            case StartStopStatus.Motion:
                image.color = Color_Motion;
                TMP_Text.text = "--> MOTION ACTIVE <--";
                break;
            case StartStopStatus.Pause:
                image.color = Color_Pause;
                TMP_Text.text = "Paused";
                break;
            case StartStopStatus.Park:
                image.color = Color_Park;
                TMP_Text.text = "Parked";
                break;
            case StartStopStatus.Transit:
                image.color = Color_Transit;
                TMP_Text.text = "In Transit";
                break;
            default:
                break;
        }
    }
}
