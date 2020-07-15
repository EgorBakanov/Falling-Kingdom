using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class OnBeginPlayerTurnTowerActionState : State
    {
        public override IEnumerator Start()
        {
            // TODO OnBeginPlayerTurnTowerActionState
            
            GameManager.Instance.StateMachine.SetState(new BeginPlayerTurnState());
            yield break;
        }
    }
}