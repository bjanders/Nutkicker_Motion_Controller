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
        sb.Clear();             //Tabula rasa!!!

        byte[] message = messagegenerator.Message;

        foreach (byte b in message)
        {
            sb.Append("<" + b + ">");
        }

        Message_String.text = sb.ToString();
    }
}
