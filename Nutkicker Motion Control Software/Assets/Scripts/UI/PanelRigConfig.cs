using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class PanelRigConfig : MonoBehaviour
{
    [Header("Hardware")]
    [SerializeField] private Platform platform_base;
    [SerializeField] private Platform platform_final;
    [SerializeField] private Platform platform_park;
    [SerializeField] private Platform platform_neutral;
    [SerializeField] private Actuators actuators;
    [SerializeField] private ServoManager servomanager;

    [Header("Hardware")]
    [SerializeField] private TMP_InputField RadiusBase;
    [SerializeField] private TMP_InputField AlphaBase;
    [Space]
    [SerializeField] private TMP_InputField RadiusFinal;
    [SerializeField] private TMP_InputField AlphaFinal;

    [Header("Input Actuators")]
    [SerializeField] private TMP_InputField ActuatorMin;
    [SerializeField] private TMP_InputField ActuatorMax;

    [Header("Input Positions")]
    [SerializeField] private TMP_InputField Input_Park_X;
    [SerializeField] private TMP_InputField Input_Park_Y;
    [SerializeField] private TMP_InputField Input_Park_Z;
    [SerializeField] private TMP_InputField Input_Neutral_X;
    [SerializeField] private TMP_InputField Input_Neutral_Y;
    [SerializeField] private TMP_InputField Input_Neutral_Z;

    [Header("Input Cranks")]
    [SerializeField] private TMP_InputField Azimuth;
    [SerializeField] private TMP_InputField Crank_length;
    [SerializeField] private TMP_InputField Rod_length;
    [SerializeField] private Toggle FlipCranks;

    private void Start()
    {
        UpdateInputs();
    }
    
    public void UpdateInputs()
    {
        RadiusBase.text = platform_base.Radius.ToString(GlobalVars.myNumberFormat());
        AlphaBase.text = platform_base.Alpha.ToString(GlobalVars.myNumberFormat());

        RadiusFinal.text = platform_final.Radius.ToString(GlobalVars.myNumberFormat());
        AlphaFinal.text = platform_final.Alpha.ToString(GlobalVars.myNumberFormat());

        ActuatorMin.text = actuators.MinLength.ToString(GlobalVars.myNumberFormat());
        ActuatorMax.text = actuators.MaxLength.ToString(GlobalVars.myNumberFormat());

        //Positions:
        Input_Park_X.text = platform_park.transform.localPosition.x.ToString(GlobalVars.myNumberFormat());
        Input_Park_Y.text = platform_park.transform.localPosition.y.ToString(GlobalVars.myNumberFormat());
        Input_Park_Z.text = platform_park.transform.localPosition.z.ToString(GlobalVars.myNumberFormat());
        Input_Neutral_X.text = platform_neutral.transform.localPosition.x.ToString(GlobalVars.myNumberFormat());
        Input_Neutral_Y.text = platform_neutral.transform.localPosition.y.ToString(GlobalVars.myNumberFormat());
        Input_Neutral_Z.text = platform_neutral.transform.localPosition.z.ToString(GlobalVars.myNumberFormat());

        Azimuth.text = servomanager.azimuth.ToString(GlobalVars.myNumberFormat());
        Crank_length.text = servomanager.crank_Length.ToString(GlobalVars.myNumberFormat());
        Rod_length.text = servomanager.rod_Length.ToString(GlobalVars.myNumberFormat());
    }
}
