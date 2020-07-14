using System.Collections.Generic;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;
using Grid = Nara.MFGJS2020.Core.Grid;
using UnityEngine.InputSystem;

namespace Nara.MFGJS2020.Holders
{
    public class GridHolder : MonoBehaviour
    {
        private Grid _grid;

        [SerializeField] private Level level;
        [Range(0f, 2f)] [SerializeField] private float spacing = 1f;
        [SerializeField] private TileHolder tilePrefab;

        public GridObjectHolder gridObjectHolder;
        public Vector2Int from, to;
        private IEnumerable<Tile> _path;

        public TileHolder[] TileHolders { get; private set; }

        private void Start()
        {
            if (tilePrefab == null)
                return;
            if (level == null)
                return;

            Init(level.Generate());
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                _path = GridUtility.FindPath(_grid[_grid.CoordinateToIndex(from)], _grid[_grid.CoordinateToIndex(to)]);
            }
        }

        public void Init(Grid grid)
        {
            _grid = grid;
            TileHolders = new TileHolder[_grid.Size];

            var start = this.transform.position;

            for (int i = 0; i < _grid.SizeX; i++)
            {
                for (int j = 0; j < _grid.SizeY; j++)
                {
                    TileHolders[_grid.CoordinateToIndex(i, j)] = Instantiate<TileHolder>(tilePrefab, transform);
                    TileHolders[_grid.CoordinateToIndex(i, j)].transform.position = start + new Vector3(i, 0, j) * spacing;
                    TileHolders[_grid.CoordinateToIndex(i, j)].Init(_grid[i, j], this, level.TileColorScheme);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_path == null)
                return;

            Gizmos.color = Color.blue;
            foreach (var tile in _path)
            {
                if (tile.Index == _grid.CoordinateToIndex(from) || tile.Index == _grid.CoordinateToIndex(to))
                    continue;

                var pos = TileHolders[tile.Index].transform.position + Vector3.up * 2;
                Gizmos.DrawSphere(pos, .3f);
                ;
            }

            Gizmos.color = Color.green;
            var posFrom = TileHolders[_grid.CoordinateToIndex(from)].transform.position + Vector3.up * 2;
            Gizmos.DrawSphere(posFrom, .5f);

            Gizmos.color = Color.red;
            var posTo = TileHolders[_grid.CoordinateToIndex(to)].transform.position + Vector3.up * 2;
            Gizmos.DrawSphere(posTo, .5f);
        }
#endif
    }
}