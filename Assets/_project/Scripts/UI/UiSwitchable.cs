using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UiSwitchable : MonoBehaviour
    {
        [SerializeField] protected RectTransform rectTransform;
        [SerializeField] protected Ease ease = Ease.Unset;

        public Tween SwitchOn(float duration)
        {
            return Animate(0, 1, duration);
        }

        public Tween SwitchOff(float duration)
        {
            return Animate(1, 0, duration);
        }

        protected abstract Tween Animate(float from, float to, float duration);

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
        }
#endif
    }
}