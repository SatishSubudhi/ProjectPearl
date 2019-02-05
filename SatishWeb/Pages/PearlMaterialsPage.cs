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
using AutoItX3Lib;

namespace PearlFramework
{
    public class PearlMaterialsPage
    {
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "adminSearch.q")]
        protected IWebElement MaterialNameSearchTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'adminSearch.doSearch()']")]
        protected IWebElement MaterialNameSearchButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'newTag.text']")]
        protected IWebElement AddTagTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click= 'adminGlobalTagsController.finish()']")]
        protected IWebElement TagItemsButton
        {
            get;
            set;
        }
        
        By MaterialSearchResults_locator = By.CssSelector("table[st-table = 'adminSearch.displayedCollection']");
        By MaterialTitle_locator = By.CssSelector("span[ng-if = 'item.metadata.title']");
        By MaterialEdit_locator = By.CssSelector("button[ng-click = \"requestCtrl.create('change')\"]");
        By MaterialChangeRequest_locator = By.CssSelector("button[ng-click = \"requestCtrl.create('scan')\"]");
        By MaterialRequestDigi_locator = By.CssSelector("button[ng-click = 'requestCtrl.logRequestDcs();requestCtrl.assginValue()']");
        By MaterialViewDigi_locator = By.CssSelector("button[ng-click = 'requestCtrl.logViewDcs();requestCtrl.getStatus(requestCtrl.ListService.data.id, requestCtrl.item.id, requestCtrl.secId)']");
        By MaterialExport_locator = By.CssSelector("button[ng-click = 'rebusExport.logExport();']");
        By MaterialTags_locator = By.CssSelector("button[ng-click = 'adminSearch.openTagModal(row)']");
        By MaterialLists_locator = By.CssSelector("button[ng-click = 'adminSearch.getLists(row)']");
        By locator = By.Id("admin-heading");

        string statusreturntext;
        string currentURL;

        public bool MaterialsPage()
        {
            try
            {
                currentURL = Driver.Instance.Url;

                //Search Materials
                SearchMaterial("Harry Potter");

                //Edit / Update a Material
                EditMaterial("Canada");

                //Export Material
                ExportMaterial("Canada");

                //Edit Materials Global Tags
                EditGlobalTags("Canada");

                // View Lists that are using the Material
                ViewListsUsed("Canada");

                //Request Copy
                RequestCopy("Canada");

                /* Commenting these functions as the |Functions should not be shown in Materials page
                //Request Digitisation - New
                RequestDigiNew("Canada");

                //Request Digitisation - Existing
                RequestDigiExisting("Canada");

                //View Digitisation
                ViewDigi("Canada");
                */

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in MaterialsPage.cs: " + e.Message);
                return false;
            }
        }

        public void SearchMaterial(string searchtext)
        {
            WaitFind.FindElem(MaterialNameSearchTextField, 10).Clear();
            Klick.On(MaterialNameSearchTextField);
            Thread.Sleep(KortextGlobals.s);
            MaterialNameSearchTextField.SendKeys(searchtext);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(MaterialNameSearchButton);
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            //IList<IWebElement> MaterialsSearchResults = Driver.Instance.FindElements(MaterialSearchResults_locator);
            if(MaterialsSearched.Count > 0)
            {
                Console.WriteLine("Materials present for the Searched text");
                /*
                foreach(IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                    Driver.HighlightElement(MaterialTitle);
                    Console.WriteLine("Material Title: " + MaterialTitle.Text);
                }
                */
            }
            else
            {
                Console.WriteLine("No Materials present for the Searched text");
            }
        }
        public void EditMaterial(string searchtext)
        {
            SearchMaterial(searchtext);
            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            if (MaterialsSearched.Count > 0)
            {
                Console.WriteLine("Edit Material. Materials present for the Searched text");
                foreach (IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    Driver.HighlightElement(MaterialSearched);
                    IWebElement EditMaterialButton = MaterialSearched.FindElement(MaterialEdit_locator);
                    if (EditMaterialButton.Displayed)
                    {
                        IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                        Console.WriteLine("Edit Material Button found for : " + MaterialTitle.Text);
                        Klick.On(EditMaterialButton);
                        Pages.PearlEditBuffer.EditCitation();
                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                        if (statusreturntext != "Material changes saved")
                        {
                            Console.WriteLine("Error while Editing Material." + statusreturntext);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Editing Material Successful");
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Edit Material. No Materials present for the Searched text");
            }
        }
        public void ExportMaterial(string searchtext)
        {
            SearchMaterial(searchtext);
            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            if (MaterialsSearched.Count > 0)
            {
                Console.WriteLine("Export Material. Materials present for the Searched text");
                foreach (IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    Driver.HighlightElement(MaterialSearched);
                    IWebElement ExportMaterialButton = MaterialSearched.FindElement(MaterialExport_locator);
                    if (ExportMaterialButton.Displayed)
                    {
                        IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                        Console.WriteLine("Export Material Button found for : " + MaterialTitle.Text);
                        Klick.On(ExportMaterialButton);
                        Thread.Sleep(KortextGlobals.ll);
                        if(Driver.Instance.Url == currentURL)
                        {
                            Console.WriteLine("Exporting Material Successful");
                            break;
                        }
                        else
                        {
                            Driver.Instance.Url = currentURL;
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Export Material. No Materials present for the Searched text");
            }
        }
        public void EditGlobalTags(string searchtext)
        {
            SearchMaterial(searchtext);
            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            if (MaterialsSearched.Count > 0)
            {
                Console.WriteLine("Edit Global Tags Material. Materials present for the Searched text");
                foreach (IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    Driver.HighlightElement(MaterialSearched);
                    IWebElement TagsMaterialButton = MaterialSearched.FindElement(MaterialTags_locator);
                    if (TagsMaterialButton.Displayed)
                    {
                        IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                        Console.WriteLine("Edit Global Tags Material Button found for : " + MaterialTitle.Text);
                        Klick.On(TagsMaterialButton);
                        Thread.Sleep(KortextGlobals.s);
                        WaitFind.FindElem(AddTagTextField, 10);
                        AddTagTextField.SendKeys("GlobalTag");
                        AddTagTextField.SendKeys(Keys.Tab);
                        Thread.Sleep(KortextGlobals.s);
                        Klick.On(TagItemsButton);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> TagSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tag in globalTagsCtrl.item.tags_global track by tag.id")));
                        if(TagSearched.Count == 1)
                        {
                            Console.WriteLine("Edit Global Tags Material Successful");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Edit Global Tags Material Not Successful." + TagSearched.Count);
                            break;
                        }
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("Edit Global Tags Material. No Materials present for the Searched text");
            }
        }
        public void ViewListsUsed(string searchtext)
        {
            SearchMaterial(searchtext);
            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            if (MaterialsSearched.Count > 0)
            {
                Console.WriteLine("View Lists using Material. Materials present for the Searched text");
                foreach (IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    Driver.HighlightElement(MaterialSearched);
                    IWebElement ListsMaterialButton = MaterialSearched.FindElement(MaterialLists_locator);
                    if (ListsMaterialButton.Displayed)
                    {
                        IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                        Console.WriteLine("View Lists using Material Button found for : " + MaterialTitle.Text);
                        Klick.On(ListsMaterialButton);
                        Thread.Sleep(KortextGlobals.s);
                        IWebElement ListsPageSubTitle = Driver.Instance.FindElement(locator);
                        if(ListsPageSubTitle.Text == "view_list Lists")
                        {
                            List<NgWebElement> ListsUsed = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminLists.ListsService.displayedCollection")));
                            if(ListsUsed.Count > 0)
                            {
                                Thread.Sleep(KortextGlobals.s);
                                Console.WriteLine("View Lists using Material Successful");
                                Driver.Instance.Navigate().Back();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("No List using the Material. Moving to the next Material to check");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Landed to an incorrect page." + ListsPageSubTitle.Text);
                            Driver.Instance.Navigate().Back();
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("View Lists using Material. No Materials present for the Searched text");
            }
        }
        public void RequestCopy(string searchtext)
        {
            SearchMaterial(searchtext);
            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            if (MaterialsSearched.Count > 0)
            {
                Console.WriteLine("RequestCopy Material. Materials present for the Searched text");
                foreach (IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    Driver.HighlightElement(MaterialSearched);
                    IWebElement RequestCopyMaterialButton = MaterialSearched.FindElement(MaterialChangeRequest_locator);
                    if (RequestCopyMaterialButton.Displayed)
                    {
                        IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                        Console.WriteLine("RequestCopy Material Button found for : " + MaterialTitle.Text);
                        Klick.On(RequestCopyMaterialButton);
                        
                        Console.WriteLine("RequestCopy Material Successful");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("RequestCopy Material. No Materials present for the Searched text");
            }
        }
        public void RequestDigiNew(string searchtext)
        {
            SearchMaterial(searchtext);
            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            if (MaterialsSearched.Count > 0)
            {
                Console.WriteLine("RequestDigiNew Material. Materials present for the Searched text");
                foreach (IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    Driver.HighlightElement(MaterialSearched);
                    IWebElement RequestDigiMaterialButton = MaterialSearched.FindElement(MaterialRequestDigi_locator);
                    if (RequestDigiMaterialButton.Displayed)
                    {
                        IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                        Console.WriteLine("RequestDigiNew Material Button found for : " + MaterialTitle.Text);
                        Klick.On(RequestDigiMaterialButton);
                        Console.WriteLine("RequestDigiNew Material Successful");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("RequestDigiNew Material. No Materials present for the Searched text");
            }
        }
        public void RequestDigiExisting(string searchtext)
        {
            SearchMaterial(searchtext);
            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            if (MaterialsSearched.Count > 0)
            {
                Console.WriteLine("RequestDigiExisting Material. Materials present for the Searched text");
                foreach (IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    Driver.HighlightElement(MaterialSearched);
                    IWebElement RequestDigiMaterialButton = MaterialSearched.FindElement(MaterialRequestDigi_locator);
                    if (RequestDigiMaterialButton.Displayed)
                    {
                        IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                        Console.WriteLine("RequestDigiExisting Material Button found for : " + MaterialTitle.Text);
                        Klick.On(RequestDigiMaterialButton);
                        Console.WriteLine("RequestDigiExisting Material Successful");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("RequestDigiExisting Material. No Materials present for the Searched text");
            }
        }
        public void ViewDigi(string searchtext)
        {
            SearchMaterial(searchtext);
            List<NgWebElement> MaterialsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
            if (MaterialsSearched.Count > 0)
            {
                Console.WriteLine("ViewDigi Material. Materials present for the Searched text");
                foreach (IWebElement MaterialSearched in MaterialsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", MaterialSearched);
                    Driver.HighlightElement(MaterialSearched);
                    IWebElement ViewDigiMaterialButton = MaterialSearched.FindElement(MaterialViewDigi_locator);
                    if (ViewDigiMaterialButton.Displayed)
                    {
                        IWebElement MaterialTitle = MaterialSearched.FindElement(MaterialTitle_locator);
                        Console.WriteLine("ViewDigi Material Button found for : " + MaterialTitle.Text);
                        Klick.On(ViewDigiMaterialButton);
                        Console.WriteLine("ViewDigi Material Successful");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("ViewDigi Material. No Materials present for the Searched text");
            }
        }
    }
}



