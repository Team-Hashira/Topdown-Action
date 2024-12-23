using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RecipeBarUI : MonoBehaviour
{
    private ItemSO itemSO;
    private Button _button;
    private RecipeView _recipeView;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _nameText;

    public void Init(RecipeView recipeView, ItemSO itemSO)
    {
        _recipeView = recipeView;
        _button = GetComponent<Button>();
        _image.sprite = itemSO.itemSprite;
        _nameText.text = itemSO.name;
    }

    public void SetButtonEvent(UnityAction unityAction)
    {
        _button.onClick.AddListener(unityAction);
    }
}
