using UnityEngine;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Tile Color Scheme", menuName = "MFGJS2020/Tile Color Scheme")]
    public class TileColorScheme : ScriptableObject
    {
        [SerializeField] private Material[] materials;

        public int Size => materials.Length;

        public Material this[int index]
        {
            get
            {
                index = Mathf.Clamp(index, 1, Size);
                return materials[index - 1];
            }
        }
    }
}