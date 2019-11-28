using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreventInputWhileMotionActive : MonoBehaviour
{
    [SerializeField] private PanelRigConfig panelRigConfig;

    private void Start()
    {
        panelRigConfig = GetComponent<PanelRigConfig>();
    }

    public void OnConnectionStatusChanged(bool b)
    {
        bool interactable = !b;
        //Platform Upper:
        panelRigConfig.RadiusFinal.interactable = interactable;
        panelRigConfig.AlphaFinal.interactable = interactable;
        //Radius Lower:
        panelRigConfig.RadiusBase.interactable = interactable;
        panelRigConfig.AlphaBase.interactable = interactable;
        //cranks:
        panelRigConfig.UsingCrankArmSystem.interactable = interactable;
        panelRigConfig.Crank_length.interactable = interactable;
        panelRigConfig.Azimuth.interactable = interactable;
        panelRigConfig.Rod_length.interactable = interactable;
        panelRigConfig.FlipCranks.interactable = interactable;
        //Actuators:
        panelRigConfig.ActuatorMin.interactable = interactable;
        panelRigConfig.ActuatorMax.interactable = interactable;
        //Positions:
        panelRigConfig.Input_Park_X.interactable = interactable;
        panelRigConfig.Input_Park_Y.interactable = interactable;
        panelRigConfig.Input_Park_Z.interactable = interactable;
        panelRigConfig.Input_Neutral_X.interactable = interactable;
        panelRigConfig.Input_Neutral_Y.interactable = interactable;
        panelRigConfig.Input_Neutral_Z.interactable = interactable;
    }
}
