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
    public class PearlAcquisitionsPage
    {
        //  [FindsBy(How = How.CssSelector, Using = "input[ng-model = 'acqController.newTag.name']")]
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "acqSettingsCtrl.newPriorityTagName")]
        protected IWebElement NewPriorityNameTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'acqSettingsCtrl.newPriorityTag()']")]
        protected IWebElement NewPriorityNameAddButton
        {
            get;
            set;
        }
        /*[FindsBy(How = How.CssSelector, Using = "ol[ng-repeat= 'acqController.PriorityTagsService.allPriorityTags']")]
        protected IWebElement ALL_Tags
        {
            get;
            set;
        }*/
        //   [FindsBy(How = How.CssSelector, Using = "input[ng-model= '$parent.$data']")]
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "$parent.$data")]
        protected IWebElement EditInPlaceField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[title= 'Submit']")]
        protected IWebElement PurchasingRatioSubmitButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[title= 'Cancel']")]
        protected IWebElement PurchasingRatioCancelButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Cancel delete']")]
        protected IWebElement cancel_delete_button
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Confirm delete']")]
        protected IWebElement confirm_delete_button
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "select[ng-model = 'rP.replacementPriorityId']")]
        protected IWebElement DeleteSelectAlternatePriorityTag
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "select[ng-model = 'aC.selectedPriorityType']")]
        protected IWebElement AddSelectMaterialType
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'rP.save()']")]
        protected IWebElement SaveButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'aC.save()']")]
        protected IWebElement AddPriorityTagSaveButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'rP.save()']")]
        protected IWebElement ReplacePriorityTagSaveButton
        {
            get;
            set;
        }

        By Tag_Names = By.CssSelector("a[editable-text = 'priority.name']");
        By PurchasingRatio_Books = By.CssSelector("a[editable-number = 'priority.ratio.books']");
        By PurchasingRatio_Students = By.CssSelector("a[editable-number = 'priority.ratio.students']");
        By delete_button_locator = By.CssSelector("button[uib-tooltip = 'Delete']");
        By PurchasingRatioSubmitButton1 = By.CssSelector("button[title= 'Submit']");
        //By onbeforesave = By.CssSelector("a[onbeforesave = 'acqController.editInPlace(row, $data, \'ratio.books\')']");
        By AcquisitionsTabName_locator = By.CssSelector("a[ng-click = 'select($event)']");
        By AllowedMaterialsSelected_locator = By.ClassName("multiselect-selected-text");
        By AllowedMaterialArticle_locator = By.CssSelector("input[value = 'string:article']");
        By AllowedMaterialBook_locator = By.CssSelector("input[value = 'string:book']");
        By AddPriorityTagPopUp_locator = By.CssSelector("h3[class = 'request-title']");
        By ReplacePriorityTagPopUp_locator = By.CssSelector("h3[class = 'modal-title']");

        string statusreturntext;
        string tagnamesearch;
      // int tagfoundat = 0;
        string firsttagname;
        string newglobaltagcreated;

        public bool AcquisitionsPage()
        {
            try
            {
                //Navigate to the Acquistions Settings Page
                NavigatetoAcquisitionSettings();

                //Select Books and Articles in Allowed Material Types
                SelectAllowedMaterialTypes();

                //Add 3 New Priority Tags
                CreatePriorityTag("Global");
                newglobaltagcreated = tagnamesearch;

                CreatePriorityTag("Book");

                CreatePriorityTag("Article");

                //Move Tags
                TagDragAndDrop("Global", 0, 1);

                TagDragAndDrop("Global", 2, 0);

                //Edit / Update Purchasing Ratio -Books
                EditPriorityTag(tagnamesearch, "Article", "No Change", "10", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to 10."+ statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to 10 Successful");
                }*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "10", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to 10." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to 10 Successful");
                }*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "A1", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to A1." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to A1 Successful");
                }*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "-1", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "You must enter a valid ratio greater than or equal to zero")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to -1." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to -1 Successful");
                }
                Klick.On(PurchasingRatioCancelButton);
                Thread.Sleep(KortextGlobals.s);*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "You must enter a valid ratio greater than or equal to zero")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to BLANK." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to BLANK Successful");
                }
                Klick.On(PurchasingRatioCancelButton);
                Thread.Sleep(KortextGlobals.s);*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "1.7", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext == "Priority tag updated")
                {
                    Console.WriteLine("Application allowed Purchasing Ratio Books to be updated to 1.7");
                }
                else
                {
                    Console.WriteLine("Purchasing Ratio Books not updated to 1.7." + statusreturntext);
                    Klick.On(PurchasingRatioCancelButton);
                    Thread.Sleep(KortextGlobals.s);
                }*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "EE", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext == "Priority tag updated")
                {
                    Console.WriteLine("Application allowed Purchasing Ratio Books to be updated to EE");
                }
                else
                {
                    Console.WriteLine("Purchasing Ratio Books not updated to EE." + statusreturntext);
                    Klick.On(PurchasingRatioCancelButton);
                    Thread.Sleep(KortextGlobals.s);
                }*/

                //Edit / Update Purchasing Ratio -Students
                EditPriorityTag(tagnamesearch, "Article", "No Change", "No Change", "10");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to 10." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to 10 Successful");
                }*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "No Change", "10");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to 10." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to 10 Successful");
                }*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "No Change", "A1");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to A1." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to A1 Successful");
                }*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "No Change", "-1");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "You must enter a valid ratio greater than or equal to zero")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to -1." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to -1 Successful");
                }
                Klick.On(PurchasingRatioCancelButton);
                Thread.Sleep(KortextGlobals.s);*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "No Change", "");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "You must enter a valid ratio greater than or equal to zero")
                {
                    Console.WriteLine("Error while Updating Purchasing Ratio Books to BLANK." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Purchasing Ratio Books to BLANK Successful");
                }
                Klick.On(PurchasingRatioCancelButton);
                Thread.Sleep(KortextGlobals.s);*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "No Change", "1.7");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext == "Priority tag updated")
                {
                    Console.WriteLine("Application allowed Purchasing Ratio Students to be updated to 1.7");
                }
                else
                {
                    Console.WriteLine("Purchasing Ratio Students not updated to 1.7." + statusreturntext);
                    Klick.On(PurchasingRatioCancelButton);
                    Thread.Sleep(KortextGlobals.s);
                }*/

                EditPriorityTag(tagnamesearch, "Article", "No Change", "No Change", "EE");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext == "Priority tag updated")
                {
                    Console.WriteLine("Application allowed Purchasing Ratio Students to be updated to EE");
                }
                else
                {
                    Console.WriteLine("Purchasing Ratio Students not updated to EE." + statusreturntext);
                    Klick.On(PurchasingRatioCancelButton);
                    Thread.Sleep(KortextGlobals.s);
                }*/

                //Edit / Update Priority Tag Name
                EditPriorityTag(tagnamesearch, "Article", tagnamesearch, "No Change", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "A tag with this name already exists")
                {
                    Console.WriteLine("Error while Updating Priority Tag Name to " + tagnamesearch + "." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Priority Tag Name to " + tagnamesearch + " Successful");
                }
                Klick.On(PurchasingRatioCancelButton);
                Thread.Sleep(KortextGlobals.s);*/

                EditPriorityTag(tagnamesearch, "Article", "abcd1234", "No Change", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Priority Tag Name to abcd1234." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Priority Tag Name to abcd1234 Successful");
                }*/

                EditPriorityTag("abcd1234", "Article", "!@#$%^&*()", "No Change", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Priority Tag Name to !@#$%^&*()." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Priority Tag Name to !@#$%^&*() Successful");
                }*/

                EditPriorityTag("!@#$%^&*()", "Article", "12345", "No Change", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Priority Tag Name to 12345." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Priority Tag Name to 12345 Successful");
                }*/

                //EditPriorityTag("12345", "Article", firsttagname, "No Change", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag updated")
                {
                    Console.WriteLine("Error while Updating Priority Tag Name to " + firsttagname + "." + statusreturntext);
                    Klick.On(PurchasingRatioCancelButton);
                }
                else
                {
                    Console.WriteLine("Updating Priority Tag Name to " + firsttagname + " Successful");
                }
                Thread.Sleep(KortextGlobals.s);*/

                EditPriorityTag(newglobaltagcreated, "Global", firsttagname, "No Change", "No Change");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "A tag with this name and type already exists")
                {
                    Console.WriteLine("Error while Updating Priority Tag Name to " + firsttagname + "." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Updating Priority Tag Name to " + firsttagname + " Successful");
                }
                Klick.On(PurchasingRatioCancelButton);
                Thread.Sleep(KortextGlobals.s);*/

                //Delete Priority Tag
                DeletePriorityTag("12345", "Article");
                /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag deleted")
                {
                    Console.WriteLine("Error while Deleting Priority Tag." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Deleting Priority Tag Successful");
                }*/

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in AcquisitionsPage.cs: " + e.Message);
                return false;
            }
        }

        public void NavigatetoAcquisitionSettings()
        {
            Thread.Sleep(KortextGlobals.s);
            List<NgWebElement> AcquisitionsTabs = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in acqController.formTabs")));
            if (AcquisitionsTabs.Count > 0)
            {
                foreach (IWebElement AcquisitionsTab in AcquisitionsTabs)
                {
                    Driver.HighlightElement(AcquisitionsTab);
                    IWebElement TabName = AcquisitionsTab.FindElement(AcquisitionsTabName_locator);
                    if (TabName.Text == "Settings")
                    {
                        Klick.On(TabName);
                        Thread.Sleep(KortextGlobals.s);
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Acquisitions Page");
            }
        }

        public void SelectAllowedMaterialTypes()
        {
            IList<IWebElement> AllowedMaterialsDropDown = new List<IWebElement>(Driver.Instance.FindElements(By.CssSelector("form[sf-schema = 'acqSettingsCtrl.schema']")));
            if (AllowedMaterialsDropDown.Count > 0)
            {
                foreach (IWebElement Dropdown in AllowedMaterialsDropDown)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Dropdown);
                    Driver.HighlightElement(Dropdown);

                    IWebElement AllowedMaterialsSelected = Dropdown.FindElement(AllowedMaterialsSelected_locator);
                    Klick.On(AllowedMaterialsSelected);

                    IWebElement AllowedMaterialArticle = Dropdown.FindElement(AllowedMaterialArticle_locator);
                    if (AllowedMaterialArticle.Selected == false)
                    {
                        Klick.On(AllowedMaterialArticle);
                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                        if (statusreturntext != "Priority Type added")
                        {
                            Console.WriteLine("Error while selecting Allowed Material Article." + statusreturntext);
                        }
                        else
                        {
                            Console.WriteLine("Allowed Material Article selecting Successful.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Allowed Material Article already selected." + statusreturntext);
                    }
                    Thread.Sleep(KortextGlobals.s);

                    IWebElement AllowedMaterialBook = Dropdown.FindElement(AllowedMaterialBook_locator);
                    if (AllowedMaterialBook.Selected == false)
                    {
                        Klick.On(AllowedMaterialBook);
                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                        if (statusreturntext != "Priority Type added")
                        {
                            Console.WriteLine("Error while selecting Allowed Material Book." + statusreturntext);
                        }
                        else
                        {
                            Console.WriteLine("Allowed Material Book selecting Successful.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Allowed Material Book already selected." + statusreturntext);
                    }

                    //IWebElement AllowedMaterialsSelected = Dropdown.FindElement(AllowedMaterialsSelected_locator);
                    Klick.On(AllowedMaterialsSelected);

                    Thread.Sleep(KortextGlobals.s);
                    Console.WriteLine("Allowed Materials Selection Completed.");
                    break;
                }           
            }
        }

        public void CreatePriorityTag(string MaterialType)
        {
            tagnamesearch = Pages.PearlCreateReadingList.SearchandReturnNewUnitName("Tag");
            NewPriorityNameTextField.SendKeys(tagnamesearch);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(NewPriorityNameAddButton);

            if (Driver.Instance.FindElement(AddPriorityTagPopUp_locator).Text == "Add Priority Tag")
            {
                new SelectElement(AddSelectMaterialType).SelectByText(MaterialType);
                Klick.On(AddPriorityTagSaveButton);
                //Thread.Sleep(KortextGlobals.s);
            }

            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "Priority tag created")
            {
                Console.WriteLine("Error while Creating Purchasing Ratio Books." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Creating Priority Tag Successful");
            }
            if (statusreturntext == "A maximum of 10 tags is allowed")
            {
                Console.WriteLine("Maximum Limit Reached for Priority Tags. Will Delete last tag to proceed.");
                string tagtobedeleted = SearchPrioritytagName(10);
                DeletePriorityTag(tagtobedeleted, MaterialType);
                Klick.On(NewPriorityNameAddButton);
                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "Priority tag created")
                {
                    Console.WriteLine("Error while Creating Purchasing Ratio Books." + statusreturntext);
                }
                else
                {
                    Console.WriteLine("Creating Priority Tag Successful");
                }
            }

            EditPriorityTag(tagnamesearch, MaterialType ,"No Change", "3", "No Change");
            /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "Priority tag updated")
            {
                Console.WriteLine("Error while Updating Purchasing Ratio Books." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Updating Purchasing Ratio Books Successful");
            }*/

            EditPriorityTag(tagnamesearch, MaterialType, "No Change", "No Change", "13");
            /*statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "Priority tag updated")
            {
                Console.WriteLine("Error while Updating Purchasing Ratio Students." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Updating Purchasing Ratio Students Successful");
            } 
            Thread.Sleep(KortextGlobals.s);*/
        }

        public void DeletePriorityTag(string tagname, string tagname_materialtype)
        {
            NgWebElement thistagname;
            int tagfound_flag = 0;

            List<NgWebElement> all_rows_group = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("type in acqSettingsCtrl.PriorityTagsService.types")));
            foreach (NgWebElement thisgroup in all_rows_group)
            {
                Driver.HighlightElement(thisgroup);
                IWebElement groupname = thisgroup.FindElement(By.ClassName("panel-heading"));
                if (groupname.Text == ("Priority tags for Material type '" + tagname_materialtype + "'"))
                {
                    //   IList<IWebElement> All_DeleteTagIcons = new List<IWebElement>(Driver.Instance.FindElements(delete_button_locator));
                    List<NgWebElement> all_rows = new List<NgWebElement>(thisgroup.FindElements(NgBy.Repeater("priority in type.priorities")));

                    //    for (int i = 0; i < all_rows.Count; i++)
                    foreach (NgWebElement this_row in all_rows)
                    {                //Find the matching tag name
                        thistagname = this_row.FindElement(Tag_Names);
                        Driver.HighlightElement(thistagname);
                        if (all_rows.Count == 0)  //This will stop it from crashing and burning because no element found
                        {
                            Console.WriteLine("There are currently no tags created");
                            break;
                        }
                        if (thistagname.Text == tagname)
                        //this is the row we want.
                        {
                            tagfound_flag = 1;
                            var deleteme = this_row.FindElement(delete_button_locator);
                            Klick.On(deleteme);
                            Thread.Sleep(KortextGlobals.s);
                            Klick.On(confirm_delete_button);
                            Thread.Sleep(KortextGlobals.s);

                            if (Driver.Instance.FindElement(ReplacePriorityTagPopUp_locator).Text == "Select Replacement Priority Tag")
                            {
                                new SelectElement(DeleteSelectAlternatePriorityTag).SelectByIndex(1);
                                Klick.On(ReplacePriorityTagSaveButton);
                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                if (statusreturntext == "Priority tag deleted")
                                {
                                    Console.WriteLine("Deleting Priority Tag Name Successful. " + tagname);
                                }
                                else
                                {
                                    Console.WriteLine("Error while Deleting Priority Tag. " + statusreturntext);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Incorrect pop-up window opened on clicking Delete Priority Tag");
                                break;
                            }                           
                            break; //leave loop now that we have deleted this.
                        }
                    }
                    break;
                }
            }
            if (tagfound_flag == 0)
            {
                Console.WriteLine("Tag not found in the Material type category " + tagname_materialtype + ".");
            }
            Thread.Sleep(KortextGlobals.s);
        }

        public void EditPriorityTag(string tagname, string tagname_materialtype, string new_tagname, string new_ratiobooks, string new_ratiostudents)
        {
            NgWebElement thistagname;
            NgWebElement thisbookratio;
            NgWebElement thisstudentratio;
            string currenttag = "";
            string currentratiobooks = "";
            string currentratiostudents = "";
            int tagfound_flag = 0;


            List<NgWebElement> all_rows_group = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("type in acqSettingsCtrl.PriorityTagsService.types")));
            foreach(NgWebElement thisgroup in all_rows_group)
            {
                Driver.HighlightElement(thisgroup);
                IWebElement groupname = thisgroup.FindElement(By.ClassName("panel-heading"));
                if (groupname.Text == ("Priority tags for Material type '" + tagname_materialtype + "'"))
                {
                    List<NgWebElement> all_rows = new List<NgWebElement>(thisgroup.FindElements(NgBy.Repeater("priority in type.priorities")));

                    //    for (int i = 0; i < all_rows.Count; i++)
                    foreach (NgWebElement this_row in all_rows)
                    {
                        //Find the matching tag name
                        thistagname = this_row.FindElement(Tag_Names);
                        Driver.HighlightElement(thistagname);
                        if (all_rows.Count == 0)  //This will stop it from crashing and burning because no element found
                        {
                            Console.WriteLine("There are currently no tags created");
                            break;
                        }
                        if (firsttagname == null)
                        {
                            firsttagname = thistagname.Text;
                        }
                        if (thistagname.Text == tagname)
                        //this is the row we want.
                        {
                            tagfound_flag = 1;

                            if (new_tagname != "No Change")
                            {
                                currenttag = thistagname.Text;
                                thistagname.Click();
                                //Ugly little hack to find out what you typed in field
                                var typed = this_row.FindElement(NgBy.Model("$parent.$data"));
                                //as soon as you click it, the editable field requires a new locator - EditInPlaceField.

                                EditInPlaceField.Clear();
                                EditInPlaceField.SendKeys(new_tagname);
                                var whatItyped = typed.Evaluate("$parent.$data");
                                Klick.On(PurchasingRatioSubmitButton);
                                //Console.WriteLine("Original Tag: " + currenttag + " What I entered: " + whatItyped + " New Tag: " + thistagname.Text);
                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                if (statusreturntext == "Priority tag updated")
                                {
                                    Console.WriteLine("Updating Priority Tag Name Successful to " + new_tagname + ". " + statusreturntext);
                                }
                                else if (statusreturntext == "A tag with this name already exists")
                                {
                                    Console.WriteLine("Tag Name already exists. " + tagnamesearch);
                                    Klick.On(PurchasingRatioCancelButton);
                                }
                                else if (statusreturntext == "A tag with this name and type already exists")
                                {
                                    Console.WriteLine("Tag Name already exists. " + tagnamesearch);
                                    Klick.On(PurchasingRatioCancelButton);
                                }
                                else
                                {
                                    Console.WriteLine("Error while Updating Priority Tag Name to " + new_tagname + ". " + statusreturntext);
                                }
                            }

                            //Updating Purchasing Ratio Books
                            if (new_ratiobooks != "No Change")
                            {
                                thisbookratio = this_row.FindElement(PurchasingRatio_Books);
                                currentratiobooks = thisbookratio.Text;
                                thisbookratio.Click();
                                //as soon as you click it, the editable field requires a new locator - EditInPlaceField.
                                EditInPlaceField.Clear();
                                EditInPlaceField.SendKeys(new_ratiobooks);
                                var typed = this_row.FindElement(NgBy.Model("$parent.$data"));
                                var whatItyped = typed.Evaluate("$parent.$data");
                                Klick.On(PurchasingRatioSubmitButton);
                                //Console.WriteLine("Original Book Ratio: " + currentratiobooks + " What I entered: " + whatItyped + " New Book Ratio: " + thisbookratio.Text);
                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                if (statusreturntext == "Priority tag updated")
                                {
                                    Console.WriteLine("Updating Purchasing Ratio Books Successful to " + new_ratiobooks + ". " + statusreturntext);
                                }
                                else if (statusreturntext == "You must enter a valid ratio greater than or equal to zero")
                                {
                                    Console.WriteLine("Updating Purchasing Ratio Books Successful.");
                                    Klick.On(PurchasingRatioCancelButton);
                                }
                                else if (statusreturntext == "A tag with this name already exists")
                                {
                                    Console.WriteLine("Tag Name already exists. " + tagnamesearch);
                                    Klick.On(PurchasingRatioCancelButton);
                                }
                                else if (statusreturntext == "A tag with this name and type already exists")
                                {
                                    Console.WriteLine("Tag Name already exists. " + tagnamesearch);
                                    Klick.On(PurchasingRatioCancelButton);
                                }
                                else
                                {
                                    if(new_ratiobooks == "1.7")
                                    {
                                        Console.WriteLine("Purchasing Ratio Books not updated to 1.7." + statusreturntext);
                                        Klick.On(PurchasingRatioCancelButton);
                                    }
                                    else if(new_ratiobooks == "EE")
                                    {
                                        Console.WriteLine("Purchasing Ratio Books not updated to EE." + statusreturntext);
                                        Klick.On(PurchasingRatioCancelButton);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while Updating Purchasing Ratio Books to " + new_ratiobooks + ". " + statusreturntext);
                                    }
                                }
                            }

                            //Updating Purchasing Ratio Students
                            if (new_ratiostudents != "No Change")
                            {
                                thisstudentratio = this_row.FindElement(PurchasingRatio_Students);
                                currentratiostudents = thisstudentratio.Text;
                                thisstudentratio.Click();
                                //as soon as you click it, the editable field requires a new locator - EditInPlaceField.
                                EditInPlaceField.Clear();
                                EditInPlaceField.SendKeys(new_ratiostudents);
                                //Ugly hack to get what you typed on the screen.
                                var typed = this_row.FindElement(NgBy.Model("$parent.$data"));
                                var whatItyped = typed.Evaluate("$parent.$data");
                                Klick.On(PurchasingRatioSubmitButton);
                                //Console.WriteLine("Original Student Ratio: " + currentratiostudents + " What I entered: " + whatItyped + " New Student Ratio: " + thisstudentratio.Text);
                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                if (statusreturntext == "Priority tag updated")
                                {
                                    Console.WriteLine("Updating Purchasing Ratio Students Successful to " + new_ratiostudents + ". " + statusreturntext);
                                }
                                else if (statusreturntext == "You must enter a valid ratio greater than or equal to zero")
                                {
                                    Console.WriteLine("Updating Purchasing Ratio Students Successful.");
                                    Klick.On(PurchasingRatioCancelButton);
                                }
                                else if (statusreturntext == "A tag with this name already exists")
                                {
                                    Console.WriteLine("Tag Name already exists. " + tagnamesearch);
                                    Klick.On(PurchasingRatioCancelButton);
                                }
                                else if (statusreturntext == "A tag with this name and type already exists")
                                {
                                    Console.WriteLine("Tag Name already exists. " + tagnamesearch);
                                    Klick.On(PurchasingRatioCancelButton);
                                }
                                else
                                {
                                    if (new_ratiostudents == "1.7")
                                    {
                                        Console.WriteLine("Purchasing Ratio Students not updated to 1.7." + statusreturntext);
                                        Klick.On(PurchasingRatioCancelButton);
                                    }
                                    else if (new_ratiostudents == "EE")
                                    {
                                        Console.WriteLine("Purchasing Ratio Students not updated to EE." + statusreturntext);
                                        Klick.On(PurchasingRatioCancelButton);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while Updating Purchasing Ratio Students to " + new_ratiostudents + ". " + statusreturntext);
                                    }
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            if (tagfound_flag == 0)
            {
                Console.WriteLine("Tag not found in the Material type category. " + tagname_materialtype);
            }
            Thread.Sleep(KortextGlobals.s);
        }

        public string SearchPrioritytagName(int position)
        {
            IList<IWebElement> All_TagNames = Driver.Instance.FindElements(Tag_Names);
            Console.WriteLine("Tag Name Searched:" + All_TagNames[position - 1].Text);
            Driver.HighlightElement(All_TagNames[position - 1]);
            return All_TagNames[position -1].Text;
        }

        public void TagDragAndDrop(string tagname_materialtype, int sourcelocation, int destlocation)
        {
            List<NgWebElement> all_rows_group = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("type in acqSettingsCtrl.PriorityTagsService.types")));
            foreach (NgWebElement thisgroup in all_rows_group)
            {
                Driver.HighlightElement(thisgroup);
                IWebElement groupname = thisgroup.FindElement(By.ClassName("panel-heading"));
                if (groupname.Text == ("Priority tags for Material type '" + tagname_materialtype + "'"))
                {
                    Thread.Sleep(KortextGlobals.s); //Need to wait for all the blue boxed in bottom right to finish.
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", groupname);
                    IList<NgWebElement> source = thisgroup.FindElements(By.ClassName("angular-ui-tree-handle"));
                    IList<NgWebElement> destination = thisgroup.FindElements(By.ClassName("angular-ui-tree-handle"));
                    Driver.HighlightElement(source[sourcelocation]);
                    Driver.HighlightElement(destination[destlocation]);

                    //Drag and Drop things
                    Actions actions = new Actions(Driver.Instance);
                    actions.MoveToElement(source[sourcelocation]).Build().Perform();
                    actions.ClickAndHold(source[sourcelocation]).Build().Perform();
                    actions.DragAndDrop(source[sourcelocation], destination[destlocation]).Build().Perform();
                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                    if (statusreturntext == "Priority tag updated")
                    {
                        Console.WriteLine("Moving Tags Successful from  " + sourcelocation + " to " + destlocation);
                    }
                    else
                    {
                        Console.WriteLine("Error while Moving items from  " + sourcelocation + " to " + destlocation + "." + statusreturntext);
                    }
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    Thread.Sleep(KortextGlobals.s);
                }
            }
        }
    }
}



