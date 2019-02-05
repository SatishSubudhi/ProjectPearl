using PearlFramework;
using NUnit.Framework;
using System.Threading;
using PearlFramework.Utilities;
using SatishTests.Utilities;


/// <summary>
/// UPDATED FOR THE STORE REDESIGN PROJECT
/// </summary>
namespace Tests
{

    [TestFixture("Chrome")]
   // [TestFixture("Edge")]
    //[TestFixture("IE11")]
    //[TestFixture("Chrome_NL")]
    //[TestFixture("Edge_NL")]
    //[TestFixture("IE11_NL")]
    //[TestFixture("Android_Chrome")]
    public class PearlAddNewUser : SetupTearDown
    {
        
        public PearlAddNewUser(string b)
        {
            KortextGlobals.platformtype = b;
        }

   
       [Category("Smoke")]
        [Test]
        public void Pearl_Add_New_User_Librarian()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Go to User Maintenance section
        //    Assert.IsTrue( Pages.PearlUserPage.GoToUserPage(), "Unable to reach Users Pages");
            //Add a new user TestUser<#>. You can pass in the user type and email address to use. Defaults to Librarian/admin@kortext.com
   //         Assert.IsTrue(Pages.PearlAddNewUser.AddUserDefault(),"Unable to create user");
            Assert.IsTrue(Pages.PearlUserPage.AddUserDefault("Librarian"), "Unable to create user");


            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
        [Category("Smoke")]
        [Test]
        public void Pearl_Add_New_User_Student()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Go to User Maintenance section
         //   Assert.IsTrue(Pages.PearlUserPage.GoToUserPage(), "Unable to reach Users Pages");
            //Add a new user TestUser<#>. You can pass in the user type and email address to use. Defaults to Librarian/admin@kortext.com
            Assert.IsTrue(Pages.PearlUserPage.AddUserDefault("Public"), "Unable to create user");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
    }
}
