using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDataView : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemName, _itemDescription;
    [SerializeField] private Button _createButton;
    [SerializeField] private NeedItemUI _needItemUI;

    public void SetItem(ItemSO itemSO)
    {
        _itemImage.sprite = itemSO.itemSprite;
        _itemName.text = itemSO.itemName;
        _itemDescription.text = itemSO.itemDescription;
    }
}
