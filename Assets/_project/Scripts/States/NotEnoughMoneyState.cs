using System.Collections;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class NotEnoughMoneyState : State
    {
        public NotEnoughMoneyState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override IEnumerator Start()
        {
            // TODO NotEnoughMoneyState
            return base.Start();
        }
    }
}