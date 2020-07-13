namespace Nara.MFGJS2020.Core
{
    public interface IGridObject
    {
        GridObjectType Type { get; }
        int PathScore { get; }
        void OnTileHeightChanged(int newHeight, int oldHeight);
        void OnTileFall();
    }

    public enum GridObjectType
    {
        None,
        Tower,
        Enemy
    }
}