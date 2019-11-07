using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SignalType
{
    Line,
    Square,
    Sine,
    PerlinNoise
}

[ExecuteInEditMode]
public class SignalGenerator : MonoBehaviour
{
    [SerializeField] SignalType mode;
    [Range(0.2f,10)]
    [SerializeField] float Period;
    [SerializeField] float Amplitude;
    [SerializeField] float Offset;
    [SerializeField] float Delay;
    [SerializeField] float Value;
    [Space]
    [SerializeField] Stream Outstream;

    private void FixedUpdate()
    {
        switch (mode)
        {
            case SignalType.Line:
                GenerateLine();
                break;
            case SignalType.Square:
                GenerateSquareWave();
                break;
            case SignalType.Sine:
                GenerateSineWave();
                break;
            case SignalType.PerlinNoise:
                break;
            default:
                break;
        }
    }

    private void GenerateLine()
    {
        Value = Offset;
        Datapoint datapoint = new Datapoint(Time.fixedTime, Value, "Line Signal");
        Outstream.Push(datapoint);
    }
    private void GenerateSineWave()
    {
        Value = Mathf.Sin((2 * Mathf.PI * Time.time / Period));

        Value *= Amplitude / 2.0f;
        Value += Offset;

        Datapoint datapoint = new Datapoint(Time.time, Value, "Sine Wave");
        Outstream.Push(datapoint);
    }
    private void GenerateSquareWave()
    {
        float determinant = ((Time.time - Delay) % Period) - Period / 2;
        bool valuePositive = (determinant < 0);

        if (valuePositive)
        {
            Value = 1;
        }
        else
        {
            Value = -1;
        }

        Value *= Amplitude / 2.0f;
        Value += Offset;

        Datapoint datapoint = new Datapoint(Time.time, Value, "Square Wave");
        Outstream.Push(datapoint);
    }
}
