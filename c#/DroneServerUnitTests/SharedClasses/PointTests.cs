using System;
using System.Collections.Generic;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.SharedClasses
{
    [TestClass]
    public class PointTests
    {
        [TestMethod]
        public void closePoints()
        {
            Point p1 = new Point(10,10,0);
            Point p2 = new Point(11, 11, 0);
            Point p3 = new Point(10, 10, 0);
            Assert.IsTrue(p1.is_close(p2));
            Assert.IsTrue(p2.is_close(p1));
            Assert.IsTrue(p1.is_close(p3));
        }

        [TestMethod]
        public void notClosePoints()
        {
            Point p1 = new Point(10, 10, 0);
            Point p2 = new Point(14, 14, 0);
            Assert.IsFalse(p1.is_close(p2));
            Assert.IsFalse(p2.is_close(p1));
        }

        [TestMethod]
        public void closePointsWithError()
        {
            Point p1 = new Point(10, 10, 0);
            Point p2 = new Point(13, 14, 0);
            Assert.IsFalse(p1.is_close(p2,5));
            Assert.IsFalse(p2.is_close(p1,5));
        }

        [TestMethod]
        public void notClosePointsWithError()
        {
            Point p1 = new Point(10, 10, 0);
            Point p2 = new Point(13, 14, 0);
            Assert.IsFalse(p1.is_close(p2, 4));
            Assert.IsFalse(p2.is_close(p1, 4));
        }

        [TestMethod]
        public void getMoves()
        {
            Point p1 = new Point(10, 10, 0);
            Point p2 = new Point(13, 14, 0);
            List<KeyValuePair<Direction, double>> moves = p1.get_moves(p2);
            foreach (KeyValuePair<Direction, double> move in moves)
            {
                if ((move.Key == Direction.left) || (move.Key == Direction.right))
                {
                    Assert.IsTrue(move.Key == Direction.right);
                    Assert.IsTrue(move.Value == 3);
                }
                else
                {
                    Assert.IsTrue(move.Key == Direction.backward);
                    Assert.IsTrue(move.Value == 4);
                }
            }
        }
        [TestMethod]
        public void getMovesDiffrentPlace()
        {
            Point p1 = new Point(14, 13, 0);
            Point p2 = new Point(10, 10, 0);
            List<KeyValuePair<Direction, double>> moves = p1.get_moves(p2);
            foreach (KeyValuePair<Direction, double> move in moves)
            {
                if ((move.Key == Direction.left) || (move.Key == Direction.right))
                {
                    Assert.IsTrue(move.Key == Direction.left);
                    Assert.IsTrue(move.Value == 4);
                }
                else
                {
                    Assert.IsTrue(move.Key == Direction.forward);
                    Assert.IsTrue(move.Value == 3);
                }
            }
        }

        [TestMethod]
        public void getMovesSameX()
        {
            Point p1 = new Point(14, 13, 0);
            Point p2 = new Point(14, 10, 0);
            List<KeyValuePair<Direction, double>> moves = p1.get_moves(p2);
            foreach (KeyValuePair<Direction, double> move in moves)
            {
                if ((move.Key == Direction.left) || (move.Key == Direction.right))
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.IsTrue(move.Key == Direction.forward);
                    Assert.IsTrue(move.Value == 3);
                }
            }
        }

        [TestMethod]
        public void getMovesSameY()
        {
            Point p1 = new Point(14, 13, 0);
            Point p2 = new Point(10, 13, 0);
            List<KeyValuePair<Direction, double>> moves = p1.get_moves(p2);
            foreach (KeyValuePair<Direction, double> move in moves)
            {
                if ((move.Key == Direction.left) || (move.Key == Direction.right))
                {
                    Assert.IsTrue(move.Key == Direction.left);
                    Assert.IsTrue(move.Value == 4);
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void getMovesClosePoints()
        {
            Point p1 = new Point(10.5, 10.7, 0);
            Point p2 = new Point(10, 10, 0);
            List<KeyValuePair<Direction, double>> moves = p1.get_moves(p2);
            foreach (KeyValuePair<Direction, double> move in moves)
            {
                if ((move.Key == Direction.left) || (move.Key == Direction.right))
                {
                    Assert.Fail();
                }
                else
                {
                    Assert.Fail();
                }
            }
        }

    }
}
