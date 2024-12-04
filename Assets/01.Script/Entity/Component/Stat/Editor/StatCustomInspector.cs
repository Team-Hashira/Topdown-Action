using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StatCompo))]
public class StatCustomInspector : Editor
{
    private SerializedProperty statProp;

    private void OnEnable()
    {
        statProp = serializedObject.FindProperty("_stat");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        if (statProp.objectReferenceValue != null)
        {
            EditorGUI.indentLevel++;
            SerializedObject so = new SerializedObject(statProp.objectReferenceValue);
            so.Update();

            SerializedProperty prop = so.GetIterator();
            prop.NextVisible(true);
            while (prop.NextVisible(false))
            {
                EditorGUILayout.PropertyField(prop, true);
            }

            so.ApplyModifiedProperties();
            EditorGUI.indentLevel--;
        }

        if (GUILayout.Button("Create new StatSO"))
        {
            ShowSaveFileDialog();
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void ShowSaveFileDialog()
    {
        // ScriptableObject ����
        StatSO newSO = ScriptableObject.CreateInstance<StatSO>();

        // ���� ���� ��� ����
        string path = EditorUtility.SaveFilePanelInProject(
            "Create StatSO",
            "NewStatSO",
            "asset",
            "Please enter a file name to create the StatSO to."
        );

        if (string.IsNullOrEmpty(path)) return;

        // �������� ����
        AssetDatabase.CreateAsset(newSO, path);
        AssetDatabase.SaveAssets();

        statProp.objectReferenceValue = newSO;
    }
}
