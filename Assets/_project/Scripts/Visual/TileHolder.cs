using System;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;

namespace Nara.MFGJS2020.Visual
{
    public class TileHolder : MonoBehaviour
    {
        [SerializeField] private Renderer visual;
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

            visual.sharedMaterial = _colorScheme[n];

            var t = visual.transform;
            t.localScale = Vector3.up * n / Tile.Grid.MaxHeight + new Vector3(1,0,1);
        }

        public void OnTileFall()
        {
            visual.enabled = false;
        }

        private void OnDestroy()
        {
            Tile.OnTileFall -= OnTileFall;
            Tile.OnTileHeightChanged -= OnTileHeightChanged;
        }
    }
}