using PearlFramework;
using NUnit.Framework;
using System.Threading;
using PearlFramework.Utilities;
using SatishTests.Utilities;
using System;

namespace Tests
{
    [TestFixture("Chrome")]
    // [TestFixture("Edge")]
    // [TestFixture("IE11")]
    //[TestFixture("Chrome_NL")]
    //[TestFixture("Edge_NL")]
    //[TestFixture("IE11_NL")]
    //[TestFixture("Android_Chrome")]

    class HierarchyMoveListsTests : SetupTearDown
    {
        public HierarchyMoveListsTests(string b)
        {
            KortextGlobals.platformtype = b;
        }
      //  [Category("Utility")]
     //   [Test]
        public void Utility_PreTest_hierarchy_Setup()
        {

            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");

            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("AutomationUnit", "Do Not Touch"));
            //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationListChild", "Do Not Touch", "List"), "Unable to add child unit");
            //    Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("AutomationUnit", "AutomationListChild", "Do Not Touch"), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationUnitChild", "Do Not Touch", "Unit"), "Unable to add child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationListChild2", "Do Not Touch", "List"), "Unable to add child unit");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }
      //  [Category("Utility")]
      //  [Test]
        public void Utility_Post_Test_Cleanup_Of_Hierarchy()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            Assert.IsNotNull(Pages.PearlHierarchyPage.DeleteChild("AutomationUnit", "AutomationListChild", "Do Not Touch", "Confirm"), "Unable to remove child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("AutomationUnit", "AutomationUnitChild", "Do Not Touch", "Confirm"), "Unable to remove child unit");

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("AutomationUnit", "Do Not Touch", "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");


            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
     [Test]
        public void Can_Move_List_Inside_Unit()
        {
        //    Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //    Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to create this Tree. It already is created");
//tree doesn't already exist so build a new one.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("AutomationUnit", "Do Not Touch"));
            //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationListChild", "Do Not Touch", "List"), "Unable to add child unit");
            //    Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("AutomationUnit", "AutomationListChild", "Do Not Touch"), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationUnitChild", "Do Not Touch", "Unit"), "Unable to add child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationListChild2", "Do Not Touch", "List"), "Unable to add child unit");

            //Move to the subunit
            Assert.IsTrue(Pages.PearlHierarchyPage.MoveListToSubUnit("AutomationUnit", "AutomationListChild2", "AutomationUnitChild"), "Unable to move list to subunit");

            //Cleanup the tree
            Pages.PearlHierarchyPage.DeleteWholeTree("AutomationUnit");
            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]  
        public void Can_Move_Whole_Tree()
        {
            //    Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //    Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to create this Tree. It already is created");
            //tree doesn't already exist so build a new one.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("AutomationUnit", "Do Not Touch"));
            //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationListChild", "Do Not Touch", "List"), "Unable to add child unit");
            //    Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("AutomationUnit", "AutomationListChild", "Do Not Touch"), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationUnitChild", "Do Not Touch", "Unit"), "Unable to add child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit", "AutomationListChild2", "Do Not Touch", "List"), "Unable to add child unit");

            //Move tree to top of the hierarchy
         //   Assert.IsTrue(Pages.PearlHierarchyPage.MoveWholeTreeToAPosition("AutomationUnit", "Top" ),"Unable to move list to top");
            Assert.IsTrue(Pages.PearlHierarchyPage.MoveWholeTreeToAPosition("AutomationUnit","Middle"),"Unable to move list to the middle position");
           // Assert.IsTrue(Pages.PearlHierarchyPage.MoveWholeTreeToAPosition("AutomationUnit", "End"), "Unable to move list to the end position");
        }
        
        [Category("Smoke")]
        [Test]
        public void Hierarchy_Move_List_and_Unit()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            //Assert.IsTrue(Pages.IsAt(PageName.LandingPage), "Cannot get to landing page");
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);

            //Pages.LandingPage.ClickOnMenu_HierarchyBtn();

            //Move tree to top of the hierarchy
            //   Assert.IsTrue(Pages.PearlHierarchyPage.MoveWholeTreeToAPosition("AutomationUnit", "Top" ),"Unable to move list to top");
            Assert.IsTrue(Pages.PearlHierarchyPage.MoveListUnit(), "Unable to move list/unit.");
        }
    }
}
