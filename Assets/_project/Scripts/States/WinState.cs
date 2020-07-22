using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class WinState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.CameraController.IsFreeLook = false;
            yield return GameManager.Instance.UiManager.HideAllUi();
            GameManager.Instance.SelectionManager.DeselectAll();
            yield return GameManager.Instance.UiManager.ShowWinMessage();
        }

        public override IEnumerator OnSubmit()
        {
            yield return GameManager.Instance.UiManager.HideWinMessage();
            GameManager.Instance.StateMachine.SetState(new SelectLevelState());
        }
    }
}