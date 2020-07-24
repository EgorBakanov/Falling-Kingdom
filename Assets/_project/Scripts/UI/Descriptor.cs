using System;
using System.Text;
using Nara.MFGJS2020.Control;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nara.MFGJS2020.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class Descriptor : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text tags;
        [SerializeField] private TMP_Text description;
        [SerializeField] private RectTransform rectTransform;
        
        public void Init(string title, UiManager.DescriptorTag descriptorTag, string description, int cost)
        {
            this.title.text = title;
            this.description.text = description;
            
            StringBuilder tagsText = new StringBuilder();

            if (descriptorTag.HasFlag(UiManager.DescriptorTag.Enemy))
                tagsText.Append("<color=red>Enemy</color>\n");
            if (descriptorTag.HasFlag(UiManager.DescriptorTag.Tower))
                tagsText.Append("<color=green>Tower</color>\n");
            if (descriptorTag.HasFlag(UiManager.DescriptorTag.Spawner))
                tagsText.Append("<color=orange>Spawner</color>\n");
            if (descriptorTag.HasFlag(UiManager.DescriptorTag.Action))
            {
                tagsText.Append("<color=blue>");
                if (descriptorTag.HasFlag(UiManager.DescriptorTag.Active))
                {
                    tagsText.Append("Active ");
                    tagsText.Append(descriptorTag.HasFlag(UiManager.DescriptorTag.Target) ? "Target " : "Non-target ");
                }
                else
                {
                    tagsText.Append("Passive ");
                    tagsText.Append(descriptorTag.HasFlag(UiManager.DescriptorTag.Target) ? "Begin-turn " : "End-turn ");
                }
                
                tagsText.Append("Ability</color>\n");
            }

            if (descriptorTag.HasFlag(UiManager.DescriptorTag.Cost))
            {
                tagsText.AppendFormat("Cost: {0}", cost);
            }

            tags.text = tagsText.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
#endif
    }
}