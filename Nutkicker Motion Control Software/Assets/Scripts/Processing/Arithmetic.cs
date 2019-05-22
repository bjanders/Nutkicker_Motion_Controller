using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arithmetic : MonoBehaviour
{
    [SerializeField] private Stream Input1;
    [SerializeField] private Stream Input2;
    [SerializeField] private Stream Output;
    [Tooltip(   "Choose the arithmetic operation you want the module to perform.\n" +
                "Addition: 1 + 2. \n" +
                "Subtraction: 1 - 2. \n" +
                "Multiplication: 1 * 2. \n" +
                "Division: 1 / 2.")]
    [SerializeField] private OperationMode operation;
    
    private enum OperationMode
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    private void Update()
    {
        switch (operation)
        {
            case OperationMode.Addition:
                Addition();
                break;
            case OperationMode.Subtraction:
                Subtraction();
                break;
        }
    }

    private void Addition()
    {
        Datapoint Temp = Input1.Youngest + Input2.Youngest;
        Output.Push(Temp);
    }
    private void Subtraction()
    {
        Datapoint Temp = Input1.Youngest - Input2.Youngest;
        Output.Push(Temp);
    }
}
