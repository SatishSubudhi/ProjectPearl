using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;

//using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using PearlFramework.Utilities;
using NUnit.Framework;
using Protractor;

namespace PearlFramework
{
    public class LandingPage
    {
        /*****************************Landing page contains the landing page and also the side bar menu items
         * 
         * *****************************************************************************************************/
        //Login button once main menu button has been pressed
        [FindsBy(How = How.Id, Using = "login")]
        protected IWebElement login_btn
        {
            get; set;
        }

        [FindsBy(How = How.Id, Using = "logout")]
        protected IWebElement LogOut
        {
            get;
            set;
        }
        //Sidebar menu items

        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/my-lists']")]
        protected IWebElement my_lists_btn
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/my-requests']")]
        protected IWebElement my_requests_btn
        {
            get;
            set;
        }

        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/requests']")]
        protected IWebElement requests_btn
        {
            get;
            set;
        }

        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/acquisition']")]
        protected IWebElement acquisitions_btn
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/reports']")]
        protected IWebElement reports_btn
        {
            get;
            set;
        }


        // [FindsBy(How = How.XPath, Using = "//li[8]/a")] //no more xpaths.......
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/hierarchy']")]
        protected IWebElement hierarchy_btn
        {
            get;
            set;
        }
       
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/search']")]
        protected IWebElement materials_btn
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/tags']")]
        protected IWebElement tags_btn
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/system-messages']")]
        protected IWebElement notices_btn
        {
            get;
            set;
        }
        // [FindsBy(How = How.XPath, Using = "//li[12]/a")] this stopped working April 27
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/users']")]
        protected IWebElement user_btn
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/permissions']")]
        protected IWebElement permissions_btn
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/global']")]
        protected IWebElement settings_btn
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-href='#/admin/about']")]
        protected IWebElement about_btn
        {
            get;
            set;
        }
/************************************************************************
 *             Variables for the footer links
 ***********************************************************************/
        [FindsBy(How=How.LinkText, Using ="Library Home")]
        protected IWebElement libraryLink
        {
            get;
            set;
        }
        [FindsBy(How = How.LinkText, Using = "Blackboard")]
        protected IWebElement blackboardLink
        {
            get;
            set;
        }
        [FindsBy(How = How.LinkText, Using = "Contact Us")]
        protected IWebElement contactusLink
        {
            get;
            set;
        }
        [FindsBy(How = How.LinkText, Using = "PTFS Europe.")]
        protected IWebElement ptfsLink
        {
            get;
            set;
        }
        [FindsBy(How = How.LinkText, Using = "KeyLinks")]
        protected IWebElement rebuslistLink
        {
            get;
            set;
        }

