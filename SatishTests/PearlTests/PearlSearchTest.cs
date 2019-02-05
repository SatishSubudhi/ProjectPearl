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
    //[TestFixture("Firefox_UK")]
   // [TestFixture("Edge")]
     // [TestFixture("IE11")]
    //[TestFixture("Chrome_NL")]
    //[TestFixture("Edge_NL")]
    //[TestFixture("IE11_NL")]
    //[TestFixture("Android_Chrome")]
    public class PearlSearchTest : SetupTearDown
    {

        public PearlSearchTest(string b)
        {
            KortextGlobals.platformtype = b;
        }


        [Category("Smoke")]
        [Test]
        public void Search_Rebus_NoResults()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //Pages.PearlSearch.SearchRebus_old();
            Assert.AreEqual(Pages.PearlRedMenuPage.SearchRebus("abcdefgh"), "Displaying 0 of 0 results found\r\nclose");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
        [Category("Smoke")]
        [Test]
        public void Search_Rebus_ReturnsResults()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            //          Pages.PearlSearch.SearchRebus_old();
            Assert.AreNotEqual(Pages.PearlRedMenuPage.SearchRebus("est"), "Displaying 0 of 0 results found\r\nclose");
            //Validate terms displayed on page match the original search term
            Assert.IsTrue(Pages.PearlRedMenuPage.ValidateSearchResults("est"), "Results displayed do not match search term");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
        [Category("Smoke")]
        [Test]
        public void Can_Travel_to_SearchResults()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");

            Assert.AreNotEqual(Pages.PearlRedMenuPage.SearchRebus("test"), "Displaying 0 of 0 results found\r\nclose");

            //  Assert.AreNotEqual(Pages.PearlRedMenuPage.SearchRebus("ics"), "Displaying 0 of 0 results found\r\nclose");
            Assert.IsTrue(Pages.PearlRedMenuPage.TraveltoFirstResult(),"Not able to travel to the list from search");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }
        [Category("Smoke")]
        [Test]
        public void Can_Clear_Search_Results()
        {
            //Assert you are at the landing page and then log in and validate you logged in ok
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Assert.IsTrue( Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");


            Assert.AreNotEqual(Pages.PearlRedMenuPage.SearchRebus("ics"), "Displaying 0 of 0 results found\r\n");
            Pages.PearlRedMenuPage.ClearResults();
           Assert.IsTrue(Pages.PearlRedMenuPage.ValidateResultsCleared(),"Clear button is not clearing search results");

            //validates if the user is logged out
            Pages.LandingPage.Do_Logout();
            //  Assert.IsTrue( Pages.LandingPage.IsLoggedOut(), "User was unable to log out successfully");

        }

    }
}
