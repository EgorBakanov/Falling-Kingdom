using System;
using DG.Tweening;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    public class PositionSwitchable : UiSwitchable
    {
        [SerializeField] private Vector3 positionOn;
        [SerializeField] private Vector3 positionOff;

        protected override Tween Animate(float from, float to, float duration)
        {
            var fromPos = Vector3.Lerp(positionOff, positionOn, from);
            var toPos = Vector3.Lerp(positionOff, positionOn, to);
            rectTransform.anchoredPosition3D = fromPos;
            return rectTransform.DOAnchorPos3D(toPos,duration).SetEase(ease);
        }
    }
}