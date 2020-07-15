using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class BeginPlayerTurnState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.AudioManager.PlayBeginTurnSound();
            yield return GameManager.Instance.UiManager.ShowBeginTurnMessage();
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerActionState());
        }
    }
}