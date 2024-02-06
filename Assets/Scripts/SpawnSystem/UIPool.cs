using System;
using System.Collections.Generic;
using UnityEngine;
using UI;
using ForInventory;

namespace SpawnSystem
{
    public class UIPool
    {
        private readonly Dictionary<Guid, GridItem> _inactives = new();
        private readonly UIFactory _factory;

        public UIPool(GridItem gridItem, Transform root) => _factory = new(gridItem, root);

        public event Action OnRenewGrid;

        public void RenewGrid() => OnRenewGrid?.Invoke();

        public GridItem Spawn(InventoryItem item)
        {
            GridItem result;
            if (_inactives.TryGetValue(item.ID, out GridItem gridItem))
            {
                result = gridItem;
                _inactives.Remove(item.ID);
            }
            else
            {
                result = _factory.Create();
            }

            OnSpawnItem(result, item);
            return result;
        }

        private void OnSpawnItem(GridItem result, InventoryItem item)
        {
            result.Fill(item);
            result.OnGetInactive += Despawn;
            OnRenewGrid += result.GetDisabled;
            result.gameObject.SetActive(true);
        }

        private void Despawn(GridItem gridItem)
        {
            _inactives.Add(gridItem.ID, gridItem);
            gridItem.OnGetInactive -= Despawn;
            OnRenewGrid -= gridItem.GetDisabled;
            gridItem.gameObject.SetActive(false);
        }
    }
}