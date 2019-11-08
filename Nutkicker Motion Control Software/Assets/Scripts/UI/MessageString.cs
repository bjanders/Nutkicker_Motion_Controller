using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;

public class MessageString : MonoBehaviour
{
    [SerializeField] MessageGenerator_AMC1280 messagegenerator;
    [SerializeField] TMP_Text Message_String;
    [SerializeField] StringBuilder sb;

    private void Start()
    {
        sb = new StringBuilder();
    }
    private void FixedUpdate()
    {
        //Message_String.text = System.Text.Encoding.Default.GetString(messagegenerator.Message);
        //Debug.Log(System.Text.Encoding.Default.GetString(messagegenerator.Message));
        
        sb.Clear();             //Tabula rasa!!!

        for (int i = 0; i < messagegenerator.Message.Length; i++)
        {
            sb.Append("<" + messagegenerator.Message[i] + ">");
            if (i >= messagegenerator.Message.Length -1)
            {
                break;
            }
            //sb.Append("-");


        }

        Message_String.text = sb.ToString();
    }
}
