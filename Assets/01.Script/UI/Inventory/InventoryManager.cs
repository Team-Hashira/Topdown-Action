using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum EInventory
{
    Main,
    Quick
}

public class InventoryManager : MonoSingleton<InventoryManager>
{
    [SerializeField] private ItemSO _itemSO1;
    [SerializeField] private ItemSO _itemSO2;
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

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Item item = new Item();
            item.itemSO = _itemSO1;
            item.SetAmount(1);
            AddItem(EInventory.Main, item);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Item item = new Item();
            item.itemSO = _itemSO2;
            item.SetAmount(1);
            AddItem(EInventory.Main, item);
        }

        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                OnQuickSlotChangeEvent?.Invoke(i, GetSlot(EInventory.Quick, i).GetAssignedItem());
            }
        }
    }

    public Slot GetSlot(EInventory inventory, int index)
        => _inventoryDictionary[inventory].GetSlot(index);
    public Slot GetSlot(EInventory inventory, Vector2Int pos)
        => _inventoryDictionary[inventory].GetSlot(pos);

    public void AddItem(EInventory inventory, Item item)
    {
        if (_itemCountDictionary.ContainsKey(item.itemSO.itemEnum))
            _itemCountDictionary[item.itemSO.itemEnum]++;
        else 
            _itemCountDictionary[item.itemSO.itemEnum] = 1;

        _inventoryDictionary[inventory].AddItem(item);
    }
    public void AddItem(EInventory inventory, ItemSO itemSO, int amount = 1)
    {
        if (_itemCountDictionary.ContainsKey(itemSO.itemEnum))
            _itemCountDictionary[itemSO.itemEnum]++;
        else
            _itemCountDictionary[itemSO.itemEnum] = 1;

        Item item = new Item();
        item.itemSO = itemSO;
        item.SetAmount(amount);
        _inventoryDictionary[inventory].AddItem(item);
    }
    public void RemoveItem(EInventory inventory, Item item)
    {
        if (_itemCountDictionary.ContainsKey(item.itemSO.itemEnum))
            _itemCountDictionary[item.itemSO.itemEnum]--;
        else
            return;

        _inventoryDictionary[inventory].AddItem(item);
    }
    public void RemoveItem(EInventory inventory, ItemSO itemSO, int amount = 1)
    {
        if (_itemCountDictionary.ContainsKey(itemSO.itemEnum))
            _itemCountDictionary[itemSO.itemEnum]--;
        else
            return;

        Item item = new Item();
        item.itemSO = itemSO;
        item.SetAmount(amount);
        _inventoryDictionary[inventory].RemoveItem(item);
    }
    public int GetItemAmount(EInventory inventory, EItemName eItemName)
    {
        if (_itemCountDictionary.TryGetValue(eItemName, out int itemAmount))
            return itemAmount;
        else
            return 0;
    }
    public void AddChangeListener(EInventory inventory, Action<int, Item> action)
    {
        _inventoryDictionary[inventory].OnInventoryChanged += action;
    }
    public void RemoveChangeListener(EInventory inventory, Action<int, Item> action)
    {
        _inventoryDictionary[inventory].OnInventoryChanged -= action;
    }
}
