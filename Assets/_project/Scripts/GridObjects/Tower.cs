using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;

namespace Nara.MFGJS2020.GridObject
{
    public class Tower : IGridObject
    {
        public Tile Tile { get; }
        public bool IsActive { get; set; }
        public int PathScore => Health;
        public int CantBuildZoneSize => Preset.CantBuildZoneSize;
        public int ExpandBuildZoneSize => Preset.ExpandBuildZoneSize;
        public int Health { get; }
        public int MaxHealth => Preset.MaxHealth;
        public TowerPreset Preset { get; }
        
        public void OnTileHeightChanged(int newHeight, int oldHeight)
        {
            //throw new System.NotImplementedException();
        }

        public void OnTileFall()
        {
            //throw new System.NotImplementedException();
        }

        public Tower(TowerPreset preset, Tile tile)
        {
            Tile = tile;
            Preset = preset;
            Health = Preset.StartHealth;
            IsActive = Preset.InitialActivity;
        }
    }
}