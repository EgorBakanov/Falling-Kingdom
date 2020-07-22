using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nara.MFGJS2020.Core;
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
        [Range(.1f, 3f)] [SerializeField] private float timeOnCalculateMove = .5f;
        [Range(.1f, 1f)] [SerializeField] private float timeToMove = .3f;
        [Range(.1f, 1f)] [SerializeField] private float timeToAttack = .3f;
        [Range(0f, 2f)] [SerializeField] private float moveJumpPower = .5f;
        [Range(0f, 2f)] [SerializeField] private float attackPower = 1f;
        [Range(0f, 1f)] [SerializeField] private float attackElasticity = 1f;
        [Range(0, 20)] [SerializeField] private int attackVibrato = 5;
        [SerializeField] private Vector3 attackOffset = Vector3.up;

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
            obj.SetVisual(preset.SpawnerVisualPrefab);
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
            obj.SetVisual(preset.VisualPrefab);
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

        public IEnumerator CalculateNextMoves(IGridObject target)
        {
            var targetTile = target.Tile;
            var wait = new WaitForSeconds(timeOnCalculateMove);
            yield return wait;
            foreach (var enemyHolder in CurrentEnemies)
            {
                var enemy = enemyHolder.GridObject;
                var currentTile = enemy.Tile;
                var path = GridUtility.FindPath(currentTile, targetTile);

                enemy.MoveIntention = path?.First();

                enemy.MoveIntention = enemy.MoveIntention ?? GridUtility.FindFallbackMove(currentTile);

                if (enemy.MoveIntention == null) continue;
                var targetHolder = enemyHolder.TileHolder.GridHolder.TileHolders[enemy.MoveIntention.Index];
                GameManager.Instance.SelectionManager.AddToEnemyTarget(targetHolder.gameObject);
                yield return wait;
            }
            GameManager.Instance.SelectionManager.DeselectAll();
        }

        public IEnumerator PerformNextMoves()
        {
            var wait = new WaitForSeconds(timeOnSpawnerCreate);
            int n = _currentEnemies.Count;
            for (int i = 0; i < n ; i++)
            {
                var enemyHolder = _currentEnemies[i];
                var target = enemyHolder.GridObject.MoveIntention;

                if (target == null || !enemyHolder.GridObject.IsActive)
                    continue;

                var targetHolder = enemyHolder.TileHolder.GridHolder.TileHolders[target.Index];
                GameManager.Instance.SelectionManager.AddToEnemyTarget(targetHolder.gameObject);
                yield return wait;
                
                if (target.GridObject != null || target.Height - enemyHolder.TileHolder.Tile.Height > 1)
                {
                    yield return enemyHolder.Attack(targetHolder, attackOffset, timeToAttack,attackPower,attackElasticity,attackVibrato);
                }
                else
                {
                    yield return enemyHolder.Move(targetHolder,moveJumpPower,timeToMove);
                }

                enemyHolder.GridObject.IsActive = false;
                
                GameManager.Instance.SelectionManager.RemoveFromEnemyTarget(targetHolder.gameObject);
                
                // workaround if one enemy killed another
                if (n == _currentEnemies.Count) continue;
                n = _currentEnemies.Count;
                i--;
            }
        }
    }
}