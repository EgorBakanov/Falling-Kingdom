using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Core
{
    public class GameStateMachine : MonoBehaviour
    {
        // tests
        private void Update()
        {
            Debug.Log(_state.GetType());
        }

        private State _state;
        private State _onTargetTowerDestroyedState;

        public void Init(State startState, State onTargetTowerDestroyedState)
        {
            if(_current != null)
                StopCoroutine(_current);
            _state = startState;
            this._onTargetTowerDestroyedState = onTargetTowerDestroyedState;
            _current = StartCoroutine(SafeStart(_state.Start()));
        }
        
        public void SetState(State state)
        {
            if(_current != null)
                StopCoroutine(_current);
            _state = state;
            _current = StartCoroutine(SafeStart(_state.Start()));
        }

        public bool IsRunning { get; private set; }
        private Coroutine _current;

        private IEnumerator SafeStart(IEnumerator routine)
        {
            IsRunning = true;
            yield return routine;
            _current = null;
            IsRunning = false;
        }
        
        public void OnTileClick(Tile tile, PointerEventData eventData)
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnTileClick(tile,eventData)));
        }
        
        public void OnTowerClick(IGridObject tower,PointerEventData eventData)
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnTowerClick(tower, eventData)));
        }

        public void OnTowerActionButtonClick(int id)
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnTowerAction(id)));
        }

        public void OnBuyTowerButtonClick(int id)
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnBuyTower(id)));
        }

        public void OnSubmit()
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnSubmit()));
        }
        
        public void OnCancel()
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnCancel()));
        }
        
        public void OnEndTurn()
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnEndTurn()));
        }

        public void OnTargetTowerDestroyed()
        {
            SetState(_onTargetTowerDestroyedState);
        }
    }
}