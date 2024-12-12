using System;

public class Item
{
    public ItemSO itemSO;
    public event Action<int, int> OnChangedAmount;
    public int Amount { get; private set; }

    public void ItemInit(ItemSO startItemSO, int amount = 1)
    {
        itemSO = startItemSO;
        Amount = amount;
    }

    public int AddAmount(int amount)
    {
        int prevAmount = Amount;
        int remain = 0;
        Amount += amount;
        if (Amount > itemSO.maxOverlapAmount)
        {
            remain = Amount - itemSO.maxOverlapAmount;
            Amount = itemSO.maxOverlapAmount;
        }
        OnChangedAmount?.Invoke(prevAmount, Amount);
        return remain;
    }
    public int RemoveAmount(int amount)
    {
        int prevAmount = Amount;
        int over = 0;
        Amount -= amount;
        if (Amount < 0)
        {
            over = -Amount;
            Amount = 0;
        }
        OnChangedAmount?.Invoke(prevAmount, Amount);
        return over;
    }
    public void SetAmount(int amount)
    {
        int prevAmount = Amount;
        Amount = amount;
        if (Amount > itemSO.maxOverlapAmount)
        {
            Amount = itemSO.maxOverlapAmount;
        }
        OnChangedAmount?.Invoke(prevAmount, Amount);
    }
    public bool IsSameItem(Item targetItem)
        => itemSO.itemType == targetItem.itemSO.itemType;
    public bool IsFull()
        => Amount == itemSO.maxOverlapAmount;

    public Item Clone()
    {
        Item item = new Item();
        item.itemSO = itemSO;
        item.Amount = Amount;
        return item;
    }
}
