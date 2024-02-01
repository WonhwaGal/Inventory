using System;
using UnityEngine;

namespace ForInventory
{
    [Serializable]
    public class Characteristic
    {
        public CharacteristicType Type;
        public Sprite Image;
        public int Value;
    }
}