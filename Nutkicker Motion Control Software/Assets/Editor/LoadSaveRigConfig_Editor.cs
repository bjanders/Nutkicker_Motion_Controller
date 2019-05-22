using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(LoadSaveRigConfig))]
public class LoadSaveRigConfig_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LoadSaveRigConfig platformconfig = (LoadSaveRigConfig)target;

        if (GUILayout.Button("Load Rig"))
        {
            platformconfig.OnClickLoad();
        }

        if (GUILayout.Button("Save Rig"))
        {
            platformconfig.OnClickSave();
        }

        for (int i = 0; i < 10; i++)
        {
            EditorUtility.SetDirty(target);        //to trigger a redraw!
            SceneView.RepaintAll();
        }
    }
}


