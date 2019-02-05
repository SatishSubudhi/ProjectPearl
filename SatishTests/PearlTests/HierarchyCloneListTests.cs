using PearlFramework;
using NUnit.Framework;
using System.Threading;
using PearlFramework.Utilities;
using SatishTests.Utilities;


namespace Tests
{

    [TestFixture("Chrome")]
   // [TestFixture("Edge")]
     // [TestFixture("IE11")]
    //[TestFixture("Chrome_NL")]
    //[TestFixture("Edge_NL")]
    //[TestFixture("IE11_NL")]
    //[TestFixture("Android_Chrome")]
    public class HierarchyCloneLists : SetupTearDown
    {

        public HierarchyCloneLists(string b)
        {
            KortextGlobals.platformtype = b;
        }

        //can login with correct email and password
        [Category("Smoke")]
        [Test]
        public void Hierarchy_Clone_List_and_Unit()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            //Assert.IsTrue(Pages.IsAt(PageName.LandingPage), "Cannot get to landing page");
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);

            //Pages.LandingPage.ClickOnMenu_HierarchyBtn();

            Assert.IsTrue(Pages.PearlHierarchyPage.CloneListUnit(), "Unable to Clone list/unit.");

            //Logs User Out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");


        }
    }
}
