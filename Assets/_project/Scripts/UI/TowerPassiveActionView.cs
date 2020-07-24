using Nara.MFGJS2020.Control;
using UnityEngine;

namespace Nara.MFGJS2020.UI
{
    public class TowerPassiveActionView : TowerActionView
    {
        [SerializeField] private bool onBeginTurn = true;

        protected override UiManager.DescriptorTag GetTags()
        {
            var tags = onBeginTurn ? UiManager.DescriptorTag.Target : UiManager.DescriptorTag.None;
            return base.GetTags() | tags;
        }
    }
}