using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class LoseState : State
    {
        public override IEnumerator Start()
        {
            GameManager.Instance.CameraController.IsFreeLook = false;
            GameManager.Instance.AudioManager.StartCoroutine(GameManager.Instance.AudioManager.PlayLose());
            yield return GameManager.Instance.UiManager.HideAllUi();
            GameManager.Instance.SelectionManager.DeselectAll();
            yield return GameManager.Instance.UiManager.ShowLoseMessage();
        }

        public override IEnumerator OnSubmit()
        {
            yield return GameManager.Instance.UiManager.HideLoseMessage();
            GameManager.Instance.StateMachine.SetState(new InitLevelState());
        }
    }
}