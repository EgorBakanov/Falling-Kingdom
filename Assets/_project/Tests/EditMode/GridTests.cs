using NUnit.Framework;
using Nara.MFGJS2020.Core;

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
        public void GridTileIsNotNullWithinCorrectRange()
        {
            var grid = new Grid(3,4);
            
            Assert.NotNull(grid[2]);
            Assert.NotNull(grid[1,2]);
        }

        [Test]
        public void GridTileIsNullWithinWrongRange()
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
        public void GridTileHeightWithinRange()
        {
            var grid = new Grid(1,2,2,new []{-1,3});
            
            Assert.GreaterOrEqual(grid[0].Height,0);
            Assert.LessOrEqual(grid[1].Height,grid.MaxHeight);
        }

        [Test]
        public void GridIndexToCoordinateReturnCorrectValues()
        {
            var grid = new Grid(2,3);

            (int x, int y) = grid.IndexToCoordinate(3);
            
            Assert.AreEqual(1,x);
            Assert.AreEqual(0,y);
        }
    }
}
