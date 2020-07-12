﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public Vector2Int from, to;
        private IEnumerable<Tile> path;

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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                path = GridPathFinder.Find(_grid[_grid.CoordinateToIndex(from)], _grid[_grid.CoordinateToIndex(to)]);
                Debug.Log((path == null) ? "Empty" : path.Count().ToString());
            }
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
                    _tiles[_grid.CoordinateToIndex(i, j)] = Instantiate<TileHolder>(tilePrefab, transform);
                    _tiles[_grid.CoordinateToIndex(i, j)].transform.position = start + new Vector3(i, 0, j) * spacing;
                    _tiles[_grid.CoordinateToIndex(i, j)].Init(_grid[i, j], level.TileColorScheme);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (path == null)
                return;

            Gizmos.color = Color.blue;
            foreach (var tile in path)
            {
                if(tile.Index == _grid.CoordinateToIndex(from) || tile.Index == _grid.CoordinateToIndex(to))
                    continue;

                var pos = _tiles[tile.Index].transform.position + Vector3.up * 2;
                Gizmos.DrawSphere(pos,.3f);
                ;
            }
            
            Gizmos.color = Color.green;
            var posFrom = _tiles[_grid.CoordinateToIndex(from)].transform.position + Vector3.up * 2;
            Gizmos.DrawSphere(posFrom,.5f);
            
            Gizmos.color = Color.red;
            var posTo = _tiles[_grid.CoordinateToIndex(to)].transform.position + Vector3.up * 2;
            Gizmos.DrawSphere(posTo,.5f);
        }
    }
}