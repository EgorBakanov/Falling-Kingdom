using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.States
{
    public static class StateUtility
    {
        public static IEnumerator OnEndTurn()
        {
            GameManager.Instance.SelectionManager.DeselectAll();
            GameManager.Instance.SelectionManager.WaitForPlayerSelection = false;
            GameManager.Instance.StateMachine.SetState(new OnEndPlayerTurnTowerActionState());
            yield break;
        }
        
        public static IEnumerator OnTowerClick(IGridObject tower, PointerEventData eventData)
        {
            GameManager.Instance.SelectionManager.DeselectAll();
            var target = tower as Tower;
            Assert.AreEqual(PointerEventData.InputButton.Left,eventData.button);
            if(eventData.button != PointerEventData.InputButton.Left || target == null)
                yield return ReturnToWait();
            
            GameManager.Instance.SelectionManager.SelectedTower = target;
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerPickTowerActionState());
        }
        
        public static IEnumerator OnBuyTower(int id)
        {
            GameManager.Instance.SelectionManager.DeselectAll();
            var preset = GameManager.Instance.TowerManager.AvailableToBuildTowers[id];
            if (preset.Cost > GameManager.Instance.CurrentMoney)
            {
                GameManager.Instance.StateMachine.SetState(new NotEnoughMoneyState());
            }
            else
            {
                GameManager.Instance.SelectionManager.SelectedTowerPresetId = id;
                GameManager.Instance.StateMachine.SetState(new TowerBuyChooseTileState());
            }
            yield break;
        }

        public static IEnumerator ReturnToWait()
        {
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerActionState());
            yield break;
        }
    }
}