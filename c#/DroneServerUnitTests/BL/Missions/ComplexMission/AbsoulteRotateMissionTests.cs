using Microsoft.VisualStudio.TestTools.UnitTesting;
using DroneServer.BL.Missions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass()]
    public class AbsoulteRotateMissionTests
    {
        [TestMethod()]
        public void is_bearing_closeTest()
        {            
            Assert.IsTrue(AbsoulteRotateMission.is_bearing_close(0, 359, 5));
        }
    }
}