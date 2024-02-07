using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ForInventory;

namespace UI
{
    public class GridItem : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
    {
        [SerializeField] private Image _mainImage;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Image _characteristicSprite;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private Button _removeButton;
        private InventoryItem _inventoryItem;
        private Inventory _inventory;

        public Guid ID => _inventoryItem.ID;

        public event Action<GridItem> OnGetInactive;

        private void OnEnable() => _removeButton.gameObject.SetActive(false);

        private void Start() => _inventory = Inventory.GetInstance();

        public void Fill(InventoryItem item)
        {
            _inventoryItem = item;
            _mainImage.sprite = item.Image;
            _countText.text = item.Count.ToString();
            _characteristicSprite.sprite = item.CharacteristicImage;
            _valueText.text = "+" + item.CharacteristicValue.ToString();
            _removeButton.onClick.AddListener(RemoveItem);
        }

        public void OnPointerDown(PointerEventData eventData)
            => _removeButton.gameObject.SetActive(true);

        public void OnPointerExit(PointerEventData eventData)
            => _removeButton.gameObject.SetActive(false);

        private void RemoveItem() => _inventory.Remove(_inventoryItem, ChangeCount);

        private void ChangeCount(int count)
        {
            _countText.text = count.ToString();
            if (count == 0)
                gameObject.SetActive(false);
        }

        public void GetDisabled() => gameObject.SetActive(false);

        private void OnDisable()
        {
            OnGetInactive?.Invoke(this);
            _removeButton.onClick.RemoveAllListeners();
        }
    }
}