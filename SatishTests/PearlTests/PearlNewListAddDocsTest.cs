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
     // [TestFixture("IE11")]
    //[TestFixture("Chrome_NL")]
    //[TestFixture("Edge_NL")]
    //[TestFixture("IE11_NL")]
    //[TestFixture("Android_Chrome")]
    public class PearlNewListAddDocs : SetupTearDown
    {
        
        public PearlNewListAddDocs(string b)
        {
            KortextGlobals.platformtype = b;
        }

        //can login with correct email and password
        [Category("Smoke")]
        [Test]
        public void Pearl_New_List_Add_Documents()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue( Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
            Assert.IsTrue(Pages.PearlNewListAddDocs.AddDocs(), "Error while Adding Initial Documents to the Reading List.");

            //Pages.PearlCreateReadingList.CreateList();
            //Pages.PearlNewListAddDocs.AddDocs();

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
    }
}
