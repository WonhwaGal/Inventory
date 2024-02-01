using ForInventory;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(InventorySO), menuName = "MyScriptables/InventorySO")]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItem> _items;

    public IReadOnlyList<InventoryItem> Items => _items;
}