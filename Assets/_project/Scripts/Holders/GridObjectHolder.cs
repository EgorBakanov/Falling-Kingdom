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
            tile.OnTileHeightChanged += OnTileHeightChanged;
        }

        private void UnsubscribeOnTile(Tile tile)
        {
            tile.OnTileFall -= OnTileFall;
            tile.OnTileHeightChanged -= OnTileHeightChanged;
        }

        protected virtual void OnTileHeightChanged(int newHeight, int _)
        {
            this.transform.position = GetPosition(TileHolder, newHeight);
        }

        protected virtual void OnTileFall()
        {
            Destroy(gameObject);
        }

        private Vector3 GetPosition(TileHolder tileHolder, int height)
        {
            return tileHolder.transform.position + Vector3.up * height / TileHolder.Tile.Grid.MaxHeight;
        }

        protected virtual void OnDestroy()
        {
            UnsubscribeOnTile(TileHolder.Tile);
        }
    }
}