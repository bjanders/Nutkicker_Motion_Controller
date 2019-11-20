using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[ExecuteInEditMode]
public class Stream : MonoBehaviour
{
    [SerializeField] public string Type;
    [SerializeField] private float CurrentValue;
    [SerializeField] private string Unit;
    [SerializeField] public float EMA_alpha = 1;

    private float EMA_Signal;
    private int MemTime;

    private void FixedUpdate()
    {
        update_EMA_Signal();
    }

    private Queue<Datapoint> Buffer = new Queue<Datapoint>();
    /// <summary>
    /// Adds a new data object to the stream. Will be kept there for ms milliseconds
    /// </summary>
    /// <param name="d">Datapoint to insert into the Datastream</param>
    public void Push(Datapoint d)
    {
        Buffer.Enqueue(d);
        ResizeTo(MemTime); //cuts away the excess datapoints that are older then "Mem_time" milliseconds (if there are any)

        //Display these values in Unitys Inspector
        CurrentValue = d.Datavalue;
        Unit = d.Unit;
        Type = d.Type;
    }
    /// <summary>
    /// Resizes the buffer to cover only the last ms milliseconds
    /// </summary>
    /// <param name="ms">Number of milliseconds to keep track of the past</param>
    private void ResizeTo(int ms)
    {
        if (ms < 0) ms = 0;         //Cannot be negative! The stream can however have a Mem_Time of 0 whenever there shall only be one item

        if (Buffer.Count == 0)
        {
            MemTime = ms;          //change only the Limit.
            return;
        }
        //only if there actually ARE elements:
        while (Oldest.Timestamp < (Youngest.Timestamp - ms))        //are there any items that are too old?
        {
            Buffer.Dequeue();         //...kick them out!
        }

        MemTime = ms;              //remember how long you want to look back (for next time).
    }
    
    public Datapoint Youngest               //returns the youngest datapoint-object in the queue
    {
        get
        {
            if (Buffer.Count <= 0)
            {
                return new Datapoint(0, 0);       //if there is no item in the queue, return an empty Datapoint-object
            }
            else
            {
                return (Buffer.ElementAt(Tailindex));
            }
        }
    }
    public Datapoint Oldest                 //returns the oldest datapoint-object in the queue
    {
        get
        {
            if (Buffer.Count <= 0)
            {
                return new Datapoint(0,0);          //if there is no item in the queue, return an empty dataset
            }
            else
            {
                return (Buffer.ElementAt(0));       //The Index [0] marks the head (most senior item) of the queue!
            }
        }
    }
    public Datapoint ElementAt(int i)       //returns the datapoint-object at index i
    {
        if (i>=0)
        {
            return Buffer.ElementAt(i);             
        }
        else
        {
            return new Datapoint(0, 0);             //indexes below zero are out of range! In these cases, we consider the Stream to be "zero".
        }
        
    }
    public Datapoint EMA()
    {
        return new Datapoint(Youngest.Timestamp, EMA_Signal);
    }

    public float Timespan                   //Returns the time in seconds that the buffer covers
    {
        get
        {
            return (Youngest.Timestamp - Oldest.Timestamp);
        }
    }
    public int Tailindex
    {
        get
        {
            return Buffer.Count - 1;
        }
    }

    public void update_EMA_Signal()
    {
        EMA_Signal = (EMA_alpha * Youngest.Datavalue ) + ((1 - EMA_alpha) * EMA_Signal);
    }

    public void Clear()
    {
        EMA_Signal = Youngest.Datavalue;
        Buffer.Clear();
    }
    public void Zero()
    {
        EMA_Signal = 0;
        Buffer.Clear();
    }
}   

public class Datapoint
{
    /*This class represents a single point of a single parameter e.g. Airspeed at a given time.
    The time is noted in the member "Timestamp". The value of the parameter itself can be accessed
    through the member "Datavalue"*/

    /// <summary>
    /// Creates a datapoint from a Time/Value pair
    /// </summary>
    /// <param name="time">Timestamp in seconds</param>
    /// <param name="v">Value to be stored in the datapoint</param>
    /// <param name="type">Type ot the datapoint</param>
    /// <param name="unit">Unit of the datapoint</param>
    public Datapoint(float time = 0, float value = 0, string type = "unknown", string unit = "unitless")
    {
        Timestamp = time;
        Datavalue = value;
        Type = type;
        Unit = unit;
    }
    
    public static Datapoint operator + (Datapoint D1, Datapoint D2)
    {
        float sum = D1.Datavalue + D2.Datavalue;
        return new Datapoint(Time.fixedTime,sum);
    }
    public static Datapoint operator + (Datapoint D1, float f)
    {
        float sum = D1.Datavalue + f;
        return new Datapoint(Time.fixedTime, sum);
    }
    public static Datapoint operator - (Datapoint D1, Datapoint D2)
    {
        float difference = D1.Datavalue - D2.Datavalue;
        return new Datapoint(Time.fixedTime, difference);
    }
    public static Datapoint operator - (Datapoint D1, float f)
    {
        float difference = D1.Datavalue - f;
        return new Datapoint(Time.fixedTime, difference);
    }
    public static Datapoint operator * (Datapoint D1, Datapoint D2)
    {
        float product = D1.Datavalue * D2.Datavalue;
        return new Datapoint(Time.fixedTime, product);
    }
    public static Datapoint operator * (Datapoint D1, float f)
    {
        float product = D1.Datavalue * f;
        return new Datapoint(Time.fixedTime, product);
    }
    public static Datapoint operator / (Datapoint D1, Datapoint D2)
    {
        float quotient = D1.Datavalue / D2.Datavalue;
        return new Datapoint(Time.fixedTime, quotient);
    }
    public static Datapoint operator / (Datapoint D1, float f)
    {
        float quotient = D1.Datavalue / f;
        return new Datapoint(Time.fixedTime, quotient);
    }

    public float Timestamp { get; set; }
    public float Datavalue { get; set; }
    public string Type { get; set; }
    public string Unit { get; set; }
}