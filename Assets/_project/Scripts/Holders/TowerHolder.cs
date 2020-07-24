using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Generators;
using Nara.MFGJS2020.GridObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Holders
{
    public class TowerHolder : GridObjectHolder<Tower>, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public override void Init(Tower obj, TileHolder tileHolder)
        {
            base.Init(obj, tileHolder);
            GridObject.OnDie += OnDie;
            GridObject.OnHealthChanged += OnHealthChanged;
        }

        protected override void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        protected override void OnTileFall()
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.Instance.StateMachine.OnTowerClick(GridObject, eventData);
        }

        private void OnDie()
        {
            UnsubscribeOnTile(TileHolder.Tile);
            GameManager.Instance.TowerManager.DestroyTower(this);
        }

        private void OnHealthChanged()
        {
            GameManager.Instance.UiManager.UpdateTowerHeading(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GridObject.OnDie -= OnDie;
            GridObject.OnHealthChanged -= OnHealthChanged;
        }

        public void Replace(TowerPreset preset)
        {
            SetVisual(preset.VisualPrefab);
            GridObject.Replace(preset);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameManager.Instance.UiManager.ShowTowerHeading(this);
            GameManager.Instance.UiManager.ShowDescriptor(GridObject.Preset.Name,UiManager.DescriptorTag.Tower,GridObject.Preset.Description);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameManager.Instance.UiManager.HideTowerHeading(this);
            GameManager.Instance.UiManager.HideDescriptor();
        }
    }
}