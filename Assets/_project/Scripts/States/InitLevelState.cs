using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.States
{
    public class InitLevelState : State
    {
        public override IEnumerator Start()
        {
            Debug.Log("InitLevelState");
            GameManager.Instance.GridHolder.Clear();
            GameManager.Instance.TowerManager.Clear();
            
            var level = GameManager.Instance.GetCurrentLevel();
            var grid = level.GenerateGrid();

            yield return GameManager.Instance.GridHolder.Init(grid, level.TileColorScheme);
            yield return GameManager.Instance.TowerManager.CreateInitialTowers();
        }
    }
}