using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;

namespace Nara.MFGJS2020.States
{
    public class CalculateNextMoveState : State
    {
        public override IEnumerator Start()
        {
            var target = GameManager.Instance.TowerManager.CurrentTargetTower;
            yield return GameManager.Instance.EnemyManager.CalculateNextMoves(target.GridObject);
            GameManager.Instance.StateMachine.SetState(new BeginPlayerTurnState());
        }
    }
}