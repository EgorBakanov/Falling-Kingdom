using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.GridObjects;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Holders
{
    public class EnemySpawnerHolder : GridObjectHolder<EnemySpawner>, IPointerEnterHandler, IPointerExitHandler
    {
        protected override void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        protected override void OnTileFall()
        {
            GameManager.Instance.EnemyManager.DestroySpawner(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameManager.Instance.UiManager.ShowDescriptor(GridObject.Preset.SpawnerName,UiManager.DescriptorTag.Spawner,string.Empty);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameManager.Instance.UiManager.HideDescriptor();
        }
    }
}