using UnityEngine;
using System;

[ExecuteInEditMode]
public class Actuators : MonoBehaviour
{
    [Header("Common Values")]
    [SerializeField] public float Diameter = 0.04f;
    [SerializeField] public float MinLength;
    [SerializeField] public float MaxLength;

    [Header("Actuators")]
    [SerializeField] public Actuator Act1;
    [SerializeField] public Actuator Act2;
    [SerializeField] public Actuator Act3;
    [SerializeField] public Actuator Act4;
    [SerializeField] public Actuator Act5;
    [SerializeField] public Actuator Act6;

    [Header("Diagnostics")]
    [Range(0, 1)] [SerializeField] private float Util_Act_1;
    [Range(0, 1)] [SerializeField] private float Util_Act_2;
    [Range(0, 1)] [SerializeField] private float Util_Act_3;
    [Range(0, 1)] [SerializeField] private float Util_Act_4;
    [Range(0, 1)] [SerializeField] private float Util_Act_5;
    [Range(0, 1)] [SerializeField] private float Util_Act_6;


    private void Update()
    {
        Util_Act_1 = Act1.Utilisation;
        Util_Act_2 = Act2.Utilisation;
        Util_Act_3 = Act3.Utilisation;
        Util_Act_4 = Act4.Utilisation;
        Util_Act_5 = Act5.Utilisation;
        Util_Act_6 = Act6.Utilisation;
    }


    public void OnMinLengthChanged(string value)
    {
        MinLength = Convert.ToSingle(value, GlobalVars.myNumberFormat());
    }
    public void OnMaxLengthChanged(string value)
    {
        MaxLength = Convert.ToSingle(value, GlobalVars.myNumberFormat());
    }
    public bool OnPollForAllInLimits()
    {
        return ((Act1.InLimits && Act2.InLimits) && (Act3.InLimits && Act4.InLimits)) && (Act5.InLimits && Act6.InLimits);
    }
}
    