        /*************************************************************
         * 
         *              FUNCTIONS
         *              
         * **********************************************************/             
        public void GoToLandingPage()
        {
          //Console.WriteLine("in go to landing page");
            Driver.Navigate_BaseAddress();


            // Add some -No validation that we are getting there.

        }


/****************************************************************
 * Accessing the sidebar menu items. Validation that they worked is left
 *  to the calling function.
 *  
 *  ****************************************************************/
        public void ClickOnLoginBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(login_btn, 20);
            Klick.On(login_btn);
        }


        public void ClickOnMenu_MyListsBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(my_lists_btn, 20);
            Klick.On(my_lists_btn);
        }
        public void ClickOnMenu_MyRequestsBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(my_requests_btn, 20);
            Klick.On(my_requests_btn);
        }
        public void ClickOnMenu_RequestsBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(requests_btn, 20);
            Klick.On(requests_btn);
        }
        public void ClickOnMenu_AcquisitionsBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(acquisitions_btn, 20);
            Klick.On(acquisitions_btn);
        }
        public bool ClickOnMenu_ReportsBtn()
        {
           Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(reports_btn, 20);
            Klick.On(reports_btn);

            if (!Pages.IsAt(PageName.PearlRedMenuPage))
            {
                Console.WriteLine("Not at Report Page-Trying again and Ignore the exception");
                Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
                Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
                WaitFind.FindElem(reports_btn, 20);
                Klick.On(reports_btn);
                //      Console.WriteLine("After trying again");

                Thread.Sleep(KortextGlobals.s);
                if (!Pages.IsAt(PageName.PearlReportsPage))
                {
                    Console.WriteLine("Unable to reach Reports page");
                    return false;
                }
                return true;
                //   Driver.Instance.Navigate().Refresh();
            }
            return true;
        }
        public bool ClickOnMenu_HierarchyBtn()
        {

            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();

            WaitFind.FindElem(hierarchy_btn, 20);
          //  Driver.ngDriver.FindElement(hierarchy_btn);
            Klick.On(hierarchy_btn);
        //  Driver.ngDriver.WaitForAngular();
          

            if (!Pages.IsAt(PageName.PearlHierarchyPage))
            {
                Console.WriteLine("Not at Hierarchy Page-Trying again and Ignore the exception");
                Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
                Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
                WaitFind.FindElem(hierarchy_btn, 20);
                Klick.On(hierarchy_btn);
                //      Console.WriteLine("After trying again");
             //   Driver.ngDriver.WaitForAngular();

             //   Thread.Sleep(KortextGlobals.s);
                if (!Pages.IsAt(PageName.PearlHierarchyPage))
                {
                    Console.WriteLine("Unable to reach Hierarchy page");
                    return false;
                }
                return true;
                //   Driver.Instance.Navigate().Refresh();
            }
            return true;
        }
        public void ClickOnMenu_MaterialsBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(materials_btn, 20);
            Klick.On(materials_btn);

        }
        public void ClickOnMenu_TagsBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(tags_btn, 20);
            Klick.On(tags_btn);

        }
        public void ClickOnMenu_NoticesBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(notices_btn, 20);
            Klick.On(notices_btn);

        }
        public bool ClickOnMenu_UserBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(user_btn, 20);
            Klick.On(user_btn);

            if (!Pages.IsAt(PageName.PearlUserPage))
            {
                Console.WriteLine("Not at Users Page-Trying again and Ignore the exception");
                Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
                Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
                WaitFind.FindElem(user_btn, 20);
                Klick.On(user_btn);
                //      Console.WriteLine("After trying again");

                Thread.Sleep(KortextGlobals.s);
                if (!Pages.IsAt(PageName.PearlUserPage))
                {
                    Console.WriteLine("Unable to reach Users page");
                    return false;
                }
                return true;
                //   Driver.Instance.Navigate().Refresh();
            }
            return true;

        }
        public void ClickOnMenu_PermissionsBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(permissions_btn, 20);
            Klick.On(permissions_btn);
        }
        public void ClickOnMenu_SettingsBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(settings_btn, 20);
            Klick.On(settings_btn);
        }
        public void ClickOnMenu_AboutBtn()
        {
            Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(about_btn, 20);
            Klick.On(about_btn);
        }
 /**********************************************************************
 *                      ACcessing links on footer.
 ************************************************************************/


        public void ClickOnFooter_LibraryHome()
        {
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(libraryLink, 20);
            Klick.On(libraryLink);
        }
        public void ClickOnFooter_BlackboardLink()
        {
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(blackboardLink, 20);
            Klick.On(blackboardLink);
        }
        public void ClickOnFooter_ContactUsLink()
        {
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(contactusLink, 20);
            Klick.On(contactusLink);
        }
        public void ClickOnFooter_PTFSEuropeLink()
        {
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(ptfsLink, 20);
            Klick.On(ptfsLink);
        }
        public void ClickOnFooter_rebuslistLink()
        {
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(rebuslistLink, 20);
            Klick.On(rebuslistLink);
        }



        public void Do_Logout()
        {

            Thread.Sleep(KortextGlobals.s);
            //    Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
            Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
            WaitFind.FindElem(LogOut, 20);
            Klick.On(LogOut);
        }
        public bool IsLoggedIn()
        {
            //Check for log out button
            try
            {

                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(20));
                var myElement = wait.Until(x => x.FindElement(By.Id("navicon-container")));
                Klick.On(myElement);
              //  Thread.Sleep(KortextGlobals.s);
                var logoutElement = wait.Until(x => x.FindElement(By.Id("logout")));
                var MenuBack = wait.Until(x => x.FindElement(By.CssSelector("div.circle.open")));
                Klick.On(MenuBack);
                return true;


            }
            catch (Exception e)
            {
                Console.WriteLine("Error logging in ", e.Message);
                return false;
            }
        }

        public bool LoginButtonIsDisplayed()
        {
            return login_btn.Displayed;

        }

        public bool IsLoggedOut()
        {
            try
            {
                var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(40));
                var myElement = wait.Until(x => x.FindElement(By.CssSelector("div.navicon-button")));
                Klick.On(myElement);
                Thread.Sleep(KortextGlobals.s);

                // var loginElement = wait.Until(x => x.FindElement(By.Id("logout")));
                return Pages.LandingPage.LoginButtonIsDisplayed();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error logging out ", e.Message);
                return false;
            }
        }



    }
}
