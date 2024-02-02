
namespace UI.MVC
{
    public class InventoryPanelController
    {
        private readonly InventoryPanelModel _model;
        private readonly InventoryPanelView _view;

        public InventoryPanelController(InventoryPanelView view, Prefabs prefabs, CharacteristicSO characteristicSO)
        {
            _model = new InventoryPanelModel(prefabs, view.GridTransform, characteristicSO);
            _view = view;
            OnAddView();
        }

        private void OnAddView()
        {
            for (int i = 0; i < _view.InventoryTypes.Length; i++)
                _view.InventoryTypes[i].OnFilter += _model.CreateGrid;
            _model.CreateFilterMenu(_view.FiltersTransform);
        }
    }
}