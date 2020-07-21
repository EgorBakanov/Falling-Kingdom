using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;
using Nara.MFGJS2020.Holders;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityFx.Outline;

namespace Nara.MFGJS2020.Control
{
    public class SelectionManager : MonoBehaviour
    {
        public enum TileSelectionType
        {
            None,
            TowerBuy,
            TowerAction
        }
        
        [SerializeField] private OutlineLayerCollection outlineLayers;
        [SerializeField] private int goodTargetId;
        [SerializeField] private int badTargetId;
        [SerializeField] private int enemyTargetId;

        public Tile SelectedTile { get; set; }
        public int SelectedTowerPresetId { get; set; } = -1;
        public Tower SelectedTower { get; set; }
        public int SelectedTowerActionId { get; set; } = -1;
        public bool WaitForPlayerSelection { get; set; } = false;

        public TileSelectionType TileSelection { get; set; } = TileSelectionType.None;
        
        public void DeselectAll()
        {
            SelectedTile = null;
            SelectedTower = null;
            SelectedTowerPresetId = -1;
            SelectedTowerActionId = -1;
            TileSelection = TileSelectionType.None;
            foreach (var layer in outlineLayers)
            {
                layer.Clear();
            }
        }

        public void AddToEnemyTarget(GameObject go) => outlineLayers[enemyTargetId].Add(go);
        public void RemoveFromEnemyTarget(GameObject go) => outlineLayers[enemyTargetId].Remove(go);
        public void AddToGoodTarget(GameObject go) => outlineLayers[goodTargetId].Add(go);
        public void RemoveFromGoodTarget(GameObject go) => outlineLayers[goodTargetId].Remove(go);
        public void AddToBadTarget(GameObject go) => outlineLayers[badTargetId].Add(go);
        public void RemoveFromBadTarget(GameObject go) => outlineLayers[badTargetId].Remove(go);

        public void AddTile(TileHolder tile)
        {
            int id = 0;
            switch (TileSelection)
            {
                case TileSelectionType.None:
                    return;
                case TileSelectionType.TowerBuy:
                {
                    id = tile.Tile.Grid.BuildZone.Contains(tile.Tile) ? goodTargetId : badTargetId;
                    break;
                }
                case TileSelectionType.TowerAction:
                {
                    var action = SelectedTower.ActiveActions[SelectedTowerActionId] as TargetAction;
                    if(action == null)
                        return;
                    id = action.CorrectTarget(tile.Tile) ? goodTargetId : badTargetId;
                    break;
                }
            }
            
            outlineLayers[id].Add(tile.gameObject);
        }

        public void RemoveTile(TileHolder tile)
        {
            outlineLayers[goodTargetId].Remove(tile.gameObject);
            outlineLayers[badTargetId].Remove(tile.gameObject);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (var i = 0; i < outlineLayers.Count; i++)
            {
                var layer = outlineLayers[i];
                
                if (layer.Name.Equals("GoodTarget"))
                {
                    goodTargetId = i;
                    continue;
                }
                
                if (layer.Name.Equals("BadTarget"))
                {
                    badTargetId = i;
                    continue;
                }
                
                if (layer.Name.Equals("EnemyTarget"))
                {
                    enemyTargetId = i;
                }
            }
        }
#endif
    }
}