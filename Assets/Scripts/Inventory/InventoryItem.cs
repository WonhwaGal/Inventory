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
        public CharacteristicType CharacteristicType;
        public int CharacteristicValue;

        public int Count { get; set; }
        public Sprite CharacteristicImage {  get; set; }
    }
}