using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    public class ScaleSwitchable : UiSwitchable
    {
        [Range(0,2)][SerializeField] private float scaleOn;
        [Range(0,2)][SerializeField] private float scaleOff;

        protected override Tween Animate(float from, float to, float duration)
        {
            from = Mathf.Lerp(scaleOff, scaleOn, from);
            to = Mathf.Lerp(scaleOff, scaleOn, to);
            rectTransform.localScale = from * Vector3.one;
            return rectTransform.DOScale(to, duration).SetEase(ease);
        }
    }
}