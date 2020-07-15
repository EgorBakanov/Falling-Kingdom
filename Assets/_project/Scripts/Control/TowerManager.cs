using System.Collections;
using System.Collections.Generic;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using Nara.MFGJS2020.GridObjects;
using Nara.MFGJS2020.Holders;
using UnityEngine;

namespace Nara.MFGJS2020.Control
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private TowerHolder towerPrefab;
        [Range(.1f, 3f)] [SerializeField] private float initialisationTime = .7f;

        private int _selectedPreset = -1;
        private List<TowerHolder> _currentTowers;
        private TowerHolder _currentTargetTower;

        public TowerPreset[] AvailableToBuildTowers => GameManager.Instance.GetCurrentLevel().AvailableToBuildTowers;
        public Level.TowerInPosition[] InitialTowers => GameManager.Instance.GetCurrentLevel().OtherInitialTowers;
        public Level.TowerInPosition TargetTower => GameManager.Instance.GetCurrentLevel().TargetTower;
        public TowerHolder TowerPrefab => towerPrefab;
        public IEnumerable<TowerHolder> CurrentTowers => _currentTowers;
        public TowerHolder CurrentTargetTower => _currentTargetTower;

        private void Awake()
        {
            _currentTowers = new List<TowerHolder>();
        }

        public void SelectPreset(int i)
        {
            _selectedPreset = Mathf.Clamp(i, 0, AvailableToBuildTowers.Length);
        }

        public IEnumerator CreateSelectedTower(Tile tile)
        {
            yield break;
        }

        public IEnumerator CreateInitialTowers()
        {
            var grid = GameManager.Instance.GridHolder.Grid;
            var wait = new WaitForSeconds(initialisationTime / (InitialTowers.Length + 1));
            
            var index = grid.CoordinateToIndex(TargetTower.Position);
            var tileHolder = GameManager.Instance.GridHolder.TileHolders[index];

            yield return CreateTargetTower(TargetTower.Preset,tileHolder, wait);

            foreach (var tower in InitialTowers)
            {
                index = grid.CoordinateToIndex(tower.Position);
                tileHolder = GameManager.Instance.GridHolder.TileHolders[index];

                yield return CreateTower(tower.Preset,tileHolder, wait);
            }
        }

        public IEnumerator CreateTower(TowerPreset preset, TileHolder tile, YieldInstruction wait = null)
        {
            var tower = new Tower(preset, tile.Tile);
            var obj = Instantiate(TowerPrefab);
            Instantiate(preset.VisualPrefab, obj.VisualRoot);
            obj.Init(tower, tile);
            _currentTowers.Add(obj);

            yield return wait;
        }

        public IEnumerator CreateTargetTower(TowerPreset preset, TileHolder tile, YieldInstruction wait = null)
        {
            var tower = new Tower(preset, tile.Tile);
            var obj = Instantiate(TowerPrefab);
            Instantiate(preset.VisualPrefab, obj.VisualRoot);
            obj.Init(tower, tile);
            _currentTowers.Add(obj);
            _currentTargetTower = obj;
            
            yield return wait;
        }

        public void Clear()
        {
            foreach (var tower in CurrentTowers)
            {
                Destroy(tower);
            }

            _currentTowers.Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }

        public void DestroyTower(TowerHolder towerHolder)
        {
            var isTarget = towerHolder == _currentTargetTower;
            _currentTowers.Remove(towerHolder);
            Destroy(towerHolder.gameObject);

            if (isTarget)
            {
                _currentTargetTower = null;
                GameManager.Instance.StateMachine.OnTargetTowerDestroyed();
            }
        }
    }
}