using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(LoadSaveGains))]
public class LoadSaveGains_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        LoadSaveGains gains = (LoadSaveGains)target;

        if (GUILayout.Button("Load Gains"))
        {
            gains.OnClickLoad();
        }

        if (GUILayout.Button("Save Gains"))
        {
            gains.OnClickSave();
        }

        for (int i = 0; i < 10; i++)
        {
            EditorUtility.SetDirty(target);        //to trigger a redraw!
            SceneView.RepaintAll();
        }
    }
}


