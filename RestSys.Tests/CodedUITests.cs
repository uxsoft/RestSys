using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
//using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Microsoft.VisualStudio.TestTools.UITest.Extension;
//using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
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

        public void newUserForTest()
        {
            I.Click("#content p a");
            //Creating user test1234
            I.Focus("#Name").Type("test1234");
            I.Focus("#Username").Type("test1234");
            I.Focus("input[name=newpassword]").Type("test1234");
            I.Focus("input[name=confirmpassword]").Type("test1234");
            I.Click("#IsWaiter");
            I.Click("input[type=submit]");
        }

        [TestMethod]
        public void NavigationTests()
        {
       /*     IISExpress.Start("RestSys");
            I.Open("http://localhost:56345/"); //TODO this isn't a global address
            I.Click("#menu li:nth-child(2) a");
            Login();*/

            //TODO rest of testing
        }

        [TestMethod]
        public void CreateNewUser()
        {

           //WORKING

           IISExpress.Start("RestSys");
            I.Open("http://localhost:56345/"); //TODO this isn't a global address
            I.Click("#menu li:nth-child(4) a");

            Login();
           
            I.Click("#content p a");

            //creating user without data
            I.Click("input[type=submit]");    

            //Creating user test1234
            I.Focus("#Name").Type("chief");
            I.Focus("#Username").Type("chief");
            I.Focus("input[name=newpassword]").Type("abcdef");
            I.Focus("input[name=confirmpassword]").Type("abcdef");
            I.Click("#IsWaiter");
            I.Click("input[type=submit]");    
     
            //is new user exists ?
            I.Assert.Text("chief").In("tbody tr:last-child > td:nth-child(1)");
            I.Assert.Text("chief").In("tbody tr:last-child > td:nth-child(2)");

            //finishing test by removing user test1234
            I.Click("tbody tr:last-child > td:last-child > a:last-child");
            I.Click("input[type=submit]");


        }
   

        [TestMethod]
        public void CreateStock() 
        {
            //WORKING

             IISExpress.Start("RestSys");
               I.Open("http://localhost:56345/"); //TODO this isn't a global address
               I.Click("#menu li:nth-child(2) a");

               Login();

               I.Click("#content p a");
  
               //test empty input
               I.Click("input[type=submit]");

              
               I.Focus("#Title").Type("STAROPRAMEN °11");
               I.Click("input[type=submit]");

               //is new stock exists ?
               I.Assert.Text("STAROPRAMEN °11").In("tbody tr:last-child > td:nth-child(1)");

               //finishing test by removing stock STAROPRAMEN °11
               I.Click("tbody tr:last-child > td:last-child > a:last-child");
               I.Click("input[type=submit]");

        }

        [TestMethod]
        public void EditProduct()
        {
            //WORKING
            
            IISExpress.Start("RestSys");
            I.Open("http://localhost:56345/"); //TODO this isn't a global address
            I.Click("#menu li:nth-child(2) a");
    
            Login();

            //before test (create stock)
            I.Click("#content p a");
            I.Focus("#Title").Type("STAROPRAMEN °11");
            I.Click("input[type=submit]");
  
            
            I.Click("#menu li:nth-child(1) a");
            
            //choise edit on last
            I.Click("tbody tr:last-child > td:last-child > a:nth-child(1)");

            //add title
            I.Focus("#Title").Press("{DEL 20}");
            I.Focus("#Title").Type("STAROPRAMEN 11° 0,5l");

            //add Stock STAROPRAMEN 11°
            I.Select("STAROPRAMEN °11").From(".selAddStock");
            I.Focus("#amount").Press("{DEL 10}");
            I.Focus("#amount").Type("0.5");
            I.Click("button[type=button]");

            //add correct count
            I.Focus("#amount").Press("{BACKSPACE 10}");
            I.Focus("#amount").Type("1");
            I.Click("button[type=button]");

            //save all
            I.Click("input[type=submit]");

            //control of adding informations
            I.Click("tbody tr:last-child > td:last-child > a:nth-child(2)");

           //title asserttesttest
            I.Assert.Text("STAROPRAMEN 11°0,5l").In("#detailTitle");
            //Stock assert
            I.Assert.Text("STAROPRAMEN °11 1").In("#detailStock > li:last-child");

            //after test (remove stock)
            I.Click("#menu li:nth-child(2) a");
            I.Click("tbody tr:last-child > td:last-child > a:last-child");
            I.Click("input[type=submit]");
            
        }

        [TestMethod]
        public void CreateProduct() {

            //WORKING

            IISExpress.Start("RestSys");
            I.Open("http://localhost:56345/"); //TODO this isn't a global address
            I.Click("#menu li:nth-child(1) a");

            Login();

            I.Click("#content p a");

            //save without any input
            I.Click("input[type=submit]");

            //check for warning messages !!!!!!!!!!

            // add title
            I.Focus("#Title").Type("STAROPRAMEN 11°0,5l");
            I.Focus("#Price").Type("25");

            //save
            I.Click("input[type=submit]");

            //is new product exists ?
            I.Assert.Text("STAROPRAMEN 11°0,5l").In("tbody tr:last-child > td:nth-child(1)");
            I.Assert.Text("25").In("tbody tr:last-child > td:nth-child(3)");

            //finishing test by removing product STAROPRAMEN 11°0,5l
            I.Click("tbody tr:last-child > td:last-child > a:last-child");
            I.Click("input[type=submit]");

        }

        [TestMethod]
        public void EditUser() 
        {
            //WORKING

            IISExpress.Start("RestSys");
            I.Open("http://localhost:56345/"); //TODO this isn't a global address
            I.Click("#menu li:nth-child(4) a");

            Login();

            newUserForTest();//create user test1234 with pass test1234

           // I.Click("#menu li:nth-child(4) a");

            I.Click("tbody tr:last-child > td:last-child > a:nth-child(1)");

            //change to null
            I.Focus("#Name").Press("{DEL 10} ");
            I.Focus("#Username").Press("{DEL 10}");
            I.Click("input[type=submit]");

            //test after setting all null
            //poresit kontrolu
          //  I.Assert.Text("The Name field is required.").In(".form-horizontal > div:nth-child(2) > div > span");
         //   I.Assert.Text(" The Username field is required.").In(".form-horizontal > div:nth-child(3) > div > span");

           //set new data of user
            I.Focus("#Name").Type("John T");
            I.Focus("#Username").Type("testcase");
            I.Focus("input[name=newpassword]").Type("12345");
            I.Focus("input[name=confirmpassword]").Type("12345");
            I.Click("#IsWaiter"); //set from true to false
            I.Click("input[type=submit]");

          //  is user name and username has changed ?
            I.Assert.Text("John T").In("tbody tr:last-child > td:nth-child(1)");
            I.Assert.Text("testcase").In("tbody tr:last-child > td:nth-child(2)");
           
            // removing user test12345
            I.Click("tbody tr:last-child > td:last-child > a:last-child");
            I.Click("input[type=submit]"); 
           
        } 

        public void Login()
        {
            //Login
            //WORKING

            I.Expect.Exists("#username").Exists("#password");

            I.Focus("#username").Type("test");
            I.Focus("#password").Type("test");
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
