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
        
        public override IEnumerator OnBuyTower(int id) => StateUtility.OnBuyTower(id);
        public override IEnumerator OnCancel() => StateUtility.ReturnToWait();
        public override IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData) =>
            StateUtility.OnTowerClick(tower, eventData);
        public override IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
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