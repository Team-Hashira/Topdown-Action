using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemSO))]
public class CustomItemSOEditor : Editor
{
    private ItemSO _itemSO;
    private void OnEnable()
    {
        _itemSO = (ItemSO)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(_itemSO.itemUseType == EItemUseType.Custom)
        {
            _itemSO.useTypeClassName = EditorGUILayout.TextField("Use Type Class Name", _itemSO.useTypeClassName);
        }
    }
}
