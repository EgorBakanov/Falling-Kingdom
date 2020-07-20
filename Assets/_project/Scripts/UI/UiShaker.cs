using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UiShaker : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [Range(0,3)][SerializeField] private float duration = 0.3f;
        [SerializeField] private Vector3 strength;
        [Range(0,40)][SerializeField] private int vibrato = 10;
        [Range(0,180)][SerializeField] private float randomness = 0;

        public IEnumerator Play()
        {
            yield return rectTransform.DOShakePosition(duration, strength, vibrato, randomness).WaitForCompletion();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
        }
#endif
    }
}