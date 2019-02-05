using PearlFramework;
using NUnit.Framework;
using System.Threading;
using PearlFramework.Utilities;
using SatishTests.Utilities;


/// <summary>
//Hierarchy view - Add new Units, Lists and child units. Edit metadata for lists and units. Delete 1 levels of units and children. Suppress Units/Lists.
/// </summary>
/// 
namespace Tests
{

    [TestFixture("Chrome")]
    // [TestFixture("Edge")]
    

    //[TestFixture("IE11")]
    //[TestFixture("Chrome_NL")]
    //[TestFixture("Edge_NL")]
    //[TestFixture("IE11_NL")]
    //[TestFixture("Android_Chrome")]
    public class HierarchyTestsBuildTree : SetupTearDown
    {

        public HierarchyTestsBuildTree(string b)
        {
            KortextGlobals.platformtype = b;
        }


        [Category("Smoke")]
        [Test]
        public void Can_Add_Unnamed_New_Unit()//kept for validation of Satish's code.
        {   //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage), "Cannot get to landing page");
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Pages.PearlCreateReadingList.CreateList();
            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }

        [Category("Smoke")]
        [Test]
        public void Can_Add_New_Named_Unit()

        {  //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //   Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTest3", KortextGlobals.UnitCourseIdentifierText));
            Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTest3", KortextGlobals.UnitCourseIdentifierText), "Unable to find Unit requested");
            //clean up after test by deleting the list

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTest3", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to cleanup after test");
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTest3", KortextGlobals.UnitCourseIdentifierText), "Error: Unit was NOT deleted");

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }


