using System;
using System.Collections;
using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    public class GameStateMachine : MonoBehaviour
    {
        private State _state;

        public State State
        {
            private get { return _state; }
            set
            {
                if(IsRunning)
                    return;
                _state = value;
                StartCoroutine(SafeStart(_state.Start()));
            }
        }
        
        public bool IsRunning { get; private set; }

        private IEnumerator SafeStart(IEnumerator routine)
        {
            IsRunning = true;
            yield return routine;
            IsRunning = false;
        }
        
        
    }
}