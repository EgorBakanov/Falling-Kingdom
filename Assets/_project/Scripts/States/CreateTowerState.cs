using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class CreateTowerState : State
    {
        public override IEnumerator Start()
        {
            var id = GameManager.Instance.SelectionManager.SelectedTowerPresetId;
            var cost = GameManager.Instance.TowerManager.AvailableToBuildTowers[id].Cost;
            GameManager.Instance.CurrentMoney -= cost;
            yield return GameManager.Instance.TowerManager.CreateSelectedTower();
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerActionState());
        }
    }
}