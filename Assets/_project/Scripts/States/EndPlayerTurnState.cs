using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class EndPlayerTurnState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.CurrentTurn++;
            yield return GameManager.Instance.UiManager.HidePlayerUi();
            yield return GameManager.Instance.UiManager.ShowEndTurnMessage();
            GameManager.Instance.StateMachine.SetState(new SpawnEnemyState());
        }
    }
}