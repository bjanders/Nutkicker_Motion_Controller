using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.Events;

public enum FilterOrder
{
    First = 1,
    Second,
    Third,
    Forth
}

[ExecuteInEditMode]
public class LowPassNthOrder : MonoBehaviour
{
    [Header("Streams")]
    [SerializeField] private Stream InStream;
    [SerializeField] private Stream OutStream;

    [Header("Filter Parameter")]
    [SerializeField] public FilterOrder Order;
    [Range(0, 1.0f)] [SerializeField] public float EMA_alpha = 0.05f;
    [SerializeField] private float Value;

    private LowPassModular[] LP = new LowPassModular[4];

    /////////////////////////////////////////////////////////////////////////
    void Start()
    {
        if (transform.parent.gameObject.GetComponent<Stream>() != null)
        {
            InStream = transform.parent.gameObject.GetComponent<Stream>();

            switch (Order)
            {
                case FilterOrder.Forth:
                    OutStream.name = InStream.name + "_LP4";
                    break;
                case FilterOrder.Third:
                    OutStream.name = InStream.name + "_LP3";
                    break;
                case FilterOrder.Second:
                    OutStream.name = InStream.name + "_LP2";
                    break;
                case FilterOrder.First:
                    OutStream.name = InStream.name + "_LP1";
                    break;
            }
        }

        for (int i = 0; i < (int)Order; i++)                //initialise all four filter instances
        {
            LP[i] = new LowPassModular();
        }
    }
    void FixedUpdate()
    {
        float temp = InStream.Youngest.Datavalue;           //Take the incoming value,

        for (int i = 0; i < (int)Order; i++)                //...and pump it through as many as four LP filters
        {
            LP[i].Update(temp, EMA_alpha);
            temp = LP[i].Output;
        }

        Value = temp;           //just to show it in the inspector

        Datapoint datapoint = new Datapoint(Time.time, Value);
        OutStream.Push(datapoint);
    }
    /////////////////////////////////////////////////////////////////////////
    
    public void On_AlphaInputChanged(string s)
    {
        EMA_alpha = Convert.ToSingle(s, GlobalVars.myNumberFormat());
    }
}

public class LowPassModular
{
    float OldValue{ get; set; }
    public float Output { get; set; }
    public void Update(float NewValue, float Alpha)
    {
        Output = NewValue * Alpha + OldValue * (1 - Alpha);
        OldValue = Output;                      //remember for next time
    }
}
