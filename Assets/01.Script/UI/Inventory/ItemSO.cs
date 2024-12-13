using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EItemName
{
    Stone,
    Wood,
    Copper,
    Iron,
    Ruby,
    Branch,
    Leaf,
    Soil
}

public enum EItemUseType
{
    None,
    Default,
    Custom
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/Item/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite itemSprite;
    public EItemName itemType;
    public int maxOverlapAmount = 1;

    public SerializedDictionary<ItemSO, int> itemRecipe;

    public EItemUseType itemUseType;
    [HideInInspector]
    public string useTypeClassName;

}