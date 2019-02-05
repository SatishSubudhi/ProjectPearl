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
    public class PearlEditBuffer
    {
        By add_items_in_subsection_button_locator = By.CssSelector("button[uib-tooltip = 'Add new item in subsection']");

        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'actionCtrl.enterEditMode()']")]
        protected IWebElement EditListData
        {
            get; set;
        }
        //[FindsBy(How = How.XPath, Using = "//form[@id='searchForm']/div/input")]
        [FindsBy(How = How.CssSelector, Using = "input[ng-model = 'addModal.query']")]
        protected IWebElement AddItemSearchText
        {
            get; set;
        }
        //[FindsBy(How = How.XPath, Using = "//form[@id='searchForm']/div/span/button")]
        [FindsBy(How = How.CssSelector, Using = "button[type = 'submit']")]
        protected IWebElement AddItemSearchButton
        {
            get; set;
        }       
        [FindsBy(How = How.XPath, Using = "//div[2]/div/div/button")]
        protected IWebElement CopacResultFirstBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//li/a")]
        protected IWebElement CopacResultFirstBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//select")]
        protected IWebElement FirstBookPriorityTagList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/button")]
        protected IWebElement CopacResultSecondBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/ul/li/a")]
        protected IWebElement CopacResultSecondBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div[2]/select")]
        protected IWebElement SecondBookPriorityTagList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/button")]
        protected IWebElement CopacResultThirdBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/ul/li/a")]
        protected IWebElement CopacResultThirdBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div[2]/select")]
        protected IWebElement ThirdBookPriorityTagList
        {
            get;
            set;
        }

        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div/div[2]/div/div/button")]
        protected IWebElement SecondCopacResultFirstBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div/div[2]/div/div/ul/li/a")]
        protected IWebElement SecondCopacResultFirstBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div/div[2]/div[2]/select")]
        protected IWebElement SecondFirstBookPriorityTagList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div[2]/div[2]/div/div/button")]
        protected IWebElement SecondCopacResultSecondBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div[2]/div[2]/div/div/ul/li/a")]
        protected IWebElement SecondCopacResultSecondBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div[2]/div[2]/div[2]/select")]
        protected IWebElement SecondSecondBookPriorityTagList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div[3]/div[2]/div/div/button")]
        protected IWebElement SecondCopacResultThirdBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div[3]/div[2]/div/div/ul/li/a")]
        protected IWebElement SecondCopacResultThirdBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[2]/div[2]/div/div/div/div[3]/div[2]/div[2]/select")]
        protected IWebElement SecondThirdBookPriorityTagList
        {
            get;
            set;
        }

        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div/div[2]/div/div/button")]
        protected IWebElement ThirdCopacResultFirstBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div/div[2]/div/div/ul/li/a")]
        protected IWebElement ThirdCopacResultFirstBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div/div[2]/div[2]/select")]
        protected IWebElement ThirdFirstBookPriorityTagList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div[2]/div[2]/div/div/button")]
        protected IWebElement ThirdCopacResultSecondBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div[2]/div[2]/div/div/ul/li/a")]
        protected IWebElement ThirdCopacResultSecondBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div[2]/div[2]/div[2]/select")]
        protected IWebElement ThirdSecondBookPriorityTagList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div[3]/div[2]/div/div/button")]
        protected IWebElement ThirdCopacResultThirdBookList
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div[3]/div[2]/div/div/ul/li/a")]
        protected IWebElement ThirdCopacResultThirdBookSelect
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/div/div/div/div[3]/div[2]/div[2]/select")]
        protected IWebElement ThirdThirdBookPriorityTagList
        {
            get;
            set;
        }

        //[FindsBy(How = How.XPath, Using = "//div[3]/button")]
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'addModal.finish()']")]
        protected IWebElement CopacFinishButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'adminMaterialController.finish()']")]
        protected IWebElement ChangeRequestFinishButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'adminMaterialController.cancel()']")]
        protected IWebElement ChangeRequestCancelButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "i.mfb-component__child-icon.ng-binding")]
        protected IWebElement AddItemBookBubble
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Publish draft']")]
        protected IWebElement PublishDraftButton
        {
            get;
            set;
        }
        /*
        [FindsBy(How = How.XPath, Using = "//div[2]/button[4]")]
        protected IWebElement SelectFirstCitation
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//li[2]/fieldset/div/div/div[2]/div/div[2]/button[4]")]
        protected IWebElement SelectSecondCitation
        {
            get;
            set;
        }
        [FindsBy(How = How.XPath, Using = "//li[3]/fieldset/div/div/div[2]/div/div[2]/button[4]")]
        protected IWebElement SelectThirdCitation
        {
            get;
            set;
        }
        */
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Create sublist from selected']")]
        protected IWebElement CreateSublistFromSelected
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Append selected to existing list']")]
        protected IWebElement AppendSelectedtoExistingList
        {
            get;
            set;
        }
        [FindsBy(How = How.PartialLinkText, Using = "Copy selected material")]
        protected IWebElement CopySelectedMaterial
        {
            get;
            set;
        }
        [FindsBy(How = How.PartialLinkText, Using = "Move selected material")]
        protected IWebElement MoveSelectedMaterial
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "list-search-input")]
        protected IWebElement SearchListInput
        {
            get;
            set;
        }
        //[FindsBy(How = How.XPath, Using = "//div[@id='list-search-modal']/div[3]/div/div/button")]
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'listChooser.finish(this)']")]
        protected IWebElement SearchListFinishButton
        {
            get;
            set;
        }
        [FindsBy(How = How.Name, Using = "listname")]
        protected IWebElement SublistName
        {
            get;
            set;
        }
        //[FindsBy(How = How.XPath, Using = "//div[@id='hierarchy-node-modal']/div[3]/div/div/button")]
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'nodeModal.finish(this)']")]
        protected IWebElement SublistFinishButton
        {
            get;
            set;
        }
        //[FindsBy(How = How.XPath, Using = "//div[@id='copy-move-results-modal']/div[3]/div/div/button")]
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'copyMoveResults.close()']")]
        protected IWebElement ResultsFinishButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'actionCtrl.tagSelected()']")]
        protected IWebElement TagSelectedItembutton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "div.tags")]
        protected IWebElement AddTagTextFocus
        {
            get;
            set;
        }
        //[FindsBy(How = How.XPath, Using = "//div[@id='tag-selected-modal']/tags-input/div/div/input")]
        [FindsBy(How = How.CssSelector, Using = "input[ng-model = 'newTag.text']")]
        protected IWebElement AddTagTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-primary")]
        protected IWebElement TagItemsButton
        {
            get;
            set;
        }        
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'actionCtrl.openModerateModal()']")]
        protected IWebElement ViewChangesMadeInThisDraftButton
        {
            get;
            set;
        }
        //[FindsBy(How = How.XPath, Using = "//div[@id='diff-modal']/div[3]/div/div/button")]
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'diffModal.cancel()']")]
        protected IWebElement ViewChangesCloseButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Save draft']")]
        protected IWebElement SaveDraftButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Submit draft']")]
        protected IWebElement SubmitDraftButton
        {
            get;
            set;
        }
        //[FindsBy(How = How.XPath, Using = "//div[@id='buffer-preview-heading']/div/div/button[3]")]
        [FindsBy(How = How.CssSelector, Using = "button[ng-show = 'actionCtrl.displayModAccept()']")]
        protected IWebElement AprroveDraftButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model = 'actionsCtrl.subsectionName']")]
        protected IWebElement SubSectionNameField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "div.input-group.ng-scope > span.input-group-btn > button.btn.btn-default")]
        protected IWebElement SubSectionAddButton
        {
            get;
            set;
        }
        [FindsBy(How = How.LinkText, Using = "Material link URLs")]
        protected IWebElement MaterialLinkURLs
        {
            get;
            set;
        }
        [FindsBy(How = How.LinkText, Using = "Identifiers")]
        protected IWebElement Identifiers
        {
            get;
            set;
        }
        [FindsBy(How = How.Name, Using = "no-isbn")]
        protected IWebElement ISBNCheckBox
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "title")]
        protected IWebElement titletextfield
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "web_link")]
        protected IWebElement weblinktextfield
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Undo']")]
        protected IWebElement UndoButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Redo']")]
        protected IWebElement RedoButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-if = 'adminMaterialController.canReview()']")]
        protected IWebElement ViewChangeRequestButton
        {
            get;
            set;
        }
        [FindsBy(How = How.ClassName, Using = "change-finish-button")]
        protected IWebElement CRSaveButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-default")]
        protected IWebElement CRCloseButton
        {
            get;
            set;
        }

        string localxpath;
        int SublistNameappend = 1;
        string statusreturntext;
        string totalnumberofitemsbefore;
        string totalnumberofitemsafter;
        string totalnumberofitemsinterim;
        string currentURL;

        public bool EditBuffer()
        {
            try
            {
                currentURL = Driver.Instance.Url;
                
                EditList();
                 
                //Moving a Section from one position to another
                DragAndDrop(0, 6);
                statusreturntext = StatusMessage();
                if (statusreturntext != "Change saved")
                {
                    Console.WriteLine("Error while Moving items from 0 to 6." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Change Saved Successful");
                }
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                Thread.Sleep(2000);
                //PublishingList();
                Console.WriteLine("Moving a Section from one position to another Completed");

                //Moving a Citation within the Section
                DragAndDrop(1, 2);
                statusreturntext = StatusMessage();
                if (statusreturntext != "Change saved")
                {
                    Console.WriteLine("Error while Moving items from 1 to 2." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Change Saved Successful");
                }
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                Thread.Sleep(2000);
                //PublishingList();
                Console.WriteLine("Moving a Citation within the Section Completed");

                //Moving a Citation from one a Section to sub-Section
                DragAndDrop(1, 4);
                statusreturntext = StatusMessage();
                if (statusreturntext != "Change saved")
                {
                    Console.WriteLine("Error while Moving items from 1 to 4." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Change Saved Successful");
                }
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                Thread.Sleep(2000);
                //PublishingList();
                Console.WriteLine("Moving a Citation from one a Section to sub-Section Completed");

                //Moving a Citation from one a Section to another Section
                DragAndDrop(1, 6);
                statusreturntext = StatusMessage();
                if (statusreturntext != "Change saved")
                {
                    Console.WriteLine("Error while Moving items from 1 to 6." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Change Saved Successful");
                }
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                Thread.Sleep(2000);
                Console.WriteLine("Moving a Citation from one a Section to another Section Completed");
                PublishingList();

                //Adding Journals to the List
                AddDocumenttoSection("journal", "WorldCat");
                PublishingList();
                Console.WriteLine("Adding Journals to the List Completed");

                //Adding Articles to the List
                AddDocumenttoSection("smith", "WorldCat");
                PublishingList();
                Console.WriteLine("Adding Articles to the List Completed");

                //Adding List Alternatives
                AddingListAlternatives();
                                
                //Create Sub-list from Selected - Copy Material
                CreateSubListFromSelectedCopyMaterial();
                //Verify the Action performed
                Driver.Instance.Navigate().GoToUrl(currentURL);
                Thread.Sleep(KortextGlobals.l);
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                Thread.Sleep(KortextGlobals.l);
                int sublistcount = Driver.Instance.FindElements(By.LinkText("TestSubList" + (SublistNameappend - 1))).Count;
                if(sublistcount > 0)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    Console.WriteLine("Create Sub-list from Selected - Copy Material Completed");
                }
                else
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    Console.WriteLine("Create Sub-list from Selected - Copy Material Unsuccessful");
                }

                //Tag Selected items
                Driver.Instance.Navigate().GoToUrl(currentURL);
                TagSelectedItems();
                
                //Create Sub-list from Selected - Move Material
                Driver.Instance.Navigate().GoToUrl(currentURL);
                CreateSubListFromSelectedMoveMaterial();
                //Verify the Action performed
                Driver.Instance.Navigate().GoToUrl(currentURL);
                Thread.Sleep(KortextGlobals.s);
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                Thread.Sleep(KortextGlobals.s);
                sublistcount = Driver.Instance.FindElements(By.LinkText("TestSubList" + (SublistNameappend - 1))).Count;
                if (sublistcount > 0)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    Console.WriteLine("Create Sub-list from Selected - Move Material Completed");
                }
                else
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    Console.WriteLine("Create Sub-list from Selected - Move Material Unsuccessful");
                }

                //Append Selected to existing List - Copy Material
                Driver.Instance.Navigate().GoToUrl(currentURL);
                AppendSelectedtoExistingListCopyMaterial();
                
                //Append Selected to existing List - Move Material
                Driver.Instance.Navigate().GoToUrl(currentURL);
                AppendSelectedtoExistingListMoveMaterial();
                
                //View Changes made in this draft - Add Citation
                Driver.Instance.Navigate().GoToUrl(currentURL);
                ViewChangesMadeInThisDraftAddCitation();
                                
                //View Changes made in this draft - Delete Citation
                ViewChangesMadeInThisDraftDeleteCitation();
                
                // View Changes made in this draft - Move Citation
                ViewChangesMadeInThisDraftMoveCitation();
                
                // Save Draft
                SaveDraft();

                // Submit Draft
                SubmitDraft();

                //Approve the Draft Submitted above
                ApproveDraftSubmitted();

                // Add SubSection
                AddSubSection();
                
                //Add Material to SubSection
                AddMaterialtoSubsection();
                
                //Create Change Request
                CreateChangeRequest();

                //Review Change Request from My Requests - From the Materials page itself
                ReviewChangeRequestMaterialPage();

                //Review Change Request from My Requests - From the Requests page
                CreateChangeRequest();
                ReviewChangeRequestRequestsPage();

                // Undo
                Driver.Instance.Navigate().GoToUrl(currentURL);
                Undo();
                
                // Redo
                Redo();
                
                //Undo (combined with View Changes made in this draft button)
                UndoCombinedwithViewChanges();
                
                //Redo (combined with View Changes made in this draft button)
                RedoCombinedwithViewChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in PearlEditBuffer.cs: " + e.Message);
                return false;
            }
        }
        public void selectlibraryformaterial(string libraryname)
        {
            IList<IWebElement> AllLibraryElements = Driver.Instance.FindElements(By.ClassName("panel-heading"));
            string[] alllibrarytext = new string[AllLibraryElements.Count];
            int j = 0;
            int k = 0;
            localxpath = null;
            foreach (IWebElement element in AllLibraryElements)
            {
                alllibrarytext[j] = element.Text;
                if (alllibrarytext[j].Contains(libraryname))
                {
                    if (j == 0)
                    {
                        localxpath = "//span/h4";
                        k = j;
                    }
                    else
                    {
                        localxpath = "//div[" + (j + 1) + "]/div/h4/a/span/h4";
                        k = j;
                    }
                }
                j++;
            }
            if (localxpath == null)
            {
                localxpath = "//span/h4";
            }

            var w3 = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(20));
            var SearchResultLibrary = w3.Until(x => x.FindElement(By.XPath(localxpath)));
            Klick.On(SearchResultLibrary);

            Thread.Sleep(KortextGlobals.s);
            if (k == 0)
            {
                Klick.On(CopacResultFirstBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(CopacResultFirstBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(FirstBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(CopacResultSecondBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(CopacResultSecondBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(SecondBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(CopacResultThirdBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(CopacResultThirdBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(ThirdBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
            }
            else if (k == 1)
            {
                Klick.On(SecondCopacResultFirstBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(SecondCopacResultFirstBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(SecondFirstBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(SecondCopacResultSecondBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(SecondCopacResultSecondBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(SecondSecondBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(SecondCopacResultThirdBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(SecondCopacResultThirdBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(SecondThirdBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
            }
            else if (k == 2)
            {
                Klick.On(ThirdCopacResultFirstBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(ThirdCopacResultFirstBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(ThirdFirstBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(ThirdCopacResultSecondBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(ThirdCopacResultSecondBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(ThirdSecondBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(ThirdCopacResultThirdBookList);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(ThirdCopacResultThirdBookSelect);
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(ThirdThirdBookPriorityTagList).SelectByIndex(1);
                Thread.Sleep(KortextGlobals.s);
            }
        }
        public string StatusMessage()
        {
            for(int i = 0; i< 30; i++)
            {
                if (Driver.Instance.FindElements(By.CssSelector("div.toast-message")).Count > 0)
                {
                    string StatusMessageText = Driver.Instance.FindElement(By.CssSelector("div.toast-message")).Text;
                    Console.WriteLine("StatusMessageText = " + StatusMessageText);
                    return StatusMessageText;
                }
                Thread.Sleep(1000);
            }
            return "Status Message Not Shown";            
        }
        public void PublishingList()
        {
            Thread.Sleep(KortextGlobals.s);
            Klick.On(PublishDraftButton);
            Thread.Sleep(KortextGlobals.s);
            for (int j = 0; j<1000; j++)
            {
                if(Driver.Instance.FindElements(By.CssSelector("button[ng-click = 'actionCtrl.enterEditMode()']")).Count > 0)
                {
                    Console.WriteLine("Successfully Published");
                    j = 1000;
                }
                else if(j==300 || j==600 || j==900)
                {
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.ll);
                    Klick.On(PublishDraftButton);
                }
                Thread.Sleep(1000);
            }
        }
        public void ClickAddMaterialtoSection(int position)
        {
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> AddDocumentstoSectionButton = Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip = 'Add new item in section']"));
            Klick.On(AddDocumentstoSectionButton[position]);
            Thread.Sleep(KortextGlobals.ll);
        }
        public void ClickAddMaterialtoSubSection(string subsectionname)
        {
            List<NgWebElement> all_section_details = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Model("sectionCtrl.section.materials")));

            foreach (NgWebElement each_section in all_section_details)
            {
                IList<IWebElement> subsections = new List<IWebElement>(each_section.FindElements(By.ClassName("section-header")));
                foreach(IWebElement subsection in subsections)
                {
                    Driver.HighlightElement(subsection);
                    IWebElement subsectiontitle = subsection.FindElement(By.CssSelector("a[editable-text='headerCtrl.subsection.title']"));
                    Driver.HighlightElement(subsectiontitle);
                    if(subsectiontitle.Text == subsectionname)
                    {
                        IWebElement subsectionaddingmaterials = subsection.FindElement(By.CssSelector("button[ng-click = 'actionsCtrl.addNewItem()']"));
                        Klick.On(subsectionaddingmaterials);
                        Thread.Sleep(KortextGlobals.ll);
                    }
                }
            }
        }
        public void ClickRequestChangesCitation(int position)
        {
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> RequestChangesCitationButton = Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip = 'Request changes']"));
            Klick.On(RequestChangesCitationButton[position]);
            Thread.Sleep(KortextGlobals.ll);
        }
        public void ClickAddListAlternatives(int position)
        {
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> AddListAlternativesButton = Driver.Instance.FindElements(By.CssSelector("button[ng-click = 'actionsCtrl.addAlternatives()']"));
            Klick.On(AddListAlternativesButton[position]);
            Thread.Sleep(KortextGlobals.ll);
        }
        public void EditList()
        {
            Thread.Sleep(KortextGlobals.l);
            Klick.On(EditListData);
            Thread.Sleep(KortextGlobals.ll);

            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.ll);

        }
        public void AddCitations(string searchtext,string librarytext)
        {
            totalnumberofitemsbefore = DisplayTotalNumberofItems();
            //Console.WriteLine("after capturing total number of items");
            WaitFind.FindElem(AddItemSearchText, 10);
            AddItemSearchText.SendKeys(searchtext);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(AddItemSearchButton);
            Thread.Sleep(20000);
            selectlibraryformaterial(librarytext);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(CopacFinishButton);
            Thread.Sleep(KortextGlobals.l);
            totalnumberofitemsafter = DisplayTotalNumberofItems();
            if (totalnumberofitemsbefore == totalnumberofitemsafter)
            {
                Console.WriteLine("Error while Adding Documents for: " + searchtext);
            }
            else
            {
                Console.WriteLine("Documents added succesfully for:" + searchtext);
            }
        }
        public void AddDocumenttoSection(string passingsearchtext,string passinglibrarytext)
        {
            EditList();
            ClickAddMaterialtoSection(0);
            AddCitations(passingsearchtext, passinglibrarytext);
        }
        public void DragAndDrop(int sourcelocation, int destlocation)
        {
            Thread.Sleep(KortextGlobals.ll); //Need to wait for all the blue boxed in bottom right to finish.
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 700)");
            IList<IWebElement> source = Driver.Instance.FindElements(By.ClassName("angular-ui-tree-handle"));  
            IList<IWebElement> destination = Driver.Instance.FindElements(By.ClassName("angular-ui-tree-handle"));
            Driver.HighlightElement(source[sourcelocation]);
            Driver.HighlightElement(destination[destlocation]);

            //Drag and Drop things
            Actions actions = new Actions(Driver.Instance);
            actions.MoveToElement(source[sourcelocation]).Build().Perform();
            actions.ClickAndHold(source[sourcelocation]).Build().Perform();
            actions.DragAndDrop(source[sourcelocation], destination[destlocation]).Build().Perform();
            //Thread.Sleep(3000);
            /*statusreturntext = StatusMessage();
            if (statusreturntext != "Change saved")
            {
                Console.WriteLine("Error while Moving items from " + sourcelocation + " to " + destlocation);
            }
            else
            {
                Console.WriteLine("Change Saved Successful");
            }
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            Thread.Sleep(2000);*/
        }
        public void selectfirst3items()
        {
            EditList();

            List<IWebElement> SelectButtons = new List<IWebElement>(Driver.Instance.FindElements(By.CssSelector("button[ng-click = 'actionsCtrl.selectToggle()']")));
            if (SelectButtons.Count > 0)
            {
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SelectButtons[0]);
                Klick.On(SelectButtons[0]);
                Thread.Sleep(KortextGlobals.s);

                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SelectButtons[1]);
                Klick.On(SelectButtons[1]);
                Thread.Sleep(KortextGlobals.s);

                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SelectButtons[2]);
                Klick.On(SelectButtons[2]);
                Thread.Sleep(KortextGlobals.s);

                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            }
            else
            {
                Console.WriteLine("No Citations present to be seleted.");
            }
        }
        public string DisplayTotalNumberofItems()
        {
            Driver.HighlightElement(Driver.Instance.FindElement(By.Id("list-item-count")));
            string totalnumberofitemslist = Driver.Instance.FindElement(By.Id("list-item-count")).Text;
            return totalnumberofitemslist;
        }
        public string ListTitle()
        {
            //Function to Get the Title of the List in the View List Page
            return Driver.Instance.FindElement(By.Id("list-title")).Text;
        }
        public void AddingListAlternatives()
        {
            EditList();
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 700)");
            ClickAddListAlternatives(0);
            AddCitations("google", "WorldCat");
            PublishingList();
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            Thread.Sleep(KortextGlobals.l);
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 500)");
            Thread.Sleep(KortextGlobals.l);
            int Alternativescount = Driver.Instance.FindElements(By.CssSelector("button[ng-click = 'actionCtrl.toggleAlternatives()']")).Count;
            if(Alternativescount > 0)
            {
                Console.WriteLine("Adding List Alternatives Completed");
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            }
            else
            {
                Console.WriteLine("Error while Adding Alternatives");
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            }
        }
        public void TagSelectedItems()
        {
            Thread.Sleep(KortextGlobals.ll);
            selectfirst3items();
            Klick.On(TagSelectedItembutton);
            Thread.Sleep(KortextGlobals.s);
            WaitFind.FindElem(AddTagTextField, 10);
            AddTagTextField.SendKeys("AutomationTag");
            AddTagTextField.SendKeys(Keys.Tab);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(TagItemsButton);
            Thread.Sleep(KortextGlobals.s);
            PublishingList();
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 700)");
            int tagscount = Driver.Instance.FindElements(By.CssSelector("span[uib-tooltip = \"Filter items with local tag 'AutomationTag'\"]")).Count;
            Console.WriteLine("tagscount = " + tagscount);
            Thread.Sleep(KortextGlobals.s);
            if (tagscount > 0)
            {
                Console.WriteLine("Tag Selected items Completed");
            }
            else
            {
                Console.WriteLine("Could not find the Added tags");
            }
        }
        public void CreateSubListFromSelectedCopyMaterial()
        {
            Thread.Sleep(KortextGlobals.ll);
            selectfirst3items();
            Klick.On(CreateSublistFromSelected);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(CopySelectedMaterial);
            Thread.Sleep(KortextGlobals.ll);
            WaitFind.FindElem(SublistName, 10);
            SublistName.SendKeys("TestSubList" + SublistNameappend);
            SublistNameappend++;
            Thread.Sleep(KortextGlobals.s);
            Klick.On(SublistFinishButton);
            Thread.Sleep(KortextGlobals.xl);
            Driver.Instance.FindElement(By.ClassName("text-success"));
            Klick.On(ResultsFinishButton);
        }
        public void CreateSubListFromSelectedMoveMaterial()
        {
            Thread.Sleep(KortextGlobals.ll);
            selectfirst3items();
            Klick.On(CreateSublistFromSelected);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(MoveSelectedMaterial);
            Thread.Sleep(KortextGlobals.ll);
            WaitFind.FindElem(SublistName, 10);
            SublistName.SendKeys("TestSubList" + SublistNameappend);
            SublistNameappend++;
            Thread.Sleep(KortextGlobals.s);
            Klick.On(SublistFinishButton);
            Thread.Sleep(KortextGlobals.xl);
            Driver.Instance.FindElement(By.ClassName("text-success"));
            Klick.On(ResultsFinishButton);
        }
        public void AppendSelectedtoExistingListCopyMaterial()
        {
            Thread.Sleep(KortextGlobals.ll);
            selectfirst3items();
            Klick.On(AppendSelectedtoExistingList);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(CopySelectedMaterial);
            Thread.Sleep(KortextGlobals.s);
            WaitFind.FindElem(SearchListInput, 10);
            SearchListInput.SendKeys("List");
            Thread.Sleep(KortextGlobals.l);
            IList<IWebElement> SearchListResults = Driver.Instance.FindElements(By.CssSelector("button[ng-click ='listChooser.toggleList(list)']"));
            Klick.On(SearchListResults[0]);
            Thread.Sleep(KortextGlobals.l);
            Klick.On(SearchListFinishButton);
            Thread.Sleep(60000);
            if(Driver.Instance.FindElements(By.ClassName("text-success")).Count == 0)
            {
                Console.WriteLine("Appending taking longer than usual");
            }
            Klick.On(ResultsFinishButton);
            Console.WriteLine("Append Selected to existing List - Copy Material Completed");
        }
        public void AppendSelectedtoExistingListMoveMaterial()
        {
            selectfirst3items();
            Klick.On(AppendSelectedtoExistingList);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(MoveSelectedMaterial);
            Thread.Sleep(KortextGlobals.s);
            WaitFind.FindElem(SearchListInput, 10);
            SearchListInput.SendKeys("List");
            Thread.Sleep(KortextGlobals.l);
            IList<IWebElement> SearchListResults = Driver.Instance.FindElements(By.CssSelector("button[ng-click ='listChooser.toggleList(list)']"));
            Klick.On(SearchListResults[0]);
            Thread.Sleep(KortextGlobals.l);
            Klick.On(SearchListFinishButton);
            Thread.Sleep(60000);
            if (Driver.Instance.FindElements(By.ClassName("text-success")).Count == 0)
            {
                Console.WriteLine("Appending taking longer than usual");
            }
            Klick.On(ResultsFinishButton);
            Console.WriteLine("Append Selected to existing List - Move Material Completed");
        }
        public void ViewChangesMadeInThisDraftAddCitation()
        {
            Thread.Sleep(KortextGlobals.ll);
            AddDocumenttoSection("Antarctica", "WorldCat");
            Klick.On(ViewChangesMadeInThisDraftButton);
            Thread.Sleep(KortextGlobals.xl);
            string AddedItemsVar = Driver.Instance.FindElement(By.CssSelector("div.diff-stat-added.ng-binding")).Text;
            if (AddedItemsVar == "add 3 items added")
            {
                Klick.On(ViewChangesCloseButton);
                Thread.Sleep(KortextGlobals.l);
                PublishingList();
                Console.WriteLine("View Changes made in this draft - Add Citation Completed");
            }
            else
            {
                Klick.On(ViewChangesCloseButton);
                Console.WriteLine("Incorrect Number of Items-Added Displayed. Expected = add 3 items added; Actual = " + AddedItemsVar);
                Klick.On(SaveDraftButton);
                Thread.Sleep(KortextGlobals.ll);
            }
        }
        public void ViewChangesMadeInThisDraftDeleteCitation()
        {
            EditList();
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 700)");
            IList<IWebElement> DeleteCitation_Buttons = Driver.Instance.FindElements(By.CssSelector("button[ng-click ='actionsCtrl.deleteItem()']"));
            if (DeleteCitation_Buttons.Count > 0)
            {
                Klick.On(DeleteCitation_Buttons[0]);
                statusreturntext = StatusMessage();
                if (statusreturntext == "Change saved")
                {
                    Console.WriteLine("Deleting First Citation Successful.");
                    IList<IWebElement> secondDeleteCitation_Buttons = Driver.Instance.FindElements(By.CssSelector("button[ng-click ='actionsCtrl.deleteItem()']"));
                    if (secondDeleteCitation_Buttons.Count > 0)
                    {
                        Klick.On(secondDeleteCitation_Buttons[0]);
                        statusreturntext = StatusMessage();
                        if (statusreturntext == "Change saved")
                        {
                            Console.WriteLine("Deleting Second Citation Successful.");
                            IList<IWebElement> thirdDeleteCitation_Buttons = Driver.Instance.FindElements(By.CssSelector("button[ng-click ='actionsCtrl.deleteItem()']"));
                            if (thirdDeleteCitation_Buttons.Count > 0)
                            {
                                Klick.On(thirdDeleteCitation_Buttons[0]);
                                statusreturntext = StatusMessage();
                                if (statusreturntext == "Change saved")
                                {
                                    Console.WriteLine("Deleting Third Citation Successful.");

                                    Klick.On(ViewChangesMadeInThisDraftButton);
                                    Thread.Sleep(KortextGlobals.xl);
                                    string DeletedItemsVar = Driver.Instance.FindElement(By.CssSelector("div.diff-stat-deleted.ng-binding")).Text;
                                    if (DeletedItemsVar == "delete 3 items removed")
                                    {
                                        Klick.On(ViewChangesCloseButton);
                                        Thread.Sleep(KortextGlobals.l);
                                        PublishingList();
                                        Console.WriteLine("View Changes made in this draft - Delete Citation Completed");
                                    }
                                    else
                                    {
                                        Klick.On(ViewChangesCloseButton);
                                        Console.WriteLine("Incorrect Number of Items-Deleted Displayed. Expected = delete 3 items removed; Actual = " + DeletedItemsVar);
                                        Klick.On(SaveDraftButton);
                                        Thread.Sleep(KortextGlobals.ll);
                                    }
                                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                                }
                                else
                                {
                                    Console.WriteLine("Error while Deleting Third Citation.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("During Third Delete Buttons not found for Citations.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error while Deleting Second Citation.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("During Second Delete Buttons not found for Citations.");
                    }
                }
                else
                {
                    Console.WriteLine("Error while Deleting First Citation.");
                }
            }
            else
            {
                Console.WriteLine("During First Delete Buttons not found for Citations.");
            }
        }
        public void ViewChangesMadeInThisDraftMoveCitation()
        {
            EditList();
            DragAndDrop(1, 2);
            statusreturntext = StatusMessage();
            if (statusreturntext != "Change saved")
            {
                Console.WriteLine("Error while Moving items from 1 to 2." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Change Saved Successful");
            }
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            Thread.Sleep(2000);

            Klick.On(ViewChangesMadeInThisDraftButton);
            Thread.Sleep(KortextGlobals.xl);
            string MovedItemsVar = Driver.Instance.FindElement(By.CssSelector("div.diff-stat-moved.ng-binding")).Text;
            if (MovedItemsVar == "open_with 1 items moved")
            {
                Klick.On(ViewChangesCloseButton);
                Thread.Sleep(KortextGlobals.l);
                PublishingList();
                Console.WriteLine("View Changes made in this draft - Move Citation Completed");
            }
            else
            {
                Klick.On(ViewChangesCloseButton);
                Console.WriteLine("Incorrect Number of Items-Moved Displayed. Expected = open_with 1 items moved; Actual = " + MovedItemsVar);
                Klick.On(SaveDraftButton);
                Thread.Sleep(KortextGlobals.ll);
            }
        }
        public void SaveDraft()
        {
            AddDocumenttoSection("Wisdom", "WorldCat");
            Klick.On(SaveDraftButton);
            statusreturntext = StatusMessage();
            if (statusreturntext != "Draft saved")
            {
                Console.WriteLine("Error while Saving the Draft." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Save Draft Completed");
            }
        }
        public void SubmitDraft()
        {
            AddDocumenttoSection("Einstein", "WorldCat");
            Klick.On(SubmitDraftButton);
            statusreturntext = StatusMessage();
            if (statusreturntext != "Draft moderation request submitted")
            {
                Console.WriteLine("Error while Submitting the Draft." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Submit Draft Completed");
            }
        }
        public void ApproveDraftSubmitted()
        {
            EditList();
            Klick.On(AprroveDraftButton);
            statusreturntext = StatusMessage();
            if (statusreturntext != "Draft approved")
            {
                Console.WriteLine("Error while Approving the Draft." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Approve Draft Completed");
            }
        }
        public void AddSubSection()
        {
            EditList();
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 600)");
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> AddSubSectionButtons = Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip = 'Add new subsection']"));
            Klick.On(AddSubSectionButtons[0]);
            Thread.Sleep(KortextGlobals.s);
            WaitFind.FindElem(SubSectionNameField, 10);
            SubSectionNameField.SendKeys("Subsection Automation 1");
            Thread.Sleep(KortextGlobals.s);
            Klick.On(SubSectionAddButton);
            statusreturntext = StatusMessage();
            if (statusreturntext != "Change saved")
            {
                List<NgWebElement> all_section_details = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Model("sectionCtrl.section.materials")));

                foreach (NgWebElement each_section in all_section_details)
                {
                    IList<IWebElement> subsection_titles = new List<IWebElement>(each_section.FindElements(By.CssSelector("a[editable-text='headerCtrl.subsection.title']")));
                    if (subsection_titles[subsection_titles.Count - 1].Text == "Subsection Automation 1")
                    {
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", subsection_titles[subsection_titles.Count - 1]);
                        Console.WriteLine("Subsection creation Successful");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error while Adding a SubSection." + statusreturntext);
                    }
                }
            }
            else
            {
                Console.WriteLine("Add SubSection Completed");
            }
            Thread.Sleep(KortextGlobals.l);
        }
        public void AddMaterialtoSubsection()
        {
            ClickAddMaterialtoSubSection("Subsection Automation 1");
            AddCitations("Automobile", "WorldCat");
            PublishingList();
            Console.WriteLine("Add Material to SubSection Completed");
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        }
        public void EditCitation()
        {
            WaitFind.FindElem(titletextfield, 10);
            titletextfield.SendKeys(" - trial text message");
            Thread.Sleep(KortextGlobals.s);
            //Driver.HighlightElement(Driver.Instance.FindElement(By.Id("has-isbn")));
            if (Driver.Instance.FindElements(By.Id("has-isbn")).Count > 0)
            {
                Klick.On(Identifiers);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(ISBNCheckBox);
                Thread.Sleep(KortextGlobals.s);
            }
            Klick.On(MaterialLinkURLs);
            WaitFind.FindElem(weblinktextfield, 10);
            weblinktextfield.SendKeys("https://www.amazon.com/Bosnia-Short-History-Noel-Malcolm/dp/0814755615");
            Thread.Sleep(KortextGlobals.s);
            Klick.On(ChangeRequestFinishButton);
            statusreturntext = StatusMessage();
            if (statusreturntext != "Material changes saved")
            {
                Console.WriteLine("Error while Creating Change Request." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Finished Creating Change Request");
            }
        }
        public void CreateChangeRequest()
        {
            EditList();
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 700)");
            ClickRequestChangesCitation(1);
            Thread.Sleep(KortextGlobals.ll);
            EditCitation();
            PublishingList();
            Console.WriteLine("Create Change Request Completed");
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        }
        public void ReviewChangeRequestMaterialPage()
        {
            EditList();
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 700)");
            ClickRequestChangesCitation(1);
            Thread.Sleep(KortextGlobals.l);

            if (ViewChangeRequestButton.Displayed == true)
            {
                Klick.On(ViewChangeRequestButton);
                Thread.Sleep(KortextGlobals.ll);

                ActiononReviewChangeRequest();

                PublishingList();
                Console.WriteLine("Reviewing Change Request Completed");
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            }
            else
            {
                Console.WriteLine("No Active Change Request present for the Citation. No Review Possible");
            }
        }
        public void ReviewChangeRequestRequestsPage()
        {
            int success_flag = 0;
            List<IWebElement> CitationsPresent = new List<IWebElement>(Driver.Instance.FindElements(By.CssSelector("li[ng-repeat = 'item in sectionCtrl.filteredMaterial = (sectionCtrl.section.materials | itemFilter) track by item.id']")));
            if(CitationsPresent.Count > 0)
            {
                IWebElement FirstCitationTitle = CitationsPresent[0].FindElement(By.CssSelector("span[ng-if = 'item.metadata.title']"));
                string FirstCitationTitleText = FirstCitationTitle.Text;

                Pages.LandingPage.ClickOnMenu_RequestsBtn();
                Thread.Sleep(KortextGlobals.ll);

                List<IWebElement> RequestsPresent = new List<IWebElement>(Driver.Instance.FindElements(By.CssSelector("li[ng-repeat = 'row in adminRequests.displayedCollection']")));
                if(RequestsPresent.Count > 0)
                {
                    foreach(IWebElement RequestPresent in RequestsPresent)
                    {
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", RequestPresent);
                        Driver.HighlightElement(RequestPresent);
                        IWebElement FirstIcon = RequestPresent.FindElement(By.ClassName("request-icon"));
                        if (FirstIcon.Text == "edit")
                        {
                            IWebElement FirstRequestTitle = RequestPresent.FindElement(By.CssSelector("span[ng-if = 'item.metadata.title']"));
                            IWebElement ReviewChangesButton = RequestPresent.FindElement(By.CssSelector("button[ng-click = \"doIt('review', request, true)\"]"));
                            if ((FirstRequestTitle.Text == FirstCitationTitleText) && (ReviewChangesButton.Displayed == true))
                            {
                                Klick.On(ReviewChangesButton);
                                Thread.Sleep(KortextGlobals.l);

                                ActiononReviewChangeRequest();

                                Driver.Instance.Navigate().Refresh();
                                Thread.Sleep(KortextGlobals.l);

                                List<IWebElement> PostRefreshRequestsPresent = new List<IWebElement>(Driver.Instance.FindElements(By.CssSelector("li[ng-repeat = 'row in adminRequests.displayedCollection']")));
                                if (PostRefreshRequestsPresent.Count > 0)
                                {
                                    foreach (IWebElement PostRefreshRequestPresent in PostRefreshRequestsPresent)
                                    {
                                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", PostRefreshRequestPresent);
                                        Driver.HighlightElement(PostRefreshRequestPresent);
                                        IWebElement PostRefreshFirstIcon = PostRefreshRequestPresent.FindElement(By.ClassName("request-icon"));
                                        if (PostRefreshFirstIcon.Text == "edit")
                                        {
                                            IWebElement PostRefreshFirstCitationTitle = PostRefreshRequestPresent.FindElement(By.CssSelector("span[ng-if = 'item.metadata.title']"));
                                            if (PostRefreshFirstCitationTitle.Text == FirstCitationTitleText)
                                            {
                                                success_flag = 0;
                                                break;
                                            }
                                            else
                                            {
                                                success_flag = 1;                                                
                                            }
                                        }
                                    }
                                    if(success_flag == 1)
                                    {
                                        Console.WriteLine("Request Reviewed Successfully from Requests Page.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Request still shown on Requests Page post Review.");
                                    }
                                }
                                break;
                            }
                        }
                    }                    
                }
            }
            else
            {
                Console.WriteLine("Error loading View List Page.");
            }
        }
        public void ActiononReviewChangeRequest()
        {
            IList<IWebElement> RejectButtons = Driver.Instance.FindElements(By.CssSelector("label[uib-tooltip = 'Reject change']"));
            IList<IWebElement> AcceptButtons = Driver.Instance.FindElements(By.CssSelector("label[uib-tooltip = 'Accept change']"));

            for (int i = 0; i < RejectButtons.Count; i = i + 2)
            {
                Klick.On(RejectButtons[i]);
                Thread.Sleep(KortextGlobals.s);
            }
            for (int j = 1; j < AcceptButtons.Count; j = j + 2)
            {
                Klick.On(AcceptButtons[j]);
                Thread.Sleep(KortextGlobals.s);
            }

            Klick.On(CRSaveButton);
            statusreturntext = StatusMessage();
            if (statusreturntext != "Request review saved")
            {
                Console.WriteLine("Error while Reviewing Change Request." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Reviewing Change Request Successful");
            }
            Klick.On(CRCloseButton);
            Thread.Sleep(KortextGlobals.ll);
            Klick.On(ChangeRequestFinishButton);
            statusreturntext = StatusMessage();
            if (statusreturntext != "Material changes saved")
            {
                Console.WriteLine("Error while Finishing Reviewing Change Request." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Finished Reviewing Change Request");
            }
            Thread.Sleep(KortextGlobals.ll);
        }
        public void Undo()
        {
            Thread.Sleep(KortextGlobals.ll);
            totalnumberofitemsbefore = DisplayTotalNumberofItems();
            Console.WriteLine("totalnumberofitemsbefore = " + totalnumberofitemsbefore);
            AddDocumenttoSection("sample", "WorldCat");
            Thread.Sleep(KortextGlobals.ll);
            totalnumberofitemsinterim = DisplayTotalNumberofItems();
            Console.WriteLine("totalnumberofitemsinterim = " + totalnumberofitemsinterim);
            Klick.On(UndoButton);
            Thread.Sleep(KortextGlobals.ll);
            totalnumberofitemsafter = DisplayTotalNumberofItems();
            Console.WriteLine("totalnumberofitemsafter = " + totalnumberofitemsafter);
            if (totalnumberofitemsbefore == totalnumberofitemsafter)
            {
                Console.WriteLine("Undo worked good");
            }
            else
            {
                Console.WriteLine("Undo not working well");
            }
            PublishingList();
            Console.WriteLine("Undo Completed");
        }
        public void Redo()
        {
            Thread.Sleep(KortextGlobals.ll);
            totalnumberofitemsbefore = DisplayTotalNumberofItems();
            Console.WriteLine("totalnumberofitemsbefore = " + totalnumberofitemsbefore);
            AddDocumenttoSection("sample", "WorldCat");
            Thread.Sleep(KortextGlobals.l);
            totalnumberofitemsinterim = DisplayTotalNumberofItems();
            Console.WriteLine("totalnumberofitemsinterim = " + totalnumberofitemsinterim);
            Klick.On(UndoButton);
            Thread.Sleep(KortextGlobals.ll);
            Klick.On(RedoButton);
            Thread.Sleep(KortextGlobals.ll);
            totalnumberofitemsafter = DisplayTotalNumberofItems();
            Console.WriteLine("totalnumberofitemsafter = " + totalnumberofitemsafter);
            if (totalnumberofitemsinterim == totalnumberofitemsafter)
            {
                Console.WriteLine("Redo worked good");
            }
            else
            {
                Console.WriteLine("Redo not working well");
            }
            PublishingList();
            Console.WriteLine("Redo Completed");
        }
        public void UndoCombinedwithViewChanges()
        {
            Thread.Sleep(KortextGlobals.ll);
            AddDocumenttoSection("Hollywood", "WorldCat");
            Klick.On(ViewChangesMadeInThisDraftButton);
            Thread.Sleep(KortextGlobals.xl);
            string AddedItemsVar = Driver.Instance.FindElement(By.CssSelector("div.diff-stat-added.ng-binding")).Text;
            if (AddedItemsVar == "add 3 items added")
            {
                Klick.On(ViewChangesCloseButton);
                Thread.Sleep(KortextGlobals.l);
                Klick.On(UndoButton);
                Thread.Sleep(KortextGlobals.l);
                Klick.On(ViewChangesMadeInThisDraftButton);
                Thread.Sleep(KortextGlobals.xl);
                string UndoAddedItemsVar = Driver.Instance.FindElement(By.CssSelector("div.diff-stat-added.ng-binding")).Text;
                if (UndoAddedItemsVar == "add 0 items added")
                {
                    Klick.On(ViewChangesCloseButton);
                    Thread.Sleep(KortextGlobals.l);
                    PublishingList();
                    Console.WriteLine("Undo (combined with View Changes made in this draft button) Completed");
                }
                else
                {
                    Klick.On(ViewChangesCloseButton);
                    Console.WriteLine("Undo (combined with View Changes made in this draft button) not working well. Expected = add 0 items added; Actual = " + UndoAddedItemsVar);
                    Klick.On(SaveDraftButton);
                    Thread.Sleep(KortextGlobals.ll);
                }
            }
            else
            {
                Klick.On(ViewChangesCloseButton);
                Console.WriteLine("Incorrect Number of Items-Added Displayed. Expected = add 3 items added; Actual = " + AddedItemsVar);
                Klick.On(SaveDraftButton);
                Thread.Sleep(KortextGlobals.ll);
            }
        }
        public void RedoCombinedwithViewChanges()
        {
            Thread.Sleep(KortextGlobals.ll);
            AddDocumenttoSection("Bollywood", "WorldCat");
            Klick.On(ViewChangesMadeInThisDraftButton);
            Thread.Sleep(KortextGlobals.xl);
            string AddedItemsVar = Driver.Instance.FindElement(By.CssSelector("div.diff-stat-added.ng-binding")).Text;
            if (AddedItemsVar == "add 3 items added")
            {
                Klick.On(ViewChangesCloseButton);
                Thread.Sleep(KortextGlobals.l);
                Klick.On(UndoButton);
                Thread.Sleep(KortextGlobals.l);
                Klick.On(ViewChangesMadeInThisDraftButton);
                Thread.Sleep(KortextGlobals.xl);
                string UndoAddedItemsVar = Driver.Instance.FindElement(By.CssSelector("div.diff-stat-added.ng-binding")).Text;
                if (UndoAddedItemsVar == "add 0 items added")
                {
                    Klick.On(ViewChangesCloseButton);
                    Thread.Sleep(KortextGlobals.l);
                    Klick.On(RedoButton);
                    Thread.Sleep(KortextGlobals.l);
                    Klick.On(ViewChangesMadeInThisDraftButton);
                    Thread.Sleep(KortextGlobals.xl);
                    string RedooAddedItemsVar = Driver.Instance.FindElement(By.CssSelector("div.diff-stat-added.ng-binding")).Text;
                    if (RedooAddedItemsVar == "add 3 items added")
                    {
                        Klick.On(ViewChangesCloseButton);
                        Thread.Sleep(KortextGlobals.l);
                        PublishingList();
                        Console.WriteLine("Redo (combined with View Changes made in this draft button) Completed");
                    }
                    else
                    {
                        Klick.On(ViewChangesCloseButton);
                        Console.WriteLine("Redo (combined with View Changes made in this draft button) not working well. Expected = add 3 items added; Actual = " + RedooAddedItemsVar);
                        Klick.On(SaveDraftButton);
                        Thread.Sleep(KortextGlobals.ll);
                    }
                }
                else
                {
                    Klick.On(ViewChangesCloseButton);
                    Console.WriteLine("Undo (combined with View Changes made in this draft button) not working well. Expected = add 0 items added; Actual = " + UndoAddedItemsVar);
                    Klick.On(SaveDraftButton);
                    Thread.Sleep(KortextGlobals.ll);
                }
            }
            else
            {
                Klick.On(ViewChangesCloseButton);
                Console.WriteLine("Incorrect Number of Items-Added Displayed. Expected = add 3 items added; Actual = " + AddedItemsVar);
                Klick.On(SaveDraftButton);
                Thread.Sleep(KortextGlobals.ll);
            }
        }
        public void GoTo()
        {
            Thread.Sleep(4000);
            Driver.Instance.Navigate().GoToUrl("https://kortext.rebuslist.com/#/list/670");
            Thread.Sleep(1000);
        }
    }
}



