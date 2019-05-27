using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[ExecuteInEditMode]
public class LowPass : MonoBehaviour
{
    //Inspector:
    [Header("Streams")]
    [SerializeField] private Stream InStream;
    [SerializeField] private Stream OutStream_LFC;
    
    [Header("Filter Parameter")]
    [Range(0, 1.0f)] [SerializeField] private float EMA_alpha = 0.05f;
    [SerializeField] private float LFC_Value = 0;

    private Stopwatch stopwatch = new Stopwatch();
    private int WatchdogTimer = 100;      //Time interval in milliseconds after which the watchdog resets the filter if no new data has arrived.

    /////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (InStream == null)
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();
        }

        OutStream_LFC.name = InStream.name + "_LP";
        UpdateEMA_alpha();
    }
    void FixedUpdate()
    {
        float Value = InStream.Youngest.Datavalue;
        LFC_Value = (EMA_alpha * Value) + ((1 - EMA_alpha) * LFC_Value);                    //Calculate the EMA...


        OutStream_LFC.Push(new Datapoint(Time.fixedTime, LFC_Value, InStream.Type));        //...and push it into the Stream.
        
        UpdateEMA_alpha();                                        //Did someone change the settings of the EMA?
        Watchdog();
    }
    /////////////////////////////////////////////////////////////////////////

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
    }
    private void UpdateEMA_alpha()
    {
        OutStream_LFC.EMA_alpha = EMA_alpha;
    }
}
