using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;

namespace Nara.MFGJS2020.GridObjects
{
    public class EnemySpawner : IGridObject
    {
        public Tile Tile { get; }
        public bool IsActive { get; set; } = true;
        public int PathScore => Tile.Grid.Size;
        public int CantBuildZoneSize => 0;
        public EnemyPreset Preset { get; }

        public void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        public void OnTileFall()
        {
            Tile.GridObject = null;
        }

        public EnemySpawner(EnemyPreset preset, Tile tile)
        {
            Preset = preset;
            Tile = tile;
        }
    }
}