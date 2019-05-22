using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteInEditMode]
public class PanelOffsets : MonoBehaviour
{
    [Header("Hardware")]
    [SerializeField] private Platform Platform_height;
    [SerializeField] private Platform Platform_final;
    [Header("Input fields")]
    [SerializeField] TMP_InputField COR_Height_abv_Gnd_Input;
    [SerializeField] TMP_InputField COR_Offset_Input;

    private Transformer transformer_height;
    private Transformer transformer_final;

    void Awake()
    {
        transformer_height = Platform_height.GetComponent<Transformer>();
        transformer_final = Platform_final.GetComponent<Transformer>();
    }
    private void Start()
    {
        UpdateInputs();
    }

    public void UpdateInputs()
    {
        float OffsetCoR =   transformer_final.Offset_Heave;                                   //usually a negative value
        float Height_CoR =  transformer_height.Offset_Heave;                                 //this value is the height from the ground to the CoR!

        //Update the input fields
        COR_Offset_Input.text =         OffsetCoR.ToString(GlobalVars.myNumberFormat());
        COR_Height_abv_Gnd_Input.text = Height_CoR.ToString(GlobalVars.myNumberFormat());
    }
}
