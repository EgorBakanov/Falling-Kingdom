using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Holders
{
    public class TileHolder : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Renderer visualRenderer;
        [SerializeField] private Transform visualTransform;
        public Tile Tile { get; private set; }
        public GridHolder GridHolder { get; private set; }

        private TileColorScheme _colorScheme;

        public void Init(Tile tile, GridHolder gridHolder, TileColorScheme colorScheme)
        {
            Tile = tile;
            GridHolder = gridHolder;
            _colorScheme = colorScheme;
            Tile.OnTileHeightChanged += OnTileHeightChanged;
            Tile.OnTileFall += OnTileFall;
            OnTileHeightChanged(Tile.Height, 0);
        }

        public void OnTileHeightChanged(int n, int _)
        {
            if (n == 0)
            {
                OnTileFall();
                return;
            }

            visualRenderer.sharedMaterial = _colorScheme[n];

            visualTransform.localScale = Vector3.up * n / Tile.Grid.MaxHeight + new Vector3(1, 0, 1);
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

        public void OnPointerClick(PointerEventData eventData)
        {
            if(Tile.Height == 0)
                return;
            
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                {
                    Tile.Height++;
                    return;
                }
                case PointerEventData.InputButton.Right:
                {
                    Tile.Height--;
                    return;
                }
                case PointerEventData.InputButton.Middle:
                {
                    if(Tile.GridObject != null)
                        return;
                    if (GridHolder.gridObjectHolder == null)
                        return;

                    var obj = Instantiate(GridHolder.gridObjectHolder);
                    obj.Init(new EmptyGridObject(), this);
                    
                    return;
                }
            }
        }
    }
}