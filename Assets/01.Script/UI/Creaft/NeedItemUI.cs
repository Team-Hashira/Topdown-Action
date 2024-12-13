using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NeedItemUI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemName, _price;
    
    private ItemSO _itemSO;
    private int _needAmount;

    private void Start()
    {
        InventoryManager.Instance.AddChangeListener(EInventory.Main, UpdatePriceText);
    }

    private void OnDestroy()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.RemoveChangeListener(EInventory.Main, UpdatePriceText);
    }

    public void SetItem(ItemSO itemSO, int needAmount)
    {
        _itemSO = itemSO;
        _needAmount = needAmount;
        _itemImage.sprite = itemSO.itemSprite;
        _itemName.text = itemSO.itemName;

        UpdatePriceText(0, null);
    }

    public void UpdatePriceText(int index, Item item)
    {
        if (_itemSO == null) return;

        int currentItemCount
            = InventoryManager.Instance.GetItemAmount(EInventory.Main, _itemSO.itemEnum);
        _price.text = $"{currentItemCount}/{_needAmount}";
        if (currentItemCount < _needAmount)
            _price.color = Color.red;
        else
            _price.color = Color.white;
    }
}
