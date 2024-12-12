using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public EInventory inventoryEnum;
    [SerializeField] private Vector2Int _inventorySize;
    [SerializeField] private ItemSO _itemSO;
    private List<List<Slot>> _slots;
    public Transform dragItemTrm;

    public void Init()
    {
        _slots = new List<List<Slot>>();
        for (int i = 0; i < _inventorySize.y; i++)
        {
            List<Slot> slotLine = new List<Slot>();
            for (int j = 0; j < _inventorySize.x; j++)
            {
                Slot slot = transform.GetChild(j + i * _inventorySize.x).GetComponent<Slot>();
                slot.SlotInit(this);
                slotLine.Add(slot);
            }
            _slots.Add(slotLine);
        }

        if (_slots.Count != 0)
        {
            Item item = new Item();
            item.ItemInit(_itemSO);
            _slots[0][0].AssignItem(item);
        }
    }

    public void AddItem(Item itme)
    {
        for (int i = 0; i < _inventorySize.y; i++)
        {
            for (int j = 0; j < _inventorySize.x; j++)
            {
                if (itme.Amount == 0) return;
                if (_slots[i][j].TryGetAssignedItem(out Item slotItem) &&
                    slotItem.IsSameItem(itme) && slotItem.IsFull() == false)
                {
                    int remain = slotItem.AddAmount(itme.Amount);
                    itme.SetAmount(remain);
                }
            }
        }

        for (int i = 0; i < _inventorySize.y; i++)
        {
            for (int j = 0; j < _inventorySize.x; j++)
            {
                if (itme.Amount == 0) return;
                if (_slots[i][j].GetAssignedItem() == null)
                {
                    _slots[i][j].AssignItem(itme);
                    return;
                }
            }
        }
    }
}