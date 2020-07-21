using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;

namespace Nara.MFGJS2020.TowerActions
{
    [CreateAssetMenu(fileName = "New Replace Tower Action", menuName = "MFGJS2020/Tower Actions/Replace Tower")]
    public class ReplaceTowerAction : NonTargetAction
    {
        [SerializeField] private TowerPreset preset;
        
        public override IEnumerator Execute()
        {
            var tower = GameManager.Instance.SelectionManager.SelectedTower;
            var tile = GameManager.Instance.GridHolder.TileHolders[tower.Tile.Index];
            GameManager.Instance.SelectionManager.AddToGoodTarget(tile.gameObject);
            yield return new WaitForSeconds(animationTime);
            GameManager.Instance.TowerManager.ReplaceTower(tower,preset);
            GameManager.Instance.SelectionManager.RemoveFromGoodTarget(tile.gameObject);
        }
    }
}