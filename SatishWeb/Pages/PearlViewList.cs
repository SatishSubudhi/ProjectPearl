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
using System.Linq;
using Protractor;
using AutoItX3Lib;

namespace PearlFramework
{
    public class PearlViewList
    {
        [FindsBy(How = How.Id, Using = "filter-dropdown")]
        protected IWebElement FilterListDropDown
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'dropdownCtrl.clear()']")]
        protected IWebElement FilterListDropDownClearALLFilter
        {
            get; set;
        }
        [FindsBy(How = How.Id, Using = "sorting-dropdown")]
        protected IWebElement SortMaterialDropDown
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-click = \"actionCtrl.sortList('title')\"]")]
        protected IWebElement SortMaterialDropDownTitle
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "a[ng-click = \"actionCtrl.sortList('rank')\"]")]
        protected IWebElement SortMaterialDropDownOriginalSort
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[export-type = 'lists']")]
        protected IWebElement ExportButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Print this list']")]
        protected IWebElement PrintButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button.print-popup-button")]
        protected IWebElement PrintButtonPrintWindow
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Edit list metadata']")]
        protected IWebElement EditListMetadataButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Edit this list']")]
        protected IWebElement EditThisListButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Manage list user roles']")]
        protected IWebElement ManageListUserRolesButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Delete']")]
        protected IWebElement DeleteListButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Confirm delete']")]
        protected IWebElement ConfirmDeleteListButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Cancel delete']")]
        protected IWebElement CancelDeleteListButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[has-taxon-priv = \"['list-suppress']\"]")]
        protected IWebElement SuppressUnsuppressButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "h1[ng-if = 'tocModalCtrl.loaded']")]
        protected IWebElement TableofContentsPageHeader
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'tocModalCtrl.cancel()']")]
        protected IWebElement TableofContentsCloseButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'rDModal.cancel()']")]
        protected IWebElement DigitisationRequestCloseButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'rDModal.createDigitisation(rDModal.digitisationBind)']")]
        protected IWebElement DigitisationRequestSubmitButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'rDModal.existingDigitisation(rDModal.existingDcs)']")]
        protected IWebElement DigitisationRequestExistingSubmitButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[btn-text = 'Export item as citation']")]
        protected IWebElement ViewCitationExportButton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'citationModal.cancel()']")]
        protected IWebElement ViewCitationCloseButton
        {
            get; set;
        }
        [FindsBy(How = How.Id, Using = "list-course-identifier")]
        protected IWebElement UnitCourseIdentifierInput
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'nodeModal.finish(this)']")]
        protected IWebElement UnitFinishBtn
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "user-search")]
        protected IWebElement UserSearchInput
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click= 'lURM.finish()']")]
        protected IWebElement ManageRolesFinishButton
        {
            get; set;
        }
        [FindsBy(How = How.Id, Using = "citation-actions")]
        protected IWebElement CitationsFormatSection
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'rDModal.digitisationBind.course_identifier']")]
        protected IWebElement DigitisationRequestCourseCodeInput
        {
            get; set;
        }
        [FindsBy(How = How.Name, Using = "isbn")]
        protected IWebElement DigitisationRequestISBNInput
        {
            get; set;
        }
        [FindsBy(How = How.Name, Using = "issn")]
        protected IWebElement DigitisationRequestISSNInput
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'rDModal.digitisationBind.title']")]
        protected IWebElement DigitisationRequestTitleInput
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'rDModal.digitisationBind.extractTitle']")]
        protected IWebElement DigitisationRequestExtractTitleInput
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'rDModal.digitisationBind.articleTitle']")]
        protected IWebElement DigitisationRequestArticleTitleInput
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'rDModal.digitisationBind.pageRange']")]
        protected IWebElement DigitisationRequestPageRangeInput
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'rDModal.existingDcs.course_identifier']")]
        protected IWebElement DigitisationRequestExistingDCSCourseCodeInput
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[ng-model= 'rDModal.existingDcs.dcsRequestId']")]
        protected IWebElement DigitisationRequestDCSIDInput
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[value= 'new']")]
        protected IWebElement DigitisationRequestNewForm
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "input[value= 'old']")]
        protected IWebElement DigitisationRequestExistingForm
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click= 'dcsModal.cancel()']")]
        protected IWebElement DigitisationRequestSuccessOKbutton
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click= 'viewDigitisationCtrl.cancel()']")]
        protected IWebElement ViewDigitisationCloseButton
        {
            get; set;
        }

        By ClearFilter_locator = By.CssSelector("button[ng-click='dropdownCtrl.clear(filter)']");
        By MaterialTypeFilter_locator = By.CssSelector("input[ng-click='dropdownCtrl.changeSelected(filter, option)']");
        By TagsFilter_locator = By.CssSelector("input[ng-model='option.selected']");
        By TableofContents_locator = By.CssSelector("button[uib-tooltip='Table of contents']");
        By DigitisationRequest_locator = By.CssSelector("button[ng-click = 'requestCtrl.logRequestDcs();requestCtrl.assginValue()']");
        By ViewDigitisation_locator = By.CssSelector("button[ng-click = 'requestCtrl.logViewDcs();requestCtrl.getStatus(requestCtrl.ListService.data.id, requestCtrl.item.id, requestCtrl.secId)']");
        By CitationsPresent_locator = By.CssSelector("li[in-view='itemCtrl.setInView(itemCtrl.item.id, $inview)']");
        By RequestDigitisation_locator = By.CssSelector("button[uib-tooltip='Request Digitisation']");
        By FilterListBadge = By.CssSelector("span[ng-if = 'dropdownCtrl.totalSelected() > 0']");
        By FilterListNames_locator = By.CssSelector("li[ng-repeat = \"option in dropdownCtrl.getAll(filter) | orderBy: 'displayName'\"]");
        By Title_Materials_locator = By.CssSelector("span[ng-if = 'item.metadata.title']");
        By List_Nodes_locator = By.CssSelector("ol[ng-show = 'sectionCtrl.filteredMaterial.length > 0']");
        By UserSearchResult_locator = By.CssSelector("button[ng-click= 'lURM.addUser(result)']");
        By UsersListManageUsers_locator = By.CssSelector("li[ng-repeat= 'user in lURM.users']");
        By UsersAssociatedManageUsers_locator = By.CssSelector("li[ng-repeat= 'user in lURM.users']");
        By UsersAssociatedNonPermRoles_locator = By.CssSelector("label[uib-btn-radio= 'nonPermissive.id']");
        By UsersAssociatedPermRoles_locator = By.CssSelector("label[uib-btn-radio= 'permissive.id']");
        By UsersNamesManageUsers_locator = By.ClassName("col-md-7");
        By UsersNamesSearchResult_locator = By.ClassName("col-md-10");
        By ViewCitations_locator = By.CssSelector("button[ng-click= 'citationCtrl.openModal()']");
        By DigitisationRequestAlert_locator = By.ClassName("alert-danger");
        By DCSSuccess_locator = By.CssSelector("div[ng-if= 'dcsModal.isSuccess']");
        By DCSFail_locator = By.CssSelector("div[ng-if= '!dcsModal.isSuccess']");


        string statusreturntext;
        string currentURL;
        string numberofselections;
        string filtertagnameselected;
        string filtermaterialnameselected;
        string RequestMessage;
        string DCSid;
        int citationscount = 0;
        IList<IWebElement> CitationsList;
        IWebElement MaterialTitle;

        public bool ViewList()
        {
            try
            {
                currentURL = Driver.Instance.Url;
                
                Pages.PearlEditBuffer.AddDocumenttoSection("Harry Potter", "WorldCat");
                Pages.PearlEditBuffer.PublishingList();
                Pages.PearlEditBuffer.TagSelectedItems();
                
                //Filter List - select tags
                filterlistselecttags();

                //Filter List (Clear Tags)
                filterlistcleartags();

                //Filter List - select Material Type
                filterlistselectmaterialtype();

                //Filter List (Clear Material Types)
                filterlistclearmaterialtype();
                
                //Filter List - Select Tags and Material Types
                filterlistselectall();

                //Filter List (Clear ALL)
                filterlistclearall();
                
                //Sort Material(Title)
                sortmaterialtitle();

                //Sort Material(Original Sorting)
                sortmaterialoriginal();
                
                //Export
                exportmaterial();

                //Print the List
                //PrintList(); //commenting for now as the print dialogue is not behaving as expected
                
                //Edit List Metadata
                EditListMetadata();
                
                //Suppress 
                SuppressUnsuppress();

                //Unsuppress
                SuppressUnsuppress();
                
                //Manage List User Roles
                manageuserlistroles("Auto","OWNER","AUTHOR");
                
                //View Citation
                viewcitations(0);

                viewcitations(3);
                
                //View Citation - Export
                ViewCitationExport(0);
                
                //Table of Contents
                TableofContents();

                //View Digitisation
                viewdigitisation();

                //Request Digitisation - New
                RequestDigitisationNew();

                //Request Digitisation - Existing
                RequestDigitisationExisting();
                                
                //View Digitisation
                viewdigitisation();

                //Delete
                DeleteList();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in PearlViewList.cs: " + e.Message);
                return false;
            }
        }
        public void filterlistselecttags()
        {
            Klick.On(FilterListDropDown);
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> FilterTagNames = Driver.Instance.FindElements(FilterListNames_locator);
            filtertagnameselected = FilterTagNames[0].Text;
            Console.WriteLine("filtertagnameselected = " + filtertagnameselected);
            IList<IWebElement> Filter_Tags = Driver.Instance.FindElements(TagsFilter_locator);
            Klick.On(Filter_Tags[0]);

            //Check if the selection is good
            if(Driver.Instance.FindElements(FilterListBadge).Count == 1)
            {
                numberofselections = Driver.Instance.FindElement(FilterListBadge).Text;
                Console.WriteLine("numberofselections = " + numberofselections);
                if(numberofselections == "1")
                {
                    Klick.On(FilterListDropDown);
                    Thread.Sleep(KortextGlobals.s);
                    CitationsList = Driver.Instance.FindElements(CitationsPresent_locator);
                    citationscount = CitationsList.Count;
                    Thread.Sleep(KortextGlobals.s);
                    if (citationscount == 3)
                    {
                        Console.WriteLine("Filter List with Tags Successful");
                    }
                    else
                    {
                        Console.WriteLine("Unexpected Count of Materials present for the Filter List with Tags." + citationscount);
                    }
                }
                else
                {
                    Console.WriteLine("Improper selection of Filter List with Tags." + citationscount);
                }
            }
        }
        public void filterlistcleartags()
        {
            Klick.On(FilterListDropDown);
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> FilterListClearButton = Driver.Instance.FindElements(ClearFilter_locator);
            Console.WriteLine("FilterListClearButton count = " + FilterListClearButton.Count);
            if (FilterListClearButton.Count > 0)
            {
                Klick.On(FilterListClearButton[0]);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(FilterListDropDown);
                Thread.Sleep(KortextGlobals.s);
                CitationsList = Driver.Instance.FindElements(CitationsPresent_locator);
                citationscount = CitationsList.Count;
                Thread.Sleep(KortextGlobals.s);
                if (citationscount.ToString() == Pages.PearlEditBuffer.DisplayTotalNumberofItems())
                {
                    Console.WriteLine("Clearing Filter List with Tags Successful");
                }
                else
                {
                    Console.WriteLine("Clear Tags:List Items number present and Count of Citations do not match." + citationscount.ToString() + " & " + Pages.PearlEditBuffer.DisplayTotalNumberofItems());
                }
            }
            else
            {
                Klick.On(FilterListDropDown);
                Thread.Sleep(KortextGlobals.s);
                FilterListClearButton = Driver.Instance.FindElements(ClearFilter_locator);
                Klick.On(FilterListClearButton[0]);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(FilterListDropDown);
            }
            if (Driver.Instance.FindElements(FilterListBadge).Count == 0)
            {
                Console.WriteLine("Clearing Filter List with Tags Completed");
            }
            else
            {
                Console.WriteLine("Clearing Filter List with Tags not as expected");
            }
        }
        public void filterlistselectmaterialtype()
        {
            Klick.On(FilterListDropDown);
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> FilterMaterialTypeNames = Driver.Instance.FindElements(FilterListNames_locator);
            int Total_Names = FilterMaterialTypeNames.Count;
            filtermaterialnameselected = FilterMaterialTypeNames[Total_Names-1].Text;
            Console.WriteLine("filtermaterialnameselected = " + filtermaterialnameselected);
            IList<IWebElement> Filter_MaterialTypes = Driver.Instance.FindElements(MaterialTypeFilter_locator);
            Klick.On(Filter_MaterialTypes[Filter_MaterialTypes.Count - 1]);

            //Check if the selection is good
            if (Driver.Instance.FindElements(FilterListBadge).Count == 1)
            {
                numberofselections = Driver.Instance.FindElement(FilterListBadge).Text;
                Console.WriteLine("numberofselections = " + numberofselections);
                if (numberofselections == "1")
                {
                    Klick.On(FilterListDropDown);
                    Thread.Sleep(KortextGlobals.ll);
                    CitationsList = Driver.Instance.FindElements(CitationsPresent_locator);
                    citationscount = CitationsList.Count;
                    if (citationscount == 8)
                    {
                        Console.WriteLine("Filter List with Material Types Successful");
                    }
                    else
                    {
                        Console.WriteLine("Unexpected Count of Materials present for the Filter List with Material Types." + citationscount);
                    }
                }
                else
                {
                    Console.WriteLine("Improper selection of Filter List with Material Types." + numberofselections);
                }
            }
        }
        public void filterlistclearmaterialtype()
        {
            Klick.On(FilterListDropDown);
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> FilterListClearButton = Driver.Instance.FindElements(ClearFilter_locator);
            Console.WriteLine("FilterListClearButton count = " + FilterListClearButton.Count);
            if (FilterListClearButton.Count > 0)
            {
                Klick.On(FilterListClearButton[1]);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(FilterListDropDown);
                Thread.Sleep(KortextGlobals.s);
                CitationsList = Driver.Instance.FindElements(CitationsPresent_locator);
                citationscount = CitationsList.Count;
                Thread.Sleep(KortextGlobals.s);
                if (citationscount.ToString() == Pages.PearlEditBuffer.DisplayTotalNumberofItems())
                {
                    Console.WriteLine("Clearing Filter List with Material Types Successful");
                }
                else
                {
                    Console.WriteLine("Clear Material Types:List Items number present and Count of Citations do not match." + citationscount.ToString() + " & " + Pages.PearlEditBuffer.DisplayTotalNumberofItems());
                }
            }
            else
            {
                Klick.On(FilterListDropDown);
                Thread.Sleep(KortextGlobals.s);
                FilterListClearButton = Driver.Instance.FindElements(ClearFilter_locator);
                Klick.On(FilterListClearButton[1]);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(FilterListDropDown);
            }
            if (Driver.Instance.FindElements(FilterListBadge).Count == 0)
            {
                Console.WriteLine("Clearing Filter List with Material Types Completed");
            }
            else
            {
                Console.WriteLine("Clearing Filter List with Material Types not as expected");
            }
        }
        public void filterlistselectall()
        {
            Klick.On(FilterListDropDown);
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> FilterNames = Driver.Instance.FindElements(FilterListNames_locator);
            filtertagnameselected = FilterNames[0].Text;
            Console.WriteLine("filtertagnameselected = " + filtertagnameselected);
            IList<IWebElement> Filter_Tags = Driver.Instance.FindElements(TagsFilter_locator);
            Klick.On(Filter_Tags[0]);

            filtermaterialnameselected = FilterNames[FilterNames.Count - 1].Text;
            Console.WriteLine("filtermaterialnameselected = " + filtermaterialnameselected);
            IList<IWebElement> Filter_MaterialTypes = Driver.Instance.FindElements(MaterialTypeFilter_locator);
            Console.WriteLine("Filter_MaterialTypes count = " + Filter_MaterialTypes.Count);
            Klick.On(Filter_MaterialTypes[Filter_MaterialTypes.Count - 1]);

            //Check if the selection is good
            if (Driver.Instance.FindElements(FilterListBadge).Count == 1)
            {
                numberofselections = Driver.Instance.FindElement(FilterListBadge).Text;
                Console.WriteLine("numberofselections = " + numberofselections);
                if (numberofselections == "2")
                {
                    Klick.On(FilterListDropDown);
                    Thread.Sleep(KortextGlobals.ll);
                    CitationsList = Driver.Instance.FindElements(CitationsPresent_locator);
                    citationscount = CitationsList.Count;
                    if (citationscount == 0)
                    {
                        Console.WriteLine("Filter List All Successful");
                    }
                    else
                    {
                        Console.WriteLine("Unexpected Count of Materials present for the Filter List All." + citationscount);
                    }
                }
                else
                {
                    Console.WriteLine("Improper selection of Filter List All." + numberofselections);
                }
            }
        }
        public void filterlistclearall()
        {
            Klick.On(FilterListDropDown);
            Thread.Sleep(KortextGlobals.s);
            if (FilterListDropDownClearALLFilter.Displayed == true)
            {
                Console.WriteLine("FilterList already open.");
                Klick.On(FilterListDropDownClearALLFilter);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(FilterListDropDown);
                Thread.Sleep(KortextGlobals.s);
                CitationsList = Driver.Instance.FindElements(CitationsPresent_locator);
                citationscount = CitationsList.Count;
                Thread.Sleep(KortextGlobals.s);
                if (citationscount.ToString() == Pages.PearlEditBuffer.DisplayTotalNumberofItems())
                {
                    Console.WriteLine("Clearing Filter List All Successful");
                }
                else
                {
                    Console.WriteLine("Clear All:List Items number present and Count of Citations do not match." + citationscount.ToString() + " & " + Pages.PearlEditBuffer.DisplayTotalNumberofItems());
                }
            }
            else
            {
                Klick.On(FilterListDropDown);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(FilterListDropDownClearALLFilter);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(FilterListDropDown);
            }
            if (Driver.Instance.FindElements(FilterListBadge).Count == 0)
            {
                Console.WriteLine("Clearing Filter List All Completed");
            }
            else
            {
                Console.WriteLine("Clearing Filter List All not as expected");
            }
        }
        public void sortmaterialtitle()
        {
            Klick.On(SortMaterialDropDown);
            Thread.Sleep(KortextGlobals.l);
            Klick.On(SortMaterialDropDownTitle);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(SortMaterialDropDown);
            Thread.Sleep(KortextGlobals.s);
            verifysortedmaterialtitles();
            Console.WriteLine("Sorting with Title Completed");
        }
        public void verifysortedmaterialtitles()
        {
            List<String> displayNames = new List<string>();
            List<String> displayNamesSorted;
            int p = 0;
            int q = 0;
            int r = 0;

            IList<IWebElement> CountOfListNodes = Driver.Instance.FindElements(List_Nodes_locator);
            Console.WriteLine("CountOfListNodes = " + CountOfListNodes.Count);

            int[] ListCountOfListNodes = new int[CountOfListNodes.Count];

            //foreach (IWebElement CountOfListNode in CountOfListNodes)
            for (p = 0; p < CountOfListNodes.Count; p++)
            {
                IList<IWebElement> TitleMaterials = new List<IWebElement>(CountOfListNodes[p].FindElements(CitationsPresent_locator));
                Console.WriteLine("Total Materials in ListNode = " + TitleMaterials.Count);
                ListCountOfListNodes[p] = TitleMaterials.Count;
            }

            List<NgWebElement> ListNodes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("item in sectionCtrl.filteredMaterial = (sectionCtrl.section.materials | itemFilter) track by item.id")));
            Console.WriteLine("Total ListNodes = " + ListNodes.Count);

            foreach (IWebElement ListNode in ListNodes)
            {
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", ListNode);
                Thread.Sleep(KortextGlobals.s);
                Driver.HighlightElement(ListNode);

                if (r < ListCountOfListNodes[q])
                {
                    MaterialTitle = ListNode.FindElement(Title_Materials_locator);
                    //Console.WriteLine("r = " + r);
                    //Console.WriteLine("ListCountOfListNodes[q] " + ListCountOfListNodes[q]);
                    Console.WriteLine("MaterialTitle.Text = " + MaterialTitle.Text);
                    displayNames.Add(MaterialTitle.Text);
                    r++;
                }
                else
                {
                    // make a copy of the displayNames array
                    displayNamesSorted = new List<string>(displayNames);
                    displayNamesSorted.Sort();
                    Console.WriteLine(displayNames.SequenceEqual(displayNamesSorted));
                    Assert.IsTrue(displayNames.SequenceEqual(displayNamesSorted));
                    q++;
                    r = 0;

                    displayNames = new List<string>();
                    MaterialTitle = ListNode.FindElement(Title_Materials_locator);
                    //Console.WriteLine("r = " + r);
                    //Console.WriteLine("ListCountOfListNodes[q] " + ListCountOfListNodes[q]);
                    Console.WriteLine("MaterialTitle.Text = " + MaterialTitle.Text);
                    displayNames.Add(MaterialTitle.Text);
                    r++;
                }
            }
        }
        public void sortmaterialoriginal()
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<String> displayNamesnatural = new List<string>();
            List<String> displayNamesoriginal = new List<string>();

            List<NgWebElement> ListNodesNatural = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("item in sectionCtrl.filteredMaterial = (sectionCtrl.section.materials | itemFilter) track by item.id")));
            Console.WriteLine("Total ListNodes = " + ListNodesNatural.Count);

            foreach (IWebElement ListNodeNatural in ListNodesNatural)
            {
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", ListNodeNatural);
                Thread.Sleep(KortextGlobals.s);
                Driver.HighlightElement(ListNodeNatural);

                MaterialTitle = ListNodeNatural.FindElement(Title_Materials_locator);
                Console.WriteLine("MaterialTitle.Text = " + MaterialTitle.Text);
                displayNamesnatural.Add(MaterialTitle.Text);
            }

            Klick.On(SortMaterialDropDown);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(SortMaterialDropDownOriginalSort);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(SortMaterialDropDown);
            Thread.Sleep(KortextGlobals.s);

            List<NgWebElement> ListNodesOriginal = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("item in sectionCtrl.filteredMaterial = (sectionCtrl.section.materials | itemFilter) track by item.id")));
            Console.WriteLine("Total ListNodes = " + ListNodesOriginal.Count);

            foreach (IWebElement ListNodeOriginal in ListNodesOriginal)
            {
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", ListNodeOriginal);
                Thread.Sleep(KortextGlobals.s);
                Driver.HighlightElement(ListNodeOriginal);

                MaterialTitle = ListNodeOriginal.FindElement(Title_Materials_locator);
                Console.WriteLine("MaterialTitle.Text = " + MaterialTitle.Text);
                displayNamesoriginal.Add(MaterialTitle.Text);
            }

            Console.WriteLine(displayNamesnatural.SequenceEqual(displayNamesoriginal));
            Assert.IsTrue(displayNamesnatural.SequenceEqual(displayNamesoriginal));
            Console.WriteLine("Sorting with Original Sort Completed");
        }
        public void exportmaterial()
        {
            Klick.On(ExportButton);
            Thread.Sleep(KortextGlobals.ll);
            AutoItX3 x = new AutoItX3();
            x.WinActivate("Save As");
            if (x.WinExists("Save As") == 1)
            {
                x.WinWaitActive("Save As");
                x.ControlFocus("Save As", "", "1148");
                //x.ControlSetText("Print", "", "1153", "50");
                Thread.Sleep(KortextGlobals.s);
                x.ControlClick("Save As", "", "1");
                Thread.Sleep(KortextGlobals.s);
            }
        }
        public void PrintList()
        {
            //Klick.On(PrintButton);
            Driver.HighlightElement(PrintButton);
            PrintButton.SendKeys(Keys.Enter);
            //Driver.Instance.SwitchTo().Window(Driver.Instance.WindowHandles.Last());
            //Driver.Instance.SwitchTo().ParentFrame();
            Thread.Sleep(KortextGlobals.l);
            AutoItX3 x = new AutoItX3();
            //Driver.HighlightElement(PrintButton);
            //PrintButton.Click();
            x.WinActivate("Print");
            if(x.WinExists("Print") == 1)
            {
                x.WinWaitActive("Print");
                x.ControlFocus("Print", "", "1148");
                //x.ControlSetText("Print", "", "1153", "50");
                Thread.Sleep(KortextGlobals.s);
                x.ControlClick("Print", "", "2");
                Thread.Sleep(KortextGlobals.s);
            }
        }
        public void EditListMetadata()
        {
            Klick.On(EditListMetadataButton);
            Thread.Sleep(KortextGlobals.xl);
            WaitFind.FindElem(UnitCourseIdentifierInput, 10).Clear();
            UnitCourseIdentifierInput.SendKeys("12345");
            Thread.Sleep(KortextGlobals.s);
            Klick.On(UnitFinishBtn);
            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "List updated")
            {
                Console.WriteLine("Error while Editing List Metadata." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Edit List Metadata Successful");
            }
        }
        public void SuppressUnsuppress()
        {
            int suppress_flag = 0;
            int unsuppress_flag = 0;
            if(Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip = 'Unsuppress [Hidden, branch suppressed]']")).Count > 0)
            {
                unsuppress_flag = 1;
            }
            else if(Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip = 'Suppress [Currently not visible due to branch suppression]']")).Count > 0)
            {
                suppress_flag = 1;
            }
            else
            {
                Console.WriteLine("Suppress/Unsuppress Button is showing exception status");
            }
            Klick.On(SuppressUnsuppressButton);
            Thread.Sleep(KortextGlobals.l);
            if(unsuppress_flag == 1 && (Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip = 'Suppress [Currently not visible due to branch suppression]']")).Count > 0))
            {
                Console.WriteLine("List Suppress Successful");
            }
            else if(suppress_flag == 1 && (Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip = 'Unsuppress [Hidden, branch suppressed]']")).Count > 0))
            {
                Console.WriteLine("List Unsuppress Successful");
            }
            else
            {
                Console.WriteLine("Error while performing Suppress/Unsuppress action");
            }
        }
        public void manageuserlistroles(string userforrole, string nonpermrole, string permrole)
        {
            Klick.On(ManageListUserRolesButton);
            Thread.Sleep(KortextGlobals.l);
            WaitFind.FindElem(UserSearchInput, 10).Clear();
            Thread.Sleep(KortextGlobals.l);
            Klick.On(UserSearchInput);
            Thread.Sleep(KortextGlobals.ll);
            UserSearchInput.SendKeys(userforrole);
            IList<IWebElement> UserSearchResult = Driver.Instance.FindElements(UserSearchResult_locator);
            Klick.On(UserSearchResult[0]);
            IList<IWebElement> UserNameSearchResult = Driver.Instance.FindElements(UsersNamesSearchResult_locator);
            string UserNameSelected = UserNameSearchResult[0].Text;
            Console.WriteLine("UserNameSelected = " + UserNameSelected);

            IList<IWebElement> AddedUsers = Driver.Instance.FindElements(UsersListManageUsers_locator);
            foreach (IWebElement AddedUser in AddedUsers)
            {
                IList<IWebElement> UserNames = new List<IWebElement>(AddedUser.FindElements(UsersNamesManageUsers_locator));
                foreach(IWebElement UserName in UserNames)
                {
                    string UsernameText = UserName.Text;
                    Console.WriteLine("UsernameText = " + UsernameText);

                    if( UsernameText == ("person " + UserNameSelected))
                    {
                        if(nonpermrole == "LEADER")
                        {
                            AddedUser.FindElements(UsersAssociatedNonPermRoles_locator)[0].Click();
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext != "Role updated")
                            {
                                Console.WriteLine("Error while Updating NonPerm Role." + statusreturntext);
                            }
                            else
                            {
                                Console.WriteLine("Clicked Leader for " + UsernameText);
                            }
                        }
                        else if(nonpermrole == "OWNER")
                        {
                            AddedUser.FindElements(UsersAssociatedNonPermRoles_locator)[1].Click();
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext != "Role updated")
                            {
                                Console.WriteLine("Error while Updating NonPerm Role." + statusreturntext);
                            }
                            else
                            {
                                Console.WriteLine("Clicked Owner for " + UsernameText);
                            }                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid NonPerm Role provided");
                        }

                        if (permrole == "AUTHOR")
                        {
                            Thread.Sleep(KortextGlobals.l);
                            AddedUser.FindElements(UsersAssociatedPermRoles_locator)[0].Click();
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext != "Role updated")
                            {
                                Console.WriteLine("Error while Updating Perm Role." + statusreturntext);
                            }
                            else
                            {
                                Console.WriteLine("Clicked Author for " + UsernameText);
                            }
                        }
                        else if(permrole == "MODERATOR")
                        {
                            AddedUser.FindElements(UsersAssociatedPermRoles_locator)[1].Click();
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext != "Role updated")
                            {
                                Console.WriteLine("Error while Updating Perm Role." + statusreturntext);
                            }
                            else
                            {
                                Console.WriteLine("Clicked Moderator for " + UsernameText);
                            }
                        }
                        else if(permrole == "EDITOR")
                        {
                            AddedUser.FindElements(UsersAssociatedPermRoles_locator)[2].Click();
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext != "Role updated")
                            {
                                Console.WriteLine("Error while Updating Perm Role." + statusreturntext);
                            }
                            else
                            {
                                Console.WriteLine("Clicked Editor for " + UsernameText);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Perm Role provided");
                        }
                    }
                }
            }
            Klick.On(ManageRolesFinishButton);
            Thread.Sleep(KortextGlobals.l);
        }
        public void viewcitations(int position)
        {
            IList<IWebElement> ViewCitations = Driver.Instance.FindElements(ViewCitations_locator);
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", ViewCitations[position]);
            Klick.On(ViewCitations[position]);
            Thread.Sleep(KortextGlobals.l);
            if(CitationsFormatSection.Displayed)
            {
                Klick.On(ViewCitationCloseButton);
                Console.WriteLine("View Citations Successful for Citation Position " + position);
            }
            else
            {
                Console.WriteLine("Error opening View Citations for Position " + position);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);
            }
        }
        public void ViewCitationExport(int position)
        {
            IList<IWebElement> ViewCitations = Driver.Instance.FindElements(ViewCitations_locator);
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", ViewCitations[position]);
            Klick.On(ViewCitations[position]);
            Thread.Sleep(KortextGlobals.l);
            if (CitationsFormatSection.Displayed)
            {
                Klick.On(ViewCitationExportButton);
                Thread.Sleep(KortextGlobals.ll);
                AutoItX3 x = new AutoItX3();
                x.WinActivate("Save As");
                if (x.WinExists("Save As") == 1)
                {
                    x.WinWaitActive("Save As");
                    x.ControlFocus("Save As", "", "1148");
                    //x.ControlSetText("Print", "", "1153", "50");
                    Thread.Sleep(KortextGlobals.s);
                    x.ControlClick("Save As", "", "1");
                    Thread.Sleep(KortextGlobals.s);
                }
                Console.WriteLine("Export Material Successful for Citation Position " + position);
            }
            else
            {
                Console.WriteLine("Error opening Export Material for Position " + position);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);
            }
        }
        public void DeleteList()
        {
            Klick.On(DeleteListButton);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(ConfirmDeleteListButton);
            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "List deleted")
            {
                Console.WriteLine("Error while Deleting the List." + statusreturntext);
            }
            else
            {
                Console.WriteLine("List Deleted Successfully");
            }
        }
        public void TableofContents()
        {
            List<NgWebElement> CitationsforTableOfContents = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("item in sectionCtrl.filteredMaterial = (sectionCtrl.section.materials | itemFilter) track by item.id")));
            Console.WriteLine("Number of citations found" + CitationsforTableOfContents.Count);
            foreach (IWebElement CitationforTableOfContents in CitationsforTableOfContents)
            {
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", CitationforTableOfContents);
                Thread.Sleep(KortextGlobals.s);
                Driver.HighlightElement(CitationforTableOfContents);
                IList<IWebElement> TableofContent = new List<IWebElement>(CitationforTableOfContents.FindElements(TableofContents_locator));

                if (TableofContent.Count > 0)
                {
                    Console.WriteLine("TableofContent = " + TableofContent[0].GetAttribute("class"));
                    if(TableofContent[0].GetAttribute("class") != "outlink-toc btn btn-default btn-xs ng-isolate-scope ng-hide")
                    {
                        Klick.On(TableofContent[0]);
                        if (TableofContentsPageHeader.Displayed)
                        {
                            Klick.On(TableofContentsCloseButton);
                            Console.WriteLine("Table of Contents verification Successful");
                        }
                        else
                        {
                            Console.WriteLine("Error while opening Table of Contents");
                        }
                        break;
                    }
                }
            }
        }
        public void RequestDigitisationNew()
        {
            List<NgWebElement> CitationsforDigitisationRequest = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("item in sectionCtrl.filteredMaterial = (sectionCtrl.section.materials | itemFilter) track by item.id")));
            Console.WriteLine("Number of citations found" + CitationsforDigitisationRequest.Count);
            foreach (IWebElement CitationforDigitisationRequest in CitationsforDigitisationRequest)
            {
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", CitationforDigitisationRequest);
                Thread.Sleep(KortextGlobals.s);
                Driver.HighlightElement(CitationforDigitisationRequest);
                IList<IWebElement> DigitisationRequest = new List<IWebElement>(CitationforDigitisationRequest.FindElements(DigitisationRequest_locator));

                if (DigitisationRequest.Count > 0)
                {
                    Klick.On(DigitisationRequest[0]);
                    Thread.Sleep(KortextGlobals.l);
                    WaitFind.FindElem(DigitisationRequestCourseCodeInput, 10).Clear();
                    DigitisationRequestCourseCodeInput.SendKeys("TEST01");

                    IList<IWebElement> DigitisationRequestAlertFields = Driver.Instance.FindElements(By.ClassName("form-group"));

                    if (DigitisationRequestAlertFields.Count > 0)
                    {
                        foreach (IWebElement DigitisationRequestAlertField in DigitisationRequestAlertFields)
                        {
                            if (DigitisationRequestAlertField.Displayed)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", DigitisationRequestAlertField);
                                Driver.HighlightElement(DigitisationRequestAlertField);
                                IList<IWebElement> ErrorAlerts = DigitisationRequestAlertField.FindElements(DigitisationRequestAlert_locator);
                                if (ErrorAlerts.Count > 0)
                                {
                                    foreach (IWebElement ErrorAlert in ErrorAlerts)
                                    {
                                        if (ErrorAlert.Displayed)
                                        {
                                            DigitisationRequestAlertCorrecting(ErrorAlert.Text);
                                            Thread.Sleep(KortextGlobals.s);
                                            break;
                                        }
                                    }
                                }

                            }
                        }
                        Klick.On(DigitisationRequestSubmitButton);
                        Thread.Sleep(KortextGlobals.l);
                        IList<IWebElement> RequestSuccessCheck = Driver.Instance.FindElements(DCSSuccess_locator);
                        if (RequestSuccessCheck.Count > 0)
                        {
                            RequestMessage = RequestSuccessCheck[0].Text;
                            if (RequestMessage == "Your request is now being processed by the Digital Content Store.")
                            {
                                DCSid = RequestSuccessCheck[1].Text;
                                Klick.On(DigitisationRequestSuccessOKbutton);
                                Console.WriteLine(DCSid);
                                Console.WriteLine("Digitisation Request Submission for New DCS ID Successful");
                            }
                            else
                            {
                                Console.WriteLine("Digitisation Request Submission for New DCS not Successful: " + RequestMessage);
                                Klick.On(DigitisationRequestSuccessOKbutton);
                                Thread.Sleep(KortextGlobals.s);
                                Klick.On(DigitisationRequestCloseButton);
                            }
                        }
                        else
                        {
                            IList<IWebElement> RequestFailCheck = Driver.Instance.FindElements(DCSFail_locator);
                            Console.WriteLine("Digitisation Request Submission for New DCS not Successful: " + RequestFailCheck[0].Text);
                            Klick.On(DigitisationRequestSuccessOKbutton);
                            Thread.Sleep(KortextGlobals.s);
                            Klick.On(DigitisationRequestCloseButton);
                        }
                    }
                    else
                    {
                        Klick.On(DigitisationRequestSubmitButton);
                        Console.WriteLine("Error while opening Digitisation Request Form for New DCS ID");
                    }
                    break;
                }
            }
        }
        public void RequestDigitisationExisting()
        {
            List<NgWebElement> CitationsforDigitisationRequest = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("item in sectionCtrl.filteredMaterial = (sectionCtrl.section.materials | itemFilter) track by item.id")));
            Console.WriteLine("Number of citations found" + CitationsforDigitisationRequest.Count);
            foreach (IWebElement CitationforDigitisationRequest in CitationsforDigitisationRequest)
            {
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", CitationforDigitisationRequest);
                Thread.Sleep(KortextGlobals.s);
                Driver.HighlightElement(CitationforDigitisationRequest);
                IList<IWebElement> DigitisationRequest = new List<IWebElement>(CitationforDigitisationRequest.FindElements(DigitisationRequest_locator));

                if (DigitisationRequest.Count > 0)
                {
                    Klick.On(DigitisationRequest[0]);
                    Thread.Sleep(KortextGlobals.l);
                    Klick.On(DigitisationRequestExistingForm);
                    WaitFind.FindElem(DigitisationRequestExistingDCSCourseCodeInput, 10).Clear();
                    DigitisationRequestExistingDCSCourseCodeInput.SendKeys("TEST01");

                    IList<IWebElement> DigitisationRequestAlertFields = Driver.Instance.FindElements(DigitisationRequestAlert_locator);
                    if (DigitisationRequestAlertFields.Count > 0)
                    {
                        foreach (IWebElement DigitisationRequestAlertField in DigitisationRequestAlertFields)
                        {
                            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", DigitisationRequestAlertField);
                            Driver.HighlightElement(DigitisationRequestAlertField);
                            if (DigitisationRequestAlertField.GetAttribute("class") != "alert alert-danger ng-hide")
                            {
                                if ((DigitisationRequestAlertField.Text == "DCS Request id must be completed") || (DigitisationRequestAlertField.Text == "Course code must be completed"))
                                {
                                    DigitisationRequestAlertCorrecting(DigitisationRequestAlertField.Text);
                                }
                            }
                        }
                        Klick.On(DigitisationRequestExistingSubmitButton);
                        Thread.Sleep(KortextGlobals.l);
                        IList<IWebElement> RequestSuccessCheck = Driver.Instance.FindElements(DCSSuccess_locator);
                        if (RequestSuccessCheck.Count > 0)
                        {
                            RequestMessage = RequestSuccessCheck[0].Text;
                            if (RequestMessage == "Your request is now being processed by the Digital Content Store.")
                            {
                                DCSid = RequestSuccessCheck[1].Text;
                                Klick.On(DigitisationRequestSuccessOKbutton);
                                Console.WriteLine(DCSid);
                                Console.WriteLine("Digitisation Request Submission for Existing DCS ID Successful");
                            }
                            else
                            {
                                Console.WriteLine("Digitisation Request Submission for Existing DCS not Successful: " + RequestMessage);
                                Klick.On(DigitisationRequestSuccessOKbutton);
                                Thread.Sleep(KortextGlobals.s);
                                Klick.On(DigitisationRequestCloseButton);
                            }
                        }
                        else
                        {
                            IList<IWebElement> RequestFailCheck = Driver.Instance.FindElements(DCSFail_locator);
                            Console.WriteLine("Digitisation Request Submission for Existing DCS not Successful: " + RequestFailCheck[0].Text);
                            Klick.On(DigitisationRequestSuccessOKbutton);
                            Thread.Sleep(KortextGlobals.s);
                            Klick.On(DigitisationRequestCloseButton);
                        }
                    }
                    else
                    {
                        Klick.On(DigitisationRequestSuccessOKbutton);
                        Console.WriteLine("Error while opening Digitisation Request Form for Existing DCS ID");
                    }
                    break;
                }
            }
        }
        public void DigitisationRequestAlertCorrecting(string AlertMessage)
        {
            switch (AlertMessage)
            {
                case "Course code must be completed":
                    Console.WriteLine("Course Code incomplete");
                    if(DigitisationRequestDCSIDInput.Displayed)
                    {
                        WaitFind.FindElem(DigitisationRequestExistingDCSCourseCodeInput, 10).Clear();
                        DigitisationRequestExistingDCSCourseCodeInput.SendKeys("TEST01");
                        break;
                    }
                    else
                    {
                        WaitFind.FindElem(DigitisationRequestCourseCodeInput, 10).Clear();
                        DigitisationRequestCourseCodeInput.SendKeys("TEST01");
                        break;
                    }
                    
                case "The ISBN must be completed":
                    Console.WriteLine("The ISBN must be completed");
                    WaitFind.FindElem(DigitisationRequestISBNInput, 10).Clear();
                    DigitisationRequestISBNInput.SendKeys("978-92-64-00914-1");
                    break;

                case "Enter a valid ISBN":
                    Console.WriteLine("Enter a valid ISBN");
                    WaitFind.FindElem(DigitisationRequestISBNInput, 10).Clear();
                    DigitisationRequestISBNInput.SendKeys("978-92-64-00914-1");
                    break;

                case "The ISSN must be completed":
                    Console.WriteLine("The ISSN must be completed");
                    WaitFind.FindElem(DigitisationRequestISSNInput, 10).Clear();
                    DigitisationRequestISSNInput.SendKeys("0001-9720");
                    break;

                case "Enter a valid ISSN":
                    Console.WriteLine("Enter a valid ISSN");
                    WaitFind.FindElem(DigitisationRequestISSNInput, 10).Clear();
                    DigitisationRequestISSNInput.SendKeys("0001-9720");
                    break;

                case "Title must be completed":
                    Console.WriteLine("Title must be completed");
                    WaitFind.FindElem(DigitisationRequestTitleInput, 10).Clear();
                    DigitisationRequestTitleInput.SendKeys("Sample Title for Digitisation Request");
                    break;

                case "Extract title must be completed":
                    Console.WriteLine("Extract title must be completed");
                    WaitFind.FindElem(DigitisationRequestExtractTitleInput, 10).Clear();
                    DigitisationRequestExtractTitleInput.SendKeys("Sample Extract Title for Digitisation Request");
                    break;

                case "The Article title must be completed":
                    Console.WriteLine("The Article title must be completed");
                    WaitFind.FindElem(DigitisationRequestArticleTitleInput, 10).Clear();
                    DigitisationRequestArticleTitleInput.SendKeys("Sample Article Title for Digitisation Request");
                    break;

                case "Page range must be completed":
                    Console.WriteLine("Page range must be completed");
                    WaitFind.FindElem(DigitisationRequestPageRangeInput, 10).Clear();
                    DigitisationRequestPageRangeInput.SendKeys("10-30");
                    break;

                case "Please enter a valid number":
                    Console.WriteLine("Please enter a valid number");
                    WaitFind.FindElem(DigitisationRequestPageRangeInput, 10).Clear();
                    DigitisationRequestPageRangeInput.SendKeys("10-30");
                    break;

                case "Please enter a page range in the format 7 or 7-9":
                    Console.WriteLine("Please enter a page range in the format 7 or 7-9");
                    WaitFind.FindElem(DigitisationRequestPageRangeInput, 10).Clear();
                    DigitisationRequestPageRangeInput.SendKeys("10-30");
                    break;

                case "Page from must be less than Page to":
                    Console.WriteLine("Page from must be less than Page to");
                    WaitFind.FindElem(DigitisationRequestPageRangeInput, 10).Clear();
                    DigitisationRequestPageRangeInput.SendKeys("10-30");
                    break;

                case "DCS Request id must be completed":
                    Console.WriteLine("DCS Request id must be completed");
                    WaitFind.FindElem(DigitisationRequestDCSIDInput, 10).Clear();
                    DigitisationRequestDCSIDInput.SendKeys("643951");
                    break;

                default:
                    Console.WriteLine("Alert Message not handled:" + AlertMessage);
                    break;
            }
        }
        public void viewdigitisation()
        {
            bool viewdigitisationpresent = false;
            List<NgWebElement> CitationsforViewDigitisation = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("item in sectionCtrl.filteredMaterial = (sectionCtrl.section.materials | itemFilter) track by item.id")));
            Console.WriteLine("Number of citations found" + CitationsforViewDigitisation.Count);
            foreach (IWebElement CitationforViewDigitisation in CitationsforViewDigitisation)
            {
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", CitationforViewDigitisation);
                Thread.Sleep(KortextGlobals.s);
                Driver.HighlightElement(CitationforViewDigitisation);
                IList<IWebElement> ViewDigitisation = new List<IWebElement>(CitationforViewDigitisation.FindElements(ViewDigitisation_locator));

                if (ViewDigitisation.Count > 0)
                {
                    viewdigitisationpresent = true;
                    Klick.On(ViewDigitisation[0]);
                    Thread.Sleep(KortextGlobals.l);
                    List<NgWebElement> RequestStatusRecords = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("requestStatus in viewDigitisationCtrl.viewDigitisationService.requestList.data")));
                    Console.WriteLine("Number of Status Records found" + RequestStatusRecords.Count);
                    if (RequestStatusRecords.Count > 0)
                    {
                        foreach (IWebElement RequestStatusRecord in RequestStatusRecords)
                        {
                            Console.WriteLine(RequestStatusRecord.Text);
                        }
                        Klick.On(ViewDigitisationCloseButton);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("View Digitisation has no status records. Error loading the Page.");
                    }
                    
                }
            }
            if(viewdigitisationpresent == false)
            {
                Console.WriteLine("View Digitisation button not present for the List");
            }
        }
        public void GoTo()
        {
            Thread.Sleep(1000);
            Driver.Instance.Navigate().GoToUrl("https://kortext.rebuslist.com/#/list/437");
            Thread.Sleep(KortextGlobals.l);
        }
    }
}