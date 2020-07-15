using System;
using System.Collections;
using System.Collections.Generic;
using Nara.MFGJS2020.Generators;
using Nara.MFGJS2020.GridObjects;
using Nara.MFGJS2020.Holders;
using UnityEngine;

namespace Nara.MFGJS2020.Control
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyHolder enemyPrefab;
        [SerializeField] private EnemySpawnerHolder spawnerPrefab;
        [Range(.1f, 3f)] [SerializeField] private float timeOnSpawnerCreate = .4f;
        [Range(.1f, 3f)] [SerializeField] private float timeOnSpawnerOpen = .4f;

        private List<EnemyHolder> _currentEnemies;
        private List<EnemySpawnerHolder> _currentSpawners;

        public IEnumerable<EnemyHolder> CurrentEnemies => _currentEnemies;
        public IEnumerable<EnemySpawnerHolder> CurrentSpawners => _currentSpawners;

        private void Awake()
        {
            _currentEnemies = new List<EnemyHolder>();
            _currentSpawners = new List<EnemySpawnerHolder>();
        }

        public void Clear()
        {
            foreach (var enemyHolder in CurrentEnemies)
            {
                if (enemyHolder != null)
                    Destroy(enemyHolder.gameObject);
            }

            foreach (var spawnerHolder in CurrentSpawners)
            {
                if (spawnerHolder != null)
                    Destroy(spawnerHolder.gameObject);
            }

            _currentEnemies.Clear();
            _currentSpawners.Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }

        public IEnumerator CreateSpawnerWave(int turn)
        {
            var grid = GameManager.Instance.GridHolder.Grid;
            var wait = new WaitForSeconds(timeOnSpawnerCreate);
            var schedule = GameManager.Instance.GetCurrentLevel().EnemySpawnerSchedule[turn].Schedule;

            for (int i = 0; i < schedule.Length; i++)
            {
                var index = grid.CoordinateToIndex(schedule[i].Position);
                var tileHolder = GameManager.Instance.GridHolder.TileHolders[index];

                if (tileHolder.Tile.Height > 0 && tileHolder.Tile.GridObject == null)
                {
                    yield return CreateSpawner(schedule[i].Preset, tileHolder, wait);
                }
            }
        }

        public IEnumerator CreateSpawner(EnemyPreset preset, TileHolder tile, YieldInstruction wait = null)
        {
            var spawner = new EnemySpawner(preset, tile.Tile);
            var obj = Instantiate(spawnerPrefab);
            Instantiate(preset.SpawnerVisualPrefab, obj.VisualRoot);
            obj.Init(spawner, tile);
            _currentSpawners.Add(obj);
            yield return wait;
        }

        public IEnumerator OpenAllSpawners()
        {
            var wait = new WaitForSeconds(timeOnSpawnerOpen);

            for (int i = _currentSpawners.Count - 1; i >= 0; i--)
            {
                var spawner = _currentSpawners[i];
                var preset = spawner.GridObject.Preset;
                var tile = spawner.TileHolder;
                DestroySpawner(spawner);
                yield return CreateEnemy(preset, tile, wait);
            }
        }

        private IEnumerator CreateEnemy(EnemyPreset preset, TileHolder tile, WaitForSeconds wait)
        {
            var enemy = new Enemy(preset, tile.Tile);
            var obj = Instantiate(enemyPrefab);
            Instantiate(preset.VisualPrefab, obj.VisualRoot);
            obj.Init(enemy, tile);
            _currentEnemies.Add(obj);
            yield return wait;
        }

        public void DestroyEnemy(EnemyHolder enemyHolder)
        {
            _currentEnemies.Remove(enemyHolder);
            Destroy(enemyHolder.gameObject);
        }

        public void DestroySpawner(EnemySpawnerHolder enemySpawnerHolder)
        {
            _currentSpawners.Remove(enemySpawnerHolder);
            Destroy(enemySpawnerHolder.gameObject);
        }

        public void ActivateAll()
        {
            foreach (var enemy in CurrentEnemies)
            {
                enemy.GridObject.IsActive = true;
            }
        }
    }
}