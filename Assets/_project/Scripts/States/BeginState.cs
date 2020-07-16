using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class BeginState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.AudioManager.StartBackground();
            yield return GameManager.Instance.UiManager.ShowTitle();
            GameManager.Instance.StateMachine.SetState(new SelectLevelState());
        }
    }
}