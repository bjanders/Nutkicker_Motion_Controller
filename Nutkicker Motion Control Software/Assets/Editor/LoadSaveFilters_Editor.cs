using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[CustomEditor(typeof(LoadSaveFilters))]
public class LoadSaveFilters_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        LoadSaveFilters filters = (LoadSaveFilters)target;

        if (GUILayout.Button("Load Filters"))
        {
            filters.OnClickLoad();
        }

        if (GUILayout.Button("Save Filters"))
        {
            filters.OnClickSave();
        }

        for (int i = 0; i < 10; i++)
        {
            EditorUtility.SetDirty(target);        //to trigger a redraw!
            SceneView.RepaintAll();
        }
    }
}


