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
            Assert.AreEqual(28, width, 0.5);
            Assert.AreEqual(50, height, 0.5);
        }

        [TestMethod]
        public void fromHeight100()
        {
            PixelConverterHelper.init(100);
            double height = PixelConverterHelper.convert_height(500);
            double width = PixelConverterHelper.convert_width(500);
            Assert.AreEqual(57, width, 0.5);
            Assert.AreEqual(101, height, 0.5);
        }

        [TestMethod]
        public void fromHeight50WithNotValidPixel()
        {
            PixelConverterHelper.init(50);
            PixelConverterHelper.convert_height(2000);
            Assert.Fail();
            double width = PixelConverterHelper.convert_width(2000);
            Assert.Fail();
        }

    }
}
