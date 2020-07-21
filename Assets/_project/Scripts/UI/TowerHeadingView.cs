using System;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Holders;
using TMPro;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    public class TowerHeadingView : MonoBehaviour
    {
        [SerializeField] private new TMP_Text name;
        [SerializeField] private TMP_Text health;
        [SerializeField] private Vector3 offset = Vector3.up;

        private TowerHolder _towerHolder;

        public void Init(TowerHolder towerHolder)
        {
            name.text = towerHolder.GridObject.Preset.Name;
            health.text = $"{towerHolder.GridObject.Health} / {towerHolder.GridObject.MaxHealth}";
            _towerHolder = towerHolder;
        }

        public void UpdateValues()
        {
            name.text = _towerHolder.GridObject.Preset.Name;
            health.text = $"{_towerHolder.GridObject.Health} / {_towerHolder.GridObject.MaxHealth}";
        }

        private void LateUpdate()
        {
            transform.position =
                GameManager.Instance.CameraController.WorldToScreenPoint(_towerHolder.transform.position + offset);
        }
    }
}