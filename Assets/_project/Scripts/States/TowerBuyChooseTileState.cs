using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public class TowerBuyChooseTileState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.SelectionManager.ShowZones = true;
            yield return GameManager.Instance.UiManager.ShowCancelButton();
        }

        public override IEnumerator OnBuyTower(int id)
        {
            GameManager.Instance.SelectionManager.DeselectAll();
            yield return StateUtility.OnBuyTower(id);
        }

        public override IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left ||
                !GameManager.Instance.GridHolder.Grid.BuildZone.Contains(tile))
            {
                GameManager.Instance.SelectionManager.DeselectAll();
                GameManager.Instance.StateMachine.SetState(new WaitForPlayerActionState());
                yield break;
            }
            
            GameManager.Instance.SelectionManager.SelectedTile = tile;
            GameManager.Instance.StateMachine.SetState(new CreateTowerState());
        }
    }
}