using UnityEngine;
using ForInventory;
using System.Collections.Generic;
using SpawnSystem;

namespace UI.MVC
{
    public sealed class InventoryPanelModel
    {
        private readonly UIPool _pool;
        private readonly Inventory _inventory;
        private readonly CharacteristicSO _characteristicSO;
        private readonly Prefabs _prefabs;
        private InventoryType _currentType;

        public InventoryPanelModel(Prefabs prefabs, Transform root, CharacteristicSO characteristicSO)
        {
            _prefabs = prefabs;
            _pool = new UIPool(prefabs.GridItem, root);
            _characteristicSO = characteristicSO;
            _inventory = Inventory.GetInstance();
        }

        public void CreateGrid(InventoryType type, CharacteristicType characteristic)
        {
            if (type != InventoryType.None)
                _currentType = type;

            _pool.RenewGrid();
            IReadOnlyCollection<InventoryItem> collection;
            if (characteristic == CharacteristicType.None || characteristic == CharacteristicType.All)
                collection = _inventory.GetByType(_currentType);
            else
                collection = _inventory.FilterBy(_currentType, characteristic);

            if (collection == null)
                return;

            foreach (var item in collection)
                _pool.Spawn(item);
        }

        public void CreateFilterMenu(Transform filterRoot)
        {
            for(int i = 0;  i < _characteristicSO.Characteristics.Count; i++)
            {
                var type = _characteristicSO.Characteristics[i].Type;
                if (type == CharacteristicType.None)
                    continue;

                var result = GameObject.Instantiate(_prefabs.FilterButton, filterRoot);
                result.Fill(InventoryType.None, type, _characteristicSO.FindByType(type));
                result.OnFilter += CreateGrid;
            }
        }
    }
}