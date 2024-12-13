using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDataView : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemName, _itemDescription;
    [SerializeField] private Transform _needItemListTrm;
    [SerializeField] private Button _createButton;
    [SerializeField] private NeedItemUI _needItemUI;

    public void SetItem(ItemSO itemSO)
    {
        _createButton.onClick.RemoveAllListeners();
        _content.SetActive(true);
        _itemImage.sprite = itemSO.itemSprite;
        _itemName.text = itemSO.itemName;
        _itemDescription.text = itemSO.itemDescription;

        for (int i = 0; i < _needItemListTrm.childCount; i++)
        {
            Destroy(_needItemListTrm.GetChild(i).gameObject);
        }

        foreach (ItemSO item in itemSO.itemRecipe.Keys)
        {
            NeedItemUI needItemUI = Instantiate(_needItemUI, _needItemListTrm);
            needItemUI.SetItem(item, itemSO.itemRecipe[item]);
        }

        _createButton.onClick.AddListener(() => InventoryManager.Instance.AddItem(EInventory.Main, itemSO));
    }
}
