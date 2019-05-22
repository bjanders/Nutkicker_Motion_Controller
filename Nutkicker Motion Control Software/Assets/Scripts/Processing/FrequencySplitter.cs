using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[ExecuteInEditMode]
public class FrequencySplitter : MonoBehaviour
{
    [Header("Streams")]
    [Tooltip("This is the stream where the data is coming from")]
    [SerializeField] private Stream InStream;

    [Tooltip("This is the stream where the low frequency content (LFC) of the signal is being sent to")]
    [SerializeField] private Stream OutStream_LFC;

    [Tooltip("This is the stream where the high frequency content (HFC) of the signal is being sent to")]
    [SerializeField] private Stream OutStream_HFC;
    

    [Header ("Filter Parameter")]
    [Tooltip("The factor to deprecate all previous datapoints with when a new datapoint arrives. Quasi cutoff frequency.")]
    [Range(0,1.0f)][SerializeField] private float EMA_alpha = 0.02f;
    [SerializeField] public float Value;
    [SerializeField] public float LFC_Value = 0;
    [SerializeField] public float HFC_Value = 0;
    
    private Stopwatch stopwatch = new Stopwatch();
    private int WatchdogTimer = 100;      //Time interval in milliseconds after which the watchdog resets the filter if no new data has arrived.

    void Start()
    {
        if (InStream == null)
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();
        }
        OutStream_LFC.name = InStream.name + "_LFC";
        OutStream_HFC.name = InStream.name + "_HFC";
    }
    void FixedUpdate()
    {
        Value = InStream.Youngest.Datavalue;
        
        if (OutStream_LFC != null)                                      //if there is a LFC_outstream...
        {
            LFC_Value = (EMA_alpha * Value) + ((1 - EMA_alpha) * LFC_Value);                    //Calculate the EMA...
            OutStream_LFC.Push(new Datapoint(Time.fixedTime, LFC_Value, InStream.Type));        //...and push it into the Stream.
        }

        if (OutStream_HFC != null)                                      //if there is a HFC_outstream...
        {
            HFC_Value = Value - LFC_Value;                              //Calculate the HFC_Value...
            OutStream_HFC.Push(new Datapoint(Time.fixedTime, HFC_Value, InStream.Type));
        }
        
        Watchdog();
    }
    
    private void Watchdog()
    {
        if (stopwatch.ElapsedMilliseconds > WatchdogTimer)
        {
            ResetFilter();
        }
        stopwatch.Restart();
    }
    private void ResetFilter()
    {
        OutStream_LFC.Clear();
        OutStream_HFC.Clear();
    } 
}
 