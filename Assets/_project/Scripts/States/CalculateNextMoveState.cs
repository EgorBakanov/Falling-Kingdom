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
            foreach (var enemy in GameManager.Instance.EnemyManager.CurrentEnemies)
            {
                enemy.GridObject.MoveIntention = CalculateNextMove(enemy.GridObject);
            }
            
            GameManager.Instance.StateMachine.SetState(new OnBeginPlayerTurnTowerActionState());
            yield break;
        }

        private Tile CalculateNextMove(Enemy enemy)
        {
            // TODO
            return null;
        }
    }
}