using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class NotEnoughMoneyState : State
    {
        public override IEnumerator Start()
        {
            yield return GameManager.Instance.UiManager.ShowNotEnoughMoney();
            GameManager.Instance.StateMachine.SetState(new WaitForPlayerActionState());
        }
    }
}