using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlFramework;
using System.Diagnostics;
using Protractor;




namespace PearlFramework.Utilities
{
    public class WaitFind
    {

        public static IWebElement FindElem(IWebElement element, int timeoutInSeconds)
        {
              WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
         //   WebDriverWait wait = new WebDriverWait(Driver.ngDriver.WrappedDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                wait.Until(drv => element);
                return element;
            }
            catch (Exception e)
            {
                Console.WriteLine("Element not found: ", e.Message);
                return null;
            }
        }



        public static bool IsElementDisplayed(IWebElement element, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            try
            {
                wait.Until(drv => element);
                return element.Displayed;
            }
            catch (Exception e)
            {
                Console.WriteLine("Element not found: ", e.Message);
                return false;
            }
        }

     /*  This doesn't appear to be used anywhere and it's breaking the build.
      *  We can use protractor.net calls to better handle waiting for the element to be clickable.
       public static void WaitUntilClickable(IWebElement element, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }*/


        //Enabled
        public static bool IsElementEnabled(By element)
        {
            IReadOnlyCollection<IWebElement> elements = Driver.Instance.FindElements(element);
            if (elements.Count > 0)
            {
                return elements.ElementAt(0).Enabled;
            }
            return false;
        }

    }
}
