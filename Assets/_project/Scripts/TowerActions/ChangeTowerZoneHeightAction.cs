using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.TowerActions
{
    [CreateAssetMenu(fileName = "New Change Tower Zone Height Action", menuName = "MFGJS2020/Tower Actions/Change Tower Zone Height")]
    public class ChangeTowerZoneHeightAction : NonTargetAction
    {
        [Range(1, 10)] [SerializeField] private int size = 1;
        [SerializeField] private int amount;
        
        public override IEnumerator Execute()
        {
            var targetTile = GameManager.Instance.SelectionManager.SelectedTower.Tile;
            var zone = GridUtility.SquaredZone(targetTile, size);
            zone.Remove(targetTile);
            
            var wait = new WaitForSeconds(animationTime);
            var targetTileHolder = GameManager.Instance.GridHolder.TileHolders[targetTile.Index];
            GameManager.Instance.SelectionManager.AddToBadTarget(targetTileHolder.gameObject);
            yield return wait;

            foreach (var tile in zone)
            {
                var tileHolder = GameManager.Instance.GridHolder.TileHolders[tile.Index];
                GameManager.Instance.SelectionManager.AddToEnemyTarget(tileHolder.gameObject);
                yield return wait;
                tile.Height += amount;
                GameManager.Instance.SelectionManager.RemoveFromEnemyTarget(tileHolder.gameObject);
            }
            
            GameManager.Instance.SelectionManager.RemoveFromBadTarget(targetTileHolder.gameObject);
        }
    }
}