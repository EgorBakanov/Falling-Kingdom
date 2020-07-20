using System;
using System.Collections;
using DG.Tweening;
using Nara.MFGJS2020.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nara.MFGJS2020.Control
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private UiSwitcher winPopup;
        [SerializeField] private UiSwitcher losePopup;
        [SerializeField] private UiTemporalSwitcher beginTurnMessage;
        [SerializeField] private UiTemporalSwitcher endTurnMessage;
        [SerializeField] private UiTemporalSwitcher titleMessage;
        [SerializeField] private UiSwitcher turnCounter;
        [SerializeField] private TMP_Text turnCounterText;
        [SerializeField] private UiTemporalSwitcher turnCounterUpdater;
        [SerializeField] private UiSwitcher player;
        [SerializeField] private UiSwitcher towerActionBar;
        [SerializeField] private TMP_Text moneyCounterText;
        [SerializeField] private TowerPresetCollectionView towerPresetCollectionView;

        private void Update()
        {
            if (Keyboard.current.aKey.isPressed)
                StartCoroutine(ShowLoseMessage());
            if (Keyboard.current.sKey.isPressed)
                StartCoroutine(HideLoseMessage());
        }

        public IEnumerator ShowWinMessage() => winPopup.SwitchOn();
        public IEnumerator HideWinMessage() => winPopup.SwitchOff();
        public IEnumerator ShowBeginTurnMessage() => beginTurnMessage.Play();
        public IEnumerator ShowPlayerUi()
        {
            moneyCounterText.text = GameManager.Instance.CurrentMoney.ToString();
            towerPresetCollectionView.Init(GameManager.Instance.GetCurrentLevel().AvailableToBuildTowers);
            yield return player.SwitchOn();
        }

        public IEnumerator UpdateRemainingTurnsCounter(int value)
        {
            void UpdateCounter() => turnCounterText.text = value.ToString();
            yield return turnCounterUpdater.Play(UpdateCounter);
        }

        public IEnumerator ShowRemainingTurnsCounter() => turnCounter.SwitchOn();

        public IEnumerator HideAllUi()
        {
            // TODO
            yield return null;
        }

        public IEnumerator HidePlayerUi() => player.SwitchOff();
        public IEnumerator ShowEndTurnMessage() => endTurnMessage.Play();
        public IEnumerator ShowLoseMessage() => losePopup.SwitchOn();
        public IEnumerator HideLoseMessage() => losePopup.SwitchOff();
        public IEnumerator ShowTitle() => titleMessage.Play();

        public IEnumerator ShowNotEnoughMoney()
        {
            // TODO
            yield return null;
        }

        public IEnumerator UpdateMoneyCounter()
        {
            // TODO
            moneyCounterText.text = GameManager.Instance.CurrentMoney.ToString();
            yield return null;
        }

        public IEnumerator HideTowerActionBar() => towerActionBar.SwitchOff();

        public IEnumerator ShowCancelButton()
        {
            // TODO
            yield return null;
        }

        public IEnumerator ShowTowerActionBar() => towerActionBar.SwitchOn();
    }
}