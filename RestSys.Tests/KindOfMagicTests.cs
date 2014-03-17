using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSys.Models;

namespace RestSys.Tests
{
    [TestClass]
    public class KindOfMagicTests
    {
        [TestMethod]
        public void MagicTest()
        {
            RSStyle e = new RSStyle();
            bool eventReceived = false;
            e.PropertyChanged += (a, b) => eventReceived = true;
            e.Title = "ChangedTitle";
            Assert.IsTrue(eventReceived);
        }
    }
}
