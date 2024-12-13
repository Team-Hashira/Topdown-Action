using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public EInventory inventoryEnum;
    [SerializeField] private Vector2Int _inventorySize;
    private List<Slot> _slots;
    public Transform dragItemTrm;

    public Action<int, Item> OnInventoryChanged;
    private Dictionary<EItemName, int> _itemCountDictionary;

    public void Init()
    {
        _slots = new List<Slot>();
        _itemCountDictionary = new Dictionary<EItemName, int>();
        for (int i = 0; i < _inventorySize.y * _inventorySize.x; i++)
        {
            Slot slot = transform.GetChild(i).GetComponent<Slot>();
            slot.SlotInit(this);
            slot.OnSlotChangedEvent += item => OnInventoryChanged?.Invoke(i, item);
            _slots.Add(slot);
        }
    }

    public void AddItem(Item item)
    {
        if (_itemCountDictionary.ContainsKey(item.itemSO.itemEnum))
            _itemCountDictionary[item.itemSO.itemEnum]++;
        else
            _itemCountDictionary[item.itemSO.itemEnum] = 1;


        for (int i = 0; i < _inventorySize.y * _inventorySize.x; i++)
        {
            if (item.Amount == 0) return;
            if (_slots[i].TryGetAssignedItem(out Item slotItem) &&
                slotItem.IsSameItem(item) && slotItem.IsFull() == false)
            {
                int remain = slotItem.AddAmount(item.Amount);
                item.SetAmount(remain);
            }
        }

        for (int i = 0; i < _inventorySize.y * _inventorySize.x; i++)
        {
            if (item.Amount == 0) return;
            if (_slots[i].GetAssignedItem() == null)
            {
                _slots[i].AssignItem(item);
                return;
            }
        }
    }

    public void RemoveItem(Item item)
    {
        if (_itemCountDictionary.ContainsKey(item.itemSO.itemEnum))
            _itemCountDictionary[item.itemSO.itemEnum]--;
        else
            return;

        for (int i = _inventorySize.y * _inventorySize.x - 1; i >= 0; i--)
        {
            if (item.Amount == 0) return;
            if (_slots[i].TryGetAssignedItem(out Item slotItem) &&
                slotItem.IsSameItem(item) && slotItem.Amount > 0)
            {
                int remain = slotItem.RemoveAmount(item.Amount);
                if (slotItem.Amount == 0)
                    _slots[i].AssignItem(null);
                item.SetAmount(remain);
            }
        }
    }
    public int GetItemAmount(EItemName eItemName)
    {
        if (_itemCountDictionary.TryGetValue(eItemName, out int itemAmount))
            return itemAmount;
        else
            return 0;
    }

    public Slot GetSlot(int index)
    {
        if (_slots.Count > index)
        {
            return _slots[index];
        }
        return null;
    }
    public Slot GetSlot(Vector2Int pos)
    {
        if (_slots.Count / _inventorySize.x > pos.y && 
            _slots.Count / _inventorySize.y > pos.x)
        {
            return _slots[pos.x + pos.y * _inventorySize.x];
        }
        return null;
    }
}