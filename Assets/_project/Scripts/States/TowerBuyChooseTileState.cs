using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public class TowerBuyChooseTileState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.SelectionManager.TileSelection = SelectionManager.TileSelectionType.TowerBuy;
            yield return GameManager.Instance.UiManager.ShowCancelButton();
        }

        public override IEnumerator OnBuyTower(int id) => StateUtility.OnBuyTower(id);
        public override IEnumerator OnCancel() => StateUtility.ReturnToWait();
        public override IEnumerator OnEndTurn() => StateUtility.OnEndTurn();
        public override IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData) =>
            StateUtility.OnTowerClick(tower, eventData);
        public override IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left ||
                !GameManager.Instance.GridHolder.Grid.BuildZone.Contains(tile))
            {
                yield return StateUtility.ReturnToWait();
            }
            
            GameManager.Instance.SelectionManager.SelectedTile = tile;
            GameManager.Instance.StateMachine.SetState(new CreateTowerState());
        }
    }
}