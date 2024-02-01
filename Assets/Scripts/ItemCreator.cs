using UnityEngine;
using Views;

public class ItemCreator
{
    private readonly InventorySO _inventorySO;
    private readonly PickUpItemView _pickUpPrefab;
    private readonly int _spawnRadius;

    public ItemCreator(InventorySO so, PickUpItemView pickUpView, int spawnRadius)
    {
        _inventorySO = so;
        _pickUpPrefab = pickUpView;
        _spawnRadius = spawnRadius;
    }

    public void CreateRandomItems()
    {
        for(int i = 0; i < _inventorySO.Items.Count; i++)
        {
            var pickItem = GameObject.Instantiate(_pickUpPrefab, Random.insideUnitSphere * _spawnRadius, Quaternion.identity);
            pickItem.AddItemData(_inventorySO.Items[i]);
        }
    }
}