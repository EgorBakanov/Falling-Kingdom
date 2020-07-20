using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class TowerActionState : State
    {
        public override IEnumerator Start()
        {
            var actionId = GameManager.Instance.SelectionManager.SelectedTowerActionId;
            var action = GameManager.Instance.SelectionManager.SelectedTower.ActiveActions[actionId];
            var resultMoney = GameManager.Instance.CurrentMoney - action.Cost;
            yield return GameManager.Instance.SetMoney(resultMoney);
            yield return action.Execute();
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerActionState());
        }
    }
}