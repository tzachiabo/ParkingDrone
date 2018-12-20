using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace missionManagerAccepanceTests.Tests
{
    [TestClass]
    public class MoveTest : BaseAcceptanceTest
    {
        [TestMethod]
        public void moveLeft()
        {
            take_off();
            move(Direction.left, 5);
            landing();
        }

        [TestMethod]
        public void moveRight()
        {
            take_off();
            move(Direction.right, 5);
            landing();
        }

        [TestMethod]
        public void movebackward()
        {
            take_off();
            move(Direction.backward, 5);
            landing();
        }

        [TestMethod]
        public void moveforward()
        {
            take_off();
            move(Direction.forward, 5);
            landing();
        }

        [TestMethod]
        public void moveUp()
        {
            take_off();
            move(Direction.up, 5);
            landing();
        }

        [TestMethod]
        public void moveDown()
        {
            take_off();
            move(Direction.up, 5);
            move(Direction.down, 5);
            landing();
        }

        [TestMethod]
        public void DoSqure()
        {
            take_off();
            move(Direction.up, 5);
            move(Direction.left, 5);
            move(Direction.forward, 5);
            move(Direction.right, 5);
            move(Direction.backward, 5);
            move(Direction.down, 5);
            landing();
        }


        [TestMethod]
        public void moveRotate()
        {
            take_off();
            move(Direction.rotate, 90);
            landing();
        }
    }
}
