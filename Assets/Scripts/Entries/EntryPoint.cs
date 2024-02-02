using UI.MVC;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private InventorySO _inventorySO;
    [SerializeField] private CharacteristicSO _characteristicSO;
    [SerializeField] private Prefabs _prefabs;
    [SerializeField] private InventoryPanelView _inventoryView;

    [Header("Spawn Settings")]
    [SerializeField] private int _spawnRadius;
    [SerializeField] private int _minSpanNumber;
    [SerializeField] private int _maxSpanNumber;

    private void Start()
    {
        var creator = new ItemCreator(_inventorySO, _characteristicSO, _prefabs.PickUpPrefab);
        creator.CreateRandomItems(_minSpanNumber, _maxSpanNumber, _spawnRadius);

        new InventoryPanelController(_inventoryView, _prefabs, _characteristicSO);
    }
}