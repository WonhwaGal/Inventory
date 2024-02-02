using UnityEngine;
using Views;

public class ItemCreator
{
    private readonly InventorySO _inventorySO;
    private readonly CharacteristicSO _characteristicSO;
    private readonly PickUpItemView _pickUpPrefab;
    private readonly Transform _root;

    public ItemCreator(InventorySO so, CharacteristicSO characteristicSO, PickUpItemView pickUpView)
    {
        _inventorySO = so;
        _characteristicSO = characteristicSO;
        _pickUpPrefab = pickUpView;
        _root = new GameObject("PickUpItemsRoot").transform;
    }

    public void CreateRandomItems(int minNumber, int maxNumber, int spawnRadius)
    {
        for(int i = 0; i < _inventorySO.Items.Count; i++)
        {
            var item = _inventorySO.Items[i];
            item.CharacteristicImage = _characteristicSO.FindByType(item.CharacteristicType);

            var randomNumber = Random.Range(minNumber, maxNumber + 1);
            for(int j = 0; j < randomNumber; j++)
            {
                var pickItem = GameObject.Instantiate(_pickUpPrefab, Random.insideUnitSphere * spawnRadius, Quaternion.identity);
                pickItem.transform.SetParent(_root);
                pickItem.AddItemData(_inventorySO.Items[i]);
            }
        }
    }
}