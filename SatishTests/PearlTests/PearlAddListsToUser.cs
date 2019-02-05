using PearlFramework;
using NUnit.Framework;
using System.Threading;
using PearlFramework.Utilities;
using SatishTests.Utilities;


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
    public class PearlAddListToUser : SetupTearDown
    {

        public PearlAddListToUser(string b)
        {
            KortextGlobals.platformtype = b;
        }

        [Category("Smoke")]
        [Test]
        public void Can_Add_List_To_User()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

         //   Assert.IsTrue(Pages.PearlUserPage.GoToUserPage(), "Unable to reach Users Pages");

            Pages.PearlUserPage.EditUserListRoles("TestUser3", "TestList");
            Pages.LandingPage.Do_Logout();

        }
    
    }
    
}
