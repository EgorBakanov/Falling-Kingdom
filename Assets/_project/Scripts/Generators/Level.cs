using System;
using Nara.MFGJS2020.Utility;
using Grid = Nara.MFGJS2020.Core.Grid;
using UnityEngine;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Level")]
    public class Level : ScriptableObject
    {
        [Serializable]
        public class TowerInPosition
        {
            [SerializeField] private TowerPreset preset;
            [SerializeField] private Vector2Int position;

            public TowerPreset Preset => preset;

            public Vector2Int Position
            {
                get => position;
                set => position = value;
            }
        }

        [Range(1, 20)] [SerializeField] private int x = 2;
        [Range(1, 20)] [SerializeField] private int y = 2;
        [Range(1, 6)] [SerializeField] private int maxHeight = 4;

        [SerializeField] private Int2D initialHeights;
        [SerializeField] private TileColorScheme tileColorScheme;
        [SerializeField] private TowerPreset[] availableToBuildTowers;
        [SerializeField] private TowerInPosition targetTower;
        [SerializeField] private TowerInPosition[] otherInitialTowers;

        public TileColorScheme TileColorScheme => tileColorScheme;
        public TowerPreset[] AvailableToBuildTowers => availableToBuildTowers;
        public TowerInPosition TargetTower => targetTower;
        public TowerInPosition[] OtherInitialTowers => otherInitialTowers;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (initialHeights == null || initialHeights.x != x || initialHeights.y != y)
            {
                initialHeights = new Int2D(x, y);
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    initialHeights[i, j] = Mathf.Clamp(initialHeights[i, j], 0, maxHeight);
                }
            }

            if (tileColorScheme != null)
                if (tileColorScheme.Size != maxHeight)
                    Debug.Log("Tile Color Scheme size is not Equal to maximum tile height!");

            var tx = Mathf.Clamp(targetTower.Position.x, 0, x - 1);
            var ty = Mathf.Clamp(targetTower.Position.y, 0, y - 1);

            targetTower.Position = new Vector2Int(tx, ty);
            if (initialHeights[tx, ty] == 0)
            {
                Debug.Log($"Target tower <{targetTower.Preset.name}> will be destroyed on level start!");
            }

            for (int i = 0; i < OtherInitialTowers.Length; i++)
            {
                var tp = OtherInitialTowers[i];
                var _x = Mathf.Clamp(tp.Position.x, 0, x - 1);
                var _y = Mathf.Clamp(tp.Position.y, 0, y - 1);

                tp.Position = new Vector2Int(_x, _y);
                if (initialHeights[_x, _y] == 0)
                {
                    Debug.Log($"Tower <{tp.Preset.name}> will be destroyed on level start!");
                }
            }
        }
#endif

        public Grid GenerateGrid()
        {
            return new Grid(x, y, maxHeight, initialHeights);
        }
    }
}