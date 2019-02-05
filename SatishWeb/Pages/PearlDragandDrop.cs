using OpenQA.Selenium;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using System;
using OpenQA.Selenium.Support.UI;

using OpenQA.Selenium.Interactions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using PearlFramework.Utilities;

namespace PearlFramework
{
    public class PearlDragandDrop
    {
        public void GoTo()
        {
            Thread.Sleep(4000);
            Driver.Instance.Navigate().GoToUrl("https://kortext.rebuslist.com/#/buffer/5690");
            Thread.Sleep(1000);
        }

        public void Dragit(int sourcelocation, int destlocation)
        {
            // 
            //   IJavaScriptExecutor js = Driver.Instance as IJavaScriptExecutor;

            // this one is ok.  
            //  js.ExecuteScript("document.getElementById('buffer').style.border = '2px solid red'");
            Thread.Sleep(10000); //Need to wait for all the blue boxed in bottom right to finish.
                                 //  This works too.  IList<IWebElement> source = Driver.Instance.FindElements(By.ClassName("md-18"));  //returns  all visible icons on page..
                                 /*IJavaScriptExecutor js = Driver.Instance as IJavaScriptExecutor;
                                 IList<IWebElement> source = Driver.Instance.FindElements(By.ClassName("angular-ui-tree-handle"));  //returns  all icons on page - 2 of them are visible. Need to scroll down to see more
                                 js.ExecuteScript("arguments[0].scrollIntoView(true);", source[1]);
                                 Console.WriteLine("just after scroll");
                                 IList<IWebElement> destination = Driver.Instance.FindElements(By.ClassName("angular-ui-tree-handle"));*/

            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 700)");

            //IJavaScriptExecutor js = Driver.Instance as IJavaScriptExecutor;
            IList<IWebElement> source = Driver.Instance.FindElements(By.ClassName("angular-ui-tree-handle"));  //returns  all icons on page - 2 of them are visible. Need to scroll down to see more
            //js.ExecuteScript("arguments[0].scrollIntoView(true);", source[1]);
            //Console.WriteLine("source Count: " + source.Count);
            
            //((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView();", source[1]);
            
            //Console.WriteLine("just after scroll");
            IList<IWebElement> destination = Driver.Instance.FindElements(By.ClassName("angular-ui-tree-handle"));
            //Console.WriteLine("destination Count: " + destination.Count);

            //dumps the current DOMS html into the output.  Useful to see if you are missing anything not loaded.
            /*    String pagesource = Driver.Instance.PageSource;
                Console.WriteLine("Current DOM: " + pagesource); */

            /*for (int i = 0; i < source.Count; i++)
            {
                Driver.HighlightElement(source[i]);
            }
            
            for (int i = 0; i < destination.Count; i++)
            {
                Driver.HighlightElement(destination[i]);
            }*/

            Driver.HighlightElement(source[sourcelocation]);
            Driver.HighlightElement(destination[destlocation]);

            //Drag and Drop things
            Actions actions = new Actions(Driver.Instance);
            actions.MoveToElement(source[sourcelocation]).Build().Perform();
            actions.ClickAndHold(source[sourcelocation]).Build().Perform();
            actions.DragAndDrop(source[sourcelocation], destination[destlocation]).Build().Perform();
            //         WaitFind.FindElem(source[1], 10).Click();

            Thread.Sleep(5000);

            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");

            Thread.Sleep(2000);
        }
    }
}