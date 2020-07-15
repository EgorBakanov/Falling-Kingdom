using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
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

            yield return GameManager.Instance.GridHolder.Init(grid, level.TileColorScheme);
            yield return GameManager.Instance.TowerManager.CreateInitialTowers();
        }

        // For tests
        public override IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    tile.Height--;
                    break;
                case PointerEventData.InputButton.Right:
                    tile.Height++;
                    break;
                case PointerEventData.InputButton.Middle:
                {
                    var gridHolder = GameManager.Instance.GridHolder;
                    var preset = GameManager.Instance.TowerManager.AvailableToBuildTowers[0];
                    yield return GameManager.Instance.TowerManager.CreateTower(preset, gridHolder.TileHolders[tile.Index]);
                    break;
                }
            }
            yield break;
        }
    }
}