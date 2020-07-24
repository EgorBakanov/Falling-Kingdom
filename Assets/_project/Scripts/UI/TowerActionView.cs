using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.UI
{
    public abstract class TowerActionView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private new TMP_Text name;
        
        protected int Id;
        protected ActionBase Action;
        
        public virtual void Init(int id, ActionBase action)
        {
            Action = action;
            Id = id;

            name.text = action.Name;
        }

        protected virtual UiManager.DescriptorTag GetTags()
        {
            return UiManager.DescriptorTag.Action;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameManager.Instance.UiManager.ShowDescriptor(Action.Name,GetTags(),Action.Description,Action.Cost);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameManager.Instance.UiManager.HideDescriptor();
        }
    }
}