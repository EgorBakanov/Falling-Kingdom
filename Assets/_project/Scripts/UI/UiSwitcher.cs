using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    public class UiSwitcher : MonoBehaviour
    {
        [Range(0, 5)] [SerializeField] private float durationOn = .5f;
        [Range(0, 5)] [SerializeField] private float durationOff = .5f;
        [SerializeField] private UiSwitchable[] switchables;
        [SerializeField] private bool turnOff = true;

        public IEnumerator SwitchOn()
        {
            var sequence = DOTween.Sequence();

            if (switchables.Length > 0)
                sequence.Append(switchables[0].SwitchOn(durationOn));

            for (var i = 1; i < switchables.Length; i++)
            {
                var switchable = switchables[i];
                sequence.Join(switchable.SwitchOn(durationOn));
            }

            if (turnOff)
                gameObject.SetActive(true);
            yield return sequence.WaitForCompletion();
        }

        public IEnumerator SwitchOff()
        {
            var sequence = DOTween.Sequence();

            if (switchables.Length > 0)
                sequence.Append(switchables[0].SwitchOff(durationOff));

            for (var i = 1; i < switchables.Length; i++)
            {
                var switchable = switchables[i];
                sequence.Join(switchable.SwitchOff(durationOff));
            }

            yield return sequence.WaitForCompletion();
            if (turnOff)
                gameObject.SetActive(false);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (switchables == null)
                switchables = GetComponents<UiSwitchable>();
        }
#endif
    }
}