using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using Nara.MFGJS2020;

namespace Tests
{
    public class GridTests
    {
        [Test]
        public void GridCorrectSize()
        {
            var grid = new Grid(3,4);
            
            Assert.AreEqual(3,grid.SizeX);
            Assert.AreEqual(4,grid.SizeY);
        }

        [Test]
        public void GridSizeGreaterThanZero()
        {
            var grid = new Grid(-1,-2);
            
            Assert.Greater(grid.SizeX,0);
            Assert.Greater(grid.SizeY,0);
        }

        [Test]
        public void GridCellIsNotNullWithinCorrectRange()
        {
            var grid = new Grid(3,4);
            
            Assert.NotNull(grid[2]);
            Assert.NotNull(grid[1,2]);
        }

        [Test]
        public void GridCellIsNullWithinWrongRange()
        {
            var grid = new Grid(3,4);
            
            Assert.IsNull(grid[-1]);
            Assert.IsNull(grid[-2,-4]);
            Assert.IsNull(grid[15]);
            Assert.IsNull(grid[2,7]);
        }

        [Test]
        public void GridMaxHeightGreaterThanZero()
        {
            var grid = new Grid(1,1,-1);
            
            Assert.Greater(grid.MaxHeight,0);
        }

        [Test]
        public void GridCellHeightWithinRange()
        {
            var grid = new Grid(1,2,2,new []{-1,3});
            
            Assert.GreaterOrEqual(grid[0].Height,0);
            Assert.LessOrEqual(grid[1].Height,grid.MaxHeight);
        }
    }
}
