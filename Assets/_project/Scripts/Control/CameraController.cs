using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Nara.MFGJS2020.Control
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [Range(.1f, 3f)] [SerializeField] private float colorChangeTime = .5f;

        public IEnumerator SetBackground(Color skyColor)
        {
            yield return camera.DOColor(skyColor, colorChangeTime);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (camera == null)
                camera = GetComponent<Camera>();
        }
#endif
    }
}