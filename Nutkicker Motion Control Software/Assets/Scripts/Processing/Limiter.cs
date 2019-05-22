using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limiter : MonoBehaviour
{
    /// <summary>
    /// This Limiter Object takes the values from the Instream, and forwards them to the Outstream.
    /// It caps the range of the values to stay beween upper and lower bound.
    /// </summary>
    [Space]
    [SerializeField] private Stream Instream;
    [SerializeField] private Stream Outstream;
    [Space]
    [SerializeField] private float UpperBound;
    [SerializeField] private float LowerBound;
    [SerializeField] private bool limiting = false;


    private void FixedUpdate()
    {
        float temp = Instream.Youngest.Datavalue;

        if (temp > UpperBound)
        {
            temp = UpperBound;
            limiting = true;
        }
        else if(temp < LowerBound)
        {
            temp = LowerBound;
            limiting = true;
        }
        else
        {
            limiting = false;
        }

        Outstream.Push(new Datapoint(Time.fixedTime, temp));
    }
}
