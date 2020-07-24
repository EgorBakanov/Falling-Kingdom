using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Generators;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.UI
{
    public class TowerPresetView : MonoBehaviour , IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text cost;
        [SerializeField] private new TMP_Text name;

        private TowerPreset _preset;
        private int _id;

        public void Init(int id, TowerPreset preset)
        {
            _id = id;
            _preset = preset;

            cost.text = preset.Cost.ToString();
            name.text = preset.Name;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
                GameManager.Instance.StateMachine.OnBuyTowerButtonClick(_id);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            var tags = UiManager.DescriptorTag.Tower | UiManager.DescriptorTag.Cost;
            GameManager.Instance.UiManager.ShowDescriptor(_preset.Name,tags,_preset.Description,_preset.Cost);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameManager.Instance.UiManager.HideDescriptor();
        }
    }
}