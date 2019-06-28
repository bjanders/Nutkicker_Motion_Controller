using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageString : MonoBehaviour
{
    [SerializeField] MessageGenerator messagegenerator;
    [SerializeField] TMP_Text Message_String;

    private void FixedUpdate()
    {
        Message_String.text = messagegenerator.Message;
    }
}
