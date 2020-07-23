using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.Holders
{
    public abstract class GridObjectHolder<T> : MonoBehaviour where T : IGridObject
    {
        [SerializeField] protected Transform visualRoot;
        private GameObject visual;
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

        public virtual void Init(T obj, TileHolder tileHolder)
        {
            GridObject = obj;
            TileHolder = tileHolder;
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

        private void SetPosition(int newHeight, int oldHeight)
        {
            this.transform.position = GetPlacementPosition(TileHolder);
        }

        protected static Vector3 GetPlacementPosition(TileHolder tile)
        {
            return tile.transform.position + Vector3.up * tile.Tile.Height / tile.Tile.Grid.MaxHeight;
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeOnTile(TileHolder.Tile);
        }

        public void SetVisual(GameObject visual)
        {
            if (this.visual != null)
            {
                Destroy(this.visual);
            }

            this.visual = Instantiate(visual, visualRoot);
        }
    }
}