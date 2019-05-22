using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

[ExecuteInEditMode]
public class HighPass : MonoBehaviour
{
    //Inspector:
    [Header("Streams")]
    [SerializeField] private Stream InStream;
    [SerializeField] private Stream OutStream_HFC;
    
    [Header("Filter Parameter")]
    [Range(0, 1.0f)] [SerializeField] private float EMA_alpha = 0.02f;
    [SerializeField] private float HFC_Value = 0;

    private Stopwatch stopwatch = new Stopwatch();
    private int WatchdogTimer = 100;      //Time interval in milliseconds after which the watchdog resets the filter if no new data has arrived.

    /////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (InStream == null)
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();
        }

        OutStream_HFC.name = InStream.name + "_HP";
        UpdateEMA_alpha();
    }
    void FixedUpdate()
    {
        float Value = InStream.Youngest.Datavalue;
        this.HFC_Value = (EMA_alpha * Value) + ((1 - EMA_alpha) * this.HFC_Value);      //Calculate the EMA_Value...
        float HFC_Value = Value - this.HFC_Value;

        OutStream_HFC.Push(new Datapoint(Time.fixedTime, HFC_Value, InStream.Type));
        
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
        OutStream_HFC.Clear();
    }
    private void UpdateEMA_alpha()
    {
        OutStream_HFC.EMA_alpha = EMA_alpha;
    }
}
