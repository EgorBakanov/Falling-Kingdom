using Nara.MFGJS2020.Control;
using Nara.MFGJS2020.Generators;
using Nara.MFGJS2020.GridObjects;

namespace Nara.MFGJS2020.Holders
{
    public class EnemySpawnerHolder : GridObjectHolder<EnemySpawner>
    {
        protected override void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        protected override void OnTileFall()
        {
            GameManager.Instance.EnemyManager.DestroySpawner(this);
        }
    }
}