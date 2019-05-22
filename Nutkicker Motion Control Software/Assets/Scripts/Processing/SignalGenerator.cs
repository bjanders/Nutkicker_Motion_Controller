using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SignalType
{
    Sine,
    PerlinNoise
}


public class SignalGenerator : MonoBehaviour
{
    [SerializeField] SignalType mode;
    [Range(0.2f,5)]
    [SerializeField] double SineFrequency;
    [SerializeField] double SineAmplitude;
    [Space]
    [SerializeField] Stream Outstream;




    

}
