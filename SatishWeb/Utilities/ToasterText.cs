using OpenQA.Selenium;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlFramework;
using System.Diagnostics;
using System.Threading;

namespace PearlFramework.Utilities
{
   public class ToasterText
    {
        [FindsBy(How = How.ClassName, Using = "toast-message")]
      protected IWebElement toastermessage
        {
            get; set;
        }

        //return toaster message in green box in bottom right corner
       /* public static string GetStatusMessage()
        {
            WaitFind.FindElem(toastermessage, 20);
            string StatusMessageText = toastermessage.Text;
            Console.WriteLine("StatusMessageText = " + StatusMessageText);
            return StatusMessageText;
        }*/
    }
}
