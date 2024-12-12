using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NeedItemUI : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemName, _price;

    public void SetItem(ItemSO itemSO, int needAmount)
    {
        _itemImage.sprite = itemSO.itemSprite;
        _itemName.text = itemSO.itemName;
    }
}
