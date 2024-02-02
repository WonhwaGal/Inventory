using UnityEngine;
using UnityEngine.UI;

namespace UI.MVC
{
    public class InventoryPanelView : MonoBehaviour
    {
        [SerializeField] private Transform _gridTransform;
        [SerializeField] private Transform _filtersTransform;
        [SerializeField] private Button _closeButton;
        [SerializeField] private FilterButton[] _inventoryTypes;

        public Transform GridTransform => _gridTransform;
        public FilterButton[] InventoryTypes => _inventoryTypes;
        public Transform FiltersTransform => _filtersTransform;

        private void Start() 
            => _closeButton.onClick.AddListener(() => gameObject.SetActive(false));

        private void OnEnable() => _inventoryTypes[0].RequestGridSorting();

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}