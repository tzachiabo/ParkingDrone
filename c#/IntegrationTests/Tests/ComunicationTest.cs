using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntegrationTests
{
    [TestClass]
    public class ComunicationTests : BaseIntegrationTest
    {
        [TestMethod]
        public void drone_disconnect_and_reconnect()
        {
            drone.close_drone();
            drone.start_drone();
            drone.close_drone();
            drone.start_drone();
        }
    }
}
