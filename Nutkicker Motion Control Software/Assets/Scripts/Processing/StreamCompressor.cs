using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.Events;

public enum CompressionType
{
    Tangent,
    Other
}

[ExecuteInEditMode]
public class StreamCompressor : MonoBehaviour
{
    [Header("Streams")]
    [SerializeField] private Stream InStream;
    [SerializeField] private Stream OutStream;

    [Header("Compression Parameter")]
    [SerializeField] private CompressionType Type;
    [SerializeField] private float LargeInput = 10.0f;                          //Pick a typically "large Input"
    [Range(0.01f, 5.0f)] [SerializeField] public float Compression = 0.01f;      //How much sensitivity do you want around zero? 0.01 = 100%, 1 = 126%, 5 = 363%
    [Space]
    [ShowOnly] [SerializeField] private float Input;
    [ShowOnly] [SerializeField] private float factor;
    [ShowOnly] [SerializeField] private float Output;
    [ShowOnly] [SerializeField] private float Utilisation;

    /////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (transform.parent.gameObject.GetComponent<Stream>() != null)         //if the parent has a Stream...
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();      //...suck on it!
        }

        OutStream = GetComponent<Stream>();

        OutStream.name = InStream.name + "_COMP";                    //Add your Tag.
    }
    void FixedUpdate()
    {
        Input = InStream.Youngest.Datavalue;

        switch (Type)
        {
            case CompressionType.Tangent:
                Compression_Tanget();
                break;
            case CompressionType.Other:
                Compression_Other();
                break;
        }

        Datapoint datapoint = new Datapoint(Time.time, Output);
        OutStream.Push(datapoint);
    }
    /////////////////////////////////////////////////////////////////////////

    private void Compression_Tanget()
    {
        Utilisation = Input / LargeInput;
        float Atan = Mathf.Atan(Utilisation * Compression) / (Mathf.PI / 2);
        float UncorrectedOutput = 2 * Atan * LargeInput;

        Output = UncorrectedOutput * Correction();
        factor = Output / Input;                            //FYI
    }

    private void Compression_Other()
    {
        throw new NotImplementedException();
    }

    private float Correction()
    {
        //This function calculates a correction value to stretch the curve back up, so that a "LargeInput" generates an equally "LargeOutput"
        float Atan = Mathf.Atan(Compression) / (Mathf.PI / 2);
        float UncorrectedOutput = 2 * Atan * LargeInput;

        return LargeInput / UncorrectedOutput;
    }
}







//Pseudo Code:
//class StreamCompressor
//{

//    float LargeInput = 10.0f;                        //Pick a typically "large Input"
//    float Coefficient = 0.01f;                       //How much sensitivity do you want around zero? 0.01 = 100%, 1 = 126%, 5 = 363%

//    float Input;
//    float Utilisation;
//    float Output;

//    /////////////////////////////////////////////////////////////////////////

//    void Compress()
//    {
//        Utilisation = Input / LargeInput;
//        float Atan = Mathf.Atan(Utilisation * Coefficient) / (Mathf.PI / 2);
//        float UncorrectedOutput = 2 * Atan * LargeInput;

//        Output = UncorrectedOutput * Correction();
//    }

//    float Correction()
//    {
//        //This function calculates a correction value to stretch the curve back up, so that a "LargeInput" generates an equally "LargeOutput"
//        float Atan = Mathf.Atan(Coefficient) / (Mathf.PI / 2);
//        float UncorrectedOutput = 2 * Atan * LargeInput;

//        return LargeInput / UncorrectedOutput;
//    }
//}
