using System;
using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    public class Tile
    {
        public int Index { get; }
        public Grid Grid { get; }
        
        public IGridObject GridObject
        {
            get => _gridObject;
            set
            {
                _gridObject = value;
                if (Height == 0)
                {
                    _gridObject?.OnTileFall();
                    OnTileFall?.Invoke();
                }
                else
                {
                    _gridObject?.OnTileHeightChanged(Height, Height);
                    OnTileHeightChanged?.Invoke(Height, Height);
                }
            }
        }

        public int Height
        {
            get => _height;
            set
            {
                int oldVal = _height;
                _height = Mathf.Clamp(value, 0, Grid.MaxHeight);
                if (oldVal != _height)
                {
                    if (_height == 0)
                    {
                        GridObject?.OnTileFall();
                        OnTileFall?.Invoke();
                    }
                    else
                    {
                        GridObject?.OnTileHeightChanged(_height, oldVal);
                        OnTileHeightChanged?.Invoke(_height, oldVal);
                    }
                }
            }
        }

        public event Action OnTileFall;
        public event Action<int, int> OnTileHeightChanged;

        private int _height;
        private IGridObject _gridObject;

        public Tile(Grid grid, int id, int h)
        {
            Grid = grid;
            Index = id;
            Height = h;
        }

        public override int GetHashCode()
        {
            return Index;
        }
    }
}