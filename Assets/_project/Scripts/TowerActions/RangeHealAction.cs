using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;
using UnityEngine;

namespace Nara.MFGJS2020.TowerActions
{
    [CreateAssetMenu(fileName = "New Range Heal Action", menuName = "MFGJS2020/Tower Actions/Range Heal")]
    public class RangeHealAction : NonTargetAction
    {
        [SerializeField] private int amount;
        [Range(1,20)][SerializeField] private int range = 1;
        public override IEnumerator Execute()
        {
            var centerTile = GameManager.Instance.SelectionManager.SelectedTower.Tile;
            var centerTileHolder = GameManager.Instance.GridHolder.TileHolders[centerTile.Index];
            var zone = GridUtility.SquaredZone(centerTile, range);
            zone.Remove(centerTile);
            var wait = new WaitForSeconds(animationTime);
            
            GameManager.Instance.SelectionManager.AddToBadTarget(centerTileHolder.gameObject);

            foreach (var tile in zone)
            {
                if (tile.GridObject is Tower tower)
                {
                    if(tower.Health == tower.MaxHealth)
                        continue;
                    
                    var tileHolder = GameManager.Instance.GridHolder.TileHolders[tile.Index];
                    GameManager.Instance.SelectionManager.AddToGoodTarget(tileHolder.gameObject);
                    yield return wait;
                    tower.Health += amount;
                    GameManager.Instance.SelectionManager.RemoveFromGoodTarget(tileHolder.gameObject);
                }
            }
            GameManager.Instance.SelectionManager.RemoveFromBadTarget(centerTileHolder.gameObject);
        }
    }
}