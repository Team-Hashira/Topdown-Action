using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeView : MonoBehaviour
{
    [SerializeField] private RecipeDataView _recipeDataView;
    [SerializeField] private RecipeBarUI _recipeBarUI;
    [SerializeField] private List<ItemSO> items;

    private ScrollRect _scrollRect;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        items.ForEach(itemSO =>
        {
            RecipeBarUI recipeBar = Instantiate(_recipeBarUI, _scrollRect.content);
            recipeBar.Init(this, itemSO);
            recipeBar.SetButtonEvent(() => _recipeDataView.ShowRecipeData(itemSO));
        });
    }
}
