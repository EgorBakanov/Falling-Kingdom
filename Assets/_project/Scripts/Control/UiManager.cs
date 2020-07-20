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
        [SerializeField] private TowerActionBarView towerActionBarView;
        [SerializeField] private UiSwitcher allInGameUi;
        [SerializeField] private UiShaker notEnoughMoney;
        [SerializeField] private UiShaker updateMoney;
        [SerializeField] private Material notEnoughMoneyMaterial;
        [SerializeField] private UiSwitcher cancelButton;

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
        public IEnumerator HideAllUi() => allInGameUi.SwitchOff();
        public IEnumerator HidePlayerUi() => player.SwitchOff();
        public IEnumerator ShowEndTurnMessage() => endTurnMessage.Play();
        public IEnumerator ShowLoseMessage() => losePopup.SwitchOn();
        public IEnumerator HideLoseMessage() => losePopup.SwitchOff();
        public IEnumerator ShowTitle() => titleMessage.Play();
        public IEnumerator ShowNotEnoughMoney()
        {
            var startMaterial = moneyCounterText.fontSharedMaterial;
            moneyCounterText.fontSharedMaterial = notEnoughMoneyMaterial;
            yield return notEnoughMoney.Play();
            moneyCounterText.fontSharedMaterial = startMaterial;
        }
        public IEnumerator UpdateMoneyCounter()
        {
            moneyCounterText.text = GameManager.Instance.CurrentMoney.ToString();
            yield return updateMoney.Play();
        }
        public IEnumerator HideTowerActionBar() => towerActionBar.SwitchOff();
        public IEnumerator ShowCancelButton() => cancelButton.SwitchOn();
        public IEnumerator HideCancelButton() => cancelButton.SwitchOff();
        public IEnumerator ShowTowerActionBar()
        {
            var preset = GameManager.Instance.SelectionManager.SelectedTower?.Preset;
            towerActionBarView.Init(preset);
            yield return towerActionBar.SwitchOn();
        }
    }
}