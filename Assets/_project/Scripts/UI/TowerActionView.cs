using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.UI
{
    public class TowerActionView : MonoBehaviour
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
    }
}