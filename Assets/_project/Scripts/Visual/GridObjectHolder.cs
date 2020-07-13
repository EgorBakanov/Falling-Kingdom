using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.Visual
{
    public class GridObjectHolder : MonoBehaviour
    {
        public IGridObject Object { get; private set; }
        public TileHolder TileHolder { get; private set; }

        public void Init(IGridObject obj, TileHolder tileHolder)
        {
            Object = obj;
            TileHolder = tileHolder;
            SubscribeOnTile(TileHolder.Tile);
            TileHolder.Tile.GridObject = Object;
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
        
        private void OnTileHeightChanged(int newHeight, int _)
        {
            this.transform.position = GetPosition(TileHolder, newHeight);
        }
        
        private void OnTileFall()
        {
#if UNITY_EDITOR
            Debug.Log($"{this.name} fell");     
#endif
            Destroy(gameObject);
        }

        private Vector3 GetPosition(TileHolder tileHolder, int height)
        {
            return tileHolder.transform.position + Vector3.up * height / TileHolder.Tile.Grid.MaxHeight;
        }

        private void OnDestroy()
        {
            UnsubscribeOnTile(TileHolder.Tile);
        }
    }
}