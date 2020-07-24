using System.Collections.Generic;
using Nara.MFGJS2020.Generators;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    public class TowerActionBarView : MonoBehaviour
    {
        [SerializeField] private TowerActiveActionView activeViewPrefab;
        [SerializeField] private TowerPassiveActionView beginViewPrefab;
        [SerializeField] private TowerPassiveActionView endViewPrefab;
        [SerializeField] private RectTransform activeActionsContainer;
        [SerializeField] private RectTransform passiveActionsContainer;

        private readonly List<TowerActionView> _allViews = new List<TowerActionView>();

        public void Init(TowerPreset preset)
        {
            Clear();

            for (int i = 0; i < preset.ActiveActions.Length; i++)
            {
                var view = Instantiate(activeViewPrefab, activeActionsContainer);
                view.Init(i,preset.ActiveActions[i]);
                _allViews.Add(view);
            }

            if (preset.BeginPlayerTurnAction != null)
            {
                var view = Instantiate(beginViewPrefab, passiveActionsContainer);
                view.Init(-1,preset.BeginPlayerTurnAction);
                _allViews.Add(view);
            }
            
            if (preset.EndPlayerTurnAction != null)
            {
                var view = Instantiate(endViewPrefab, passiveActionsContainer);
                view.Init(-1,preset.EndPlayerTurnAction);
                _allViews.Add(view);
            }
        }

        private void Clear()
        {
            for (int i = _allViews.Count - 1; i >= 0; i--)
            {
                Destroy(_allViews[i].gameObject);
            }
            _allViews.Clear();
        }
    }
}