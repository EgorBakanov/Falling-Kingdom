using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.TowerActions
{
    [UnityEngine.CreateAssetMenu(fileName = "New Destroy Action", menuName = "MFGJS2020/Tower Actions/Destroy")]
    public class DestroyAction : NonTargetAction
    {
        public override IEnumerator Execute()
        {
            var tower = GameManager.Instance.SelectionManager.SelectedTower;
            var tile = GameManager.Instance.GridHolder.TileHolders[tower.Tile.Index];
            GameManager.Instance.SelectionManager.AddToEnemyTarget(tile.gameObject);
            yield return new WaitForSeconds(animationTime);
            tower.Die();
            GameManager.Instance.SelectionManager.RemoveFromEnemyTarget(tile.gameObject);
        }
    }
}