using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.TowerActions
{
    [UnityEngine.CreateAssetMenu(fileName = "New Destroy Action", menuName = "MFGJS2020/Tower Actions/Destroy")]
    public class DestroyAction : NonTargetAction
    {
        public override IEnumerator Execute()
        {
            GameManager.Instance.SelectionManager.SelectedTower.Die();
            yield break;
        }
    }
}