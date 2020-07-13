using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Visuals
{
    public class ScaleOnEnterAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] [Range(0, 2f)] private float amount = 1.2f;
        [SerializeField] [Range(0, 2f)] private float duration = .1f;
        [SerializeField] private Ease ease = Ease.Linear;

        private Vector3 startScale, endScale;

        private void Start()
        {
            startScale = transform.localScale;
            endScale = startScale * amount;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlayAnimation(endScale);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PlayAnimation(startScale);
        }

        public void OnSelect(BaseEventData eventData)
        {
            PlayAnimation(endScale);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            PlayAnimation(startScale);
        }

        private void PlayAnimation(Vector3 endScale)
        {
            transform.DOScale(endScale, duration).SetEase(ease);
        }
    }
}