using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public class TowerBuyChooseTileState : State
    {
        public override IEnumerator Start()
        {
            // TODO TowerBuyChooseTileState
            yield return GameManager.Instance.UiManager.ShowCancelButton();
            yield return GameManager.Instance.CameraController.OutlineZones();
        }

        public override IEnumerator OnBuyTower(int id)
        {
            yield return GameManager.Instance.CameraController.UnoutlineZones();
            GameManager.Instance.TowerManager.DeselectAll();
            yield return StateUtility.OnBuyTower(id);
        }

        public override IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
            yield return GameManager.Instance.CameraController.UnoutlineZones();
            if (eventData.button != PointerEventData.InputButton.Left ||
                !GameManager.Instance.GridHolder.Grid.BuildZone.Contains(tile))
            {
                GameManager.Instance.TowerManager.DeselectAll();
                GameManager.Instance.StateMachine.SetState(new WaitForPlayerActionState());
                yield break;
            }
            
            GameManager.Instance.TowerManager.SelectTile(tile);
            GameManager.Instance.StateMachine.SetState(new CreateTowerState());
        }
    }
}