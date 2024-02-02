using UnityEngine;
using Views;

public class ItemCreator
{
    private readonly InventorySO _inventorySO;
    private readonly CharacteristicSO _characteristicSO;
    private readonly PickUpItemView _pickUpPrefab;
    private readonly int _spawnRadius;
    private readonly Transform _root;

    public ItemCreator(InventorySO so, CharacteristicSO characteristicSO, PickUpItemView pickUpView, int spawnRadius)
    {
        _inventorySO = so;
        _characteristicSO = characteristicSO;
        _pickUpPrefab = pickUpView;
        _spawnRadius = spawnRadius;
        _root = new GameObject("PickUpItemsRoot").transform;
    }

    public void CreateRandomItems()
    {
        for(int i = 0; i < _inventorySO.Items.Count; i++)
        {
            var item = _inventorySO.Items[i];
            item.CharacteristicImage = _characteristicSO.FindByType(item.CharacteristicType);

            var randomNumber = Random.Range(0, 5);
            for(int j = 0; j < randomNumber; j++)
            {
                var pickItem = GameObject.Instantiate(_pickUpPrefab, Random.insideUnitSphere * _spawnRadius, Quaternion.identity);
                pickItem.transform.SetParent(_root);
                pickItem.AddItemData(_inventorySO.Items[i]);
            }
        }
    }
}