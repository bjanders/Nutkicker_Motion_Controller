using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.Events;
using UnityEngine;

public class Processor : MonoBehaviour
{
    [SerializeField] private AirlockReader airlockreader;
    [Space]
    [SerializeField] private Stream Stream_IAS;
    [SerializeField] private Stream Stream_MACH;
    [SerializeField] private Stream Stream_TAS;
    [SerializeField] private Stream Stream_GS;
    [SerializeField] private Stream Stream_AOA;
    [SerializeField] private Stream Stream_VS;
    [SerializeField] private Stream Stream_HGT;

    [SerializeField] private Stream Stream_BANK;
    [SerializeField] private Stream Stream_YAW;
    [SerializeField] private Stream Stream_PITCH;

    [SerializeField] private Stream Stream_Wx;
    [SerializeField] private Stream Stream_Wy;
    [SerializeField] private Stream Stream_Wz;

    [SerializeField] private Stream Stream_Ax;
    [SerializeField] private Stream Stream_Ay;
    [SerializeField] private Stream Stream_Az;

    [SerializeField] private Stream Stream_INDEX;
    
    private void Start()
    {
        ResetStreams();
    }
    private void FixedUpdate()
    {
        ChopParseAndPackage(airlockreader.RawDataString);
    }
   
    void ChopParseAndPackage(string s)
    {
        //chop up the datastring by the commas,
        string[] temp = s.Split(',');

        //...parse every piece into a float...
        float[] data = new float[temp.Length];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Convert.ToSingle(temp[i],GlobalVars.myNumberFormat());
        }

        //then pack those into Datapoint-objects and push them into their Datastreams.
        float current_time = Time.fixedTime;                              //Time is needed for every Datapoint object as a timestamp
        
        //-----------AIRDATA----------------------------------      //Index[0-6] are airdata
        Stream_IAS.Push(new Datapoint(current_time, data[0],    "IAS","m/s"));
        Stream_MACH.Push(new Datapoint(current_time, data[1],   "Machnumber","MACH"));
        Stream_TAS.Push(new Datapoint(current_time, data[2],    "TAS", "m/s"));
        Stream_GS.Push(new Datapoint(current_time, data[3],     "GS", "m/s"));
        Stream_AOA.Push(new Datapoint(current_time, data[4],    "AOA", "Degree"));
        Stream_VS.Push(new Datapoint(current_time, data[5],     "VS", "m/s"));
        Stream_HGT.Push(new Datapoint(current_time, data[6],    "HGT", "m"));

        //------------EULER-----------------------------------      //Index[7-9] are Euler-angles
        Stream_BANK.Push(new Datapoint(current_time, data[7],  "Bank", "RAD"));
        Stream_YAW.Push(new Datapoint(current_time, data[8],   "Yaw", "RAD"));
        Stream_PITCH.Push(new Datapoint(current_time, data[9], "Pitch", "RAD"));

        //------------RATES----------------------------------        //Index[10-12] are x/y/z angular rates
        Stream_Wx.Push(new Datapoint(current_time, data[10], "Wx", "RAD/s"));
        Stream_Wy.Push(new Datapoint(current_time, data[11], "Wy", "RAD/s"));
        Stream_Wz.Push(new Datapoint(current_time, data[12], "Wz", "RAD/s"));

        //------------ACCEL----------------------------------       //Index[13-15] are x/y/z accelerations
        Stream_Ax.Push(new Datapoint(current_time, data[13], "Ax", "G"));
        Stream_Ay.Push(new Datapoint(current_time, data[14], "Ay", "G"));
        Stream_Az.Push(new Datapoint(current_time, data[15], "Az", "G"));

        //-------------META----------------------------------       //Index[16-17] are metadata
        Stream_INDEX.Push(new Datapoint(current_time, data[17]));
    }

    private void ResetStreams()
    {
        Stream_IAS.Clear();
        Stream_MACH.Clear();
        Stream_TAS.Clear();
        Stream_GS.Clear();
        Stream_AOA.Clear();
        Stream_VS.Clear();
        Stream_HGT.Clear();

        Stream_BANK.Clear();
        Stream_YAW.Clear();
        Stream_PITCH.Clear();

        Stream_Wx.Clear();
        Stream_Wy.Clear();
        Stream_Wz.Clear();

        Stream_Ax.Clear();
        Stream_Ay.Clear();
        Stream_Az.Clear();

        Stream_INDEX.Clear();
    }

    
}
