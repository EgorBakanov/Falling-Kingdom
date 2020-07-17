using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public class WaitForPlayerPickTowerActionState : State
    {
        public override IEnumerator Start()
        {
            yield return GameManager.Instance.UiManager.ShowTowerActionBar();
        }

        public override IEnumerator OnTowerAction(int id)
        {
            GameManager.Instance.SelectionManager.SelectedTowerActionId = id;
            var tower = GameManager.Instance.SelectionManager.SelectedTower;
            var action = tower.ActiveActions[id];
            if (action.Cost > GameManager.Instance.CurrentMoney)
            {
                GameManager.Instance.StateMachine.SetState(new NotEnoughMoneyState());
            }
            else if (action.IsTarget)
            {
                GameManager.Instance.StateMachine.SetState(new TowerActionChooseTileState());
            }
            else
            {
                GameManager.Instance.StateMachine.SetState(new TowerActionState());
            }
            yield break;
        }

        public override IEnumerator OnEndTurn() => StateUtility.OnEndTurn();
        public override IEnumerator OnBuyTower(int id) => StateUtility.OnBuyTower(id);
        public override IEnumerator OnTileClick(Tile tile, PointerEventData eventData) => StateUtility.ReturnToWait();
        public override IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData) =>
            StateUtility.OnTowerClick(tower, eventData);
        public override IEnumerator OnCancel() => StateUtility.ReturnToWait();
    }
}