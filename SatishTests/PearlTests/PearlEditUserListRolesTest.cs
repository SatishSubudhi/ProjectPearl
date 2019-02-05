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
    public class PearlEditUserListRoles : SetupTearDown
    {

        public PearlEditUserListRoles(string b)
        {
            KortextGlobals.platformtype = b;
        }

        //can login with correct email and password
        [Category("Smoke")]
        [Test]
        public void Can_Add_User_to_List_nonblank_roles()
        {
             //Assert you are at the landing page and then log in and validate you logged in ok
              Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
              Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            //Only Passing the Perm and Non-Perm Roles. User and List would be created in the workflow. so no need to pass it separately.
            Assert.IsTrue(Pages.PearlUserPage.AddUsertoList("Leader", "Editor"),"Unable to Add User to List");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
             

        }


        [Category("Smoke")]
        [Test]
        public void Can_Add_User_to_List_blank_perm()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            //Only Passing the Perm and Non-Perm Roles. User and List would be created in the workflow. so no need to pass it separately.
            Assert.IsTrue(Pages.PearlUserPage.AddUsertoList("Owner", ""), "Unable to Add User to List");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();


        }

        [Category("Smoke")]
        [Test]
        public void Can_Add_User_to_List_blank_nonperm()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            //Only Passing the Perm and Non-Perm Roles. User and List would be created in the workflow. so no need to pass it separately.
            Assert.IsTrue(Pages.PearlUserPage.AddUsertoList("", "Moderator"), "Unable to Add User to List");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();


        }

       
        [Category("Smoke")]
        [Test]
        public void Can_update_User_List_Roles_nonblank_roles()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            Assert.IsTrue(Pages.PearlUserPage.UpdateUserListRoles("Leader", "Owner", "Editor", "Author"), "Unable to update User List Roles");

            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]
        public void Can_update_User_List_Roles_blank_perm()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            Assert.IsTrue(Pages.PearlUserPage.UpdateUserListRoles("Leader", "Leader", "Editor", ""), "Unable to update User List Roles");

            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]
        public void Can_update_User_List_Roles_blank_nonperm()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            Assert.IsTrue(Pages.PearlUserPage.UpdateUserListRoles("Leader", "", "Editor", "Editor"), "Unable to update User List Roles");

            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]
        public void Can_update_User_List_Roles_blank_roles()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            Assert.IsTrue(Pages.PearlUserPage.UpdateUserListRoles("Leader", "", "Editor", ""), "Unable to update User List Roles");

            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]
        public void Can_update_User_List_Roles_blank_perm_changed_nonperm()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            Assert.IsTrue(Pages.PearlUserPage.UpdateUserListRoles("Leader", "Owner", "Editor", ""), "Unable to update User List Roles");

            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]
        public void Can_update_User_List_Roles_blank_nonperm_changed_perm()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");

            Assert.IsTrue(Pages.PearlUserPage.UpdateUserListRoles("Leader", "", "Editor", "Moderator"), "Unable to update User List Roles");

            Pages.LandingPage.Do_Logout();
        }
    }
    
}
