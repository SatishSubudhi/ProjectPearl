
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PearlFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium.Support;
using PearlFramework;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using Protractor;

//Pages keeps track of all of the various pages.  
//Only thing not on here is the navigation HeaderPages on the header.
//Titles of pages are referenced in other code (changed to public strings in that case) but maintained here.

namespace PearlFramework
{
    public static class Pages
    {
        //Titles of pages.  
        //Main store pages
     //   private static string LandingPageTitle = "Staffordshire University Reading Lists Online";
        //private static string PearlPageTitle = "rebus:list";
        private static string PearlLoginPageTitle = "Login to rebus:list";

        private static T GetPage<T>() where T : new()
        {
            var page = new T();

                 PageFactory.InitElements(Driver.Instance, page);
          //  PageFactory.InitElements(Driver.ngDriver, page);
            return page;
        }

        public static TraverseBufferPage TraverseBufferPage
        {
            get

            {
                return DoInitialize.PageElementsIn<TraverseBufferPage>();
            }
        }

        public static LandingPage LandingPage
        {
            get
            {
                return DoInitialize.PageElementsIn<LandingPage>();
            }

        }
        //Header and Footer Pages Use Seleniums PageObjects
        public static PearlRedMenuPage PearlRedMenuPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlRedMenuPage>();
            }
        }

        public static PearlLoginPage PearlLoginPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlLoginPage>();
            }
        }
       //replacing this with a hierarchy page as you should not create classes that are based upon functionality.
         public static PearlCreateReadingList PearlCreateReadingList
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlCreateReadingList>();
            }
        }
        public static PearlHierarchyPage PearlHierarchyPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlHierarchyPage>();
            }
        }
        public static PearlNewListAddDocs PearlNewListAddDocs
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlNewListAddDocs>();
            }
        }
        public static PearlUserPage PearlUserPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlUserPage>();
            }
        }


        public static PearlEditBuffer PearlEditBuffer
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlEditBuffer>();
            }
        }
        public static PearlAcquisitionsPage PearlAcquisitionsPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlAcquisitionsPage>();
            }
        }
        public static PearlViewList PearlViewList
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlViewList>();
            }
        }
        public static PearlMaterialsPage PearlMaterialsPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlMaterialsPage>();
            }
        }
        public static PearlTagsPage PearlTagsPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlTagsPage>();
            }
        }
        public static PearlNoticesPage PearlNoticesPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlNoticesPage>();
            }
        }
        public static PearlPermissionsPage PearlPermissionsPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlPermissionsPage>();
            }
        }
        public static PearlSettingsPage PearlSettingsPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlSettingsPage>();
            }
        }
        public static PearlAboutPage PearlAboutPage
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlAboutPage>();
            }
        }
        public static PearlDragandDrop PearlDragandDrop
        {
            get
            {
                return DoInitialize.PageElementsIn<PearlDragandDrop>();
            }
        }

        //Utility that checks if you are at the right page by looking at the page title
        //Page titles are defined at the top of this class.  Pages are enum at the bottom of the class.
        public static bool IsAt(PageName page)
        {
           // Console.WriteLine("in ISAT");
            IWebElement PageSubTitle;
            By locator = By.Id("admin-heading");
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            IWebElement LandingPageWelcome;
         By landinglocator = By.ClassName("front-and-center");
           

            switch (page)
            {
                case PageName.LandingPage:
                  Console.WriteLine("In pages.IsAt(landingpage)");
                    Thread.Sleep(KortextGlobals.s);
                 //  LandingPageWelcome = Driver.ngDriver.FindElement(landinglocator);
                   LandingPageWelcome = Driver.Instance.FindElement(landinglocator);

                  //  wait.Until(x => x.FindElement(landinglocator));
        Console.WriteLine("welcome string:" + LandingPageWelcome.Text + "pages.cs");
                    return LandingPageWelcome.Text.Equals("Welcome to KeyLinks QA");
                case PageName.PearlLoginPage:
                    Thread.Sleep(KortextGlobals.s);
                    return Driver.Title == PearlLoginPageTitle;
                case PageName.PearlUserPage:
                    Thread.Sleep(KortextGlobals.s);
                    try
                    {
                       // PageSubTitle = Driver.ngDriver.FindElement(locator);

                        PageSubTitle = Driver.Instance.FindElement(locator);
                        wait.Until(x => x.FindElement(locator));
                        //   Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("people Users");
                    }
               
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlUserPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlHierarchyPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(locator);

                  //      PageSubTitle = Driver.ngDriver.FindElement(locator);
                      //  wait.Until(x => x.FindElement(locator));
                  //   Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("sort Hierarchy");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlHierarchyPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlAcquisitionsPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(locator);

                      //  PageSubTitle = Driver.ngDriver.FindElement(locator);
                       // wait.Until(x => x.FindElement(locator));
                        //Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("payment Acquisitions");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlAcquisitionsPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlMaterialsPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(locator);

                        //  PageSubTitle = Driver.ngDriver.FindElement(locator);
                        // wait.Until(x => x.FindElement(locator));
                        //Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("search Materials");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlMaterialsPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlTagsPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(locator);

                        //  PageSubTitle = Driver.ngDriver.FindElement(locator);
                        // wait.Until(x => x.FindElement(locator));
                        //Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("bookmark Tags");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlTagsPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlNoticesPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(locator);

                        //  PageSubTitle = Driver.ngDriver.FindElement(locator);
                        // wait.Until(x => x.FindElement(locator));
                        //Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("chat Notices");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlNoticesPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlPermissionsPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(locator);

                        //  PageSubTitle = Driver.ngDriver.FindElement(locator);
                        // wait.Until(x => x.FindElement(locator));
                        //Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("supervisor_account Permissions");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlPermissionsPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlSettingsPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(locator);

                        //  PageSubTitle = Driver.ngDriver.FindElement(locator);
                        // wait.Until(x => x.FindElement(locator));
                        //Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("settings Settings");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlSettingsPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlAboutPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(By.ClassName("container"));

                        //  PageSubTitle = Driver.ngDriver.FindElement(locator);
                        // wait.Until(x => x.FindElement(locator));
                        //Console.WriteLine("Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("About");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlAboutPage: " + e.Message);
                        return false;
                    }
                case PageName.PearlRequestsPage:
                    Thread.Sleep(KortextGlobals.s);
                    // By locator = By.Id("admin-heading")
                    try
                    {
                        PageSubTitle = Driver.Instance.FindElement(locator);

                       // PageSubTitle = Driver.ngDriver.FindElement(locator);
                     //   wait.Until(x => x.FindElement(locator));
                       Console.WriteLine("Pages.IsAt(): Subtitle:" + PageSubTitle.Text + "pages.cs");
                        return PageSubTitle.Text.Equals("question_answer Requests");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception in PageName.PearlRequestsPage: " + e.Message);
                        return false;
                    }

                default:
                    return false;

            }
        }
        //Every new page that you create for the pages factory needs to be listed below.  Then it will be initialized.
    }
    public enum PageName
    {
        LandingPage,
        PearlLoginPage,
        PearlUserPage,
        PearlNewListAddDocs,
        PearlEditBuffer,
        PearlDragandDrop,
        PearlRedMenuPage,
        PearlHierarchyPage,
        PearlAcquisitionsPage,
        PearlViewList,
        PearlMaterialsPage,
        PearlTagsPage,
        PearlNoticesPage,
        PearlPermissionsPage,
        PearlSettingsPage,
        PearlAboutPage,
        PearlRequestsPage,
        PearlReportsPage
    }

}