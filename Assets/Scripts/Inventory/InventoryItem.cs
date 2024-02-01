using System;
using UnityEngine;

namespace ForInventory
{
    [Serializable]
    public sealed class InventoryItem
    {
        public string Name;
        public int Id;
        public InventoryType ItemType;
        public Sprite Image;
        public Characteristic Characteristic;
        private int _count = 1;

        public int Count { get => _count; set => _count = value; }
    }
}