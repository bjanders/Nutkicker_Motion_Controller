using System;
using UnityEngine;

[ExecuteInEditMode]
public class MessageGenerator_AMC1280 : MonoBehaviour
{
    [Header("Actuators")]
    [SerializeField] private Actuator Act1;
    [SerializeField] private Actuator Act2;
    [SerializeField] private Actuator Act3;
    [SerializeField] private Actuator Act4;
    [SerializeField] private Actuator Act5;
    [SerializeField] private Actuator Act6;
    [Header("Message")]
    [ShowOnly] [SerializeField] public byte[] Message;
    [ShowOnly][SerializeField] private byte[] StartBlock;
    [SerializeField] private byte[] A1;
    [SerializeField] private byte[] A2;
    [SerializeField] private byte[] A3;
    [SerializeField] private byte[] A4;
    [SerializeField] private byte[] A5;
    [SerializeField] private byte[] A6;
    [SerializeField] private byte[] Additionals;
    [ShowOnly][SerializeField] private byte[] EndBlock;

    private void Start()
    {
        Message = new byte[20];
        StartBlock = new byte[] {255, 255 };
        Additionals = new byte[] { 0, 0, 0, 0 };
        EndBlock = new byte[]   {10, 13 };
    }
    void FixedUpdate()
    {
        ComposeMessage_AMC1280();
    }
    private void ComposeMessage_AMC1280()
    {
        A1 = GenerateBytes(Act1);
        A2 = GenerateBytes(Act2);
        A3 = GenerateBytes(Act3);
        A4 = GenerateBytes(Act4);
        A5 = GenerateBytes(Act5);
        A6 = GenerateBytes(Act6);

        Message[0] = StartBlock[0];
        Message[1] = StartBlock[1];
        Message[2] = A1[0];
        Message[3] = A1[1];
        Message[4] = A2[0];
        Message[5] = A2[1];
        Message[6] = A3[0];
        Message[7] = A3[1];
        Message[8] = A4[0];
        Message[9] = A4[1];
        Message[10] = A5[0];
        Message[11] = A5[1];
        Message[12] = A6[0];
        Message[13] = A6[1];
        Message[14] = Additionals[0];
        Message[15] = Additionals[1];
        Message[16] = Additionals[2];
        Message[17] = Additionals[3];
        Message[18] = EndBlock[0];
        Message[19] = EndBlock[1];
    }
    private byte[] GenerateBytes(Actuator actuator)
    {
        UInt16 value;

        if (actuator.Utilisation <= 0.0f)
        {
            return new byte[2] { 0, 0 };        //return a min value
        }
        else if (actuator.Utilisation >= 1.0f)
        {
            return new byte[2] { 255, 255 };        //return a max value
        }
        else
        {
            value = (UInt16)(UInt16.MaxValue * actuator.Utilisation);
        }

        Byte[] Bytes = BitConverter.GetBytes(value);
        Array.Reverse(Bytes);

        return Bytes;
    }
}
