using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class ActivateAllState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.EnemyManager.ActivateAll();
            GameManager.Instance.TowerManager.ActivateAll();

            var level = GameManager.Instance.GetCurrentLevel();
            var turn = GameManager.Instance.CurrentTurn;
            yield return GameManager.Instance.UiManager.UpdateRemainingTurnsCounter(level.TurnsToSurvive - turn);
            GameManager.Instance.StateMachine.SetState(new ShowNextEnemySpawnState());
        }
    }
}