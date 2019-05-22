using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RawDataValues : MonoBehaviour
{
    [SerializeField] private Stream Stream_IAS;
    [SerializeField] private Stream Stream_MACH;
    [SerializeField] private Stream Stream_TAS;
    [SerializeField] private Stream Stream_GS;
    [SerializeField] private Stream Stream_AOA;
    [SerializeField] private Stream Stream_VS;
    [SerializeField] private Stream Stream_HGT;
    [Space]
    [SerializeField] private Stream Stream_BANK;
    [SerializeField] private Stream Stream_YAW;
    [SerializeField] private Stream Stream_PITCH;
    [Space]
    [SerializeField] private Stream Stream_Wx;
    [SerializeField] private Stream Stream_Wy;
    [SerializeField] private Stream Stream_Wz;
    [Space]
    [SerializeField] private Stream Stream_Ax;
    [SerializeField] private Stream Stream_Ay;
    [SerializeField] private Stream Stream_Az;
    //[SerializeField] private Stream Stream_INDEX;

    [SerializeField] private TextMeshProUGUI content;

    public void FixedUpdate()
    {
        if (Server.Status == ServerStatus.reading)
        {
            content.text =  "\n" +
                            Stream_IAS.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_MACH.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_TAS.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_GS.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_AOA.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_VS.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_HGT.Youngest.Datavalue.ToString("0.00") + "\n" +
                            "\n" +
                            "\n" +
                            Stream_BANK.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_YAW.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_PITCH.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_Wx.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_Wy.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_Wz.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_Ax.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_Ay.Youngest.Datavalue.ToString("0.00") + "\n" +
                            Stream_Az.Youngest.Datavalue.ToString("0.00") + "\n";
        }
        else
        {
            content.text = "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "\n" +
                            "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n" +
                            "--" + "\n";
        } 
    }
}
