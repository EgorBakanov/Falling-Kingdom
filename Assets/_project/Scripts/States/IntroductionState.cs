using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.States
{
    public class IntroductionState : State
    {
        public override IEnumerator Start()
        {
            var intro = GameManager.Instance.GetCurrentLevel().Introduction;
            if (!string.IsNullOrEmpty(intro))
            {
                yield return GameManager.Instance.UiManager.ShowIntroduction(intro);
            }
            else
            {
                GameManager.Instance.StateMachine.SetState(new ShowNextEnemySpawnState());
            }
        }

        public override IEnumerator OnSubmit()
        {
            yield return GameManager.Instance.UiManager.HideIntroduction();
            GameManager.Instance.StateMachine.SetState(new ShowNextEnemySpawnState());
        }

        public override IEnumerator OnCancel()
        {
            yield return GameManager.Instance.UiManager.HideIntroduction();
            GameManager.Instance.StateMachine.SetState(new ShowNextEnemySpawnState());
        }
    }
}