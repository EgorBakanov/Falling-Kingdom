using Nara.MFGJS2020.Generators;
using UnityEngine;

namespace Nara.MFGJS2020.Control
{
    [CreateAssetMenu(fileName = "New Level Manager", menuName = "MFGJS2020/Level Manager")]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private Level[] levels;

        public int Size => levels.Length;
        public Level Get(int i)
        {
            if (i < 0 || i >= Size)
                return null;
            return levels[i];
        }
    }
}