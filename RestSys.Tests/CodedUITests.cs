﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using FluentAutomation;
using Trackyon.CUIT.Helpers;


namespace RestSys.Tests
{
    [TestClass]
    public class CodedUITests : FluentTest
    {
        public CodedUITests()
        {
            SeleniumWebDriver.Bootstrap(SeleniumWebDriver.Browser.Firefox);
        }

        [TestMethod]
        public void CodedUITest1()
        {
            IISExpress.Start("RestSys");
            I.Open("http://localhost:52844/"); //TODO this isn't a global address
            I.Click("#menu li:nth-child(2) a");
            Login();


        }

        public void Login()
        {
            //Login
            I.Expect.Exists("#username").Exists("#password");

            I.Focus("#username").Type("uxsoft");
            I.Focus("#password").Type("TotallySecretPassword");
            I.Click("input[type=submit]");
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }
    }
}