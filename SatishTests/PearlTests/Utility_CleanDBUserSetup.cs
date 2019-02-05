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
    class Utility_CleanDBUserSetup : SetupTearDown
    {
        public Utility_CleanDBUserSetup(string b)
        {
            KortextGlobals.platformtype = b;
        }

        /*********************************************************************************************************************************************
   *                                  NOTE!!!!!!!
   * 
   *                                   These two functions/Tests to be run only after a clean DB has been installed. 
   *                                    It sets up a Test User and a hierarchy for automation tests that is not removed 
   *                                    and can be used for other automation tests on users.
   *                                    
   ********************************************************************************************************************************************/
        [Category("Utility")]
        [Test]
        public void Clean_DB_User_Setup()
        {

            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Go to User Maintenance section
            //     Assert.IsTrue(Pages.PearlUserPage.GoToUserPage(), "Unable to reach Users Pages");
            Assert.IsTrue(Pages.PearlUserPage.AddUserDefault("Librarian", "AutomationListUserLib", "susanm@kortext.com"), "Unable to create user");
            Assert.IsTrue(Pages.PearlUserPage.AddUserDefault("Librarian", "AutomationListUserLib", "susanm@kortext.com"), "Unable to create user");
            Assert.IsTrue(Pages.PearlUserPage.AddUserDefault("Staff", "AutomationListUserStaff", "susanm@kortext.com"), "Unable to create user");
            Assert.IsTrue(Pages.PearlUserPage.AddUserDefault("Staff", "AutomationListUserStaff", "susanm@kortext.com"), "Unable to create user");
            Assert.IsTrue(Pages.PearlUserPage.AddUserDefault("Public", "AutomationListUserPub", "susanm@kortext.com"), "Unable to create user");
            Assert.IsTrue(Pages.PearlUserPage.AddUserDefault("Public", "AutomationListUserPub", "susanm@kortext.com"), "Unable to create user");
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("AutomationUnit", "Do Not Touch"), "Unable to create Unit as requested");
            //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationListChild", "Do Not Touch", "List"), "Unable to add child unit");
            // Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("AutomationUnit", "AutomationListChild", "Do Not Touch"), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationUnitChild", "Do Not Touch", "Unit"), "Unable to add child unit");
            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }

    }
}
