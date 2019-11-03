using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

[ExecuteInEditMode]
public class ServerIndicator : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public TextMeshProUGUI TMP_Text;

    [SerializeField] private Color Color_Offline;
    [SerializeField] private Color Color_Starting;
    [SerializeField] private Color Color_Listening;
    [SerializeField] private Color Color_Connected;
    [SerializeField] private Color Color_Reading;
    [SerializeField] private Color Color_Shutting;


    void FixedUpdate()
    {
        switch (Server.Status)
        {
            case ServerStatus.offline:
                image.color = Color_Offline;
                TMP_Text.text = "Server offline";
                break;
            case ServerStatus.starting:
                image.color = Color_Starting;
                TMP_Text.text = "Server starting";
                break;
            case ServerStatus.listening:
                image.color = Color_Listening;
                TMP_Text.text = "Server listening";
                break;
            case ServerStatus.connected:
                image.color = Color_Connected;
                TMP_Text.text = "Client connected";
                break;
            case ServerStatus.reading:
                image.color = Color_Reading;
                TMP_Text.text = "Server reading data";
                break;
            case ServerStatus.shutting_down:
                image.color = Color_Shutting;
                TMP_Text.text = "Server shutting down";
                break;
            default:
                break;
        }
    }
}
