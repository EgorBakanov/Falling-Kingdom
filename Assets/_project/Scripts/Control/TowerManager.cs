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
        [Range(.1f, 3f)] [SerializeField] private float timeOnTowerCreate = .4f;

        private List<TowerHolder> _currentTowers;
        private TowerHolder _currentTargetTower;

        public TowerPreset[] AvailableToBuildTowers => GameManager.Instance.GetCurrentLevel().AvailableToBuildTowers;
        public Level.TowerInPosition[] InitialTowers => GameManager.Instance.GetCurrentLevel().OtherInitialTowers;
        public Level.TowerInPosition TargetTower => GameManager.Instance.GetCurrentLevel().TargetTower;
        public TowerHolder TowerPrefab => towerPrefab;
        public IEnumerable<TowerHolder> CurrentTowers => _currentTowers;
        public TowerHolder CurrentTargetTower => _currentTargetTower;

        private void Awake() => _currentTowers = new List<TowerHolder>();
        private void OnDestroy() => Clear();

        public IEnumerator CreateSelectedTower()
        {
            var tile = GameManager.Instance.GridHolder.TileHolders[
                GameManager.Instance.SelectionManager.SelectedTile.Index];
            var preset = AvailableToBuildTowers[GameManager.Instance.SelectionManager.SelectedTowerPresetId];
            var wait = new WaitForSeconds(timeOnTowerCreate);

            yield return CreateTower(preset, tile, wait);
        }

        public IEnumerator CreateInitialTowers()
        {
            var grid = GameManager.Instance.GridHolder.Grid;
            var wait = new WaitForSeconds(timeOnTowerCreate);

            var index = grid.CoordinateToIndex(TargetTower.Position);
            var tileHolder = GameManager.Instance.GridHolder.TileHolders[index];

            yield return CreateTargetTower(TargetTower.Preset, tileHolder, wait);

            foreach (var tower in InitialTowers)
            {
                index = grid.CoordinateToIndex(tower.Position);
                tileHolder = GameManager.Instance.GridHolder.TileHolders[index];

                yield return CreateTower(tower.Preset, tileHolder, wait);
            }
        }

        public IEnumerator CreateTower(TowerPreset preset, TileHolder tile, YieldInstruction wait = null)
        {
            var tower = new Tower(preset, tile.Tile);
            var obj = Instantiate(TowerPrefab);
            obj.SetVisual(preset.VisualPrefab);
            obj.Init(tower, tile);
            _currentTowers.Add(obj);

            yield return wait;
        }

        public IEnumerator CreateTargetTower(TowerPreset preset, TileHolder tile, YieldInstruction wait = null)
        {
            var tower = new Tower(preset, tile.Tile);
            var obj = Instantiate(TowerPrefab);
            obj.SetVisual(preset.VisualPrefab);
            obj.Init(tower, tile);
            _currentTowers.Add(obj);
            _currentTargetTower = obj;

            yield return wait;
        }

        public void Clear()
        {
            foreach (var tower in CurrentTowers)
            {
                if (tower != null)
                    Destroy(tower.gameObject);
            }

            _currentTowers.Clear();
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

        public void DestroyTower(Tower tower)
        {
            var towerHolder = _currentTowers.Find((holder) => holder.GridObject == tower);
            if (towerHolder != null)
                DestroyTower(towerHolder);
        }

        public void ReplaceTower(TowerHolder tower, TowerPreset preset)
        {
            tower.Replace(preset);
        }

        public void ReplaceTower(Tower tower, TowerPreset preset)
        {
            var towerHolder = _currentTowers.Find((holder) => holder.GridObject == tower);
            if (towerHolder != null)
                ReplaceTower(towerHolder, preset);
        }

        public void ActivateAll()
        {
            foreach (var tower in CurrentTowers)
            {
                tower.GridObject.IsActive = true;
            }
        }

        public IEnumerator PerformBeginTurnActions()
        {
            int n = _currentTowers.Count;

            for (var i = 0; i < n; i++)
            {
                var towerHolder = _currentTowers[i];
                var tower = towerHolder.GridObject;
                var action = tower.BeginPlayerTurnAction;
                if (action != null)
                {
                    GameManager.Instance.SelectionManager.SelectedTower = tower;
                    yield return action.Execute();
                }

                // workaround if one tower killed another
                if (n == _currentTowers.Count) continue;
                n = _currentTowers.Count;
                if (towerHolder != _currentTowers[i])
                    i--;
            }
        }
        
        public IEnumerator PerformEndTurnActions()
        {
            int n = _currentTowers.Count;

            for (var i = 0; i < n; i++)
            {
                var towerHolder = _currentTowers[i];
                var tower = towerHolder.GridObject;
                var action = tower.EndPlayerTurnAction;
                if (action != null)
                {
                    GameManager.Instance.SelectionManager.SelectedTower = tower;
                    yield return action.Execute();
                }

                // workaround if one tower killed another
                if (n == _currentTowers.Count) continue;
                n = _currentTowers.Count;
                if (towerHolder != _currentTowers[i])
                    i--;
            }
        }
    }
}