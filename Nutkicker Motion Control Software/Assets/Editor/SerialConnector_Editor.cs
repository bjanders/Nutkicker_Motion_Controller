using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(SerialConnector))]
public class SerialConnector_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SerialConnector serialconnector = (SerialConnector)target;

        if (GUILayout.Button("Open"))
        {
            serialconnector.OnClick_Open();
        }
        if (GUILayout.Button("Close"))
        {
            serialconnector.OnClick_Close();
        }
        if (GUILayout.Button("Write test"))
        {
            serialconnector.OnClick_Write();
        }
        if (GUILayout.Button("Reset"))
        {
            serialconnector.OnClick_Reset();
        }
    }
}


