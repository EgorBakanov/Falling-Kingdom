using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    public class Grid
    {
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int MaxHeight { get; private set; }
        public int Size => SizeX * SizeY;

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
        }

        public Grid(int x, int y, int maxH) : this(x, y, maxH, new int[x * y])
        {
        }

        public Grid(int x, int y) : this(x, y, 5, new int[x * y])
        {
        }
    }
}