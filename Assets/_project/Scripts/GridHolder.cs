using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Nara.MFGJS2020
{
    public class GridHolder : MonoBehaviour
    {
        private Grid _grid;

        [FormerlySerializedAs("_level")] [SerializeField] private Level level;
        [Range(0f, 2f)] [SerializeField] private float spacing = 1f;
        [SerializeField] private Transform cell;
        [SerializeField] private bool showGizmos;

        private void Start()
        {
            if(_grid==null)
                return;
            if(cell==null)
                return;

            var start = this.transform.position;
            
            for (int i = 0; i < _grid.SizeX; i++)
            {
                for (int j = 0; j < _grid.SizeY; j++)
                {
                    if(_grid[i,j].Height==0) continue;
                    
                    var t = Instantiate<Transform>(cell, transform);
                    t.position = start + new Vector3(i, 0, j) * spacing;
                    t.localScale = Vector3.up * _grid[i, j].Height / _grid.MaxHeight + new Vector3(1,0,1);
                }
            }
        }

        private void OnValidate()
        {
            if (level != null)
                _grid = level.Generate();
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            if (_grid == null) return;
            var start = this.transform.position;
            Gizmos.color = Color.blue;

            for (int i = 0; i < _grid.SizeX; i++)
            {
                for (int j = 0; j < _grid.SizeY; j++)
                {
                    var pos = start + new Vector3(i, 0, j) * spacing +
                              Vector3.up * _grid[i, j].Height / _grid.MaxHeight * spacing;
                    Gizmos.DrawSphere(pos, 0.1f);
                }
            }
        }
    }
}