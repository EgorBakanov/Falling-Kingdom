using NUnit.Framework;
using UnityEngine;
using Grid = Nara.MFGJS2020.Core.Grid;

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

        [Test]
        public void GridCoordinateToIndexReturnCorrectValue()
        {
            var grid = new Grid(2,3);

            int index = grid.CoordinateToIndex(1, 0);
            
            Assert.AreEqual(3,index);
        }

        [Test]
        public void GridIsCorrectIndexReturnCorrectValue()
        {
            var grid = new Grid(2,3);
            
            Assert.IsTrue(grid.IsCorrectIndex(3));
            Assert.IsFalse(grid.IsCorrectIndex(-2));
            Assert.IsFalse(grid.IsCorrectIndex(10));
        }

        [Test]
        public void GridIsCorrectCoordinateReturnCorrectValue()
        {
            var grid = new Grid(2,3);
            var correct = new Vector2Int(1,0);
            var wrong1 = new Vector2Int(-1,0);
            var wrong2 = new Vector2Int(10,0);
            var wrong3 = new Vector2Int(1,-1);
            var wrong4 = new Vector2Int(1,10);
            var wrong5 = new Vector2Int(-1,-1);
            var wrong6 = new Vector2Int(10,10);
            
            Assert.IsTrue(grid.IsCorrectCoordinate(correct));
            Assert.IsTrue(grid.IsCorrectCoordinate(correct.x,correct.y));
            
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong1));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong1.x,wrong1.y));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong2));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong2.x,wrong2.y));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong3));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong3.x,wrong3.y));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong4));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong4.x,wrong4.y));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong5));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong5.x,wrong5.y));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong6));
            Assert.IsFalse(grid.IsCorrectCoordinate(wrong6.x,wrong6.y));
        }
    }
}
