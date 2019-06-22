using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(StartStopLogic))]
public class StartStopSwitch_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StartStopLogic startstopswitch = (StartStopLogic)target;

        if (GUILayout.Button("Motion-->Pause"))
        {
            startstopswitch.OnClick_Motion2Pause();
        }

        if (GUILayout.Button("Pause-->Park"))
        {
            startstopswitch.OnClick_Pause2Park();
        }

        if (GUILayout.Button("Park-->Pause"))
        {
            startstopswitch.OnClick_Park2Pause();
        }

        if (GUILayout.Button("Pause-->Motion"))
        {
            startstopswitch.OnClick_Pause2Motion();
        }
    }
}


