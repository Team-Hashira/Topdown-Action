using System;
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

    private ItemSO _currentItemSO;

    private void Awake()
    {
        _createButton.onClick.AddListener(HandleCreateBtnEvent);
        HideRecipeData();
    }

    private void HandleCreateBtnEvent()
    {
        if (CreateItem())
        {
            Debug.Log("자원생성");
        }
        else
        {
            Debug.Log("자원부족");
        }

    }

    public void ShowRecipeData(ItemSO itemSO)
    {
        _currentItemSO = itemSO;
        _content.SetActive(true);
        _itemImage.sprite = itemSO.itemSprite;
        _itemName.text = itemSO.itemName;
        _itemDescription.text = itemSO.itemDescription;

        for (int i = 0; i < _needItemListTrm.childCount; i++)
        {
            Destroy(_needItemListTrm.GetChild(i).gameObject);
        }

        foreach (ItemSO recipeItemSO in itemSO.itemRecipe.Keys)
        {
            NeedItemUI needItemUI = Instantiate(_needItemUI, _needItemListTrm);
            needItemUI.SetItem(recipeItemSO, itemSO.itemRecipe[recipeItemSO]);
        }
    }
    public void HideRecipeData()
    {
        _content.SetActive(false);
    }

    public bool CreateItem()
    {
        bool canCreate = true;
        foreach (ItemSO recipeItemSO in _currentItemSO.itemRecipe.Keys)
        {
            if (InventoryManager.Instance.GetItemAmount(EInventory.Main
                , recipeItemSO.itemEnum) < _currentItemSO.itemRecipe[recipeItemSO])
                canCreate = false;
        }
        if (canCreate)
        {
            foreach (ItemSO recipeItemSO in _currentItemSO.itemRecipe.Keys)
            {
                InventoryManager.Instance.RemoveItem
                    (EInventory.Main, recipeItemSO, _currentItemSO.itemRecipe[recipeItemSO]);
            }
            InventoryManager.Instance.AddItem(EInventory.Main, _currentItemSO);
        }
        return canCreate;
    }
}
