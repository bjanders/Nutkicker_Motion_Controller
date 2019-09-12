using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class PanelOffsets : MonoBehaviour
{
    [Header("Hardware")]
    [SerializeField] private Transformer transformer_height;
    [SerializeField] private Transformer transformer_final;
    [Header("Input fields")]
    [SerializeField] TMP_InputField COR_Height_abv_Gnd_Input;
    [SerializeField] TMP_InputField COR_Offset_Input;
    
    private void Start()
    {
        UpdateInputs();         //..so that the user sees the numbers in the input fields that are actually in use
    }

    public void UpdateInputs()
    {
        float OffsetCoR =   transformer_final.Offset_Heave;             //usually a negative value
        float Height_CoR =  transformer_height.Offset_Heave;            //this value is the height from the ground to the CoR!

        //Update the input fields
        COR_Offset_Input.text =         OffsetCoR.ToString(GlobalVars.myNumberFormat());
        COR_Height_abv_Gnd_Input.text = Height_CoR.ToString(GlobalVars.myNumberFormat());
    }
}
