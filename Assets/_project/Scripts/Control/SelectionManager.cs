using System;
using System.Collections;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;
using UnityEngine;
using UnityFx.Outline;

namespace Nara.MFGJS2020.Control
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private OutlineLayerCollection outlineLayers;
        [SerializeField] private int buildZoneId;
        [SerializeField] private int cantBuildZoneId;
        [SerializeField] private int targetId;
        [SerializeField] private int enemyTargetId;

        public Tile SelectedTile { get; set; }
        public int SelectedTowerPresetId { get; set; } = -1;
        public Tower SelectedTower { get; set; }
        
        public void DeselectAll()
        {
            SelectedTile = null;
            SelectedTower = null;
            SelectedTowerPresetId = -1;
            foreach (var layer in outlineLayers)
            {
                layer.Clear();
            }
        }

        public void AddToEnemyTarget(GameObject go) => outlineLayers[enemyTargetId].Add(go);
        public void RemoveFromEnemyTarget(GameObject go) => outlineLayers[enemyTargetId].Remove(go);

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (var i = 0; i < outlineLayers.Count; i++)
            {
                var layer = outlineLayers[i];
                
                if (layer.Name.Equals("BuildZone"))
                {
                    buildZoneId = i;
                    continue;
                }
                
                if (layer.Name.Equals("CantBuildZone"))
                {
                    cantBuildZoneId = i;
                    continue;
                }
                
                if (layer.Name.Equals("Target"))
                {
                    targetId = i;
                    continue;
                }
                
                if (layer.Name.Equals("EnemyTarget"))
                {
                    enemyTargetId = i;
                    continue;
                }
            }
        }
#endif
    }
}