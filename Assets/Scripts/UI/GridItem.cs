using ForInventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridItem : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
    [SerializeField] private Image _mainImage;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Image _characteristicSprite;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private Button _removeButton;
    private InventoryItem _inventoryItem;
    private Inventory _inventory;

    private void OnEnable() => _removeButton.gameObject.SetActive(false);

    private void Start() => _inventory = Inventory.GetInstance();

    public void Fill(InventoryItem item)
    {
        _inventoryItem = item;
        _mainImage.sprite = item.Image;
        _countText.text = item.Count.ToString();
        _characteristicSprite.sprite = item.Characteristic.Image;
        _valueText.text = item.Characteristic.Value.ToString();
        _removeButton.onClick.AddListener(RemoveItem);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _removeButton.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _removeButton.gameObject.SetActive(false);
    }

    private void RemoveItem()
    {
        _inventory.Remove(_inventoryItem);
    }

    private void OnDestroy()
    {
        _removeButton.onClick.RemoveAllListeners();
    }
}