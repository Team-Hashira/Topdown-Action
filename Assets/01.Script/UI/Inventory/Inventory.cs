using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public EInventory inventoryEnum;
    [SerializeField] private Vector2Int _inventorySize;
    [SerializeField] private ItemSO _itemSO;
    private List<Slot> _slots;
    public Transform dragItemTrm;

    public event Action<int, Item> OnInventoryChanged;

    public void Init()
    {
        _slots = new List<Slot>();
        for (int i = 0; i < _inventorySize.y * _inventorySize.x; i++)
        {
            Slot slot = transform.GetChild(i).GetComponent<Slot>();
            slot.SlotInit(this);
            slot.OnSlotChangedEvent += item => OnInventoryChanged?.Invoke(i, item);
            _slots.Add(slot);
        }

        if (_slots.Count != 0)
        {
            Item item = new Item();
            item.ItemInit(_itemSO);
            _slots[0].AssignItem(item);
        }
    }

    public void AddItem(Item itme)
    {
        for (int i = 0; i < _inventorySize.y * _inventorySize.x; i++)
        {
            if (itme.Amount == 0) return;
            if (_slots[i].TryGetAssignedItem(out Item slotItem) &&
                slotItem.IsSameItem(itme) && slotItem.IsFull() == false)
            {
                int remain = slotItem.AddAmount(itme.Amount);
                itme.SetAmount(remain);
            }
        }

        for (int i = 0; i < _inventorySize.y * _inventorySize.x; i++)
        {
            if (itme.Amount == 0) return;
            if (_slots[i].GetAssignedItem() == null)
            {
                _slots[i].AssignItem(itme);
                return;
            }
        }
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