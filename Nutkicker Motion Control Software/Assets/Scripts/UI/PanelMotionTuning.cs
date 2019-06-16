using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteInEditMode]
public class PanelMotionTuning : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Transformer transformer_LFC;
    [SerializeField] private Transformer transformer_HFC;

    [SerializeField] private LowPassNthOrder Wx_HP_LP1;
    [SerializeField] private LowPassNthOrder Wy_HP_LP1;
    [SerializeField] private LowPassNthOrder Wz_HP_LP1;
    [SerializeField] private LowPassNthOrder Ax_HP_LP2;
    [SerializeField] private LowPassNthOrder Ay_HP_LP2;
    [SerializeField] private LowPassNthOrder Az_HP_LP2;
    [SerializeField] private LowPassNthOrder Ax_LP3;
    [SerializeField] private LowPassNthOrder Az_LP3;

    [SerializeField] private HighPass Wx_HP;
    [SerializeField] private HighPass Wy_HP;
    [SerializeField] private HighPass Wz_HP;
    [SerializeField] private HighPass Ax_HP;
    [SerializeField] private HighPass Ay_HP;
    [SerializeField] private HighPass Az_HP;

    [Header("HP Filter Inputs")]
    [SerializeField] TMP_InputField HP_Roll_HFC;
    [SerializeField] TMP_InputField HP_Yaw_HFC;
    [SerializeField] TMP_InputField HP_Pitch_HFC;
    [SerializeField] TMP_InputField HP_Surge_HFC;
    [SerializeField] TMP_InputField HP_Heave_HFC;
    [SerializeField] TMP_InputField HP_Sway_HFC;

    [Header("LP Filter Inputs")]
    [SerializeField] TMP_InputField LP_Roll_HFC;
    [SerializeField] TMP_InputField LP_Yaw_HFC;
    [SerializeField] TMP_InputField LP_Pitch_HFC;
    [SerializeField] TMP_InputField LP_Surge_HFC;
    [SerializeField] TMP_InputField LP_Heave_HFC;
    [SerializeField] TMP_InputField LP_Sway_HFC;
    [SerializeField] TMP_InputField LP_Pitch_LFC;
    [SerializeField] TMP_InputField LP_Roll_LFC;

    [Header("Gain Inputs")]
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
        //---------HighPass--------
        HP_Roll_HFC.text =  Wx_HP.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        HP_Yaw_HFC.text =   Wy_HP.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        HP_Pitch_HFC.text = Wz_HP.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        HP_Surge_HFC.text = Ax_HP.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        HP_Heave_HFC.text = Ay_HP.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        HP_Sway_HFC.text =  Az_HP.EMA_alpha.ToString(GlobalVars.myNumberFormat());

        //---------LowPass--------
        LP_Roll_HFC.text =  Wx_HP_LP1.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        LP_Yaw_HFC.text =   Wy_HP_LP1.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        LP_Pitch_HFC.text = Wz_HP_LP1.EMA_alpha.ToString(GlobalVars.myNumberFormat());

        LP_Surge_HFC.text = Ax_HP_LP2.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        LP_Heave_HFC.text = Ay_HP_LP2.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        LP_Sway_HFC.text =  Az_HP_LP2.EMA_alpha.ToString(GlobalVars.myNumberFormat());

        LP_Pitch_LFC.text = Ax_LP3.EMA_alpha.ToString(GlobalVars.myNumberFormat());
        LP_Roll_LFC.text =  Az_LP3.EMA_alpha.ToString(GlobalVars.myNumberFormat());

        //---------GAINS--------
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