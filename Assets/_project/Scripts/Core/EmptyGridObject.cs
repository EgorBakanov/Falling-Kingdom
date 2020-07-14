namespace Nara.MFGJS2020.Core
{
    public class EmptyGridObject : IGridObject
    {
        public EmptyGridObject(Tile tile)
        {
            Tile = tile;
        }

        public Tile Tile { get; }
        public bool IsActive { get; set; }
        public int PathScore => Tile.Grid.Size;
        public int CantBuildZoneSize => 0;

        public void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        public void OnTileFall()
        {
        }
    }
}