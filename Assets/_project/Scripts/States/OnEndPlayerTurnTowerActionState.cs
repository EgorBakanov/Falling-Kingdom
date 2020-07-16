using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class OnEndPlayerTurnTowerActionState : State
    {
        public override IEnumerator Start()
        {
            // TODO OnEndPlayerTurnTowerActionState
            GameManager.Instance.StateMachine.SetState(new EndPlayerTurnState());
            yield break;
        }
    }
}