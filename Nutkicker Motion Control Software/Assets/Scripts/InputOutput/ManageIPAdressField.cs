using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageIPAdressField : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputfield;

    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.isOn = true;
        inputfield.text = "127.0.0.1";
    }
    
    public void OnCheckedChanged(bool checkbox)
    {
        if (checkbox)
        {
            inputfield.text = "127.0.0.1";
        }
        else
        {
            inputfield.text = Utility.GetLocalIPAddress();
        }
    }
}
