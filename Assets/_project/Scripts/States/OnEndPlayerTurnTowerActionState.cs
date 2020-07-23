using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class OnEndPlayerTurnTowerActionState : State
    {
        public override IEnumerator Start()
        {
            yield return GameManager.Instance.TowerManager.PerformEndTurnActions();
            GameManager.Instance.StateMachine.SetState(new EndPlayerTurnState());
        }
    }
}