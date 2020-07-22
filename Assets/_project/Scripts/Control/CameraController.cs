using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Nara.MFGJS2020.Control
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [Range(.1f, 3f)] [SerializeField] private float colorChangeTime = .5f;
        [SerializeField] private CinemachineFreeLook freeLook;
        [SerializeField] private CinemachineVirtualCamera defaultVCamera;
        [Range(0, 10)] [SerializeField] private float offset = 3;
        [Range(0, 2)] [SerializeField] private float topRigMultiplier = .3f;
        [Range(0, 2)] [SerializeField] private float middleRigMultiplier = 1.1f;
        [Range(0, 10)] [SerializeField] private float freeLookSpeed = 1;

        public InputAction FreeLookAction { get; set; }

        private void Start()
        {
            defaultVCamera.Priority = 10;
            freeLook.Priority = 0;
        }

        private void Update()
        {
            if (FreeLookAction == null || !IsFreeLook)
                return;
            
            Vector2 lookMovement = FreeLookAction.ReadValue<Vector2>();
            
            lookMovement.x *= 180f;

            freeLook.m_XAxis.Value += lookMovement.x * freeLookSpeed * Time.deltaTime;
            freeLook.m_YAxis.Value += lookMovement.y * freeLookSpeed * Time.deltaTime;
        }

        public bool IsFreeLook
        {
            get => freeLook.Priority > defaultVCamera.Priority;
            set
            {
                defaultVCamera.Priority = value ? 0 : 10;
                freeLook.Priority = value ? 10 : 0;
            }
        }

        public IEnumerator SetBackground(Color skyColor)
        {
            if (camera.backgroundColor != skyColor)
            {
                yield return camera.DOColor(skyColor, colorChangeTime).WaitForCompletion();
            }
            else
            {
                yield return null;
            }
        }

        public void UpdateFreeLook()
        {
            var distance = GameManager.Instance.GridHolder.UpdateCorners();
            freeLook.m_Orbits[2].m_Radius = distance + offset;
            freeLook.m_Orbits[0].m_Radius = freeLook.m_Orbits[2].m_Radius * topRigMultiplier;
            freeLook.m_Orbits[1].m_Radius = freeLook.m_Orbits[2].m_Radius * middleRigMultiplier;
        }

        public Vector3 WorldToScreenPoint(Vector3 pos)
        {
            return camera.WorldToScreenPoint(pos);
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