﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Core
{
    public class GameStateMachine : MonoBehaviour
    {
        private State _state;

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
            IsRunning = false;
        }
        
        public void OnTileClick(Tile tile, PointerEventData eventData)
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnTileClick(tile,eventData)));
        }
        
        public void OnTowerClick(PointerEventData eventData)
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnTowerClick(eventData)));
        }

        public void OnTowerActionButtonClick(int id)
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnTowerActionButtonClick(id)));
        }

        public void OnBuyTowerButtonClick(int id)
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnBuyTowerButtonClick(id)));
        }

        public void OnUISubmit()
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnUISubmit()));
        }
        
        public void OnUICancel()
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnUICancel()));
        }
        
        public void OnUIEndTurn()
        {
            if(IsRunning)
                return;
            StartCoroutine(SafeStart(_state.OnUIEndTurn()));
        }
    }
}