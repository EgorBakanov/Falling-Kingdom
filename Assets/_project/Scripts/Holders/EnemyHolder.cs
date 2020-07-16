using System.Collections;
using DG.Tweening;
using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.GridObjects;

namespace Nara.MFGJS2020.Holders
{
    public class EnemyHolder : GridObjectHolder<Enemy>
    {
        protected override void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        protected override void OnTileFall()
        {
            GameManager.Instance.EnemyManager.DestroyEnemy(this);
        }

        public IEnumerator Move(Tile tile)
        {
            TileHolder = TileHolder.GridHolder.TileHolders[tile.Index];
            GridObject.Move(tile);

            yield return transform.DOJump(GetPlacementPosition(tile.Height), 1, 1,1f).WaitForCompletion();
        }
    }
}