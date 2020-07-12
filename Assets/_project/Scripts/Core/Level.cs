using Nara.MFGJS2020.Utility;
using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Level")]
    public class Level : ScriptableObject
    {
        [System.Serializable]
        private struct Row
        {
            public int[] values;
        }

        [Range(1, 20)] [SerializeField] private int x = 2;
        [Range(1, 20)] [SerializeField] private int y = 2;
        [Range(1, 6)] [SerializeField] private int maxHeight = 4;

        [SerializeField] private Int2D initialHeights;

        private void OnValidate()
        {
            if (initialHeights == null || initialHeights.x != x || initialHeights.y != y)
            {
                initialHeights = new Int2D(x,y); 
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    initialHeights[i,j] = Mathf.Clamp(initialHeights[i,j], 0, maxHeight);
                }
            }
        }

        public Grid Generate()
        {
            return new Grid(x, y, maxHeight, initialHeights);
        }
    }
}