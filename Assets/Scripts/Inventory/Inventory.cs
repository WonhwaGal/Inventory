using System;
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
            if(_inventoryList.TryGetValue(item.ItemType, out LinkedList<InventoryItem> linked))
            {
                var listItem = FindItem(linked, item.ID);
                if(listItem != null)
                    listItem.Count++;
                else
                    linked.AddLast(item).Value.Count = 1;
            }
            else
            {
                var newList = new LinkedList<InventoryItem>();
                _inventoryList.Add(item.ItemType, newList);
                newList.AddFirst(item).Value.Count = 1;
            }
        }

        public int Remove(InventoryItem item)
        {
            if (_inventoryList.TryGetValue(item.ItemType, out LinkedList<InventoryItem> linked))
            {
                var listItem = FindItem(linked, item.ID);
                if(listItem != null)
                {
                    listItem.Count--;
                    if (listItem.Count == 0)
                        _inventoryList[item.ItemType].Remove(listItem);
                    return listItem.Count;
                }
            }
            return 0;
        }

        private InventoryItem FindItem(LinkedList<InventoryItem> list, Guid id)
        {
            foreach (var listItem in list)
            {
                if (listItem.ID == id)
                    return listItem;
            }
            return null;
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