namespace Nara.MFGJS2020.Core
{
    public interface IGridObject
    {
        Tile Tile { get; }
        bool IsActive { get; set; }
        int PathScore { get; }
        int CantBuildZoneSize { get; }
        int ExpandBuildZoneSize { get; }
        void OnTileHeightChanged(int newHeight, int oldHeight);
        void OnTileFall();
    }
}