using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Holders
{
    public class TileHolder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
            
            GameManager.Instance.StateMachine.OnTileClick(Tile,eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(!GameManager.Instance.SelectionManager.WaitForPlayerSelection)
                return;
            
            GameManager.Instance.SelectionManager.AddTile(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(!GameManager.Instance.SelectionManager.WaitForPlayerSelection)
                return;

            GameManager.Instance.SelectionManager.RemoveTile(this);
        }
    }
}