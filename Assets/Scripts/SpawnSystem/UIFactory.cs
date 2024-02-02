using UI;
using UnityEngine;

namespace SpawnSystem
{
    public class UIFactory
    {
        private readonly GridItem _gridItem;
        private readonly Transform _root;

        public UIFactory(GridItem gridItem, Transform root)
        {
            _gridItem = gridItem;
            _root = root;
        }

        public GridItem Create() => Object.Instantiate(_gridItem, _root);
    }
}