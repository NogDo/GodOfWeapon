using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CJsonSave))]
public class CJsonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CJsonSave save = (CJsonSave)target;

        if (GUILayout.Button("Save"))
        {
            save.SaveEnemyStatsList();
        }
    }
}
