using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class FilterButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private InventoryType _inventoryType;
        [SerializeField] private CharacteristicType _characteristicType;
        [SerializeField] private Image _image;

        public event Action<InventoryType, CharacteristicType> OnFilter;

        public void OnPointerDown(PointerEventData eventData) => RequestGridSorting();

        public void RequestGridSorting() => OnFilter?.Invoke(_inventoryType, _characteristicType);

        public void Fill(InventoryType type, CharacteristicType filter, Sprite sprite)
        {
            _inventoryType = type;
            _characteristicType = filter;
            _image.sprite = sprite;
        }

        private void OnDestroy() => OnFilter = null;
    }
}