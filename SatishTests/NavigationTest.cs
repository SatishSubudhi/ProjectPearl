using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlFramework;
using PearlFramework.Utilities;
using NUnit.Framework;
using SatishTests.Utilities;


namespace SatishTests
{
    [TestFixture("Chrome")]
   // [TestFixture("Edge")]
     // [TestFixture("IE11")]
    public class NavigationTest : SetupTearDown
    {
        public NavigationTest(string b)
        {
            Console.WriteLine(b);
            KortextGlobals.platformtype = b;
        }
        [Category("Utility")]
        [Test]
        public void NavigateRehbus()
        {

            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);

            Pages.LandingPage.ClickOnMenu_AboutBtn();
            Pages.LandingPage.ClickOnMenu_AcquisitionsBtn();
            Assert.IsTrue(Pages.LandingPage.ClickOnMenu_HierarchyBtn(), "Hierarchy Menu button not working");
            Pages.LandingPage.ClickOnMenu_MaterialsBtn();
            Pages.LandingPage.ClickOnMenu_MyListsBtn();
       //     Pages.LandingPage.ClickOnMenu_MyRequestsBtn(); // No longer working - change request
            Pages.LandingPage.ClickOnMenu_NoticesBtn();
            Pages.LandingPage.ClickOnMenu_PermissionsBtn();
            Pages.LandingPage.ClickOnMenu_ReportsBtn();
            Pages.LandingPage.ClickOnMenu_RequestsBtn();
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Pages.LandingPage.ClickOnMenu_TagsBtn();
           Assert.IsTrue( Pages.LandingPage.ClickOnMenu_UserBtn());

            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
       //     Pages.LandingPage.ClickOnFooter_BlackboardLink();
          //  Pages.LandingPage.ClickOnFooter_ContactUsLink();
           // Pages.LandingPage.ClickOnFooter_LibraryHome();
            Pages.LandingPage.ClickOnFooter_PTFSEuropeLink();
            Pages.LandingPage.ClickOnFooter_rebuslistLink();


        }

    }
}
