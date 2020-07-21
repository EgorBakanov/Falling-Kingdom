using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.TowerActions
{
    [CreateAssetMenu(fileName = "New Change Target Zone Height Action", menuName = "MFGJS2020/Tower Actions/Change Target Zone Height")]
    public class ChangeTargetZoneHeightAction : TargetAction
    {
        [Range(1,20)][SerializeField] private int range = 5;
        [Range(0, 10)] [SerializeField] private int size = 0;
        [SerializeField] private int amount;
        
        public override IEnumerator Execute()
        {
            var targetTile = GameManager.Instance.SelectionManager.SelectedTile;
            var zone = GridUtility.SquaredZone(targetTile, size);
            
            var wait = new WaitForSeconds(animationTime);
            var tower = GameManager.Instance.SelectionManager.SelectedTower;
            var towerTileHolder = GameManager.Instance.GridHolder.TileHolders[tower.Tile.Index];
            GameManager.Instance.SelectionManager.AddToBadTarget(towerTileHolder.gameObject);
            yield return wait;
            
            foreach (var tile in zone)
            {
                var tileHolder = GameManager.Instance.GridHolder.TileHolders[tile.Index];
                GameManager.Instance.SelectionManager.AddToEnemyTarget(tileHolder.gameObject);
                yield return wait;
                tile.Height += amount;
                GameManager.Instance.SelectionManager.RemoveFromEnemyTarget(tileHolder.gameObject);
            }
            
            GameManager.Instance.SelectionManager.RemoveFromBadTarget(towerTileHolder.gameObject);
        }

        public override bool CorrectTarget(Tile tile)
        {
            var tower = GameManager.Instance.SelectionManager.SelectedTower;
            var zone = GridUtility.SquaredZone(tower.Tile, range);
            zone.Remove(tower.Tile);
            return zone.Contains(tile);
        }
    }
}