using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SerialIndicator : MonoBehaviour
{
    [SerializeField] private Color ColorOpen;
    [SerializeField] private Color ColorClosed;

    [SerializeField] private SerialConnector serialconnector;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI TMP_Text;
    

    void FixedUpdate()
    {
        if (serialconnector.serialport.IsOpen)
        {
            TMP_Text.text = "COM open";
            image.color = ColorOpen;
        }
        else
        {
            TMP_Text.text = "COM closed";
            image.color = ColorClosed;
        }
        
    }
}
