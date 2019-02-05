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

    class HierarchyDeleteWholeTree : SetupTearDown
    {
        public HierarchyDeleteWholeTree(string b)
        {
            KortextGlobals.platformtype = b;
        }
       [Category("Utility")]
       [Test]

        public void Can_Delete_Whole_Tree()
        {
            Assert.IsTrue(Pages.IsAt(PageName.LandingPage));
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            //           Assert.IsTrue(Pages.LandingPage.IsLoggedIn(), "User was not able to log in successfully");
             Assert.IsNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");

             Assert.IsTrue(Pages.PearlHierarchyPage.CreateNewUnit("AutomationUnit2", "Do Not Touch"));
             //   Assert.IsNotNull(Pages.PearlHierarchyPage.FindUnit("AutomationUnit", "Do Not Touch"), "Unable to find Unit requested");
             Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit2", "Automation2ListChild", "Do Not Touch", "List"), "Unable to add child unit");
             //    Assert.IsNotNull(Pages.PearlHierarchyPage.FindSubChildUnit("AutomationUnit", "AutomationListChild", "Do Not Touch"), "Unable to find child unit");
             Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit2", "Automation2UnitChild", "Do Not Touch", "Unit"), "Unable to add child unit");
             Assert.IsTrue(Pages.PearlHierarchyPage.AddChildUnitList("AutomationUnit2", "Automation2ListChild2", "Do Not Touch", "List"), "Unable to add child unit");
             
            Pages.PearlHierarchyPage.DeleteWholeTree("AutomationUnit2");
           // Pages.PearlHierarchyPage.DeleteWholeTree("ABCDUniversity1");
           // Pages.PearlHierarchyPage.DeleteWholeTree("ABCDUniversity2");
          //  Pages.PearlHierarchyPage.DeleteWholeTree("ABCDUniversity4");
          //  Pages.PearlHierarchyPage.DeleteWholeTree("ABCDUniversity3");
          //  Pages.PearlHierarchyPage.DeleteWholeTree("ABCDUniversity6");
          //  Pages.PearlHierarchyPage.DeleteWholeTree("TestArul");
        }




    }
}
