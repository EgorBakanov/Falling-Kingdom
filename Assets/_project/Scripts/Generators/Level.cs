using System;
using DG.Tweening;
using Nara.MFGJS2020.Utility;
using Grid = Nara.MFGJS2020.Core.Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Level", menuName = "MFGJS2020/Level")]
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

        [Serializable]
        public class EnemySpawnerInPosition
        {
            [SerializeField] private EnemyPreset preset;
            [SerializeField] private Vector2Int position;

            public EnemyPreset Preset => preset;

            public Vector2Int Position
            {
                get => position;
                set => position = value;
            }
        }

        [Serializable]
        public class EnemySpawnerScheduleElement
        {
            [SerializeField] private EnemySpawnerInPosition[] schedule;

            public EnemySpawnerInPosition[] Schedule
            {
                get => schedule;
                set => schedule = value;
            }
        }

        [SerializeField] private string title;
        [SerializeField] private int initialMoney = 0;
        [Range(1, 30)] [SerializeField] private int turnsToSurvive = 1;
        [Range(1, 50)] [SerializeField] private int x = 2;
        [Range(1, 50)] [SerializeField] private int y = 2;
        [Range(1, 6)] [SerializeField] private int maxHeight = 5;

        [SerializeField] private Int2D initialHeights;
        [SerializeField] private LevelColorScheme levelColorScheme;
        [TextArea][SerializeField] private string introduction;
        
        [SerializeField] private TowerPreset[] availableToBuildTowers;
        [SerializeField] private TowerInPosition targetTower;
        [SerializeField] private TowerInPosition[] otherInitialTowers;
        [SerializeField] private EnemySpawnerScheduleElement[] enemySpawnerSchedule;

        public LevelColorScheme LevelColorScheme => levelColorScheme;
        public TowerPreset[] AvailableToBuildTowers => availableToBuildTowers;
        public TowerInPosition TargetTower => targetTower;
        public TowerInPosition[] OtherInitialTowers => otherInitialTowers;
        public int InitialMoney => initialMoney;
        public int TurnsToSurvive => turnsToSurvive;
        public EnemySpawnerScheduleElement[] EnemySpawnerSchedule => enemySpawnerSchedule;
        public string Introduction => introduction;
        public string Title => title;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (initialHeights == null)
            {
                initialHeights = new Int2D(x, y);
            }

            if (initialHeights.x != x || initialHeights.y != y)
            {
                Array.Resize(ref initialHeights.m,x*y);
                initialHeights.x = x;
                initialHeights.y = y;
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    initialHeights[i, j] = Mathf.Clamp(initialHeights[i, j], 0, maxHeight);
                }
            }

            if (levelColorScheme != null)
                if (levelColorScheme.Size != maxHeight)
                    Debug.Log("Tile Color Scheme size is not Equal to maximum tile height!");

            var tx = Mathf.Clamp(targetTower.Position.x, 0, x - 1);
            var ty = Mathf.Clamp(targetTower.Position.y, 0, y - 1);

            targetTower.Position = new Vector2Int(tx, ty);
            if (initialHeights[tx, ty] == 0 && targetTower.Preset != null)
            {
                Debug.Log($"Target tower <{targetTower.Preset.name}> will be destroyed on level start!");
            }

            for (int i = 0; i < OtherInitialTowers.Length; i++)
            {
                var tp = OtherInitialTowers[i];
                var _x = Mathf.Clamp(tp.Position.x, 0, x - 1);
                var _y = Mathf.Clamp(tp.Position.y, 0, y - 1);

                tp.Position = new Vector2Int(_x, _y);
                if (initialHeights[_x, _y] == 0&& tp.Preset != null)
                {
                    Debug.Log($"Tower <{tp.Preset.name}> will be destroyed on level start!");
                }
            }

            if (enemySpawnerSchedule == null)
            {
                enemySpawnerSchedule = new EnemySpawnerScheduleElement[turnsToSurvive];
            }

            if (enemySpawnerSchedule.Length != turnsToSurvive)
            {
                Array.Resize(ref enemySpawnerSchedule,turnsToSurvive);
            }

            for (int i = 0; i < enemySpawnerSchedule.Length; i++)
            {
                var se = enemySpawnerSchedule[i];
                if(se == null)
                    continue;
                if (se.Schedule == null)
                    continue;

                for (int j = 0; j < se.Schedule.Length; j++)
                {
                    var spawer = se.Schedule[j];

                    var _x = Mathf.Clamp(spawer.Position.x, 0, x - 1);
                    var _y = Mathf.Clamp(spawer.Position.y, 0, y - 1);

                    spawer.Position = new Vector2Int(_x, _y);
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