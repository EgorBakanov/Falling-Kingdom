using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class EnemyTurnState : State
    {
        public override IEnumerator Start()
        {
            yield return GameManager.Instance.EnemyManager.PerformNextMoves();
            
            if (GameManager.Instance.CurrentTurn < GameManager.Instance.GetCurrentLevel().TurnsToSurvive)
            {
                GameManager.Instance.StateMachine.SetState(new ActivateAllState());
            }
            else
            {
                GameManager.Instance.StateMachine.SetState(new WinState());
            }
        }
    }
}