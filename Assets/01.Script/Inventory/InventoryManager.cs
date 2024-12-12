using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EInventory
{
    Main,
    Quick
}

public class InventoryManager : MonoSingleton<InventoryManager>
{
    [SerializeField] private ItemSO _itemSO;
    private Dictionary<EInventory, Inventory> _inventoryDictionary;
    private Slot _dragSlot;
    public Slot DragSlot
    {
        get => _dragSlot;
        set => _dragSlot = value;
    }
    private Slot _pointerSlot;
    public Slot PointerSlot
    {
        get => _pointerSlot;
        set => _pointerSlot = value;
    }

    private void Awake()
    {
        _inventoryDictionary = new Dictionary<EInventory, Inventory>();
        FindObjectsByType<Inventory>(FindObjectsSortMode.None).ToList()
            .ForEach(inventory =>
            {
                inventory.Init();
                _inventoryDictionary.Add(inventory.inventoryEnum, inventory);
            });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Item item = new Item();
            item.itemSO = _itemSO;
            item.SetAmount(1);
            AddItem(EInventory.Main, item);
        }
    }

    public void AddItem(EInventory inventory, Item item)
    {
        _inventoryDictionary[inventory].AddItem(item);
    }
}
