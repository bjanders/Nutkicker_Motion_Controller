using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CombineTwoInputs : MonoBehaviour
{
    private enum OperationMode
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    [SerializeField] TMP_InputField Input1;
    [SerializeField] TMP_InputField Input2;
    [SerializeField] TMP_InputField Output;

    [Tooltip("Choose the arithmetic operation you want the module to perform.\n" +
                "Addition: 1 + 2. \n" +
                "Subtraction: 1 - 2. \n" +
                "Multiplication: 1 * 2. \n" +
                "Division: 1 / 2.")]
    [SerializeField] private OperationMode operation;

    float value1;
    float value2;
    float result;

    public void OnInputChanged()
    {
        value1 = Convert.ToSingle(Input1.text, GlobalVars.myNumberFormat());
        value2 = Convert.ToSingle(Input2.text, GlobalVars.myNumberFormat());

        switch (operation)
        {
            case OperationMode.Addition:
                Addition();
                break;
            case OperationMode.Subtraction:
                Subtraction();
                break;
            case OperationMode.Multiplication:
                Multiplication();
                break;
            case OperationMode.Division:
                Division();
                break;
        }

        Output.text = result.ToString(GlobalVars.myNumberFormat());
    }

    
    private void Addition()
    {
        result = value1 + value2;
    }
    private void Subtraction()
    {
        result = value1 - value2;
    }
    private void Multiplication()
    {
        result = value1 * value2;
    }
    private void Division()
    {
        result = value1 / value2;
    }
}
        