        [Category("Smoke")]
        [Test]
        public void Can_Delete_Unit_with_No_Lists()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);

            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            //Create a list so you can delete it.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText));
            //  Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText), "Unable to find Unit requested");

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to delete Unit  as requested");
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText), "Unable to delete Unit  as requested");

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }

        [Category("Smoke")]
        [Test]
        public void Can_delete_NOT_Unit_with_Lists_Attached()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //   Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            //Add a unit and a child as part of set up.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "List"), "Unable to add child unit");

            Assert.IsFalse(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
          
            //Clean up
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to find child unit");
            //  Assert.IsNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
        [Category("Full")]
        [Test]
        public void Can_delete_Cancel()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            //Create a list so you can delete it.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //     Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText, "Cancel"), "Unable to cancel a delete request");
            Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText), "Error: Unit was deleted in error");
            //Clean up after ourselves
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to cleanup after test");
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestDelete", KortextGlobals.UnitCourseIdentifierText), "Error: Unit was NOT deleted");

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
        [Category("Smoke")]
        [Test]
        public void Can_Add_Child_Unit()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            //Create a new Unit so you can add a child unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //      Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");

            //Now that we have our Unit, lets add a child Unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText, "Unit"), "Unable to add child unit");
            Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");

            //Clean up after ourselves
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to find child unit");

            //  Assert.IsNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }

        [Category("Smoke")]
        [Test]
        public void Can_Add_Child_List()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            //Create a new Unit so you can add a child unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");

            //Now that we have our Unit, lets add a child Unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "List"), "Unable to add child unit");
            Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");
            //Clean up after ourselves
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to find child unit");

            //  Assert.IsNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));


            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }


        [Category("Full")]
        [Test]
        public void Can_delete_subUnit_In_Unit()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            //Create a new Unit so you can add a child unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");

            //Now that we have our Unit, lets add a child Unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText, "Unit"), "Unable to add child unit");
            //     Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to find child unit");
            //  Assert.IsNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }
        [Category("Full")]
        [Test]
        public void Can_delete_subList_In_Unit()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
            //Create a new Unit so you can add a child unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //       Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");

            //Now that we have our Unit, lets add a child Unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "List"), "Unable to add child unit");
            //     Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestUnitChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to find child unit");

            Assert.IsNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");
            //clean up
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }
        [Category("Utility")]
        [Test]
        public void Utility_Can_find_Unit()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("Faculty of Business, Law and Education", KortextGlobals.UnitCourseIdentifierText), "Unable to find unit");

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }

        [Category("Utility")]
        [Test]
        public void Utility_Can_find_SubChild()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("Faculty of Business, Law and Education", "School of Education", KortextGlobals.UnitCourseIdentifierText), "Unable to find unit");

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }
        [Category("Smoke")]
        [Test]
        public void Can_Edit_Unit_Metadata_Name()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //    Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");

            //Edit the units metadata - must send in Unit name, course identifier, which field and new value

            Assert.IsTrue(Pages.PearlHierarchyPage.EditUnitMetadata("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, FieldToChange.Name, "MyNewName"), "Unable to edit metadata Unit Name");
            //Find the new value and then make sure that the old value doesn't exist anymore.
            Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("MyNewName", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unit doesn't appear to be renamed as requested");

            //Cleanup after test
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("MyNewName", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("MyNewName", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }
        [Category("Smoke")]
        [Test]
        public void Can_Edit_Unit_Metadata_CourseIdentifier()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //       Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");

            //Edit the units metadata - must send in Unit name, course identifier, which field and new value

            Assert.IsTrue(Pages.PearlHierarchyPage.EditUnitMetadata("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, FieldToChange.CourseIdentifier, "MyNewCourse"), "Unable to edit metadata Unit Name");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateMetadataChange("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, FieldToChange.CourseIdentifier, "MyNewCourse"), "Unable to edit metadata Course Identifier");

            //Cleanup after test
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }
        [Category("Utility")]
        [Test]
        public void Utility_Can_validate_Unit_Metadata_CourseIdentifier()
        {
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateMetadataChange("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, FieldToChange.CourseIdentifier, "MyNewCourse"), "Unable to edit metadata Course Identifier");

            Pages.LandingPage.Do_Logout();
        }
        [Category("Utility")]
        [Test]
        public void Utility_Can_validate_Unit_Metadata_year()
        {
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateMetadataChange("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, FieldToChange.Year, "2018"), "Unable to validate metadata Year");

            Pages.LandingPage.Do_Logout();
        }


        [Category("Smoke")]
        [Test]
        public void Can_Edit_Unit_Metadata_Year()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");

            //Edit the units metadata - must send in Unit name, course identifier, which field and new value

            Assert.IsTrue(Pages.PearlHierarchyPage.EditUnitMetadata("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, FieldToChange.Year, "2018"), "Unable to edit metadata Unit Year");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateMetadataChange("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, FieldToChange.Year, "2018"), "Unable to edit metadata Course Identifier");

            //Cleanup after test
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }

        [Category("Smoke")]
        [Test]
        public void Can_Edit_List_Metadata_Name()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");
            //Now that we have our Unit, lets add a child Unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "List"), "Unable to add child unit");
            //    Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");

            //Edit the units metadata - must send in Unit name, course identifier, which field and new value

            Assert.IsTrue(Pages.PearlHierarchyPage.EditChildMetadata("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, FieldToChange.Name, "MyNewName"), "Unable to edit metadata Unit Name");
            //Find the new value and then make sure that the old value doesn't exist anymore.
            Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "MyNewName", KortextGlobals.UnitCourseIdentifierText), "Unable to find newly renamed child as requested");
            Assert.IsNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText), "Original Child name still exists");

            ////   //Cleanup after test
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("ASusanTestUnit", "MyNewName", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to find child unit");

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            // Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }
        [Category("Smoke")]
        [Test]
        public void Can_Edit_List_Metadata_CourseIdentifier()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //      Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");
            //Now that we have our Unit, lets add a child Unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "List"), "Unable to add child unit");
            //    Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");

            //Edit the units metadata - must send in Unit name, course identifier, which field and new value

            Assert.IsTrue(Pages.PearlHierarchyPage.EditChildMetadata("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, FieldToChange.CourseIdentifier, "MyNewCourse"), "Unable to edit metadata Unit Name");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateChildMetadataChange("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, FieldToChange.CourseIdentifier, "MyNewCourse"), "Unable to validate metadata Course Identifier");

            //Cleanup after test
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to find child unit");

            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }
        [Category("Smoke")]
        [Test]
        public void Can_Edit_List_Metadata_Year()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to find new Unit as requested");
            //Now that we have our Unit, lets add a child Unit to it.
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "List"), "Unable to add child unit");
            //    Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText), "Unable to find child unit");

            //Edit the units metadata - must send in Unit name, course identifier, which field and new value

            Assert.IsTrue(Pages.PearlHierarchyPage.EditChildMetadata("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, FieldToChange.Year, "2018"), "Unable to edit metadata year");
            Assert.IsTrue(Pages.PearlHierarchyPage.ValidateChildMetadataChange("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, FieldToChange.Year, "2018"), "Unable to edit metadata Course Identifier");

            //Cleanup after test
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteChild("ASusanTestUnit", "SusanTestListChild", KortextGlobals.UnitCourseIdentifierText, "Confirm"), "Unable to find child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.DeleteUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText, "Confirm"));
            Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText));

            //Logs User Out.  Can add validation if needed by uncommenting line below
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");
        }

    //    [Category("Smoke")]
    //    [Test]
        public void Can_Suppress_A_Unit()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            //       Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //     Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            //       Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //     Assert.IsTrue(Pages.PearlHierarchyPage.Suppressunit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText),"Unable to suppress Unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.Suppressunit("Darius", KortextGlobals.UnitCourseIdentifierText), "Unable to suppress Unit");
            Pages.LandingPage.Do_Logout();

        }
    //    [Category("Smoke")]
    //    [Test]
        public void Can_UnSuppress_A_Unit()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            //       Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //     Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            //       Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //     Assert.IsTrue(Pages.PearlHierarchyPage.Suppressunit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText),"Unable to suppress Unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.UnSuppressunit("Darius", KortextGlobals.UnitCourseIdentifierText), "Unable to suppress Unit");
            Pages.LandingPage.Do_Logout();

        }
    //    [Category("Smoke")]
   //     [Test]
        public void Can_UnSuppress_A_Child_List()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            //       Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //     Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit so you can edit it's metadata
            //       Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            //     Assert.IsTrue(Pages.PearlHierarchyPage.Suppressunit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText),"Unable to suppress Unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.UnSuppresschild("Darius", "List1", KortextGlobals.UnitCourseIdentifierText), "Unable to suppress Unit");
            Pages.LandingPage.Do_Logout();

        }
       // [Category("Smoke")]
       // [Test]
        public void Can_Suppress_A_Child_List()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            //       Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //     Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Create a new Unit and child so you can edit it's metadata
            Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("ASusanTestUnit", KortextGlobals.UnitCourseIdentifierText), "Unable to create a new Unit as requested");
            Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("ASusanTestUnit", "Abracadabra", KortextGlobals.UnitCourseIdentifierText, "List"), "Unable to add child unit");
            Assert.IsTrue(Pages.PearlHierarchyPage.Suppresschild("ASusanTestUnit", "Abracadabra", KortextGlobals.UnitCourseIdentifierText), "Unable to suppress Unit");
            //Validate that suppress worked.
            Pages.LandingPage.Do_Logout();
            Assert.IsTrue(Pages.PearlRedMenuPage.ValidateResultsCleared(), "Clear button is not clearing search results");
            Assert.AreEqual(Pages.PearlRedMenuPage.SearchRebus("Abracadabra"), "Displaying 0 of 0 results found\r\n");
            Pages.PearlRedMenuPage.ClearResults();




        }
    }
}
