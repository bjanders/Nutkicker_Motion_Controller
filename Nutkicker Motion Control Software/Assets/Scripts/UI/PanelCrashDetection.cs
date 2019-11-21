﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class PanelCrashDetection : MonoBehaviour
{
    [Header("Detectors")]
    [SerializeField] private ExceedanceDetectorAndReporter ExecDaR_Wx;
    [SerializeField] private ExceedanceDetectorAndReporter ExecDaR_Wy;
    [SerializeField] private ExceedanceDetectorAndReporter ExecDaR_Wz;
    [SerializeField] private ExceedanceDetectorAndReporter ExecDaR_Ax;
    [SerializeField] private ExceedanceDetectorAndReporter ExecDaR_Ay;
    [SerializeField] private ExceedanceDetectorAndReporter ExecDaR_Az;
    [Header("Input fields")]
    [SerializeField] TMP_InputField Threshold_Wx;
    [SerializeField] TMP_InputField Threshold_Wy;
    [SerializeField] TMP_InputField Threshold_Wz;
    [SerializeField] TMP_InputField Threshold_Ax;
    [SerializeField] TMP_InputField Threshold_Ay;
    [SerializeField] TMP_InputField Threshold_Az;
    
    private void Start()
    {
        UpdateInputs();         //..so that the user sees the numbers in the input fields that are actually in use
    }

    public void UpdateInputs()
    {
        //float OffsetCoR =   transformer_final.Offset_Heave;             //usually a negative value
        //float Height_CoR =  transformer_height.Offset_Heave;            //this value is the height from the ground to the CoR!

        ////Update the input fields
        //COR_Offset_Input.text =         OffsetCoR.ToString(GlobalVars.myNumberFormat());
        //COR_Height_abv_Gnd_Input.text = Height_CoR.ToString(GlobalVars.myNumberFormat());
    }
}
