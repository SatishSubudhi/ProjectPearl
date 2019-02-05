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
    public class HierarchyUserRolesTest : SetupTearDown
    {
        public HierarchyUserRolesTest(string b)
        {
            KortextGlobals.platformtype = b;
        }


        [Category("Smoke")]
        [Test]
        public void Hierarchy_Workflow_Child_Add_Users_and_Edit_User_Roles()
        {
            Utility_PreTest_hierarchy_Setup();
            List_Can_Add_User_and_Roles();
            List_Can_Edit_User_Roles();
            List_Can_Add_2nd_User();
            Utility_Post_Test_Cleanup_Of_Hierarchy();

        }

        [Category("Smoke")]
        [Test]
        public void Hierarchy_Workflow_Unit_Add_Users_and_Edit_User_Roles()
        {
            // Post_Test_Cleanup_Of_Hierarchy();
            Utility_PreTest_hierarchy_Setup();
            Unit_Can_Add_User_and_Roles();
            Unit_Can_Edit_User_Roles();
            Unit_Can_Add_2ndUser();
            Utility_Post_Test_Cleanup_Of_Hierarchy();
        }
        [Category("Utility")]
        [Test]
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
            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }


        [Category("Utility")]
        [Test]
        //basic add user to a list
        public void Unit_Can_Add_User_and_Roles()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
         //   Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlHierarchyPage.AddUserToParentUnit("AutomationListUserLib1", "AutomationUnit","Owner","Moderator"), "Unable to add user to List");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateUserRoles("AutomationListUserLib1", "AutomationUnit", "Owner", "Moderator"), "Unable to validate user roles set correctly");
            Pages.LandingPage.Do_Logout();
        }
        [Category("Utility")]
        [Test]
        //Adding a user with different roles.
        public void Unit_Can_Add_2ndUser()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
               Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
           Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlHierarchyPage.AddUserToParentUnit("AutomationListUserLib2", "AutomationUnit","Owner","Author"), "Unable to add user to Parent Unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateUserRoles("AutomationListUserLib2", "AutomationUnit", "Owner", "Author"), "Unable to validate user roles set correctly");

            Pages.LandingPage.Do_Logout();
        }


        //Editing an existing list/user - changing roles.
        [Category("Utility")]
        [Test]
        public void Unit_Can_Edit_User_Roles()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlHierarchyPage.EditUserRolesUnit("AutomationListUserLib1", "AutomationUnit", "Leader", "Editor"),"Unable to change User Roles");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateUserRoles("AutomationListUserLib1", "AutomationUnit", "Leader", "Editor"),"Unable to validate user roles set correctly");
            Pages.LandingPage.Do_Logout();
        }

        [Category("Utility")]
        [Test]
        public void Utility_Post_Test_Cleanup_Of_Hierarchy()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            Assert.IsNotNull(Pages.PearlHierarchyPage.DeleteChild("AutomationUnit", "AutomationListChild", "Do Not Touch", "Confirm"), "Unable to remove child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("AutomationUnit", "AutomationUnitChild", "Do Not Touch", "Confirm"), "Unable to remove child unit");

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("AutomationUnit", "Do Not Touch","Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");


            Pages.LandingPage.Do_Logout();
        }

        //Adding a user to a unit(Parent) adds to all subchildren
        //Adding a user to a sub unit adds to all it's sub lists.
        //Adding a user to a list does not add to parent(s)

        [Category("Utility")]
        [Test]
        public void List_Can_Add_User_and_Roles()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            //   Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

         Assert.IsTrue(Pages.PearlHierarchyPage.AddUserToChild("AutomationListUserLib1", "AutomationUnit", "AutomationListChild","Owner", "Moderator"), "Unable to add user to List");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateChildUserRoles("AutomationListUserLib1", "AutomationUnit", "AutomationListChild", "Owner", "Moderator"), "Unable to validate user roles set correctly");
            Pages.LandingPage.Do_Logout();
        }
        [Category("Utility")]
        [Test]
        public void List_Can_Add_2nd_User()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            //   Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlHierarchyPage.AddUserToChild("AutomationListUserStaff2", "AutomationUnit", "AutomationListChild", "Owner", "Author"), "Unable to add user to List");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateChildUserRoles("AutomationListUserStaff2", "AutomationUnit", "AutomationListChild", "Owner", "Author"), "Unable to validate user roles set correctly");
            Pages.LandingPage.Do_Logout();
        }
        //Editing an existing list/user - changing roles.
        [Category("Utility")]
        [Test]
        public void List_Can_Edit_User_Roles()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlHierarchyPage.EditUserRolesChild("AutomationListUserLib1", "AutomationUnit", "AutomationListChild", "Leader", "Editor"), "Unable to change User Roles");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateChildUserRoles("AutomationListUserLib1", "AutomationUnit", "AutomationListChild","Leader", "Editor"), "Unable to validate user roles set correctly");
        }


    }
}
