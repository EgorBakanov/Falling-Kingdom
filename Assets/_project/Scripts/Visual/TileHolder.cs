using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;

namespace Nara.MFGJS2020.Visual
{
    public class TileHolder : MonoBehaviour
    {
        [SerializeField] private Renderer visualRenderer;
        [SerializeField] private Transform visualTransform;
        public Tile Tile { get; private set; }

        private TileColorScheme _colorScheme;

        public void Init(Tile tile, TileColorScheme colorScheme)
        {
            Tile = tile;
            _colorScheme = colorScheme;
            Tile.OnTileHeightChanged += OnTileHeightChanged;
            Tile.OnTileFall += OnTileFall;
            OnTileHeightChanged(Tile.Height,0);
        }

        public void OnTileHeightChanged(int n, int _)
        {
            if (n == 0)
            {
                OnTileFall();
                return;
            }

            visualRenderer.sharedMaterial = _colorScheme[n];

            visualTransform.localScale = Vector3.up * n / Tile.Grid.MaxHeight + new Vector3(1,0,1);
        }

        public void OnTileFall()
        {
            visualRenderer.enabled = false;
        }

        private void OnDestroy()
        {
            Tile.OnTileFall -= OnTileFall;
            Tile.OnTileHeightChanged -= OnTileHeightChanged;
        }
    }
}