using System.Collections.Generic;
using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    public class Grid
    {
        public int SizeX { get; }
        public int SizeY { get; }
        public int MaxHeight { get; }
        public int Size => SizeX * SizeY;
        public HashSet<Tile> BuildZone { get; }
        public HashSet<Tile> CantBuildZone { get; }

        private Tile[] _tiles;

        public Tile this[int index] => IsCorrectIndex(index) ? _tiles[index] : null;

        public Tile this[int x, int y] => IsCorrectCoordinate(x, y) ? _tiles[y + x * SizeY] : null;

        public Tile this[Vector2Int coordinate] => this[CoordinateToIndex(coordinate)];

        public (int x, int y) IndexToCoordinate(int index)
        {
            int x = index / SizeY;
            int y = index % SizeY;

            return (x, y);
        }

        public int CoordinateToIndex(int x, int y)
        {
            return x * SizeY + y;
        }

        public int CoordinateToIndex(Vector2Int coordinate)
        {
            return CoordinateToIndex(coordinate.x, coordinate.y);
        }

        public bool IsCorrectIndex(int index)
        {
            return (index >= 0 && index < Size);
        }

        public bool IsCorrectCoordinate(int x, int y)
        {
            return (x >= 0 && x < SizeX && y >= 0 && y < SizeY);
        }

        public bool IsCorrectCoordinate(Vector2Int coordinate)
        {
            return IsCorrectCoordinate(coordinate.x, coordinate.y);
        }

        public Grid(int x, int y, int maxH, int[] heights)
        {
            SizeX = Mathf.Max(x, 1);
            SizeY = Mathf.Max(y, 1);
            MaxHeight = Mathf.Max(maxH, 1);

            _tiles = new Tile[SizeX * SizeY];

            for (int i = 0; i < SizeX * SizeY; i++)
            {
                _tiles[i] = new Tile(this, i, heights[i]);
            }
            BuildZone = new HashSet<Tile>();
            CantBuildZone = new HashSet<Tile>();
        }

        public Grid(int x, int y, int maxH) : this(x, y, maxH, new int[x * y])
        {
        }

        public Grid(int x, int y) : this(x, y, 5, new int[x * y])
        {
        }

        public void RecalculateZones()
        {
            BuildZone.Clear();
            CantBuildZone.Clear();

            foreach (var tile in _tiles)
            {
                var obj = tile.GridObject;
                if(obj == null)
                    continue;

                var build = GridUtility.SquaredZone(tile, obj.ExpandBuildZoneSize);
                var cantBuild = GridUtility.SquaredZone(tile, obj.CantBuildZoneSize);

                BuildZone.UnionWith(build);
                CantBuildZone.UnionWith(cantBuild);
            }

            BuildZone.ExceptWith(CantBuildZone);
        }
    }
}