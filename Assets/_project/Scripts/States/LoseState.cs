using System.Collections;

namespace Nara.MFGJS2020.GameStateMachine
{
    public class LoseState : State
    {
        public LoseState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override IEnumerator Start()
        {
            // TODO LoseState
            return base.Start();
        }
    }
}