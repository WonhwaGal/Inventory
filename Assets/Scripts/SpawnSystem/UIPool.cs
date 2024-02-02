using System;
using System.Collections.Generic;
using UnityEngine;
using UI;
using ForInventory;

namespace SpawnSystem
{
    public class UIPool
    {
        private readonly Dictionary<int, GridItem> _inactives = new();
        private readonly UIFactory _factory;

        public UIPool(GridItem gridItem, Transform root) => _factory = new(gridItem, root);

        public event Action OnRenewGrid;

        public GridItem Spawn(InventoryItem item)
        {
            GridItem result;
            if (!_inactives.ContainsKey(item.Id))
            {
                result = _factory.Create();
            }
            else
            {
                result = _inactives[item.Id];
                _inactives.Remove(item.Id);
            }

            if (result != null)
                OnSpawnItem(result, item);
            return result;
        }

        public void RenewGrid() => OnRenewGrid?.Invoke();

        private void OnSpawnItem(GridItem result, InventoryItem item)
        {
            result.Fill(item);
            result.gameObject.SetActive(true);
            result.OnGetInactive += Despawn;
            OnRenewGrid += result.GetDisabled;
        }

        private void Despawn(GridItem gridItem)
        {
            _inactives.Add(gridItem.ID, gridItem);
            gridItem.gameObject.SetActive(false);
            gridItem.OnGetInactive -= Despawn;
            OnRenewGrid -= gridItem.GetDisabled;
        }
    }
}