using System;
using System.Collections;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    [RequireComponent(typeof(UiSwitcher))]
    public class UiTemporalSwitcher : MonoBehaviour
    {
        [Range(0, 5)][SerializeField] private float durationHold = .5f;
        [SerializeField] private UiSwitcher switcher;

        public IEnumerator Play(Action onSwitchOn = null, Action onSwitchOff = null)
        {
            yield return switcher.SwitchOn();
            onSwitchOn?.Invoke();
            yield return new WaitForSeconds(durationHold);
            yield return switcher.SwitchOff();
            onSwitchOff?.Invoke();
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (switcher == null)
                switcher = GetComponent<UiSwitcher>();
        }
#endif
    }
}