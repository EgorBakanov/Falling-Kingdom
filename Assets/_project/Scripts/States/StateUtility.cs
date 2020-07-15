using System.Collections;
using Nara.MFGJS2020.Control;

namespace Nara.MFGJS2020.States
{
    public static class StateUtility
    {
        public static IEnumerator OnUIEndTurn()
        {
            yield return GameManager.Instance.UiManager.HidePlayerUI();
            GameManager.Instance.StateMachine.SetState(new OnEndPlayerTurnTowerActionState());
        }
    }
}