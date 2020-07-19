using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public class WaitForPlayerActionState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.GridHolder.Grid.RecalculateZones();
            GameManager.Instance.SelectionManager.DeselectAll();
            GameManager.Instance.SelectionManager.WaitForPlayerSelection = true;
            yield break;
        }

        public override IEnumerator OnEndTurn() => StateUtility.OnEndTurn();
        public override IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData) =>
            StateUtility.OnTowerClick(tower, eventData);
        public override IEnumerator OnBuyTower(int id) => StateUtility.OnBuyTower(id);
    }
}