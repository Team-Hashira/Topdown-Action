using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EItemName
{
    Stone = 0,
    Wood,
    Copper,
    Iron,
    Ruby,
    Branch,
    Leaf,
    Soil,




    WoodSword = 1000,

}

[CreateAssetMenu(fileName = "ItemSO", menuName = "SO/Item/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite itemSprite;
    public EItemName itemEnum;
    public int maxOverlapAmount = 1;
    public Vector2 itemPivot;

    public SerializedDictionary<ItemSO, int> itemRecipe;
}