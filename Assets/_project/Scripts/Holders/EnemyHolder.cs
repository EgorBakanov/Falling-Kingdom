﻿using Nara.MFGJS2020.Control;
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
    }
}