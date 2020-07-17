using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.Holders
{
    public abstract class GridObjectHolder<T> : MonoBehaviour where T : IGridObject
    {
        [SerializeField] private Transform visualRoot;
        private TileHolder _tileHolder;
        public virtual T GridObject { get; private set; }

        public TileHolder TileHolder
        {
            get => _tileHolder;
            protected set
            {
                if (_tileHolder != null)
                    UnsubscribeOnTile(_tileHolder.Tile);
                _tileHolder = value;
                SubscribeOnTile(_tileHolder.Tile);
            }
        }

        public Transform VisualRoot => visualRoot;

        public virtual void Init(T obj, TileHolder tileHolder)
        {
            GridObject = obj;
            TileHolder = tileHolder;
            SubscribeOnTile(TileHolder.Tile);
            TileHolder.Tile.GridObject = GridObject;
        }

        protected void SubscribeOnTile(Tile tile)
        {
            tile.OnTileFall += OnTileFall;
            tile.OnTileHeightChanged += SetPosition;
            tile.OnTileHeightChanged += OnTileHeightChanged;
        }

        protected void UnsubscribeOnTile(Tile tile)
        {
            tile.OnTileFall -= OnTileFall;
            tile.OnTileHeightChanged -= SetPosition;
            tile.OnTileHeightChanged -= OnTileHeightChanged;
        }

        protected abstract void OnTileHeightChanged(int newHeight, int oldHeight);
        protected abstract void OnTileFall();

        private void SetPosition(int newHeight, int _)
        {
            this.transform.position = GetPlacementPosition(TileHolder.Tile);
        }

        protected Vector3 GetPlacementPosition(Tile tile)
        {
            var tileHolder = GameManager.Instance.GridHolder.TileHolders[tile.Index];
            return tileHolder.transform.position + Vector3.up * tileHolder.Tile.Height / tileHolder.Tile.Grid.MaxHeight;
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeOnTile(TileHolder.Tile);
        }
    }
}