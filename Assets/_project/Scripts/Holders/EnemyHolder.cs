using System.Collections;
using DG.Tweening;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;
using UnityEngine;

namespace Nara.MFGJS2020.Holders
{
    public class EnemyHolder : GridObjectHolder<Enemy>
    {
        protected override void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        protected override void OnTileFall()
        {
            Debug.Log("fall");
            GameManager.Instance.EnemyManager.DestroyEnemy(this);
        }

        public IEnumerator Move(Tile tile, float jumpPower, float duration)
        {
            yield return transform.DOJump(GetPlacementPosition(tile), jumpPower, 1,duration).WaitForCompletion();
            TileHolder = TileHolder.GridHolder.TileHolders[tile.Index];
            GridObject.Move(tile);

            
        }
    }
}