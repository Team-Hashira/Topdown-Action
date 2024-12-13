using System;
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
    private Dictionary<EItemName, int> _itemCountDictionary;
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

    public Action<int, Item> OnQuickSlotChangeEvent;

    private void Awake()
    {
        _itemCountDictionary = new Dictionary<EItemName, int>();
        _inventoryDictionary = new Dictionary<EInventory, Inventory>();
        FindObjectsByType<Inventory>(FindObjectsSortMode.None).ToList()
            .ForEach(inventory =>
            {
                inventory.Init();
                _inventoryDictionary.Add(inventory.inventoryEnum, inventory);
            });

        _inventoryDictionary[EInventory.Quick].OnInventoryChanged += (index, item) => OnQuickSlotChangeEvent?.Invoke(index, item);

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

    public Slot GetQuickSlot(int index)
        => _inventoryDictionary[EInventory.Quick].GetSlot(index);

    public void AddItem(EInventory inventory, Item item)
    {
        if (_itemCountDictionary.TryGetValue(item.itemSO.itemType, out int amount))
            _itemCountDictionary[item.itemSO.itemType] = amount + 1;
        else 
            _itemCountDictionary[item.itemSO.itemType] = 0;

        _inventoryDictionary[inventory].AddItem(item);
    }
    public void AddItem(EInventory inventory, ItemSO itemSO, int amount = 1)
    {
        if (_itemCountDictionary.TryGetValue(itemSO.itemType, out int dictAmount))
            _itemCountDictionary[itemSO.itemType] = dictAmount + 1;
        else
            _itemCountDictionary[itemSO.itemType] = 0;

        Item item = new Item();
        item.itemSO = itemSO;
        item.SetAmount(amount);
        _inventoryDictionary[inventory].AddItem(item);
    }
}
