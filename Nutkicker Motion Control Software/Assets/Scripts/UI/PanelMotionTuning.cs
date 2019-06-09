using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteInEditMode]
public class PanelMotionTuning : MonoBehaviour
{
    [Header("Hardware")]
    [SerializeField] private Transformer transformer_LFC;
    [SerializeField] private Transformer transformer_HFC;
    [Header("Input fields")]
    [SerializeField] TMP_InputField Gain_Roll_HFC;
    [SerializeField] TMP_InputField Gain_Yaw_HFC;
    [SerializeField] TMP_InputField Gain_Pitch_HFC;

    [SerializeField] TMP_InputField Gain_Surge_HFC;
    [SerializeField] TMP_InputField Gain_Heave_HFC;
    [SerializeField] TMP_InputField Gain_Sway_HFC;

    [SerializeField] TMP_InputField Gain_Pitch_LFC;
    [SerializeField] TMP_InputField Gain_Roll_LFC;
    
    private void Start()
    {
        UpdateInputs();     //..so that the user sees the numbers in the input fields that are actually in use
    }

    public void UpdateInputs()
    {
        Gain_Roll_HFC.text = transformer_HFC.Gain_Roll.ToString(GlobalVars.myNumberFormat());
        Gain_Yaw_HFC.text = transformer_HFC.Gain_Yaw.ToString(GlobalVars.myNumberFormat());
        Gain_Pitch_HFC.text = transformer_HFC.Gain_Pitch.ToString(GlobalVars.myNumberFormat());

        Gain_Surge_HFC.text = transformer_HFC.Gain_Surge.ToString(GlobalVars.myNumberFormat());
        Gain_Heave_HFC.text = transformer_HFC.Gain_Heave.ToString(GlobalVars.myNumberFormat());
        Gain_Sway_HFC.text = transformer_HFC.Gain_Sway.ToString(GlobalVars.myNumberFormat());

        Gain_Pitch_LFC.text = transformer_LFC.Gain_Pitch.ToString(GlobalVars.myNumberFormat());
        Gain_Roll_LFC.text = transformer_LFC.Gain_Roll.ToString(GlobalVars.myNumberFormat());
    }
}