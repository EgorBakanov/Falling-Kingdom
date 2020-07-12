using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    [System.Serializable]
    public class Grid
    {
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int MaxHeight { get; private set; }
        public int Size => SizeX * SizeY;

        private Tile[] _tiles;

        public Tile this[int index]
        {
            get
            {
                if (index < 0 || index > SizeX * SizeY)
                    return null;

                return _tiles[index];
            }
        }

        public Tile this[int x, int y]
        {
            get
            {
                if (x < 0 || x > SizeX)
                    return null;

                if (y < 0 || y > SizeY)
                    return null;

                return _tiles[y + x * SizeY];
            }
        }

        public (int x, int y) IndexToCoordinate(int index)
        {
            if (index >= SizeX * SizeY)
            {
                index %= SizeX * SizeY;
            }
            else if (index < 0)
            {
                index = 0;
            }

            int x = index / SizeY;
            int y = index % SizeY;

            return (x, y);
        }

        public int CoordinateToIndex(int x, int y)
        {
            x = Mathf.Clamp(x, 0, SizeX-1);
            y = Mathf.Clamp(y, 0, SizeY - 1);
            return x * SizeY + y;
        }

        public int CoordinateToIndex(Vector2Int coordinate)
        {
            return CoordinateToIndex(coordinate.x, coordinate.y);
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