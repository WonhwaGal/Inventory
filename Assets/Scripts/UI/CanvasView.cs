using UI.MVC;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private InventoryPanelView _inventoryPanel;

        private void Start()
        {
            _openButton.onClick.AddListener(OpenInventory);
            _inventoryPanel.gameObject.SetActive(false);
        }

        private void OpenInventory() 
            => _inventoryPanel.gameObject.SetActive(true);

        private void OnDestroy() 
            => _openButton.onClick.RemoveListener(OpenInventory);
    }
}