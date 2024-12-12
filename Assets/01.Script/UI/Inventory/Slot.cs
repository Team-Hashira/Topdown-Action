using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _itemRectTrm;
    [SerializeField] private Image _selectOutline, _itemImage;
    [SerializeField] private TextMeshProUGUI _amountText;

    private Inventory _inventory;
    private Item _assignedItem;
    private Vector3 _dragStartPos;
    private bool _isDrag;

    public void SlotInit(Inventory inventory)
    {
        _inventory = inventory;
        _selectOutline.color = new Color(1, 1, 1, 0);
        AssignItem(null);
    }

    public void AssignItem(Item item)
    {
        if (_assignedItem != null) _assignedItem.OnChangedAmount -= HandleChangedAmountEvent;

        if (item == null)
        {
            _itemImage.color = new Color(1, 1, 1, 0);
            _amountText.text = "";
        }
        else
        {
            Debug.Log("Assign");
            _itemImage.sprite = item.itemSO.itemSprite;
            _itemImage.color = new Color(1, 1, 1, 1);
            _amountText.text = item.Amount.ToString();
            item.OnChangedAmount -= HandleChangedAmountEvent;
        }
        _assignedItem = item;

        if (_assignedItem != null) _assignedItem.OnChangedAmount += HandleChangedAmountEvent;
    }

    private void HandleChangedAmountEvent(int prevAmount, int newAmount)
    {
        _amountText.text = newAmount.ToString();
    }

    public void ChangeSlot(Slot slot)
    {
        Item item = slot.GetAssignedItem()?.Clone();
        slot.AssignItem(_assignedItem);
        AssignItem(item);
    }

    public bool TryGetAssignedItem(out Item item)
    {
        item = _assignedItem;
        return _assignedItem != null;
    }
    public Item GetAssignedItem()
    {
        return _assignedItem;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isDrag) return;
        _selectOutline.color = new Color(1, 1, 0, 0.8f);
        InventoryManager.Instance.PointerSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isDrag) return;
        _selectOutline.color = new Color(1, 1, 0, 0f);
        InventoryManager.Instance.PointerSlot = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_assignedItem == null) return;

        _isDrag = true;
        _selectOutline.color = new Color(1, 1, 0, 0f);
        _dragStartPos = Input.mousePosition;
        InventoryManager.Instance.DragSlot = this;
        _itemRectTrm.SetParent(_inventory.dragItemTrm);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isDrag)
        {
            InventoryManager.Instance.DragSlot = null;
            Slot pointerSlot = InventoryManager.Instance.PointerSlot;
            if (pointerSlot != null && pointerSlot != this)
            {
                Item item = pointerSlot.GetAssignedItem();
                if (item != null && item.IsSameItem(_assignedItem))
                {
                    int remain = item.AddAmount(_assignedItem.Amount);
                    if (remain == 0)
                        AssignItem(null);
                    else
                        _assignedItem.SetAmount(remain);
                }
                else
                {
                    pointerSlot.ChangeSlot(this);
                }
            }
            _itemRectTrm.SetParent(transform);
        }

        _itemRectTrm.anchoredPosition = Vector3.zero;
        _isDrag = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isDrag == false) return;
        _itemRectTrm.anchoredPosition = Input.mousePosition;
    }
}
