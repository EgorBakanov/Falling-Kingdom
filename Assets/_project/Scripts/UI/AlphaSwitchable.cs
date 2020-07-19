using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class AlphaSwitchable : UiSwitchable
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [Range(0,1)][SerializeField] private float alphaOn;
        [Range(0,1)][SerializeField] private float alphaOff;
        
        protected override Tween Animate(float from, float to, float duration)
        {
            from = Mathf.Lerp(alphaOff, alphaOn, from);
            to = Mathf.Lerp(alphaOff, alphaOn, to);
            canvasGroup.alpha = from;
            return canvasGroup.DOFade(to,duration).SetEase(ease);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();
        }
#endif
    }
}