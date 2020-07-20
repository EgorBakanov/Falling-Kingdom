using System;
using System.Collections.Generic;
using Nara.MFGJS2020.Generators;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Nara.MFGJS2020.UI
{
    public class TowerPresetCollectionView : MonoBehaviour
    {
        [SerializeField] private TowerPresetView viewPrefab;

        private readonly List<TowerPresetView> _presetViews = new List<TowerPresetView>();

        public void Init(TowerPreset[] presets)
        {
            Clear();

            for (var i = 0; i < presets.Length; i++)
            {
                var preset = presets[i];
                var view = Instantiate(viewPrefab,transform);
                view.Init(i,preset);
                _presetViews.Add(view);
            }
        }

        private void Clear()
        {
            for (int i = _presetViews.Count - 1; i >= 0; i--)
            {
                Destroy(_presetViews[i].gameObject);
            }
            _presetViews.Clear();
        }
    }
}