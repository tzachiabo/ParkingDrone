using System;
using DroneServer.BL.Missions;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests
{
    [TestClass]
    public class MoveTest
    {
        private Comm comm = Comm.getInstance();

        private void take_off()
        {
            TakeOff take_off = new TakeOff();
            CompletionHanlder take_off_mission = comm.sendMission(take_off);
        }

        private void landing()
        {
            DroneServer.BL.Missions.StartLanding start_landing = new DroneServer.BL.Missions.StartLanding();
            CompletionHanlder start_landing_mission = comm.sendMission(start_landing);

            ConfirmLanding conf_landing = new ConfirmLanding();
            CompletionHanlder conf_landing_mission = comm.sendMission(conf_landing);
        }

        private void move(Direction direction, double distance)
        {
            MoveMission move = new MoveMission(direction, distance);
            CompletionHanlder move_mission = comm.sendMission(move);
        }

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


    }
}
