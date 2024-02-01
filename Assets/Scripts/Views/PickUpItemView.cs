using UnityEngine;
using ForInventory;

namespace Views
{
    public class PickUpItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        public InventoryItem Item { get; private set; }

        public void AddItemData(InventoryItem item)
        {
            Item = item;
            _renderer.sprite = Item.Image;
        }

        private void OnMouseUp()
        {
            Inventory.GetInstance().Add(Item);
            gameObject.SetActive(false);
        }
    }
}