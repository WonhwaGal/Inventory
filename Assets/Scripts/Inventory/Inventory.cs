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
                item.Count = 1;
                return;
            }

            var list = _inventoryList[item.ItemType];
            foreach (InventoryItem listItem in list)
            {
                if (listItem.Id != item.Id)
                    continue;

                listItem.Count++;
                return;
            }

            list.AddLast(item);
        }

        public int Remove(InventoryItem item)
        {
            var list = _inventoryList[item.ItemType];
            foreach (InventoryItem listItem in list)
            {
                if (listItem.Id != item.Id)
                    continue;

                listItem.Count--;
                if (listItem.Count == 0)
                    _inventoryList[item.ItemType].Remove(listItem);

                return listItem.Count;
            }
            return 0;
        }

        public IReadOnlyCollection<InventoryItem> FilterBy(InventoryType type, CharacteristicType characteristic)
        {
            var selection = new LinkedList<InventoryItem>();
            foreach (InventoryItem item in _inventoryList[type])
            {
                if (item.CharacteristicType == characteristic)
                    selection.AddLast(item);
            }
            return selection;
        }

        public IReadOnlyCollection<InventoryItem> GetByType(InventoryType type)
        {
            if (_inventoryList.ContainsKey(type))
                return _inventoryList[type];

            return null;
        }
    }
}