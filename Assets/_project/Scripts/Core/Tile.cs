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
                int newVal = Mathf.Clamp(value, 0, Grid.MaxHeight);
                if (newVal != _height)
                {
                    if (newVal == 0)
                    {
                        GridObject?.OnTileFall();
                        OnTileFall?.Invoke();
                    }
                    else
                    {
                        GridObject?.OnTileHeightChanged(newVal, _height);
                        OnTileHeightChanged?.Invoke(newVal, _height);
                    }

                    _height = newVal;
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