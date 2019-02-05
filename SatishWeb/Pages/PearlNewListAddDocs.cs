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
    public class PearlNewListAddDocs
    {      
        [FindsBy(How = How.CssSelector, Using = "button[ng-click='actionCtrl.enterEditMode()']")]
        protected IWebElement EditListData
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "select[ng-model='actionCtrl.selectedTemplate']")]
        protected IWebElement NewTemplateList
        {
            get; set;
        }
        /*
        [FindsBy(How = How.XPath, Using = "(//button[@type='button'])[23]")]
        protected IWebElement AddItemWeek2Day3
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "(//button[@type='button'])[21]")]
        protected IWebElement AddItemWeek2Day2
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "(//button[@type='button'])[19]")]
        protected IWebElement AddItemWeek2Day1
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "(//button[@type='button'])[16]")]
        protected IWebElement AddItemWeek2
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "(//button[@type='button'])[14]")]
        protected IWebElement AddItemWeek1Day3
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "(//button[@type='button'])[12]")]
        protected IWebElement AddItemWeek1Day2
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "(//button[@type='button'])[10]")]
        protected IWebElement AddItemWeek1Day1
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "(//button[@type='button'])[7]")]
        protected IWebElement AddItemWeek1
        {
            get; set;
        }
        */
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Publish draft']")]
        protected IWebElement PublishDraftButton
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "private-note")]
        protected IWebElement PrivateNote
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "public-note")]
        protected IWebElement PublicNote
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "list-summary")]
        protected IWebElement ListSummary
        {
            get;
            set;
        }

        By AddDocument_locator = By.CssSelector("button[ng-click = 'actionsCtrl.addNewItem()']");

        public bool AddDocs()
        {
            string numberofitemsbeforepublish;
            string numberofitemsafterpublish;

            try
            {
                string currentURL = Driver.Instance.Url;
                Pages.TraverseBufferPage.EditThisList();
                WaitFind.FindElem(PrivateNote, 10);
                PrivateNote.SendKeys("Concepts of Physics");
                Thread.Sleep(KortextGlobals.s);
                WaitFind.FindElem(PublicNote, 10);
                PublicNote.SendKeys("Basics of Physics");
                Thread.Sleep(KortextGlobals.s);
                WaitFind.FindElem(ListSummary, 10);
                ListSummary.SendKeys("Physics");
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(NewTemplateList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);

                IList<IWebElement> AddDocuments_Icon = new List<IWebElement>(Driver.Instance.FindElements(AddDocument_locator));
                if (AddDocuments_Icon.Count > 0 && AddDocuments_Icon.Count > 7)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        switch (i)
                        {
                            case 7:
                                Klick.On(AddDocuments_Icon[i]);
                                Thread.Sleep(KortextGlobals.s);
                                Pages.PearlEditBuffer.AddCitations("Canada", "WorldCat");
                                Thread.Sleep(KortextGlobals.s);
                                break;

                            case 6:
                                Klick.On(AddDocuments_Icon[i]);
                                Thread.Sleep(KortextGlobals.s);
                                Pages.PearlEditBuffer.AddCitations("India", "WorldCat");
                                Thread.Sleep(KortextGlobals.s);
                                break;

                            case 5:
                                Klick.On(AddDocuments_Icon[i]);
                                Thread.Sleep(KortextGlobals.s);
                                Pages.PearlEditBuffer.AddCitations("British", "WorldCat");
                                Thread.Sleep(KortextGlobals.s);
                                break;
                            case 4:
                                Klick.On(AddDocuments_Icon[i]);
                                Thread.Sleep(KortextGlobals.s);
                                Pages.PearlEditBuffer.AddCitations("France", "WorldCat");
                                Thread.Sleep(KortextGlobals.s);
                                break;

                            case 3:
                                Klick.On(AddDocuments_Icon[i]);
                                Thread.Sleep(KortextGlobals.s);
                                Pages.PearlEditBuffer.AddCitations("Australia", "WorldCat");
                                Thread.Sleep(KortextGlobals.s);
                                break;

                            case 2:
                                Klick.On(AddDocuments_Icon[i]);
                                Thread.Sleep(KortextGlobals.s);
                                Pages.PearlEditBuffer.AddCitations("Singapore", "WorldCat");
                                Thread.Sleep(KortextGlobals.s);
                                break;

                            case 1:
                                Klick.On(AddDocuments_Icon[i]);
                                Thread.Sleep(KortextGlobals.s);
                                Pages.PearlEditBuffer.AddCitations("Thailand", "WorldCat");
                                Thread.Sleep(KortextGlobals.s);
                                break;

                            case 0:
                                Klick.On(AddDocuments_Icon[i]);
                                Thread.Sleep(KortextGlobals.s);
                                Pages.PearlEditBuffer.AddCitations("Africa", "WorldCat");
                                Thread.Sleep(KortextGlobals.s);
                                break;

                            default:
                                Console.WriteLine("Incorrect Add Document Icon fetched.");
                                return false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Either Add Documents icon NOT present in the List Page, or less number of Add Documents icons present on the screen.");
                    return false;
                }

                numberofitemsbeforepublish = Pages.PearlEditBuffer.DisplayTotalNumberofItems();
                Pages.PearlEditBuffer.PublishingList();

                //Verify the action performed above
                Driver.Instance.Url = currentURL;
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);
                Pages.TraverseBufferPage.EditThisList();
                numberofitemsafterpublish = Pages.PearlEditBuffer.DisplayTotalNumberofItems();

                if(numberofitemsbeforepublish == numberofitemsafterpublish)
                {
                    Pages.PearlEditBuffer.PublishingList();
                    Console.WriteLine("Adding Initial Documents to List Completed");
                    return true;
                }
                else
                {
                    Console.WriteLine("Error Adding Initial Documents to List.");
                    return false;
                }                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in PearlNewListAddDocs.cs: " + e.Message);
                return false;
            }
        }
    }
}



