using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public class InitLevelState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.GridHolder.Clear();
            GameManager.Instance.TowerManager.Clear();
            
            var level = GameManager.Instance.GetCurrentLevel();
            var grid = level.GenerateGrid();

            yield return GameManager.Instance.CameraController.SetBackground(level.SkyColor);
            yield return GameManager.Instance.GridHolder.Init(grid, level.TileColorScheme);
            yield return GameManager.Instance.TowerManager.CreateInitialTowers();

            GameManager.Instance.CurrentMoney = level.InitialMoney;
            GameManager.Instance.CurrentTurn = 0;
            yield return GameManager.Instance.UiManager.ShowRemainingTurnsCounter();
            
            GameManager.Instance.StateMachine.SetState(new ShowNextEnemySpawnState());
        }
    }
}