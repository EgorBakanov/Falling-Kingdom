using System.Collections;
using Nara.MFGJS2020.Core;
using UnityEngine.SceneManagement;

namespace Nara.MFGJS2020.States
{
    public class EndGameState : State
    {
        public override IEnumerator Start()
        {
            SceneManager.LoadScene(1);
            yield break;
        }
    }
}