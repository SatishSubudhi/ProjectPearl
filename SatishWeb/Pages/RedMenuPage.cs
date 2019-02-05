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
    public class PearlRedMenuPage
    {
        [FindsBy(How = How.XPath, Using = "//div[@id='site-search-box']/input")]
        protected IWebElement searchinputbox
        {

            get; set;
        }

        [FindsBy(How = How.Id, Using = "close-results")]
        protected IWebElement closeresults
        {
            get; set;
        }
        //   [FindsBy(How = How.XPath, Using = "//div[@id='results-count']/div[2]")]
        [FindsBy(How = How.Id, Using = "results-count")]

        protected IWebElement SearchResultString
        {
            get; set;
        }
        //   [FindsByAll]
        [FindsBy(How = How.ClassName, Using = "highlight-text")]
        protected IWebElement SearchTermsReturned
        {
            get; set;
        }
        [FindsBy(How = How.Id, Using = "list-title")]
        protected IWebElement listtitle
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "//div[@id='results']/div/div/div[2]/a")]
        protected IWebElement SearchResultTableFirst
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "//div[@id='results']/div[3]/div/div[2]/a")]
        protected IWebElement SearchResultTableThird
        {
            get; set;
        }




        //     [FindsBy(How = How.CssSelector, Using = "a.navbar-brand")]
        [FindsBy(How = How.Id, Using = "site-logo")]
        protected IWebElement rebuslabel_btn
        {
            get; set;
        }

        [FindsBy(How = How.CssSelector, Using = "div.navicon-button")]
        protected IWebElement main_menu_btn
        {
            get; set;
        }

        [FindsBy(How = How.Id, Using = "navbar-browse")]
        protected IWebElement browse_icon
        {
            get; set;
        }

        public void ClickOnRebusLabelBtn()
        {
            WaitFind.FindElem(rebuslabel_btn, 20);
            Klick.On(rebuslabel_btn);
        }

        public void ClickOnMainMenuBtn()
        {
            WaitFind.FindElem(main_menu_btn,20);
            Klick.On(main_menu_btn);
        }

        public void ClickOnBrowseBtn()
        {
            //   var browseButton = TestBase.driver.FindElement(By.Id("navbar-browse"));
            //   Instance.FindElement(By.Id("wp-submit"));
            //   browseButton.Click();

            WaitFind.FindElem(browse_icon, 20);
            Klick.On(browse_icon);


        }
/*************************************************************************************************
 * 
 *          SEARCH FUNCTIONS
 *          
 ************************************************************************************************/          
        public string SearchRebus(string searchstr)
        {
            Thread.Sleep(KortextGlobals.s);
            //make sure search box on screen then clear it
            WaitFind.FindElem(searchinputbox, 10).Clear();
            searchinputbox.SendKeys(searchstr);
            Thread.Sleep(KortextGlobals.l);
            WaitFind.FindElem(SearchResultString, 30);
            Driver.HighlightElement(SearchResultString);
            Console.WriteLine("searchresults: " + SearchResultString.Text);
            return SearchResultString.Text;
        }

        //compares the first list in the search results to the page that comes up when you click it.
        public bool TraveltoFirstResult()
        {
            if (SearchResultString.Text != "Displaying 0 of 0 results found")
            {
                WaitFind.FindElem(SearchResultTableFirst, 10);
                var liststring = SearchResultTableFirst.Text;
                Console.WriteLine("String is: " + liststring);
                Klick.On(SearchResultTableFirst);
                Thread.Sleep(KortextGlobals.ll);
                Console.WriteLine("Title is: " + listtitle.Text);
                //compare the string
                return listtitle.Text.Contains(liststring);
            }
            return false;
        }

        public bool ValidateResultsCleared()
        {
            Console.WriteLine("inputbox" + searchinputbox.Text);
            if (searchinputbox.Text == "")
            {
                return true;
            }
            return false;
        }

        public void ClearResults()
        {
            closeresults.Click();
        }

        public bool ValidateSearchResults(string search_str)
        {
            int searchcount;
            Boolean matches = true;
            IList<IWebElement> ReturnListings = Driver.Instance.FindElements(By.ClassName("highlight-text"));
            searchcount = ReturnListings.Count;
            Console.WriteLine(ReturnListings.Count);
            for (int i = 0; i < searchcount; i++)
            {
                Console.WriteLine(ReturnListings[i].Text);
                //  Console.WriteLine("i is " + i + " string: " + ReturnListings[i].Text);
                if (!ReturnListings[i].Text.Contains(search_str))
                {
                 
                    matches = false; }

            }
            return matches;
        }
    }
}
