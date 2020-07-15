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
            GameManager.Instance.StateMachine.SetState(new ShowNextEnemySpawnState());
            yield return GameManager.Instance.UiManager.UpdateRemainingTurnsCounter();
        }
    }
}