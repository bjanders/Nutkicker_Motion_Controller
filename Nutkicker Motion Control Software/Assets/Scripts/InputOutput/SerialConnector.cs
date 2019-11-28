using UnityEngine;
using System;
using System.IO.Ports;
using TMPro;
using System.Text;
using UnityEngine.Events;

[ExecuteInEditMode]
public class SerialConnector : MonoBehaviour
{
    [SerializeField] public string TestMessage;
    [SerializeField] public string COM_Port;
    [SerializeField] public int BaudRate;
    [SerializeField] public int WriteTimeout;
    [SerializeField] public bool IsOpen;
    [SerializeField] public TMP_Dropdown dropdown;
    [SerializeField] public TextMeshProUGUI label;
    [SerializeField] private MessageGenerator_AMC1280 messagegenerator;

    [SerializeField] public SerialPort serialport;

    //define events:
    [Serializable] public class ConnectionStatusChangedEvent : UnityEvent<bool> { }
    //instantiate events:
    [SerializeField] public ConnectionStatusChangedEvent ConnectionStatusChanged;

    /////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        messagegenerator = GetComponent<MessageGenerator_AMC1280>();
        serialport = new SerialPort();
        
        TestMessage = "<Test message>";
        COM_Port = "Not assigned";
        BaudRate = 250000;
        WriteTimeout = 2000;
        IsOpen = false;
    }
    private void Update()
    {
        if (IsOpen)
        {
            Write(messagegenerator.Message);
        }
    }
    /////////////////////////////////////////////////////////////////////////

    public void OnClick_Open()
    {
        Open();
        ConnectionStatusChanged.Invoke(true);
    }
    public void OnClick_Close()
    {
        Close();
        ConnectionStatusChanged.Invoke(false);
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

                serialport.PortName = COM_Port;
                serialport.BaudRate = BaudRate;
                serialport.WriteTimeout = WriteTimeout;

                serialport.Open();
                IsOpen = serialport.IsOpen;

                dropdown.gameObject.SetActive(false);
                label.enabled = true;
                label.text = COM_Port;

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
                IsOpen = serialport.IsOpen;

                COM_Port = "Not assigned";
                dropdown.gameObject.SetActive(true);
                label.enabled = false;


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
                try
                {
                    serialport.Write(msg, 0, msg.Length);
                }
                catch (Exception)
                {
                    Close();
                }
                
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
