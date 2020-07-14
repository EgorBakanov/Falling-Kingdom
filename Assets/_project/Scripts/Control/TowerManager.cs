using System.Collections;
using System.Collections.Generic;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using Nara.MFGJS2020.GridObject;
using Nara.MFGJS2020.Holders;
using UnityEngine;

namespace Nara.MFGJS2020.Control
{
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private TowerHolder towerPrefab;
        [Range(.1f, 3f)] [SerializeField] private float initialisationTime = .7f;

        private int selectedPreset = -1;
        private List<TowerHolder> towers;

        public TowerPreset[] AvailableToBuildTowers => GameManager.Instance.GetCurrentLevel().AvailableToBuildTowers;
        public Level.TowerInPosition[] InitialTowers => GameManager.Instance.GetCurrentLevel().InitialTowers;
        public TowerHolder TowerPrefab => towerPrefab;
        public IEnumerable<TowerHolder> Towers => towers;

        private void Awake()
        {
            towers = new List<TowerHolder>();
        }

        public void SelectPreset(int i)
        {
            selectedPreset = Mathf.Clamp(i, 0, AvailableToBuildTowers.Length);
        }

        public IEnumerator CreateSelectedTower(Tile tile)
        {
            yield break;
        }

        public IEnumerator CreateInitialTowers()
        {
            var grid = GameManager.Instance.GridHolder.Grid;
            var wait = new WaitForSeconds(initialisationTime / grid.SizeX);

            foreach (var tower in InitialTowers)
            {
                var index = grid.CoordinateToIndex(tower.Position);
                var tileHolder = GameManager.Instance.GridHolder.TileHolders[index];

                CreateTower(tower.Preset,tileHolder);

                yield return wait;
            }
        }

        public void CreateTower(TowerPreset preset, TileHolder tile)
        {
            var tower = new Tower(preset, tile.Tile);
            var obj = Instantiate(TowerPrefab);
            Instantiate(preset.VisualPrefab, obj.VisualRoot);
            obj.Init(tower, tile);
            towers.Add(obj);
        }

        public void Clear()
        {
            foreach (var tower in Towers)
            {
                Destroy(tower);
            }

            towers.Clear();
        }

        private void OnDestroy()
        {
            Clear();
        }

        public void DestroyTower(TowerHolder towerHolder)
        {
            towers.Remove(towerHolder);
            Destroy(towerHolder.gameObject);
        }
    }
}