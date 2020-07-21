﻿using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class OnBeginPlayerTurnTowerActionState : State
    {
        public override IEnumerator Start()
        {
            var towers = GameManager.Instance.TowerManager.CurrentTowers;

            foreach (var towerHolder in towers)
            {
                var tower = towerHolder.GridObject;
                var action = tower.BeginPlayerTurnAction;
                if (action != null)
                {
                    GameManager.Instance.SelectionManager.SelectedTower = tower;
                    yield return action.Execute();
                }
            }
            
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerActionState());
        }
    }
}