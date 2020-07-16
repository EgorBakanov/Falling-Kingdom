using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class LoseState : State
    {
        public override IEnumerator Start()
        {
            yield return GameManager.Instance.UiManager.HideAllUI();
            yield return GameManager.Instance.UiManager.ShowLoseMessage();
        }

        public override IEnumerator OnSubmit()
        {
            yield return GameManager.Instance.UiManager.HideLoseMessage();
            GameManager.Instance.StateMachine.SetState(new InitLevelState());
        }
    }
}