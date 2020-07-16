using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class SelectLevelState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.NextLevel();
            if (GameManager.Instance.HasNextLevel)
            {
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