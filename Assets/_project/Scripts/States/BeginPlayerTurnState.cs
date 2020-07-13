using System.Collections;

namespace Nara.MFGJS2020.GameStateMachine
{
    public class BeginPlayerTurnState : State
    {
        public BeginPlayerTurnState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override IEnumerator Start()
        {
            // TODO BeginPlayerTurnState
            return base.Start();
        }
    }
}