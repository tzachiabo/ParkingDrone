using System;
using DroneServer.SharedClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject.SharedClasses
{
    [TestClass]
    public class PixelConverterTests
    {
        [TestMethod]
        public void fromHeight50()
        {
            PixelConverterHelper.init(50);
            double height=PixelConverterHelper.convert_height(500);
            double width = PixelConverterHelper.convert_width(500);
            Assert.AreEqual(21, width, 0.5);
            Assert.AreEqual(23, height, 0.5);
        }

        [TestMethod]
        public void fromHeight100()
        {
            PixelConverterHelper.init(100);
            double height = PixelConverterHelper.convert_height(500);
            double width = PixelConverterHelper.convert_width(500);
            Assert.AreEqual(43, width, 0.5);
            Assert.AreEqual(45, height, 0.5);
        }
    }
}
