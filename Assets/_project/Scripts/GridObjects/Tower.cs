using System;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;

namespace Nara.MFGJS2020.GridObjects
{
    public class Tower : IGridObject
    {
        public Tile Tile { get; }
        public bool IsActive { get; set; }
        public int PathScore => Health;
        public int CantBuildZoneSize => Preset.CantBuildZoneSize;
        public int ExpandBuildZoneSize => Preset.ExpandBuildZoneSize;

        public int Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, MaxHealth);
                if (_health == 0)
                    Die();
            }
        }

        public void Die()
        {
            Tile.GridObject = null;
            OnDie?.Invoke();
        }

        public int MaxHealth => Preset.MaxHealth;
        public TowerPreset Preset { get; }

        public event Action OnDie;
        
        private int _health;
        
        public void OnTileHeightChanged(int newHeight, int oldHeight)
        {
            Health -= Mathf.Abs(oldHeight - newHeight);
        }

        public void OnTileFall()
        {
            Die();
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