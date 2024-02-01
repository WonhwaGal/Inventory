using UnityEngine;
using Views;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private InventorySO _inventorySO;
    [SerializeField] private PickUpItemView _pickUpPrefab;
    [SerializeField] private int _spawnRadius;

    private void Start()
    {
        var creator = new ItemCreator(_inventorySO, _pickUpPrefab, _spawnRadius);
        creator.CreateRandomItems();
    }
}