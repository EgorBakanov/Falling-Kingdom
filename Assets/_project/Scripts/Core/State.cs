using System.Collections;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Core
{
    public abstract class State
    {
        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
            yield break;
        }

        public virtual IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData)
        {
            yield break;
        }

        public virtual IEnumerator OnTowerAction(int id)
        {
            yield break;
        }

        public virtual IEnumerator OnBuyTower(int id)
        {
            yield break;
        }

        public virtual IEnumerator OnSubmit()
        {
            yield break;
        }
        
        public virtual IEnumerator OnCancel()
        {
            yield break;
        }
        
        public virtual IEnumerator OnEndTurn()
        {
            yield break;
        }
        
        public virtual IEnumerator OnTargetTowerDestroyed()
        {
            yield break;
        }
    }
}