using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CheckInputLimits : MonoBehaviour
{
    [SerializeField] private TMP_InputField ActuatorMin;
    [SerializeField] private TMP_InputField ActuatorMax;
    [SerializeField] private PanelRigConfig panelRigConfig;
    private float MinDiff = 0.01f;                      //minimum difference between MIN and MAX actuator length to prevent division by zero
    
    public void OnEndEditMin(string value)
    {
        float min = Convert.ToSingle(ActuatorMin.text, GlobalVars.myNumberFormat());
        float max = Convert.ToSingle(ActuatorMax.text, GlobalVars.myNumberFormat());

        if (min > max - MinDiff)
        {
            min = max - MinDiff;
            ActuatorMin.text = min.ToString(GlobalVars.myNumberFormat());
            Debug.LogError( "MinLength must be less than MaxLength. Entered value was cropped to match!\n" +
                            "Min: " + min + ", max: " + max);
        }
       
    }
    public void OnEndEditMax(string value)
    {
        float min = Convert.ToSingle(ActuatorMin.text, GlobalVars.myNumberFormat());
        float max = Convert.ToSingle(ActuatorMax.text, GlobalVars.myNumberFormat());

        if (min > max - MinDiff)
        {
            max = min + MinDiff;
            ActuatorMax.text = max.ToString(GlobalVars.myNumberFormat());
            Debug.LogError("MaxLength must be more than MinLength. Entered value was cropped to match!\n" +
                            "Min: " + min + ", max: " + max);
        }
    }
}
