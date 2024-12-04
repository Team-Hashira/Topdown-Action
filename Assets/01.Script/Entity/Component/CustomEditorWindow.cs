using UnityEditor;
using UnityEngine;
using System.IO;

public class CustomEditorWindow : EditorWindow
{
    [MenuItem("Tools/Custom File Save")]
    public static void ShowWindow()
    {
        GetWindow<CustomEditorWindow>("File Save Example");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Save File"))
        {
            ShowSaveFileDialog();
        }
    }

    private void ShowSaveFileDialog()
    {
        // ScriptableObject ����
        StatSO newSO = ScriptableObject.CreateInstance<StatSO>();

        // ���� ���� ��� ����
        string path = EditorUtility.SaveFilePanelInProject(
            "Save ScriptableObject",
            "NewDataObject",
            "asset",
            "Please enter a file name to save the ScriptableObject to."
        );

        if (string.IsNullOrEmpty(path)) return;

        // �������� ����
        AssetDatabase.CreateAsset(newSO, path);
        AssetDatabase.SaveAssets();

        Debug.Log($"ScriptableObject created at: {path}");
    }
}