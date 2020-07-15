using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.Holders
{
    public abstract class GridObjectHolder<T> : MonoBehaviour where T : IGridObject
    {
        [SerializeField] private Transform visualRoot;
        public virtual T GridObject { get; private set; }
        public TileHolder TileHolder { get; private set; }
        public Transform VisualRoot => visualRoot;

        public virtual void Init(T obj, TileHolder tileHolder)
        {
            GridObject = obj;
            TileHolder = tileHolder;
            SubscribeOnTile(TileHolder.Tile);
            TileHolder.Tile.GridObject = GridObject;
        }

        private void SubscribeOnTile(Tile tile)
        {
            tile.OnTileFall += OnTileFall;
            tile.OnTileHeightChanged += SetPosition;
            tile.OnTileHeightChanged += OnTileHeightChanged;
        }

        private void UnsubscribeOnTile(Tile tile)
        {
            tile.OnTileFall -= OnTileFall;
            tile.OnTileHeightChanged -= SetPosition;
            tile.OnTileHeightChanged -= OnTileHeightChanged;
        }

        protected abstract void OnTileHeightChanged(int newHeight, int oldHeight);
        protected abstract void OnTileFall();

        private void SetPosition(int newHeight, int _)
        {
            var newPos = TileHolder.transform.position + Vector3.up * newHeight / TileHolder.Tile.Grid.MaxHeight;
            this.transform.position = newPos;
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeOnTile(TileHolder.Tile);
        }
    }
}