using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.GridObjects;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Holders
{
    public class TowerHolder : GridObjectHolder<Tower>, IPointerClickHandler
    {
        public override void Init(Tower obj, TileHolder tileHolder)
        {
            base.Init(obj, tileHolder);
            GridObject.OnDie += OnDie;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.Instance.StateMachine.OnTowerClick(GridObject, eventData);
        }

        public void OnDie()
        {
            GameManager.Instance.TowerManager.DestroyTower(this);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GridObject.OnDie -= OnDie;
        }
    }
}