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
            
            if (!tower.IsActive)
                yield break;
            
            if (action.Cost > GameManager.Instance.CurrentMoney)
            {
                yield return GameManager.Instance.UiManager.HideTowerActionBar();
                GameManager.Instance.StateMachine.SetState(new NotEnoughMoneyState());
            }
            else if (action.IsTarget)
            {
                GameManager.Instance.StateMachine.SetState(new TowerActionChooseTileState());
            }
            else
            {
                yield return GameManager.Instance.UiManager.HideTowerActionBar();
                GameManager.Instance.StateMachine.SetState(new TowerActionState());
            }
        }

        public override IEnumerator OnEndTurn()
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return StateUtility.OnEndTurn();
        }

        public override IEnumerator OnBuyTower(int id)
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return StateUtility.OnBuyTower(id);
        }

        public override IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return StateUtility.ReturnToWait();
        }

        public override IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData)
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return null;
            yield return StateUtility.OnTowerClick(tower, eventData);
        }

        public override IEnumerator OnCancel()
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
            yield return StateUtility.ReturnToWait();
        }
    }
}