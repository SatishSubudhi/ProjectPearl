using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PearlFramework;
using System.Diagnostics;
//using OpenQA.Selenium.Appium.Android;
using System.Threading;

namespace PearlFramework.Utilities
{
    class Klickit
    {

        public static void Click(IWebElement element)
        {
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].click();", element);
            Thread.Sleep(KortextGlobals.s);
        }
       
    }
}
