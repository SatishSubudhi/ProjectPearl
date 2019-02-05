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
using PearlFramework;
using Protractor;

namespace PearlFramework
{
    public class PearlAboutPage
    {
        [FindsBy(How = How.CssSelector, Using = "h4[ng-click='adminAbout.clearLocalStorage()']")]
        protected IWebElement PurgeButton
        {
            get;
            set;
        }

        By PurgeName_locator = By.ClassName("keylinks-summary");

        string statusreturntext;

        public bool PurgeLocalStorage()
        {
            try
            {
                //Purge All Local Storage
                if(!PurgeAllLocalStorage())
                {
                    Console.WriteLine("Error during Purging Local Storage");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in PurgeLocalStorage.cs: " + e.Message);
                return false;
            }
        }
        
        public bool PurgeAllLocalStorage()
        {
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> LinksinAboutPage = Driver.Instance.FindElements(PurgeName_locator);
            if(LinksinAboutPage.Count > 0)
            {
                foreach(IWebElement LinkinAboutPage in LinksinAboutPage)
                {
                    if(LinkinAboutPage.Text == "Purge Local Storage")
                    {
                        Klick.On(LinkinAboutPage);
                        Thread.Sleep(KortextGlobals.s);
                        WaitFind.FindElem(PurgeButton, 10);
                        Klick.On(PurgeButton);

                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                        if (statusreturntext == "Local storage purged")
                        {
                            Console.WriteLine("Purging All Local Storage Successful.");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Error while Purging Local Storage." + statusreturntext);
                            return false;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Error Loading About Page.");
                return false;
            }
            Console.WriteLine("Could Not find Purge link in About Page.");
            return false;
        }
    }
}



