using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class SelectLevelState : State
    {
        public override IEnumerator Start()
        {
            if (GameManager.Instance.HasNextLevel)
            {
                GameManager.Instance.NextLevel();
                GameManager.Instance.StateMachine.SetState(new InitLevelState());
            }
            else
            {
                GameManager.Instance.StateMachine.SetState(new EndGameState());
            }
            yield break;
        }
    }
}