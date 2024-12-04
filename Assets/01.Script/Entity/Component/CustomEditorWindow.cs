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
        // ScriptableObject 생성
        StatSO newSO = ScriptableObject.CreateInstance<StatSO>();

        // 파일 저장 경로 지정
        string path = EditorUtility.SaveFilePanelInProject(
            "Save ScriptableObject",
            "NewDataObject",
            "asset",
            "Please enter a file name to save the ScriptableObject to."
        );

        if (string.IsNullOrEmpty(path)) return;

        // 에셋으로 저장
        AssetDatabase.CreateAsset(newSO, path);
        AssetDatabase.SaveAssets();

        Debug.Log($"ScriptableObject created at: {path}");
    }
}