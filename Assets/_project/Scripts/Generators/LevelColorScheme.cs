using System.Collections.Generic;
using UnityEngine;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Level Color Scheme", menuName = "MFGJS2020/Level Color Scheme")]
    public class LevelColorScheme : ScriptableObject
    {
        [SerializeField] private Color skyColor = Color.white;
        [SerializeField] private Material baseTileMaterial;
        [SerializeField] private Color[] tileColors;

        private readonly Dictionary<int, Material> _materials = new Dictionary<int, Material>();

        public int Size => tileColors?.Length ?? 0;
        public Color SkyColor => skyColor;

        public Material this[int index]
        {
            get
            {
                index = Mathf.Clamp(index, 1, Size);
                if (_materials.TryGetValue(index, out var material))
                {
                    return material;
                }
                else
                {
                    return _materials[index] = new Material(baseTileMaterial) {color = tileColors[index - 1]};
                }
            }
        }
    }
}