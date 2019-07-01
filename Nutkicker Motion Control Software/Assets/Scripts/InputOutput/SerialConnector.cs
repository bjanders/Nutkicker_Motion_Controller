using UnityEngine;
using System;
using System.IO.Ports;
using TMPro;
using System.Text;

[ExecuteInEditMode]
public class SerialConnector : MonoBehaviour
{
    [SerializeField] public string TestMessage;
    [SerializeField] public string COM_Port;
    [SerializeField] public int BaudRate;
    [SerializeField] public int WriteTimeout;
    [SerializeField] public bool IsOpen;
    [SerializeField] public TMP_Dropdown dropdown;
    [SerializeField] private MessageGenerator_AMC1280 messagegenerator;

    public SerialPort serialport = new SerialPort();

    /////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        messagegenerator = GetComponent<MessageGenerator_AMC1280>();

        TestMessage = "<Test message>";
        COM_Port = "Not assigned";
        BaudRate = 115200;
        WriteTimeout = 2000;
        IsOpen = false;
    }
    private void FixedUpdate()
    {
        IsOpen = serialport.IsOpen;
        if (IsOpen)
        {
            Write(messagegenerator.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////

    public void OnClick_Open()
    {
        Open();
    }
    public void OnClick_Close()
    {
        Close();
    }
    public void OnClick_Write()
    {
        Write(Encoding.ASCII.GetBytes(TestMessage));
    }
    public void OnClick_Reset()
    {
        Reset();
    }

    public void Open()
    {
        if (Application.isPlaying)
        {
            if (serialport.IsOpen)
            {
                Debug.LogError("Serialport already open.");
            }
            else
            {
                COM_Port = dropdown.options[dropdown.value].text;

                serialport = new SerialPort(COM_Port, BaudRate);
                serialport.WriteTimeout = WriteTimeout;

                serialport.Open();

                Debug.Log("Serialport opened: " + COM_Port);
            }
        }
        else
        {
            Debug.LogError("Opening only possible in play mode");
        }
        
    }
    public void Close()
    {
        if (Application.isPlaying)
        {
            if (serialport.IsOpen)
            {
                serialport.Close();
                COM_Port = "Not assigned";

                Debug.Log("Serialport closed.");
            }
            else
            {
                Debug.LogError("Serialport alredy closed.");
            }
        }
        else
        {
            Debug.LogError("Closing only possible in play mode.");
        }
            
    }
    public void Write(byte[] msg)
    {
        if (Application.isPlaying)
        {
            if (serialport.IsOpen)
            {
                serialport.Write(msg, 0, msg.Length);
            }
            else
            {
                Debug.LogError("Cannot write. Serialport not open.");
            }
        }
        else
        {
            Debug.LogError("Writing only possible in play mode.");
        }
        
    }
    public void Reset()
    {
        if (Application.isPlaying)
        {
            if (serialport.IsOpen)
            {
                serialport.Close();
                Debug.Log("Serialport reset.");
            }
        }
        else
        {
            Debug.LogError("Resetting only possible in play mode.");
        }
        
    }
}
