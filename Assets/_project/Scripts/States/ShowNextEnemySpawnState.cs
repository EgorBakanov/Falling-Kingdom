using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.States
{
    public class ShowNextEnemySpawnState : State
    {
        public override IEnumerator Start()
        {
            var turn = GameManager.Instance.CurrentTurn;
            yield return GameManager.Instance.EnemyManager.CreateSpawnerWave(turn);
            //GameManager.Instance.StateMachine.SetState(new CalculateNextMoveState());
            
            // For test
            yield return new WaitForSeconds(2f);
            GameManager.Instance.StateMachine.SetState(new SpawnEnemyState());
        }
    }
}