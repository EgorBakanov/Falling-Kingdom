using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.States
{
    public class BeginState : State
    {
        public override IEnumerator Start()
        {
            // TODO BeginState
            Debug.Log("BeginState");
            GameManager.Instance.StateMachine.SetState(new SelectLevelState());
            yield break;
        }
    }
}