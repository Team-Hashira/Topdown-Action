using System.Collections.Generic;
using UnityEngine;

public enum EItemType
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

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/Item/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite itemSprite;
    public EItemType itemType;
    public int maxOverlapAmount = 1;

    public Dictionary<ItemSO, int> itemRecipe;
}