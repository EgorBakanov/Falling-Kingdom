using System.Collections;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.TowerActions
{
    [CreateAssetMenu(fileName = "New Add Money Action", menuName = "MFGJS2020/Tower Actions/Add Money")]
    public class AddMoneyAction : NonTargetAction
    {
        [SerializeField] private int amount;
        
        public override IEnumerator Execute()
        {
            var result = GameManager.Instance.CurrentMoney + amount;
            yield return GameManager.Instance.SetMoney(result);
        }
    }
}