using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ModeSelector : MonoBehaviour
{
    [SerializeField] public GameObject EulerManipulator;
    [SerializeField] public GameObject EulerModel;
    [Space]
    [SerializeField] public GameObject SiddarthaManipulator;
    [SerializeField] public GameObject SiddarthaModel;
    [Space]
    [SerializeField] public GameObject CueingManipulator;
    [SerializeField] public GameObject CueingModel;

    public void OnModeChange(int mode)
    {
        switch (mode)
        {
            case 0:
                Debug.Log("Motion Cueing mode selected");
                SelectMode("Cueing");
                break;
                
            case 1:
                Debug.Log("Euler mode selected");
                SelectMode("Euler");
                break;
                
            case 2:
                Debug.Log("Siddartha mode selected");
                SelectMode("Siddartha");
                break;

            default:
                Debug.Log("No mode found");
                break;
        }
    }

    public void SelectMode(string mode)
    {
        switch (mode)
        {
            case "Cueing":
                EulerManipulator.SetActive(false);
                SiddarthaManipulator.SetActive(false);
                CueingManipulator.SetActive(true);

                EulerModel.SetActive(false);
                SiddarthaModel.SetActive(false);
                CueingModel.SetActive(true);
                break;

            case "Euler":
                CueingManipulator.SetActive(false);
                SiddarthaManipulator.SetActive(false);
                EulerManipulator.SetActive(true);

                CueingModel.SetActive(false);
                SiddarthaModel.SetActive(false);
                EulerModel.SetActive(true);
                break;

            case "Siddartha":
                CueingManipulator.SetActive(false);
                EulerManipulator.SetActive(false);
                SiddarthaManipulator.SetActive(true);

                CueingModel.SetActive(false);
                EulerModel.SetActive(false);
                SiddarthaModel.SetActive(true);
                break;
            default:
                Debug.Log(mode + " mode nicht gefunden.");
                break;
        }
    }
}
