using UI;
using UI.MVC;
using UnityEngine;
using Views;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private InventorySO _inventorySO;
    [SerializeField] private CharacteristicSO _characteristicSO;
    [SerializeField] private Prefabs _prefabs;
    [SerializeField] private InventoryPanelView _inventoryView;
    [SerializeField] private int _spawnRadius;

    private void Start()
    {
        var creator = new ItemCreator(_inventorySO, _characteristicSO, _prefabs.PickUpPrefab, _spawnRadius);
        creator.CreateRandomItems();

        new InventoryPanelController(_inventoryView, _prefabs, _characteristicSO);
    }
}