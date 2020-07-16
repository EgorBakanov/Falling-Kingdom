using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public static class StateUtility
    {
        public static IEnumerator OnEndTurn()
        {
            GameManager.Instance.TowerManager.DeselectPreset();
            GameManager.Instance.TowerManager.DeselectTower();
            yield return GameManager.Instance.UiManager.HidePlayerUI();
            GameManager.Instance.StateMachine.SetState(new OnEndPlayerTurnTowerActionState());
        }
        
        public static IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData)
        {
            var target = (Tower) tower;
            if(eventData.button != PointerEventData.InputButton.Left || tower == null)
                yield break;
            
            GameManager.Instance.TowerManager.SelectTower(target);
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerPickTowerActionState());
        }
        
        public static IEnumerator OnBuyTower(int id)
        {
            yield return GameManager.Instance.UiManager.HideTowerActionBar();
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
        }
    }
}