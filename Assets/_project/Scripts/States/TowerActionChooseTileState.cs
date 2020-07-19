using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public class TowerActionChooseTileState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.SelectionManager.TileSelection = SelectionManager.TileSelectionType.TowerAction;
            yield return GameManager.Instance.UiManager.ShowCancelButton();
        }
        
        public override IEnumerator OnBuyTower(int id)
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return StateUtility.OnBuyTower(id);
        }

        public override IEnumerator OnCancel()
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return StateUtility.ReturnToWait();
        }

        public override IEnumerator OnEndTurn()
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return StateUtility.OnEndTurn();
        }

        public override IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData)
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return StateUtility.OnTowerClick(tower, eventData);
        }

        public override IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            var actionId = GameManager.Instance.SelectionManager.SelectedTowerActionId;
            var action = GameManager.Instance.SelectionManager.SelectedTower.ActiveActions[actionId] as TargetAction;
            if (action == null || !action.CorrectTarget(tile))
            {
                yield return StateUtility.ReturnToWait();
            }
            else
            {
                GameManager.Instance.SelectionManager.SelectedTile = tile;
                GameManager.Instance.StateMachine.SetState(new TowerActionState());
            }
        }
    }
}