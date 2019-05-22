using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Management;
using TMPro;

[ExecuteInEditMode]
public class ComPortFinder : MonoBehaviour
{
    [SerializeField] public string[] PortNames;

    [SerializeField] private List<string>PortOptions;
    [SerializeField] private TMP_Dropdown dropdown;     //to FILL the dropdown menu with all avalilable COM ports.
    
    void Start()
    {
        InvokeRepeating("UpdateComPorts", 0.0f, 4.0f);
    }

    void UpdateComPorts ()
    {
        PortNames = SerialPort.GetPortNames();
        PortOptions = new List<string>(PortNames);

        dropdown.ClearOptions();
        dropdown.AddOptions(PortOptions);
    }
}
