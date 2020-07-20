using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;

namespace Nara.MFGJS2020.GridObjects
{
    public class Enemy : IGridObject
    {
        public Tile Tile { get; private set; }
        public bool IsActive { get; set; }
        public int PathScore => Tile.Grid.Size;
        public int CantBuildZoneSize => Preset.CantBuildZoneSize;
        public int ExpandBuildZoneSize => 0;
        public EnemyPreset Preset { get; }
        public Tile MoveIntention { get; set; }
        
        public void OnTileHeightChanged(int newHeight, int oldHeight)
        {
        }

        public void OnTileFall()
        {
            Tile.GridObject = null;
        }

        public Enemy(EnemyPreset preset, Tile tile)
        {
            Tile = tile;
            Preset = preset;
            IsActive = Preset.InitialActivity;
            MoveIntention = null;
        }

        public void Move(Tile tile)
        {
            Tile.GridObject = null;
            Tile = tile;
            tile.GridObject = this;
        }
    }
}