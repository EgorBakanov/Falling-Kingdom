using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nara.MFGJS2020
{
    [System.Serializable]
    public class Grid
    {
        public class Cell
        {
            public int Index { get; }
            public Grid Grid { get; }
            public int Height
            {
                get => _height;
                set => _height = Mathf.Clamp(value, 0, Grid.MaxHeight);
            }

            private int _height;

            public Cell(Grid grid, int id, int h)
            {
                Grid = grid;
                Index = id;
                Height = h;
            }
        }

        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int MaxHeight { get; private set; }

        private Cell[] _cells;

        public Cell this[int index]
        {
            get
            {
                if (index < 0 || index > SizeX * SizeY)
                    return null;

                return _cells[index];
            }
        }

        public Cell this[int x, int y]
        {
            get
            {
                if (x < 0 || x > SizeX)
                    return null;

                if (y < 0 || y > SizeY)
                    return null;

                return _cells[y + x * SizeY];
            }
        }
        
        public Grid(int x, int y, int maxH, int[] heights )
        {
            SizeX = Mathf.Max(x, 1);
            SizeY = Mathf.Max(y, 1);
            MaxHeight = Mathf.Max(maxH, 1);

            _cells = new Cell[SizeX * SizeY];

            for (int i = 0; i < SizeX*SizeY; i++)
            {
                _cells[i] = new Cell(this,i,heights[i]);
            }
        }
        
        public Grid(int x, int y, int maxH) : this(x,y,maxH,new int[x*y]) {}

        public Grid(int x, int y) : this(x,y,5,new int[x*y]) {}
    }
}