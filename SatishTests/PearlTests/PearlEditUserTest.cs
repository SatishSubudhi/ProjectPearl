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
    public class PearlEditUser : SetupTearDown
    {

        public PearlEditUser(string b)
        {
            KortextGlobals.platformtype = b;
        }

        //can login with correct email and password
        [Category("Smoke")]
        [Test]
        public void Can_Edit_User_Type()
        {
             //Assert you are at the landing page and then log in and validate you logged in ok
              Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
              Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");


            //Go to User Maintenance section
         //   Assert.IsTrue(Pages.PearlUserPage.GoToUserPage(), "Unable to reach Users Pages");
            //Cleanup tells EditUser_Role function to restore user back to original settings after validating change happened.
            Assert.IsTrue(Pages.PearlUserPage.EditUser_Type("ASusanUser1", "Public"),"Unable to edit User Type");

            //cleanup by restoring back to original values
            Assert.IsTrue(Pages.PearlUserPage.EditUser_Type("ASusanUser1", "Admin"), "Unable to restore User Type to original values");
            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
             

        }

        [Category("Smoke")]
        [Test]
        public void Can_Edit_User_Info_email()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlUserPage.GoToUserPage(), "Unable to reach Users Pages");

            //      //Cleanup tells EditUser_Email function to restore user back to original settings after validating change happened.
            Assert.IsTrue(Pages.PearlUserPage.EditUser_Email("ASusanUser1", "newEmail@rogers.com"),"Unable to edit User Email");

            //clean up after yourself
            Assert.IsTrue(Pages.PearlUserPage.EditUser_Email("ASusanUser1", "admin@kortext.com"), "Unable to restore User Email");
            Pages.LandingPage.Do_Logout();

        }
        [Category("Smoke")]
        [Test]
        public void Can_Edit_User_Info_Loginname()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlUserPage.GoToUserPage(), "Unable to reach Users Pages");

            //      //Cleanup tells EditUser_Email function to restore user back to original settings after validating change happened.
            Assert.IsTrue(Pages.PearlUserPage.EditUser_LoginName("ASusanUser1", "testinguser1"), "Unable to edit User Login Name");

            //clean up after yourself
            Assert.IsTrue(Pages.PearlUserPage.EditUser_LoginName("ASusanUser1", "ASusanUser1"), "Unable to restore User Login Name");
            Pages.LandingPage.Do_Logout();

        }

        [Category("Smoke")]
        [Test]
        public void Can_Edit_User_Info_Fullname()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Assert.IsTrue(Pages.PearlUserPage.GoToUserPage(), "Unable to reach Users Pages");

            //      //Cleanup tells EditUser_Email function to restore user back to original settings after validating change happened.
            Assert.IsTrue(Pages.PearlUserPage.EditUser_FullName("ASusanUser1", "testinguser1"), "Unable to edit User Full Name");

            //clean up after yourself
            Assert.IsTrue(Pages.PearlUserPage.EditUser_FullName("testinguser1", "ASusanUser1"), "Unable to restore User Full Name");
            Pages.LandingPage.Do_Logout();

        }

        [Category("Smoke")]
        [Test]
        public void Can_Delete_User()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlUserPage.DeleteUser(), "Unable to Delete User");

            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]
        public void Can_Archive_User()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlUserPage.ArchiveUser(), "Unable to Archive User");

            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]
        public void Can_Restore_User()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlUserPage.RestoreUser(), "Unable to Restore User");

            Pages.LandingPage.Do_Logout();
        }

        [Category("Smoke")]
        [Test]
        public void Can_User_Password_Reset()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlUserPage.UserPwdReset(), "Unable to Reset User Password");

            Pages.LandingPage.Do_Logout();
        }
    }
    
}
