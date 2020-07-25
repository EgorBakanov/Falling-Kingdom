using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class BeginPlayerTurnState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.AudioManager.PlayBeginTurn();
            yield return GameManager.Instance.UiManager.ShowBeginTurnMessage();
            yield return GameManager.Instance.UiManager.ShowPlayerUi();
            GameManager.Instance.StateMachine.SetState(new OnBeginPlayerTurnTowerActionState());
        }
    }
}