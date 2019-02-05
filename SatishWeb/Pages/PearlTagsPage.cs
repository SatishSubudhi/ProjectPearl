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
    public class PearlTagsPage
    {
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "adminTags.newTag")]
        protected IWebElement TagNameSearchTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'adminTags.createTag()']")]
        protected IWebElement TagNameSearchButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'adminTags.searchSt']")]
        protected IWebElement FilterTagTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= '$parent.$data']")]
        protected IWebElement TagUpdateTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[type = 'submit']")]
        protected IWebElement TagUpdateSubmit
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = '$form.$cancel()']")]
        protected IWebElement TagUpdateCancel
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "label[uib-btn-radio='50']")]
        protected IWebElement Select50perPage
        {
            get;
            set;
        }


        By TagTitle_locator = By.CssSelector("a[onbeforesave = 'adminTags.updateTag(row.id,row.text,$data)']");
        By TagFindPage_locator = By.CssSelector("span[ng-click = 'adminTags.goToMaterial(row)']");
        By TagListCount_locator = By.CssSelector("td[class = 'ng-binding']");
        By TagDelete_locator = By.CssSelector("button[ng-click = 'adminTags.deleteTag(row)']");
        By locator = By.Id("admin-heading");
        By MaterialsSelectPage_locator = By.CssSelector("a[ng-click = 'selectPage(page)']");

        string statusreturntext;
        string currentURL;
        string TagName;
        string UserNameText;
        int usernameappend;
        int actual_count = 0;
        string expected_count;
        int materialsiconfound_flag = 0;

        public bool TagsPage()
        {
            try
            {
                currentURL = Driver.Instance.Url;
                
                TagName = SearchAndReturnNewTagName("GlobalTag");

                //Add a New Tag
                AddNewTag(TagName);

                //Filter / Search Tags
                FilterTags(TagName);

                //Edit / Update Tag
                UpdateTag(TagName,"New" + TagName);

                UpdateTag("New" + TagName, "New" + TagName);

                UpdateTag("New" + TagName, "abcd1234");

                UpdateTag("abcd1234", "abcd!@#$%^&*()");

                UpdateTag("abcd!@#$%^&*()", "12345");

                UpdateTag("12345", TagName);
                
                //Find Materials Associated with the Tag
                FindMaterialsAssociated();

                //Delete Tag
                DeleteTags(TagName);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in TagsPage.cs: " + e.Message);
                return false;
            }
        }

        private string SearchAndReturnNewTagName(string username)
        {
            //search for TestUser and increment suffix until you find one that hasn't been created yet.
            //Return that user name to be added.
            usernameappend = 1;
            for (int i = 0; i < i + 1; i++)
            {
                UserNameText = username + usernameappend;
                WaitFind.FindElem(FilterTagTextField, 10).Clear();
                Klick.On(FilterTagTextField);
                Thread.Sleep(KortextGlobals.s);
                FilterTagTextField.SendKeys(UserNameText);
                Thread.Sleep(KortextGlobals.s);
                try
                {
                    List<NgWebElement> TagsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminTags.displayedCollection")));
                    if(TagsSearched.Count > 0)
                    {
                        usernameappend = usernameappend + 1;
                    }
                    else
                    {
                        Console.WriteLine("Username found" + UserNameText);
                        return UserNameText;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Search -" + UserNameText + " User Not Found; Using this username " + e.Message);
                    return UserNameText;
                }
            }
            return UserNameText;
        }

        public void AddNewTag(string searchtext)
        {
            WaitFind.FindElem(TagNameSearchTextField, 10).Clear();
            Klick.On(TagNameSearchTextField);
            Thread.Sleep(KortextGlobals.s);
            TagNameSearchTextField.SendKeys(searchtext);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(TagNameSearchButton);
            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "Tag created")
            {
                Console.WriteLine("Error while Creating a New Global Tag." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Creating a New Global Tag Successful");
            }
            Thread.Sleep(KortextGlobals.s);
            Console.WriteLine("Searching Tag after Creation");
            FilterTags(searchtext);
        }
        public void FilterTags(string searchtext)
        {
            int i = 0;
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.ll);
            WaitFind.FindElem(FilterTagTextField, 10).Clear();
            Klick.On(FilterTagTextField);
            Thread.Sleep(KortextGlobals.s);
            FilterTagTextField.SendKeys(searchtext);
            Thread.Sleep(KortextGlobals.s);

            List<NgWebElement> TagsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminTags.displayedCollection")));
            if(TagsSearched.Count > 0)
            {
                foreach (IWebElement TagSearched in TagsSearched)
                {
                    IWebElement TagTitle = TagSearched.FindElement(TagTitle_locator);
                    if (TagTitle.Text == searchtext)
                    {
                        Console.WriteLine(searchtext + " Tag found");
                        i = 1;
                        break;
                    }
                }
                if(i==0)
                {
                    Console.WriteLine(searchtext + " Tag not found");
                }
            }
            else
            {
                Console.WriteLine(searchtext + " Tag not found");
            }
        }
        public void UpdateTag(string oldtagname, string newtagname)
        {
            WaitFind.FindElem(FilterTagTextField, 10).Clear();
            Klick.On(FilterTagTextField);
            Thread.Sleep(KortextGlobals.s);
            FilterTagTextField.SendKeys(oldtagname);
            Thread.Sleep(KortextGlobals.s);

            List<NgWebElement> TagsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminTags.displayedCollection")));
            if (TagsSearched.Count > 0)
            {
                foreach (IWebElement TagSearched in TagsSearched)
                {
                    IWebElement TagTitle = TagSearched.FindElement(TagTitle_locator);
                    if (TagTitle.Text == oldtagname)
                    {
                        Klick.On(TagTitle);
                        WaitFind.FindElem(TagUpdateTextField, 10).Clear();
                        Klick.On(TagUpdateTextField);
                        Thread.Sleep(KortextGlobals.s);
                        TagUpdateTextField.SendKeys(newtagname);

                        Klick.On(TagUpdateSubmit);
                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                        if (statusreturntext == "Tag updated")
                        {
                            Console.WriteLine("Tag Update Successful from " + oldtagname + " to " + newtagname);
                            FilterTags(oldtagname);
                            FilterTags(newtagname);
                        }
                        else 
                        {
                            Console.WriteLine("Error while updating tag from " + oldtagname + " to " + newtagname + "." + statusreturntext);
                            FilterTags(oldtagname);
                            FilterTags(newtagname);
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine(oldtagname + " Tag not found to be updated");
            }
        }
        public void DeleteTags(string searchtext)
        {
            WaitFind.FindElem(FilterTagTextField, 10).Clear();
            Klick.On(FilterTagTextField);
            Thread.Sleep(KortextGlobals.s);
            FilterTagTextField.SendKeys(searchtext);
            Thread.Sleep(KortextGlobals.s);

            List<NgWebElement> TagsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminTags.displayedCollection")));
            if (TagsSearched.Count > 0)
            {
                foreach (IWebElement TagSearched in TagsSearched)
                {
                    IWebElement TagTitle = TagSearched.FindElement(TagTitle_locator);
                    if (TagTitle.Text == searchtext)
                    {
                        try
                        {
                            IWebElement TagFindList = TagSearched.FindElement(TagFindPage_locator);
                        }
                        catch
                        {
                            IWebElement TagDeleteButton = TagSearched.FindElement(TagDelete_locator);
                            Klick.On(TagDeleteButton);
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext != "Tag deleted")
                            {
                                Console.WriteLine("Error while Deleting Tag." + statusreturntext);
                            }
                            else
                            {
                                Console.WriteLine("Deleting Tag Successful");
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(searchtext + " Tag not found to be deleted");
            }
            Console.WriteLine("Searching Tag after Deleting");
            FilterTags(searchtext);
        }
        public void FindMaterialsAssociated()
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.ll);
            Klick.On(Select50perPage);
            Thread.Sleep(KortextGlobals.s);

            IList<IWebElement> TagSelectPages = Driver.Instance.FindElements(MaterialsSelectPage_locator);
            if (TagSelectPages.Count > 0)
            {
                foreach (IWebElement TagSelectPage in TagSelectPages)
                {
                    MaterialsAssociated_function();
                    if(materialsiconfound_flag == 1)
                    {
                        materialsiconfound_flag = 0;
                        break;
                    }
                }
            }
            else
            {
                MaterialsAssociated_function();
            }                   
        }
        private void MaterialsAssociated_function()
        {
            List<NgWebElement> TagsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminTags.displayedCollection")));
            if (TagsSearched.Count > 0)
            {
                foreach (IWebElement TagSearched in TagsSearched)
                {
                    Thread.Sleep(KortextGlobals.s);
                    IList<IWebElement> TagFindPage = new List<IWebElement>(TagSearched.FindElements(TagFindPage_locator));
                    if (TagFindPage.Count > 0)
                    {
                        if (TagFindPage[0].Displayed)
                        {
                            materialsiconfound_flag = 1;
                            IWebElement TagCountMaterials = TagSearched.FindElement(TagListCount_locator);
                            expected_count = TagCountMaterials.Text;
                            Console.WriteLine("TagCountMaterials Text = " + TagCountMaterials.Text);
                            expected_count = expected_count.Replace(" FIND_IN_PAGE", "");
                            Klick.On(TagFindPage[0]);
                            Thread.Sleep(KortextGlobals.s);
                            IWebElement ListsPageSubTitle = Driver.Instance.FindElement(locator);
                            if (ListsPageSubTitle.Text == "search Materials")
                            {
                                IList<IWebElement> SelectPages = Driver.Instance.FindElements(MaterialsSelectPage_locator);
                                if (SelectPages.Count > 0)
                                {
                                    foreach (IWebElement SelectPage in SelectPages)
                                    {
                                        Klick.On(SelectPage);
                                        List<NgWebElement> ListsUsed = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
                                        actual_count = actual_count + ListsUsed.Count;
                                        Thread.Sleep(KortextGlobals.s);
                                    }
                                }
                                else
                                {
                                    List<NgWebElement> ListsUsed = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSearch.displayedCollection")));
                                    actual_count = actual_count + ListsUsed.Count;
                                    Thread.Sleep(KortextGlobals.s);
                                }

                                if (Convert.ToString(actual_count) == expected_count)
                                {
                                    Console.WriteLine("The number of Materials Match in FIND_IN_PAGE. expected_count" + expected_count + ". actual_count " + actual_count);
                                }
                                else
                                {
                                    Console.WriteLine("The number of Materials DO NOT match in FIND_IN_PAGE. expected_count" + expected_count + ". actual_count " + actual_count);
                                }
                                Driver.Instance.Url = currentURL;
                            }
                            else
                            {
                                Console.WriteLine("Landed to an incorrect page." + ListsPageSubTitle.Text);
                                Driver.Instance.Navigate().Back();
                                break;
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("No Tags present in the system");
            }
        }
    }
}



