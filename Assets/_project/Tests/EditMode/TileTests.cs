using System.Collections;
using Nara.MFGJS2020.Core;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class TileTests
    {
        [Test]
        public void TileOnTileHeightChangedIsInvoked()
        {
            var tile = new Tile(new Grid(1, 1, 2), 0, 1);

            bool invoked = false;
            tile.OnTileHeightChanged += (int n, int o) => invoked = true;
            tile.Height = 2;

            Assert.IsTrue(invoked);
        }

        [Test]
        public void TileOnTileFallIsInvoked()
        {
            var tile = new Tile(new Grid(1, 1, 2), 0, 1);

            bool invoked = false;
            tile.OnTileFall += () => invoked = true;
            tile.Height = 0;

            Assert.IsTrue(invoked);
        }
    }
}