using System;
using System.Collections.Generic;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;
using Grid = Nara.MFGJS2020.Core.Grid;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

namespace Nara.MFGJS2020.Holders
{
    public class GridHolder : MonoBehaviour
    {
        [Range(0f, 2f)] [SerializeField] private float spacing = 1f;
        [SerializeField] private TileHolder tilePrefab;
        
        public Grid Grid { get; private set; }
        public TileHolder[] TileHolders { get; private set; }

        public void Init(Grid grid, TileColorScheme tileColorScheme)
        {
            if (Grid != null)
            {
                Debug.Log("Level is already created!");
                return;
            }
            
            Grid = grid;

            TileHolders = new TileHolder[Grid.Size];

            var start = this.transform.position;

            for (int i = 0; i < Grid.SizeX; i++)
            {
                for (int j = 0; j < Grid.SizeY; j++)
                {
                    TileHolders[Grid.CoordinateToIndex(i, j)] = Instantiate<TileHolder>(tilePrefab, transform);
                    TileHolders[Grid.CoordinateToIndex(i, j)].transform.position = start + new Vector3(i, 0, j) * spacing;
                    TileHolders[Grid.CoordinateToIndex(i, j)].Init(Grid[i, j], this, tileColorScheme);
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < TileHolders.Length; i++)
            {
                Destroy(TileHolders[i]);
            }

            TileHolders = null;
            Grid = null;
        }
    }
}