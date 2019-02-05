using OpenQA.Selenium;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using System;
using OpenQA.Selenium.Support.UI;
using PearlFramework.Utilities;
using OpenQA.Selenium.Interactions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace PearlFramework
{
    public class PearlCreateReadingList
    {
        By unit_title = By.CssSelector("a[ng-show = 'nameCtrl.display']"); //unit locator - looks for a unit on the hierarchy page.

        string UnitNameText;
        int unitnameappend = 1;
        string xpathUnitSearchName;
        
        public bool CreateUnit(string unitname, string listname)
        {
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            Pages.LandingPage.ClickOnMenu_HierarchyBtn();
            //validate you are there.
            if (!Pages.IsAt(PageName.PearlHierarchyPage))
            {
                Console.WriteLine("Unable to reach Hierarchy Page");
                return false; 
            }
            string searchresult = SearchandReturnNewUnitName("ABCDUniversity");
            Thread.Sleep(KortextGlobals.s);
            if (!Pages.PearlHierarchyPage.CreateNewUnit(searchresult, KortextGlobals.UnitCourseIdentifierText))
            {
                Console.WriteLine("Error while Creating New Unit");
                return false;
            }

            //Verify the Newly Created Unit
            if (Pages.PearlHierarchyPage.FindUnit(searchresult, KortextGlobals.UnitCourseIdentifierText) == null)
            {
                Console.WriteLine("Unable to find the Newly Created Unit: " + searchresult);
                return false;
            }
            Thread.Sleep(KortextGlobals.ll);
            return true;  
        }
        public bool CreateList()
        {
            try
            {
                Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
                Pages.LandingPage.ClickOnMenu_HierarchyBtn();

                //Klick.On(MenuHierarchy);
                //Validate you get tto the hierarchy page.
                if (!Pages.IsAt(PageName.PearlHierarchyPage))
                {
                    Console.WriteLine("Unable to reach Hierarchy Page");
                    return false;
                }
                string searchresult = SearchandReturnNewUnitName("ABCDUniversity");
                Thread.Sleep(KortextGlobals.s);
                if(!Pages.PearlHierarchyPage.CreateNewUnit(searchresult, KortextGlobals.UnitCourseIdentifierText))
                {
                    Console.WriteLine("Error while Creating New Unit");
                    return false;
                }

                //Verify the Newly Created Unit
                if(Pages.PearlHierarchyPage.FindUnit(searchresult, KortextGlobals.UnitCourseIdentifierText) == null)
                {
                    Console.WriteLine("Unable to find the Newly Created Unit: " + searchresult);
                    return false;
                }
                Thread.Sleep(KortextGlobals.ll);

                if(!Pages.PearlHierarchyPage.AddChildUnitList(searchresult, "TestList" + unitnameappend, KortextGlobals.UnitCourseIdentifierText, "List"))
                {
                    Console.WriteLine("Error while Creating New List");
                    return false;
                }

                //Verify the Newly Created List
                if (Pages.PearlHierarchyPage.FindSubChildUnit(searchresult, "TestList" + unitnameappend, KortextGlobals.UnitCourseIdentifierText) == null)
                {
                    Console.WriteLine("Unable to find the Newly Created List: " + "TestList" + unitnameappend);
                    return false;
                }
                Thread.Sleep(KortextGlobals.ll);

                IWebElement childcontainer = Pages.PearlHierarchyPage.FindSubChildUnit(searchresult, "TestList" + unitnameappend, KortextGlobals.UnitCourseIdentifierText);
                IWebElement NewListName = childcontainer.FindElement(unit_title);
                Driver.HighlightElement(NewListName);
                Klick.On(NewListName);

                Thread.Sleep(KortextGlobals.l);

                //Verify whether the page is landed on the View List page
                if(!(Pages.PearlEditBuffer.ListTitle() == ("TestList" + unitnameappend + " [Test Course]")))
                {
                    Console.WriteLine("Did not land properly to View List Page of the New List");
                    return false;
                }

                Thread.Sleep(KortextGlobals.s);
                Console.WriteLine("Creating New List Successful");
                return true;  
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in PearlCreateReadingList.cs: " + e.Message);
                return false;
            }
        }

        public string SearchandReturnNewUnitName(string unitname)
        {
            unitnameappend = 1;
            for (int i = 0; i < i + 1; i++)
            {
                UnitNameText = unitname + unitnameappend;
                xpathUnitSearchName = "//a[contains(text(),'" + UnitNameText + "')]";
                try
                {
                    var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(20));
                    var Unitispresent = wait.Until(x => x.FindElement(By.XPath(xpathUnitSearchName)));
                    unitnameappend = unitnameappend + 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception in SearchandReturnNewUnitName.cs: " + e.Message);
                    return UnitNameText;
                }
            }
            return UnitNameText;
        }
    }
}