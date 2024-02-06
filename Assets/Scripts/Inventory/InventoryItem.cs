using System;
using UnityEngine;

namespace ForInventory
{
    [Serializable]
    public sealed class InventoryItem
    {
        public string Name;
        public InventoryType ItemType;
        public Sprite Image;
        public CharacteristicType CharacteristicType;
        public int CharacteristicValue;

        private Guid _id;
        public Guid ID
        {
            get => _id;
            set
            {
                if(_id == Guid.Empty)
                    _id = value;
            }
        }

        public int Count { get; set; }
        public Sprite CharacteristicImage {  get; set; }
    }
}