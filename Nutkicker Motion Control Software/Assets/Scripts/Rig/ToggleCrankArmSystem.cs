using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ToggleCrankArmSystem : MonoBehaviour
{
    [SerializeField] private Actuators actuators;
    [SerializeField] private GameObject CrankArmSystem;
    [Space]
    [SerializeField] private GameObject pnlActuators;
    [SerializeField] private GameObject pnlCrankArmSystem;

    [SerializeField] private Toggle toggle;
    

    //--------------------------------------------------
    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    void Update()
    {
        
    }
    //--------------------------------------------------

    public void OnCheckedChanged()
    {
        if (toggle.isOn == true)                            //Does the user have a crank arm system?
        {
            //Hide the actuators
            actuators.gameObject.SetActive(false);
            pnlActuators.SetActive(false);
            //Show the cranks
            CrankArmSystem.SetActive(true);
            pnlCrankArmSystem.SetActive(true);

        }
        else
        {
            //Show the actuators
            actuators.gameObject.SetActive(true);
            pnlActuators.SetActive(true);
            //Hide the cranks
            CrankArmSystem.SetActive(false);
            pnlCrankArmSystem.SetActive(false);
        }
    }

}
