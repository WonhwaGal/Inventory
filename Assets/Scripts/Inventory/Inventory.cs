using System.Collections.Generic;
using UnityEngine;

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
            if (!_inventoryList.ContainsKey(item.ItemType))
            {
                _inventoryList.Add(item.ItemType, new LinkedList<InventoryItem>(new InventoryItem[] { item }));
                Debug.Log($"added {item.Name}, count is {item.Count}");
                return;
            }

            var list = _inventoryList[item.ItemType];
            foreach (InventoryItem listItem in list)
            {
                if (listItem.Id != item.Id)
                    continue;

                listItem.Count++;
                Debug.Log($"added {item.Name}, count is {item.Count}, items of this type {list.Count}");
                return;
            }

            list.AddLast(item);
            Debug.Log($"added new item : {item.Name}, count is {item.Count}, items of this type {list.Count}");
        }

        public void Remove(InventoryItem item)
        {
            if (!_inventoryList.ContainsKey(item.ItemType))
                Debug.LogWarning($"Type {item.ItemType} was not found, " +
                    $"{item.Name} with ID {item.Id} will not be removed");

            var list = _inventoryList[item.ItemType];
            foreach (InventoryItem listItem in list)
            {
                if (listItem.Id != item.Id)
                    continue;

                listItem.Count--;
                if (listItem.Count == 0)
                {
                    _inventoryList[item.ItemType].Remove(listItem);
                    Debug.Log($"removed {item.Name} completely");
                }

                Debug.Log($"removed {item.Name}, count is {item.Count}, items of this type {list.Count}");
                return;
            }
        }

        public IReadOnlyCollection<InventoryItem> FilterBy(InventoryType type, CharacteristicType characteristic)
        {
            var selection = new LinkedList<InventoryItem>();
            foreach (InventoryItem item in _inventoryList[type])
            {
                if (item.Characteristic.Type == characteristic)
                    selection.AddLast(item);
            }
            return selection;
        }

        public IReadOnlyCollection<InventoryItem> GetByType(InventoryType type) => _inventoryList[type];
    }
}