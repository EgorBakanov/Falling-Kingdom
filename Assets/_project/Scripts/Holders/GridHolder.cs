using System.Collections;
using Nara.MFGJS2020.Generators;
using UnityEngine;
using Grid = Nara.MFGJS2020.Core.Grid;

namespace Nara.MFGJS2020.Holders
{
    public class GridHolder : MonoBehaviour
    {
        [Range(0f, 2f)] [SerializeField] private float spacing = 1f;
        [SerializeField] private TileHolder tilePrefab;
        [Range(.05f, 3f)] [SerializeField] private float timeOnRowCreate = .1f;

        [SerializeField] private Transform corner00;
        [SerializeField] private Transform corner01;
        [SerializeField] private Transform corner10;
        [SerializeField] private Transform corner11;

        public Grid Grid { get; private set; }
        public TileHolder[] TileHolders { get; private set; }

        public IEnumerator Init(Grid grid, LevelColorScheme levelColorScheme)
        {
            if (Grid != null)
            {
                Debug.Log("Level is already created!");
                yield break;
            }

            Grid = grid;

            TileHolders = new TileHolder[Grid.Size];

            var start = this.transform.position;
            var wait = new WaitForSeconds(timeOnRowCreate);

            for (int i = 0; i < Grid.SizeX; i++)
            {
                for (int j = 0; j < Grid.SizeY; j++)
                {
                    TileHolders[Grid.CoordinateToIndex(i, j)] = Instantiate<TileHolder>(tilePrefab, transform);
                    TileHolders[Grid.CoordinateToIndex(i, j)].transform.position = new Vector3(i, 0, j) * spacing;
                    TileHolders[Grid.CoordinateToIndex(i, j)].Init(Grid[i, j], this, levelColorScheme);
                }

                yield return wait;
            }
        }

        public void Clear()
        {
            if (TileHolders == null)
                return;

            for (int i = 0; i < TileHolders.Length; i++)
            {
                Destroy(TileHolders[i].gameObject);
            }

            TileHolders = null;
            Grid = null;
        }

        public float UpdateCorners()
        {
            var index00 = Grid.CoordinateToIndex(0, 0);
            var index01 = Grid.CoordinateToIndex(0, Grid.SizeY - 1);
            var index10 = Grid.CoordinateToIndex(Grid.SizeX - 1, 0);
            var index11 = Grid.CoordinateToIndex(Grid.SizeX - 1, Grid.SizeY - 1);

            corner00.position = TileHolders[index00].transform.position;
            corner01.position = TileHolders[index01].transform.position;
            corner10.position = TileHolders[index10].transform.position;
            corner11.position = TileHolders[index11].transform.position;
            return Vector3.Distance(corner00.position, corner11.position);
        }
    }
}