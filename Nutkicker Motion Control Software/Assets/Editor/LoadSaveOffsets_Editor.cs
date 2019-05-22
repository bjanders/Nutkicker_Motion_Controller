using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(LoadSaveOffsets))]
public class LoadSaveOffsets_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        LoadSaveOffsets Offsets = (LoadSaveOffsets)target;

        if (GUILayout.Button("Load Offsets"))
        {
            Offsets.OnClickLoad();
        }

        if (GUILayout.Button("Save Offsets"))
        {
            Offsets.OnClickSave();
        }

        for (int i = 0; i < 10; i++)
        {
            EditorUtility.SetDirty(target);        //to trigger a redraw!
            SceneView.RepaintAll();
        }
    }
}


