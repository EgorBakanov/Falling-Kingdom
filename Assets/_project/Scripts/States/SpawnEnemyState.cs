﻿using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.States
{
    public class SpawnEnemyState : State
    {
        public override IEnumerator Start()
        {
            yield return GameManager.Instance.EnemyManager.OpenAllSpawners();
            //GameManager.Instance.StateMachine.SetState(new EnemyTurnState());
            
            // For test
            yield return new WaitForSeconds(2f);
            if(++GameManager.Instance.CurrentTurn < GameManager.Instance.GetCurrentLevel().TurnsToSurvive)
                GameManager.Instance.StateMachine.SetState(new ShowNextEnemySpawnState());
        }
    }
}