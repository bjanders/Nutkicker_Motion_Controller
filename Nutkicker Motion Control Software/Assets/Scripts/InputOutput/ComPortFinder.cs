using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Management;
using TMPro;

[ExecuteInEditMode]
public class ComPortFinder : MonoBehaviour
{
    [SerializeField] private List<string>Ports;
    [SerializeField] private TMP_Dropdown dropdown;         //to FILL the dropdown menu with all avalilable COM ports.
    [SerializeField] private SerialConnector connector;     //to know when the connection is open. When it IS open, only the active COM port should be displayed in the Dropdown.

    private void Start()
    {
        UpdateComPorts();                                               //so that we can start with a populated dropdown.
    }
    void UpdateComPorts ()
    {
        Ports = new List<string>(SerialPort.GetPortNames());
        UpdateDropdown();
    }
    void UpdateDropdown()
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(Ports);
    }

    public void OnOpenDropdown()
    {
        UpdateComPorts();
    }
}
