using Nara.MFGJS2020.Core;

namespace Nara.MFGJS2020.GridObject
{
    public class Tower : IGridObject
    {
        public Tile Tile { get; }
        public bool IsActive { get; set; }
        public int PathScore { get; }
        public int CantBuildZoneSize { get; }
        public void OnTileHeightChanged(int newHeight, int oldHeight)
        {
            throw new System.NotImplementedException();
        }

        public void OnTileFall()
        {
            throw new System.NotImplementedException();
        }
    }
}