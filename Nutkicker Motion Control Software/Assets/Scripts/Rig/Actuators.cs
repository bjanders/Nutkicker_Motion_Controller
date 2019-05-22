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

    public void OnMinLengthChanged(string value)
    {
        MinLength = Convert.ToSingle(value, GlobalVars.myNumberFormat());
    }
    public void OnMaxLengthChanged(string value)
    {
        MaxLength = Convert.ToSingle(value, GlobalVars.myNumberFormat());
    }
}
    