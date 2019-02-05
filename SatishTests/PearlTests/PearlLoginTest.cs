using PearlFramework;
using NUnit.Framework;
using System.Threading;
using PearlFramework.Utilities;
using SatishTests.Utilities;
using System;



/// <summary>
/// 
/// </summary>
namespace Tests
{

    [TestFixture("Chrome")]
   // [TestFixture("Edge")]
     // [TestFixture("IE11")]
    //[TestFixture("Chrome_NL")]
    //[TestFixture("Edge_NL")]
    //[TestFixture("IE11_NL")]
    //[TestFixture("Android_Chrome")]
    public class PearlLogin : SetupTearDown
    {

        public PearlLogin(string b)
        {
            Console.WriteLine(b);
            KortextGlobals.platformtype = b;


        }


        //can login with correct email and password and log back out
        [Category("Smoke")]
        [Test]
        public void PEARL_login()
        {

            //Assert you are at the landing page and then log in
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));

            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);

            //validates if the user is logged in
            Assert.IsTrue( Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            Pages.LandingPage.Do_Logout();
            //validates if the user is logged out
            Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
        /*
        //incorrect password entered 5 times locks the account
        [Test]
        public void PEARL_Store_Credentials_InvalidPasswords_5Times_Lock()
        {
            Pages.HeaderPage.SelectLogin();
            //Thread.Sleep(Globals.s);
            Assert.IsTrue(Pages.PearlLoginPage.IsAtPage(), "Couldn't reach login page");
            //Thread.Sleep(Globals.s);
            if (KortextGlobals.AlreadyLockedOut == false)
            {
                for (int i = 0; i <= 4; i++)
                {
                    Pages.PearlLoginPage.LoginAs(KortextGlobals.username_forlock, "justfor");
                    Thread.Sleep(KortextGlobals.s);
                    Assert.AreEqual(Pages.PearlLoginPage.GetMessage(), "The credentials provided are incorrect");
                    Thread.Sleep(KortextGlobals.s);
                }
                Pages.PearlLoginPage.LoginAs(KortextGlobals.username_forlock, KortextGlobals.password_forlock);
                Thread.Sleep(KortextGlobals.s);
                Assert.AreEqual(Pages.PearlLoginPage.GetMessage(), "Customer is locked out");
                Thread.Sleep(KortextGlobals.s);
                KortextGlobals.AlreadyLockedOut = true;
            }
            else
            {
                Pages.PearlLoginPage.LoginAs(KortextGlobals.username_forlock, KortextGlobals.password_forlock);
                Thread.Sleep(KortextGlobals.s);
                Assert.AreEqual(Pages.PearlLoginPage.GetMessage(), "Customer is locked out");
                Thread.Sleep(KortextGlobals.s);
            }
        }
        */
        //cannot login with incorrect password and validate error generated and validate the app stays on the same page
        [Category("Full")]
        [Test]
        public void PEARL_InvalidPasswords_Give_Error()
        {
            //Assert you are at the landing page.
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage),"Couldn't reach Landing page");
           Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, "invalid");
      //      Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);

            Assert.AreEqual(Pages.PearlLoginPage.GetMessage(), "Wrong username or password", "did not get the right error message");
            Assert.IsTrue(Pages.PearlLoginPage.IsAtPage(), "Error: Invalid Login should have remained at login page");

        }

        //cannot login with blank password and validate error generated  and validate the app stays on the same page
        [Category("Full")]
        [Test]
        public void PEARL_Blank_Passwords_Give_Error()
        {
            //Assert you are at the landing page.
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage),"Couldn't reach Landing page");
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, "");
            Assert.AreEqual(Pages.PearlLoginPage.GetMessage(), "Wrong username or password", "did not get the right error message");
            Assert.IsTrue(Pages.PearlLoginPage.IsAtPage(), "Error: Invalid Login should have remained at login page");
        }
        //cannot login with blank username and validate error generated  and validate the app stays on the same page
        [Category("Full")]
        [Test]
        public void PEARL_Blank_username_Give_Error()
        {
            //Assert you are at the landing page.
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage), "Couldn't reach Landing page");
            Pages.PearlLoginPage.LoginAs("",KortextGlobals.Pearl_password);
            Assert.AreEqual(Pages.PearlLoginPage.GetMessage(), "Wrong username or password", "did not get the right error message");
            Assert.IsTrue(Pages.PearlLoginPage.IsAtPage(), "Error: Invalid Login should have remained at login page");
        }
        [Category("Full")]
        [Test]
        public void PEARL_Blank_credentials_Give_Error()
        {
            //Assert you are at the landing page.
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage), "Couldn't reach Landing page");
            Pages.PearlLoginPage.LoginAs("", "");
            Assert.AreEqual(Pages.PearlLoginPage.GetMessage(), "Wrong username or password", "did not get the right error message");
            Assert.IsTrue(Pages.PearlLoginPage.IsAtPage(), "Error: Invalid Login should have remained at login page");
        }
        [Category("Full")]
        [Test]
        public void PEARL_invalid_credentials_Give_Error()
        {
            //Assert you are at the landing page.
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage), "Couldn't reach Landing page");
            Pages.PearlLoginPage.LoginAs("blah", "blah");
            Assert.AreEqual(Pages.PearlLoginPage.GetMessage(), "Wrong username or password", "did not get the right error message");
            Assert.IsTrue(Pages.PearlLoginPage.IsAtPage(), "Error: Invalid Login should have remained at login page");
        }
    }
}
