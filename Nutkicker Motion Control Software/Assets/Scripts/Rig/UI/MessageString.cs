using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessageString : MonoBehaviour
{
    [SerializeField] MessageGenerator_AMC1280 messagegenerator;
    [SerializeField] TMP_Text Message_String;

    private void FixedUpdate()
    {
        Message_String.text = System.Text.Encoding.Default.GetString(messagegenerator.Message);
    }
}
