using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;
using Grid = Nara.MFGJS2020.Core.Grid;

namespace Nara.MFGJS2020.Visual
{
    public class GridHolder : MonoBehaviour
    {
        private Grid _grid;
        private TileHolder[] _tiles;

        [SerializeField] private Level level;
        [Range(0f, 2f)] [SerializeField] private float spacing = 1f;
        [SerializeField] private TileHolder tilePrefab;

        private void Start()
        {
            if (tilePrefab == null)
                return;
            if (level == null)
                return;
            
            Init(level.Generate());
        }

        public void Init(Grid grid)
        {
            _grid = grid;
            _tiles = new TileHolder[_grid.Size];

            var start = this.transform.position;

            for (int i = 0; i < _grid.SizeX; i++)
            {
                for (int j = 0; j < _grid.SizeY; j++)
                {
                    _tiles[_grid.CoordinateToIndex(i,j)] = Instantiate<TileHolder>(tilePrefab, transform);
                    _tiles[_grid.CoordinateToIndex(i,j)].transform.position = start + new Vector3(i, 0, j) * spacing;
                    _tiles[_grid.CoordinateToIndex(i,j)].Init(_grid[i, j], level.TileColorScheme);
                }
            }
        }
    }
}