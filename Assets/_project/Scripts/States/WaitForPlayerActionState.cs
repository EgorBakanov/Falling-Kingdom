using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public class WaitForPlayerActionState : State
    {
        public override IEnumerator Start()
        {
            yield return GameManager.Instance.UiManager.ShowPlayerUI();
        }

        public override IEnumerator OnUIEndTurn() => StateUtility.OnUIEndTurn();

        public override IEnumerator OnBuyTowerButtonClick(int id)
        {
            var preset = GameManager.Instance.TowerManager.AvailableToBuildTowers[id];
            if (preset.Cost > GameManager.Instance.CurrentMoney)
            {
                GameManager.Instance.StateMachine.SetState(new NotEnoughMoneyState());
            }
            else
            {
                GameManager.Instance.TowerManager.SelectPreset(id);
                GameManager.Instance.StateMachine.SetState(new TowerBuyChooseTileState());
            }
            
            yield break;
        }

        public override IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData)
        {
            var target = (Tower) tower;
            if(eventData.button != PointerEventData.InputButton.Left || tower == null)
                yield break;
            
            GameManager.Instance.TowerManager.SelectTower(target);
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerPickTowerActionState());
        }
    }
}