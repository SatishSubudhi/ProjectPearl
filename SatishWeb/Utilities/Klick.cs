using OpenQA.Selenium;
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
    public class Klick
    {
        public static void On(IWebElement element)
        {
            Driver.HighlightElement(element,"red", 1);
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].click();", element);
            Thread.Sleep(KortextGlobals.s);
        }
    }
}
