using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.UI
{
    public class TowerActiveActionView : TowerActionView, IPointerClickHandler
    {
        [SerializeField] private RectTransform isTargetMarker;
        [SerializeField] private TMP_Text cost;
        
        public override void Init(int id, ActionBase action)
        {
            base.Init(id, action);
            cost.text = action.Cost.ToString();
            isTargetMarker.gameObject.SetActive(action.IsTarget);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
                GameManager.Instance.StateMachine.OnTowerActionButtonClick(Id);
        }

        protected override UiManager.DescriptorTag GetTags()
        {
            var tags = (Action.IsTarget) ? UiManager.DescriptorTag.Target : UiManager.DescriptorTag.None;
            return base.GetTags() | UiManager.DescriptorTag.Active | UiManager.DescriptorTag.Cost | tags;
        }
    }
}