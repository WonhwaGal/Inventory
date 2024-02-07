using System;
using System.Collections.Generic;

namespace ForInventory
{
    public sealed class Inventory
    {
        /*ѕоскольку нет информации об архитектуре проекта,
        € решила использовать Singleton исключительно дл€ решени€ данной задачи*/
        #region Singleton
        public static Inventory Instance;

        private Inventory() { }

        public static Inventory GetInstance()
        {
            if (Instance == null)
                Instance = new Inventory();
            return Instance;
        }
        #endregion

        private readonly Dictionary<InventoryType, LinkedList<InventoryItem>> _inventoryList = new();

        public void Add(InventoryItem item)
        {
            if (!_inventoryList.TryGetValue(item.ItemType, out LinkedList<InventoryItem> linked))
            {
                linked = new LinkedList<InventoryItem>();
                _inventoryList.Add(item.ItemType, linked);
            }

            if(TryFindItem(linked, item.ID, out InventoryItem listItem))
                listItem.Count++;
            else
                linked.AddLast(item).Value.Count = 1;
        }

        public void Remove(InventoryItem item, Action<int> changedCallback = null)
        {
            if (_inventoryList.TryGetValue(item.ItemType, out LinkedList<InventoryItem> linked))
            {
                if(TryFindItem(linked, item.ID, out InventoryItem listItem))
                {
                    listItem.Count--;
                    if (listItem.Count == 0)
                        _inventoryList[item.ItemType].Remove(listItem);
                    changedCallback?.Invoke(listItem.Count);
                }
            }
        }

        private bool TryFindItem(LinkedList<InventoryItem> linked, Guid id, out InventoryItem item)
        {
            item = null;
            foreach (var listItem in linked)
            {
                if (listItem.ID == id)
                    item = listItem;
            }
            return item != null;
        }

        public IReadOnlyCollection<InventoryItem> FilterBy(InventoryType type, CharacteristicType characteristic)
        {
            var selection = new LinkedList<InventoryItem>();
            if(_inventoryList.TryGetValue(type, out LinkedList<InventoryItem> linked))
            {
                foreach (InventoryItem item in linked)
                {
                    if (item.CharacteristicType == characteristic)
                        selection.AddLast(item);
                }
            }
            return selection;
        }

        public IReadOnlyCollection<InventoryItem> GetByType(InventoryType type)
        {
            if (_inventoryList.TryGetValue(type, out LinkedList<InventoryItem> linked))
                return linked;
            return null;
        }
    }
}