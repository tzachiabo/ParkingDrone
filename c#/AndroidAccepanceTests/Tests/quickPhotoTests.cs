using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndroidAccepanceTests.Tests
{
    [TestClass]
    public class quickPhotoTests : BaseAcceptanceTest
    {
        [TestMethod]
        public void simpleQuickPhoto()
        {
            take_off();
            takeQuickPicture();
        }
    }
}
