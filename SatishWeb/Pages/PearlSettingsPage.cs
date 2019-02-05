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
    public class PearlSettingsPage
    {
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "templateCtrl.section.title")]
        protected IWebElement SectionTitleTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "templateCtrl.subSection.title")]
        protected IWebElement SubSectionTitleTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'templateCtrl.saveSection(templateCtrl.section)']")]
        protected IWebElement AddingSectionFinishButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'templateCtrl.addSubSection(templateCtrl.subSection.title)']")]
        protected IWebElement AddingSubSectionFinishButton
        {
            get;
            set;
        }
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "actionCtrl.selectedTemplate")]
        protected IWebElement EditBufferNewTemplateList
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Undo']")]
        protected IWebElement EditBufferUndoButton
        {
            get;
            set;
        }

        By TemplateNameTextField_locator = By.CssSelector("input[ng-model = 'templateCtrl.model.name']");
        By AddTemplateButton_locator = By.CssSelector("button[ng-click = 'templateCtrl.addTemplate()']");
        By SettingName_locator = By.CssSelector("a[ng-click = 'select($event)']");
        By ControlName_locator = By.ClassName("control-label");
        By MiscTextField_locator = By.CssSelector("input[ng-model = \"model['value']\"]");
        By MiscListField_locator = By.CssSelector("select[ng-model = \"model['value']\"]");
        By AllowedMaterialsSelected_locator = By.ClassName("multiselect-selected-text");
        By SearchField_locator = By.CssSelector("input[placeholder = 'Search']");
        By AllowedMaterialBill_locator = By.CssSelector("input[value = 'string:bill']");
        By AllowedMaterialArticle_locator = By.CssSelector("input[value = 'string:article']");
        By TemplateTitle_locator = By.CssSelector("a[ng-show = 'nameCtrl.display']");
        By TemplateEditButton_locator = By.CssSelector("button[ng-click = 'templateCtrl.toggleToolbar(node.id);templateCtrl.checkType(node.recordType)']");
        By SectionAddButton_locator = By.CssSelector("button[uib-tooltip = 'Add Secton']");
        By SubSectionAddButton_locator = By.CssSelector("button[uib-tooltip = 'Add sub secton']");
        By SectionClickButton_locator = By.CssSelector("button[ng-repeat = 'thisBtn in btnSlideout.optionsBtns']");
        By DeleteButton_locator = By.CssSelector("button[uib-tooltip = 'Delete']");
        By ConfirmDeleteButton_locator = By.CssSelector("button[uib-tooltip = 'Confirm delete']");
        By CancelDeleteButton_locator = By.CssSelector("button[uib-tooltip = 'Cancel delete']");

        string statusreturntext;
        string currentURL;
        string TemplateName;
        string UserNameText;
        int usernameappend;
        string ListURL;

        public bool SettingsPage()
        {
            try
            {
                currentURL = Driver.Instance.Url;

                //Edit / Update Miscellaneous Settings
                MiscSettings();
                
                //Edit / Update Custom Content &CSS Settings
                CSSSettings();
                
                //Edit / Update CSL Settings
                CSLSettings();
                
                //Edit / Update Requests Settings
                RequestSettings();
                                
                //Edit / Update List Templates Settings
                ListTemplateSettings();
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in SettingsPage.cs: " + e.Message);
                return false;
            }
        }

        public void MiscSettings()
        {
            LikeDislike();
            BookCover();
            GlobalAdminEmail();
            LinkResolverURL();
            StartRunningReport();
            StopRunningReport();
            DisplayCourseCode();
            DisplayListYear();
            DisplayYearFormat();
            AllowedMaterialTypes();
        }

        public void LikeDislike()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Display likes / dislikes")
                                {
                                    List<NgWebElement> YesNoButtons = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                    foreach (IWebElement YesNoButton in YesNoButtons)
                                    {
                                        if (YesNoButton.GetAttribute("class") == "btn btn-default")
                                        {
                                            Klick.On(YesNoButton);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext != "Preference updated")
                                            {
                                                Console.WriteLine("Error while updating Display likes / dislikes to " + YesNoButton.Text + "." + statusreturntext);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Display likes / dislikes update Successful to " + YesNoButton.Text);
                                            }
                                            if (YesNoButton.Text == "NO")
                                            {
                                                if (verifyLikesDislikes() == false)
                                                {
                                                    Console.WriteLine("Likes/Dislikes update to NO is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Likes/Dislikes update to NO is not working as expected.");
                                                }
                                            }
                                            else if (YesNoButton.Text == "YES")
                                            {
                                                if (verifyLikesDislikes() == true)
                                                {
                                                    Console.WriteLine("Likes/Dislikes update to YES is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Likes/Dislikes update to YES is not working as expected.");
                                                }
                                            }
                                            break;
                                        }
                                    }

                                    Pages.LandingPage.ClickOnMenu_SettingsBtn();
                                    List<NgWebElement> SecondRoundSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
                                    if (SecondRoundSettingTypes.Count > 0)
                                    {
                                        foreach (IWebElement SecondRoundSettingType in SecondRoundSettingTypes)
                                        {
                                            Driver.HighlightElement(SecondRoundSettingType);
                                            IWebElement SecondRoundSettingName = SecondRoundSettingType.FindElement(SettingName_locator);
                                            if (SecondRoundSettingName.Text == "Miscellaneous settings")
                                            {
                                                Klick.On(SecondRoundSettingName);
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SecondRoundWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                                                if (SecondRoundWidgets.Count > 0)
                                                {
                                                    foreach (NgWebElement SecondRoundWidget in SecondRoundWidgets)
                                                    {
                                                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SecondRoundWidget);
                                                        Driver.HighlightElement(SecondRoundWidget);
                                                        IWebElement SecondRoundControlName = SecondRoundWidget.FindElement(ControlName_locator);
                                                        if (SecondRoundControlName.Text == "Display likes / dislikes")
                                                        {
                                                            //Clicking Yes/No button again to bring to previous picture
                                                            List<NgWebElement> NoYesButtons = new List<NgWebElement>(SecondRoundWidget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                                            foreach (IWebElement NoYesButton in NoYesButtons)
                                                            {
                                                                if (NoYesButton.GetAttribute("class") == "btn btn-default")
                                                                {
                                                                    Klick.On(NoYesButton);
                                                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                                    if (statusreturntext != "Preference updated")
                                                                    {
                                                                        Console.WriteLine("Error while reverting Display likes / dislikes to " + NoYesButton.Text + "." + statusreturntext);
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("Display likes / dislikes revert Successful to " + NoYesButton.Text);
                                                                    }
                                                                    if (NoYesButton.Text == "NO")
                                                                    {
                                                                        if (verifyLikesDislikes() == false)
                                                                        {
                                                                            Console.WriteLine("Likes/Dislikes revert to NO is working good.");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Likes/Dislikes revert to NO is not working as expected.");
                                                                        }
                                                                    }
                                                                    else if (NoYesButton.Text == "YES")
                                                                    {
                                                                        if (verifyLikesDislikes() == true)
                                                                        {
                                                                            Console.WriteLine("Likes/Dislikes revert to YES is working good.");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Likes/Dislikes revert to YES is not working as expected.");
                                                                        }
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    Console.WriteLine("Display likes / dislikes Completed");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void BookCover()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Book cover providers")
                                {
                                    int Amazon_flag = 0;
                                    int Google_flag = 0;
                                    int OpenLibrary_flag = 0;
                                    int prev_Amazon_flag = 0;
                                    int prev_Google_flag = 0;
                                    int prev_OpenLibrary_flag = 0;

                                    List<NgWebElement> BookCoverProviders = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater("val in titleMapValues track by $index")));
                                    foreach (NgWebElement BookCoverProvider in BookCoverProviders)
                                    {
                                        Driver.HighlightElement(BookCoverProvider);
                                        if (BookCoverProvider.Text == "Amazon")
                                        {
                                            IWebElement BookProvidercheckbox = BookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            if (BookProvidercheckbox.Selected == true)
                                            {
                                                Amazon_flag = 1;
                                                prev_Amazon_flag = 1;
                                            }
                                        }
                                        else if (BookCoverProvider.Text == "Google Books")
                                        {
                                            IWebElement BookProvidercheckbox = BookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            if (BookProvidercheckbox.Selected == true)
                                            {
                                                Google_flag = 1;
                                                prev_Google_flag = 1;
                                            }
                                        }
                                        else if (BookCoverProvider.Text == "Open Library")
                                        {
                                            IWebElement BookProvidercheckbox = BookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            if (BookProvidercheckbox.Selected == true)
                                            {
                                                OpenLibrary_flag = 1;
                                                prev_OpenLibrary_flag = 1;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("BookCoverProvider Not Handled.");
                                        }
                                    }

                                    //Selecting all the Book Cover Providers
                                    List<NgWebElement> SelectingAllBookCoverProviders = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater("val in titleMapValues track by $index")));
                                    foreach (IWebElement SelectingAllBookCoverProvider in SelectingAllBookCoverProviders)
                                    {
                                        Driver.HighlightElement(SelectingAllBookCoverProvider);
                                        if (SelectingAllBookCoverProvider.Text == "Amazon" && Amazon_flag == 0)
                                        {
                                            IWebElement BookProvidercheckbox = SelectingAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            Amazon_flag = 1;
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + SelectingAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider shows Error Message." + SelectingAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + SelectingAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                        }
                                        if (SelectingAllBookCoverProvider.Text == "Google Books" && Google_flag == 0)
                                        {
                                            IWebElement BookProvidercheckbox = SelectingAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            Google_flag = 1;
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + SelectingAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider shows Error Message." + SelectingAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + SelectingAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                        }
                                        if (SelectingAllBookCoverProvider.Text == "Open Library" && OpenLibrary_flag == 0)
                                        {
                                            IWebElement BookProvidercheckbox = SelectingAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            OpenLibrary_flag = 1;
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + SelectingAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider shows Error Message." + SelectingAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + SelectingAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                        }
                                    }

                                    //Unselecting all the Book Cover Providers
                                    List<NgWebElement> UnselectingAllBookCoverProviders = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater("val in titleMapValues track by $index")));
                                    foreach (IWebElement UnselectingAllBookCoverProvider in UnselectingAllBookCoverProviders)
                                    {
                                        Driver.HighlightElement(UnselectingAllBookCoverProvider);
                                        if (UnselectingAllBookCoverProvider.Text == "Amazon" && Amazon_flag == 1)
                                        {
                                            IWebElement BookProvidercheckbox = UnselectingAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            Amazon_flag = 0;
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Unselecting Book Cover Provider Successful." + UnselectingAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider shows Error Message." + UnselectingAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider didn't show any Status Message." + UnselectingAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                        }
                                        if (UnselectingAllBookCoverProvider.Text == "Google Books" && Google_flag == 1)
                                        {
                                            IWebElement BookProvidercheckbox = UnselectingAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            Google_flag = 0;
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Unselecting Book Cover Provider Successful." + UnselectingAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider shows Error Message." + UnselectingAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider didn't show any Status Message." + UnselectingAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                        }
                                        if (UnselectingAllBookCoverProvider.Text == "Open Library" && OpenLibrary_flag == 1)
                                        {
                                            IWebElement BookProvidercheckbox = UnselectingAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            OpenLibrary_flag = 0;
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Unselecting Book Cover Provider Successful." + UnselectingAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Unselecting Book Cover Provider Successful." + UnselectingAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider didn't show any Status Message." + UnselectingAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                        }
                                    }

                                    //Setting the Book Cover Providers to earlier picture
                                    List<NgWebElement> PrevAllBookCoverProviders = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater("val in titleMapValues track by $index")));
                                    foreach (IWebElement PrevAllBookCoverProvider in PrevAllBookCoverProviders)
                                    {
                                        Driver.HighlightElement(PrevAllBookCoverProvider);
                                        if (PrevAllBookCoverProvider.Text == "Amazon" && prev_Amazon_flag == 1)
                                        {
                                            IWebElement BookProvidercheckbox = PrevAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + PrevAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider shows Error Message." + PrevAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + PrevAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                            Amazon_flag = 0;
                                        }
                                        if (PrevAllBookCoverProvider.Text == "Google Books" && prev_Google_flag == 1)
                                        {
                                            IWebElement BookProvidercheckbox = PrevAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + PrevAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider shows Error Message." + PrevAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + PrevAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                            Google_flag = 0;
                                        }
                                        if (PrevAllBookCoverProvider.Text == "Open Library" && prev_OpenLibrary_flag == 1)
                                        {
                                            IWebElement BookProvidercheckbox = PrevAllBookCoverProvider.FindElement(NgBy.Model("titleMapValues[$index]"));
                                            Klick.On(BookProvidercheckbox);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext == "Preference updated")
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + PrevAllBookCoverProvider.Text);
                                            }
                                            else if (statusreturntext == "Could not update preference")
                                            {
                                                Console.WriteLine("Error as Selecting Book Cover Provider shows Error Message." + PrevAllBookCoverProvider.Text);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Selecting Book Cover Provider Successful." + PrevAllBookCoverProvider.Text + "." + statusreturntext);
                                            }
                                            OpenLibrary_flag = 0;
                                        }
                                    }
                                    Console.WriteLine("Book cover providers Completed.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void GlobalAdminEmail()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Global admin email address")
                                {
                                    string prev_emailtext = null;
                                    IWebElement GlobalAdminEmailText = Widget.FindElement(MiscTextField_locator);
                                    prev_emailtext = GlobalAdminEmailText.GetAttribute("value");
                                    Console.WriteLine("prev_emailtext = " + prev_emailtext);
                                    WaitFind.FindElem(GlobalAdminEmailText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Global Admin Email Address to Blank.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Updating Global Admin Email Address to Blank Successful.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Global Admin Email Address." + statusreturntext);
                                    }
                                    Klick.On(GlobalAdminEmailText);
                                    Thread.Sleep(KortextGlobals.s);
                                    GlobalAdminEmailText.SendKeys("admin@kortext.com");
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Global Admin Email Address Successful." + "admin@kortext.com");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Global Admin Email Address." + "admin@kortext.com");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Global Admin Email Address." + "admin@kortext.com" + "." + statusreturntext);
                                    }

                                    //Reverting the value to previous value
                                    WaitFind.FindElem(GlobalAdminEmailText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Global Admin Email Address to Blank.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Updating Global Admin Email Address to Blank Successful.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Global Admin Email Address." + statusreturntext);
                                    }
                                    Klick.On(GlobalAdminEmailText);
                                    Thread.Sleep(KortextGlobals.s);
                                    GlobalAdminEmailText.SendKeys(prev_emailtext);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Global Admin Email Address Successful." + prev_emailtext);
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Global Admin Email Address." + prev_emailtext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Global Admin Email Address." + prev_emailtext + "." + statusreturntext);
                                    }
                                    Console.WriteLine("Global Admin Email Address Completed.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void LinkResolverURL()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Link resolver base URL")
                                {
                                    IWebElement BaseURLText = Widget.FindElement(MiscTextField_locator);
                                    string prev_BaseURLtext = null;
                                    prev_BaseURLtext = BaseURLText.GetAttribute("value");
                                    WaitFind.FindElem(BaseURLText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Link resolver base URL  to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Link resolver base URL to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Link resolver base URL." + statusreturntext);
                                    }
                                    Klick.On(BaseURLText);
                                    Thread.Sleep(KortextGlobals.s);
                                    BaseURLText.SendKeys("http://myresolver.com/abcd");
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Link resolver base URL Successful." + "http://myresolver.com/abcd");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Link resolver base URL." + "http://myresolver.com/abcd");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Link resolver base URL." + "http://myresolver.com/abcd" + "." + statusreturntext);
                                    }

                                    //Reverting the value to previous value
                                    WaitFind.FindElem(BaseURLText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Link resolver base URL  to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Link resolver base URL to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Link resolver base URL to Blank." + statusreturntext);
                                    }
                                    Klick.On(BaseURLText);
                                    Thread.Sleep(KortextGlobals.s);
                                    BaseURLText.SendKeys(prev_BaseURLtext);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Link resolver base URL Successful." + prev_BaseURLtext);
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Link resolver base URL." + prev_BaseURLtext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Link resolver base URL." + prev_BaseURLtext + "." + statusreturntext);
                                    }
                                    Console.WriteLine("Link resolver base URL Completed.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void StartRunningReport()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Start running reports time")
                                {
                                    IWebElement StartReportTimeText = Widget.FindElement(MiscTextField_locator);
                                    string prev_StartReportTimeText = null;
                                    prev_StartReportTimeText = StartReportTimeText.GetAttribute("value");
                                    WaitFind.FindElem(StartReportTimeText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Start running reports time to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Start running reports time to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Start running reports time." + statusreturntext);
                                    }
                                    Klick.On(StartReportTimeText);
                                    Thread.Sleep(KortextGlobals.s);
                                    StartReportTimeText.SendKeys("abcd");
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Start running reports time Successful." + "abcd");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Start running reports time." + "abcd");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Start running reports time." + "abcd" + "." + statusreturntext);
                                    }

                                    //Reverting the value to previous value
                                    WaitFind.FindElem(StartReportTimeText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Start running reports time to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Start running reports time to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Start running reports time." + statusreturntext);
                                    }
                                    Klick.On(StartReportTimeText);
                                    Thread.Sleep(KortextGlobals.s);
                                    StartReportTimeText.SendKeys(prev_StartReportTimeText);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Start running reports time Successful." + prev_StartReportTimeText);
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Start running reports time." + prev_StartReportTimeText);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Start running reports time." + prev_StartReportTimeText + "." + statusreturntext);
                                    }
                                    Console.WriteLine("Start running reports time Completed.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void StopRunningReport()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Stop running reports time")
                                {
                                    IWebElement StopReportTimeText = Widget.FindElement(MiscTextField_locator);
                                    string prev_StopReportTimeText = null;
                                    prev_StopReportTimeText = StopReportTimeText.GetAttribute("value");
                                    WaitFind.FindElem(StopReportTimeText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Stop running reports time to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Stop running reports time to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Stop running reports time." + statusreturntext);
                                    }
                                    Klick.On(StopReportTimeText);
                                    Thread.Sleep(KortextGlobals.s);
                                    StopReportTimeText.SendKeys("abcd");
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Stop running reports time Successful." + "abcd");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Stop running reports time." + "abcd");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Stop running reports time." + "abcd" + "." + statusreturntext);
                                    }

                                    //Reverting the value to previous value
                                    WaitFind.FindElem(StopReportTimeText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Stop running reports time to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Stop running reports time to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Stop running reports time to Blank." + statusreturntext);
                                    }
                                    Klick.On(StopReportTimeText);
                                    Thread.Sleep(KortextGlobals.s);
                                    StopReportTimeText.SendKeys(prev_StopReportTimeText);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Stop running reports time Successful." + prev_StopReportTimeText);
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Stop running reports time." + prev_StopReportTimeText);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Stop running reports time." + prev_StopReportTimeText + "." + statusreturntext);
                                    }
                                    Console.WriteLine("Stop running reports time Completed.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void DisplayCourseCode()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Display course code with list title")
                                {
                                    List<NgWebElement> CourseCodeYesNoButtons = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                    foreach (IWebElement CourseCodeYesNoButton in CourseCodeYesNoButtons)
                                    {
                                        if (CourseCodeYesNoButton.GetAttribute("class") == "btn btn-default")
                                        {
                                            Klick.On(CourseCodeYesNoButton);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext != "Preference updated")
                                            {
                                                Console.WriteLine("Error while updating Display course code with list title. " + CourseCodeYesNoButton.Text + "." + statusreturntext);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Display course code with list title update Successful to " + CourseCodeYesNoButton.Text);
                                            }
                                            if (CourseCodeYesNoButton.Text == "NO")
                                            {
                                                if (verifyCourseCodeDisplay() == false)
                                                {
                                                    Console.WriteLine("Course Code display update to NO is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Course Code display update to NO is not working as expected.");
                                                }
                                            }
                                            else if (CourseCodeYesNoButton.Text == "YES")
                                            {
                                                if (verifyCourseCodeDisplay() == true)
                                                {
                                                    Console.WriteLine("Course Code display update to YES is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Course Code display update to YES is not working as expected.");
                                                }
                                            }
                                            break;
                                        }
                                    }

                                    Pages.LandingPage.ClickOnMenu_SettingsBtn();
                                    List<NgWebElement> SecondRoundSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
                                    if (SecondRoundSettingTypes.Count > 0)
                                    {
                                        foreach (IWebElement SecondRoundSettingType in SecondRoundSettingTypes)
                                        {
                                            Driver.HighlightElement(SecondRoundSettingType);
                                            IWebElement SecondRoundSettingName = SecondRoundSettingType.FindElement(SettingName_locator);
                                            if (SecondRoundSettingName.Text == "Miscellaneous settings")
                                            {
                                                Klick.On(SecondRoundSettingName);
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SecondRoundWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                                                if (SecondRoundWidgets.Count > 0)
                                                {
                                                    foreach (NgWebElement SecondRoundWidget in SecondRoundWidgets)
                                                    {
                                                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SecondRoundWidget);
                                                        Driver.HighlightElement(SecondRoundWidget);
                                                        IWebElement SecondRoundControlName = SecondRoundWidget.FindElement(ControlName_locator);
                                                        if (SecondRoundControlName.Text == "Display course code with list title")
                                                        {
                                                            //Clicking Yes/No button again to bring to previous picture
                                                            List<NgWebElement> CourseCodeNoYesButtons = new List<NgWebElement>(SecondRoundWidget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                                            foreach (IWebElement CourseCodeNoYesButton in CourseCodeNoYesButtons)
                                                            {
                                                                if (CourseCodeNoYesButton.GetAttribute("class") == "btn btn-default")
                                                                {
                                                                    Klick.On(CourseCodeNoYesButton);
                                                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                                    if (statusreturntext != "Preference updated")
                                                                    {
                                                                        Console.WriteLine("Error while updating Display course code with list title. " + CourseCodeNoYesButton.Text + "." + statusreturntext);
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("Display course code with list title update Successful to " + CourseCodeNoYesButton.Text);
                                                                    }
                                                                    if (CourseCodeNoYesButton.Text == "NO")
                                                                    {
                                                                        if (verifyCourseCodeDisplay() == false)
                                                                        {
                                                                            Console.WriteLine("Course Code revert to NO is working good.");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Course Code revert to NO is not working as expected.");
                                                                        }
                                                                    }
                                                                    else if (CourseCodeNoYesButton.Text == "YES")
                                                                    {
                                                                        if (verifyCourseCodeDisplay() == true)
                                                                        {
                                                                            Console.WriteLine("Course Code revert to YES is working good.");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Course Code revert to YES is not working as expected.");
                                                                        }
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    Console.WriteLine("Display course code with list title Completed");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void DisplayListYear()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Display list year with list title")
                                {
                                    List<NgWebElement> ListYearYesNoButtons = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                    foreach (IWebElement ListYearYesNoButton in ListYearYesNoButtons)
                                    {
                                        if (ListYearYesNoButton.GetAttribute("class") == "btn btn-default")
                                        {
                                            Klick.On(ListYearYesNoButton);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext != "Preference updated")
                                            {
                                                Console.WriteLine("Error while updating Display list year with list title. " + ListYearYesNoButton.Text + "." + statusreturntext);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Display list year with list title update Successful to " + ListYearYesNoButton.Text);
                                            }
                                            if (ListYearYesNoButton.Text == "NO")
                                            {
                                                if (verifyListYearDisplay() == false)
                                                {
                                                    Console.WriteLine("List Year display update to NO is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("List Year display update to NO is not working as expected.");
                                                }
                                            }
                                            else if (ListYearYesNoButton.Text == "YES")
                                            {
                                                if (verifyListYearDisplay() == true)
                                                {
                                                    Console.WriteLine("List Year display update to YES is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("List Year display update to YES is not working as expected.");
                                                }
                                            }
                                            break;
                                        }
                                    }

                                    Pages.LandingPage.ClickOnMenu_SettingsBtn();
                                    List<NgWebElement> SecondRoundSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
                                    if (SecondRoundSettingTypes.Count > 0)
                                    {
                                        foreach (IWebElement SecondRoundSettingType in SecondRoundSettingTypes)
                                        {
                                            Driver.HighlightElement(SecondRoundSettingType);
                                            IWebElement SecondRoundSettingName = SecondRoundSettingType.FindElement(SettingName_locator);
                                            if (SecondRoundSettingName.Text == "Miscellaneous settings")
                                            {
                                                Klick.On(SecondRoundSettingName);
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SecondRoundWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                                                if (SecondRoundWidgets.Count > 0)
                                                {
                                                    foreach (NgWebElement SecondRoundWidget in SecondRoundWidgets)
                                                    {
                                                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SecondRoundWidget);
                                                        Driver.HighlightElement(SecondRoundWidget);
                                                        IWebElement SecondRoundControlName = SecondRoundWidget.FindElement(ControlName_locator);
                                                        if (SecondRoundControlName.Text == "Display list year with list title")
                                                        {
                                                            //Clicking Yes/No button again to bring to previous picture
                                                            List<NgWebElement> ListYearNoYesButtons = new List<NgWebElement>(SecondRoundWidget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                                            foreach (IWebElement ListYearNoYesButton in ListYearNoYesButtons)
                                                            {
                                                                if (ListYearNoYesButton.GetAttribute("class") == "btn btn-default")
                                                                {
                                                                    Klick.On(ListYearNoYesButton);
                                                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                                    if (statusreturntext != "Preference updated")
                                                                    {
                                                                        Console.WriteLine("Error while updating Display list year with list title. " + ListYearNoYesButton.Text + "." + statusreturntext);
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("Display list year with list title update Successful to " + ListYearNoYesButton.Text);
                                                                    }
                                                                    if (ListYearNoYesButton.Text == "NO")
                                                                    {
                                                                        if (verifyListYearDisplay() == false)
                                                                        {
                                                                            Console.WriteLine("List Year revert to NO is working good.");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("List Year revert to NO is not working as expected.");
                                                                        }
                                                                    }
                                                                    else if (ListYearNoYesButton.Text == "YES")
                                                                    {
                                                                        if (verifyListYearDisplay() == true)
                                                                        {
                                                                            Console.WriteLine("List Year revert to YES is working good.");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("List Year revert to YES is not working as expected.");
                                                                        }
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    Console.WriteLine("Display list year with list title Completed");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void DisplayYearFormat()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);

                        //To check whether the Display List Year with List Title is switched to YES
                        List<NgWebElement> YearSwitchWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (YearSwitchWidgets.Count > 0)
                        {
                            foreach (NgWebElement YearSwitchWidget in YearSwitchWidgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", YearSwitchWidget);
                                Driver.HighlightElement(YearSwitchWidget);
                                IWebElement YearSwitchControlName = YearSwitchWidget.FindElement(ControlName_locator);
                                if (YearSwitchControlName.Text == "Display list year with list title")
                                {
                                    List<NgWebElement> YearSwitchYesNoButtons = new List<NgWebElement>(YearSwitchWidget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                    foreach (IWebElement YearSwitchYesNoButton in YearSwitchYesNoButtons)
                                    {
                                        if ((YearSwitchYesNoButton.GetAttribute("class") == "btn btn-default") && YearSwitchYesNoButton.Text == "YES")
                                        {
                                            Klick.On(YearSwitchYesNoButton);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext != "Preference updated")
                                            {
                                                Console.WriteLine("Error while updating Display list year with list title. " + YearSwitchYesNoButton.Text + "." + statusreturntext);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Display list year with list title update Successful to " + YearSwitchYesNoButton.Text);
                                            }
                                            if (YearSwitchYesNoButton.Text == "NO")
                                            {
                                                if (verifyListYearDisplay() == false)
                                                {
                                                    Console.WriteLine("List Year display update to NO is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("List Year display update to NO is not working as expected.");
                                                }
                                            }
                                            else if (YearSwitchYesNoButton.Text == "YES")
                                            {
                                                if (verifyListYearDisplay() == true)
                                                {
                                                    Console.WriteLine("List Year display update to YES is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("List Year display update to YES is not working as expected.");
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }

                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Year display format")
                                {
                                    //List<NgWebElement> YearFormatLists = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater ("item in form.titleMap")));
                                    IWebElement YearFormatLists = Widget.FindElement(MiscListField_locator);
                                    string prev_YearFormatLists = YearFormatLists.GetAttribute("value");
                                    Driver.HighlightElement(YearFormatLists);
                                    if (prev_YearFormatLists == "string:yyyy")
                                    {
                                        new SelectElement(YearFormatLists).SelectByValue("string:yyyy-yy");
                                    }
                                    else if (prev_YearFormatLists == "string:yy-yy")
                                    {
                                        new SelectElement(YearFormatLists).SelectByValue("string:yyyy-yy");
                                    }
                                    else if (prev_YearFormatLists == "string:yyyy-yy")
                                    {
                                        new SelectElement(YearFormatLists).SelectByValue("string:yyyy-yyyy");
                                    }
                                    else if (prev_YearFormatLists == "string:yyyy-yyyy")
                                    {
                                        new SelectElement(YearFormatLists).SelectByValue("string:yyyy-yy");
                                    }
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext != "Preference updated")
                                    {
                                        Console.WriteLine("Error while updating Year display format." + Widget.FindElement(MiscListField_locator).GetAttribute("value") + "." + statusreturntext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Year display format update Successful." + Widget.FindElement(MiscListField_locator).GetAttribute("value"));
                                    }
                                    Thread.Sleep(KortextGlobals.s);
                                    string post_YearFormatLists = YearFormatLists.GetAttribute("value");
                                    string convertedYearFormat = post_YearFormatLists.Replace("string:","");
                                    if (verifyFormatYearDisplay(convertedYearFormat) == true)
                                    {
                                        Console.WriteLine("Year Display Format update is working good." + convertedYearFormat);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Year Display Format update is not working good." + convertedYearFormat);
                                    }
                                                                        
                                    Pages.LandingPage.ClickOnMenu_SettingsBtn();
                                    List<NgWebElement> SecondRoundSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
                                    if (SecondRoundSettingTypes.Count > 0)
                                    {
                                        foreach (IWebElement SecondRoundSettingType in SecondRoundSettingTypes)
                                        {
                                            Driver.HighlightElement(SecondRoundSettingType);
                                            IWebElement SecondRoundSettingName = SecondRoundSettingType.FindElement(SettingName_locator);
                                            if (SecondRoundSettingName.Text == "Miscellaneous settings")
                                            {
                                                Klick.On(SecondRoundSettingName);
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SecondRoundWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                                                if (SecondRoundWidgets.Count > 0)
                                                {
                                                    foreach (NgWebElement SecondRoundWidget in SecondRoundWidgets)
                                                    {
                                                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SecondRoundWidget);
                                                        Driver.HighlightElement(SecondRoundWidget);
                                                        IWebElement SecondRoundControlName = SecondRoundWidget.FindElement(ControlName_locator);
                                                        if (SecondRoundControlName.Text == "Year display format")
                                                        {
                                                            //Reverting the Year format to previous format
                                                            IWebElement SecondRoundYearFormatLists = SecondRoundWidget.FindElement(MiscListField_locator);
                                                            Driver.HighlightElement(SecondRoundYearFormatLists);
                                                            new SelectElement(SecondRoundYearFormatLists).SelectByValue(prev_YearFormatLists);
                                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                            if (statusreturntext != "Preference updated")
                                                            {
                                                                Console.WriteLine("Error while updating Year display format. " + prev_YearFormatLists + "." + statusreturntext);
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Year display format update Successful to " + prev_YearFormatLists);
                                                            }
                                                            Thread.Sleep(KortextGlobals.s);
                                                            string postpost_YearFormatLists = SecondRoundYearFormatLists.GetAttribute("value");
                                                            string postconvertedYearFormat = postpost_YearFormatLists.Replace("string:", "");
                                                            if (verifyFormatYearDisplay(postconvertedYearFormat) == true)
                                                            {
                                                                Console.WriteLine("Year Display Format revert is working good." + postconvertedYearFormat);
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Year Display Format revert is not working good." + postconvertedYearFormat);
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    Console.WriteLine("Year display format Completed");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void AllowedMaterialTypes()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            int initial_selected_flag = 0;

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Allowed material types")
                                {
                                    string post_AllowedMaterialsSelected_number;
                                    string prev_AllowedMaterialsSelected_number;

                                    IWebElement AllowedMaterialsSelected = Widget.FindElement(AllowedMaterialsSelected_locator);
                                    string prev_AllowedMaterialsSelected = AllowedMaterialsSelected.Text;
                                    Klick.On(AllowedMaterialsSelected);

                                    IWebElement AllowedMaterialArticle = Widget.FindElement(AllowedMaterialArticle_locator);
                                    if (AllowedMaterialArticle.Selected == true)
                                    {
                                        Klick.On(AllowedMaterialArticle);
                                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                        if (statusreturntext != "Preference updated")
                                        {
                                            Console.WriteLine("Error while updating Allowed Material Article." + statusreturntext);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Allowed Material Article update Successful.");
                                        }

                                        IWebElement post_AllowedMaterialsSelected = Widget.FindElement(AllowedMaterialsSelected_locator);
                                        post_AllowedMaterialsSelected_number = post_AllowedMaterialsSelected.Text.Replace(" SELECTED.", "");
                                        prev_AllowedMaterialsSelected_number = prev_AllowedMaterialsSelected.Replace(" SELECTED.", "");
                                        Console.WriteLine("if post_AllowedMaterialsSelected_number = " + post_AllowedMaterialsSelected_number);
                                        Console.WriteLine("if prev_AllowedMaterialsSelected_number = " + prev_AllowedMaterialsSelected_number);
                                        if (Convert.ToInt32(post_AllowedMaterialsSelected_number) == (Convert.ToInt32(prev_AllowedMaterialsSelected_number) - 1))
                                        {
                                            Console.WriteLine("Allowed Materials update Successful.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error while updating Allowed Materials.");
                                        }
                                    }
                                    else
                                    {
                                        Klick.On(AllowedMaterialArticle);
                                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                        if (statusreturntext != "Preference updated")
                                        {
                                            Console.WriteLine("Error while updating Allowed Material Article." + statusreturntext);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Allowed Material Article update Successful.");
                                        }

                                        IWebElement post_AllowedMaterialsSelected = Widget.FindElement(AllowedMaterialsSelected_locator);
                                        post_AllowedMaterialsSelected_number = post_AllowedMaterialsSelected.Text.Replace(" SELECTED.", "");
                                        prev_AllowedMaterialsSelected_number = prev_AllowedMaterialsSelected.Replace(" SELECTED.", "");
                                        Console.WriteLine("else post_AllowedMaterialsSelected_number = " + post_AllowedMaterialsSelected_number);
                                        Console.WriteLine("else prev_AllowedMaterialsSelected_number = " + prev_AllowedMaterialsSelected_number);
                                        if (Convert.ToInt32(post_AllowedMaterialsSelected_number) == (Convert.ToInt32(prev_AllowedMaterialsSelected_number) + 1))
                                        {
                                            Console.WriteLine("Allowed Materials update Successful.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error while updating Allowed Materials.");
                                        }
                                        initial_selected_flag = 1;
                                    }
                                    if (initial_selected_flag == 1)
                                    {
                                        if (verifyActualAllowedMaterials("article") == true)
                                        {
                                            Console.WriteLine("Allowed Materials Types update is working good.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Allowed Materials Types update is not working good.");
                                        }
                                    }
                                    else
                                    {
                                        if (verifyActualAllowedMaterials("article") == false)
                                        {
                                            Console.WriteLine("Allowed Materials Types update is working good.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Allowed Materials Types update is not working good.");
                                        }
                                    }
                                    Thread.Sleep(KortextGlobals.s);

                                    Pages.LandingPage.ClickOnMenu_SettingsBtn();
                                    List<NgWebElement> SecondRoundSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
                                    if (SecondRoundSettingTypes.Count > 0)
                                    {
                                        foreach (IWebElement SecondRoundSettingType in SecondRoundSettingTypes)
                                        {
                                            Driver.HighlightElement(SecondRoundSettingType);
                                            IWebElement SecondRoundSettingName = SecondRoundSettingType.FindElement(SettingName_locator);
                                            if (SecondRoundSettingName.Text == "Miscellaneous settings")
                                            {
                                                Klick.On(SecondRoundSettingName);
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SecondRoundWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                                                if (SecondRoundWidgets.Count > 0)
                                                {
                                                    foreach (NgWebElement SecondRoundWidget in SecondRoundWidgets)
                                                    {
                                                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SecondRoundWidget);
                                                        Driver.HighlightElement(SecondRoundWidget);
                                                        IWebElement SecondRoundControlName = SecondRoundWidget.FindElement(ControlName_locator);
                                                        if (SecondRoundControlName.Text == "Allowed material types")
                                                        {
                                                            //Reverting the Allowed Materials Section
                                                            IWebElement SecondRoundAllowedMaterialsSelected = SecondRoundWidget.FindElement(AllowedMaterialsSelected_locator);
                                                            Klick.On(SecondRoundAllowedMaterialsSelected);

                                                            IWebElement SecondRoundAllowedMaterialArticle = SecondRoundWidget.FindElement(AllowedMaterialArticle_locator);
                                                            Klick.On(SecondRoundAllowedMaterialArticle);
                                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                            if (statusreturntext != "Preference updated")
                                                            {
                                                                Console.WriteLine("Error while reverting Allowed Material Article." + statusreturntext);
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Allowed Material Article revert Successful.");
                                                            }
                                                            Thread.Sleep(KortextGlobals.s);
                                                            if (initial_selected_flag == 0)
                                                            {
                                                                if (verifyActualAllowedMaterials("article") == true)
                                                                {
                                                                    Console.WriteLine("Allowed Materials Types revert is working good.");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Allowed Materials Types revert is not working good.");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (verifyActualAllowedMaterials("article") == false)
                                                                {
                                                                    Console.WriteLine("Allowed Materials Types revert is working good.");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Allowed Materials Types revert is not working good.");
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    Console.WriteLine("Allowed Materials Completed.");
                                    //Klick.On(Widget.FindElement(AllowedMaterialsSelected_locator));
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void CSSSettings()
        {
            CustomerName();
            SiteTitle();
        }

        public void CustomerName()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Custom content & CSS")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Customer name")
                                {
                                    IWebElement CustomerNameText = Widget.FindElement(MiscTextField_locator);
                                    string prev_CustomerNameText = null;
                                    prev_CustomerNameText = CustomerNameText.GetAttribute("value");
                                    WaitFind.FindElem(CustomerNameText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Customer Name to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Customer Name to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Customer Name to Blank." + statusreturntext);
                                    }
                                    Klick.On(CustomerNameText);
                                    Thread.Sleep(KortextGlobals.s);
                                    CustomerNameText.SendKeys("abcd");
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Customer Name Successful." + "abcd");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Customer Name." + "abcd");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Customer Name." + "abcd" + "." + statusreturntext);
                                    }

                                    //Reverting the value to previous value
                                    WaitFind.FindElem(CustomerNameText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Customer Name to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Customer Name to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Customer Name to Blank." + statusreturntext);
                                    }
                                    Klick.On(CustomerNameText);
                                    Thread.Sleep(KortextGlobals.s);
                                    CustomerNameText.SendKeys(prev_CustomerNameText);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Customer Name Successful." + prev_CustomerNameText);
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Customer Name." + prev_CustomerNameText);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Customer Name." + prev_CustomerNameText + "." + statusreturntext);
                                    }
                                    Console.WriteLine("Customer Name Completed.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void SiteTitle()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            string prev_SiteTitleText = null;

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Custom content & CSS")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Site title tag")
                                {
                                    string ActualSiteTitleText = Driver.Instance.Title;
                                    Console.WriteLine("ActualSiteTitleText = " + ActualSiteTitleText);
                                    IWebElement SiteTitleText = Widget.FindElement(MiscTextField_locator);
                                    prev_SiteTitleText = SiteTitleText.GetAttribute("value");
                                    string ActualSiteTitleBreakup = ActualSiteTitleText.Replace(prev_SiteTitleText, "");
                                    Console.WriteLine("ActualSiteTitleBreakup = " + ActualSiteTitleBreakup);
                                    WaitFind.FindElem(SiteTitleText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Site Title to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Site Title to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Site Title to Blank." + statusreturntext);
                                    }
                                    Klick.On(SiteTitleText);
                                    Thread.Sleep(KortextGlobals.s);
                                    SiteTitleText.SendKeys("abcd");
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Site Title Successful." + "abcd");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Site Title." + "abcd");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Site Title." + "abcd" + "." + statusreturntext);
                                    }
                                    string expected_SiteTitle = ActualSiteTitleBreakup + "abcd";
                                    Driver.Instance.Navigate().Refresh();
                                    Thread.Sleep(KortextGlobals.l);
                                    string New_ActualSiteTitle = Driver.Instance.Title;
                                    if (expected_SiteTitle == New_ActualSiteTitle)
                                    {
                                        Console.WriteLine("Site Title working as expected.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Site Title not working as expected. Actual Title and Expected Title do not match."+ expected_SiteTitle +"."+ New_ActualSiteTitle);
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }

            List<NgWebElement> ForRevertSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (ForRevertSettingTypes.Count > 0)
            {
                foreach (IWebElement ForRevertSettingType in ForRevertSettingTypes)
                {
                    Driver.HighlightElement(ForRevertSettingType);
                    IWebElement ForRevertSettingName = ForRevertSettingType.FindElement(SettingName_locator);
                    if (ForRevertSettingName.Text == "Custom content & CSS")
                    {
                        Klick.On(ForRevertSettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> ForRevertWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (ForRevertWidgets.Count > 0)
                        {
                            foreach (NgWebElement ForRevertWidget in ForRevertWidgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", ForRevertWidget);
                                Driver.HighlightElement(ForRevertWidget);
                                IWebElement ForRevertControlName = ForRevertWidget.FindElement(ControlName_locator);
                                if (ForRevertControlName.Text == "Site title tag")
                                {
                                    IWebElement ForRevertSiteTitleText = ForRevertWidget.FindElement(MiscTextField_locator);
                                    //Reverting the value to previous value
                                    WaitFind.FindElem(ForRevertSiteTitleText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Site Title to Blank Successful.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Site Title to Blank.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Site Title to Blank." + statusreturntext);
                                    }
                                    Klick.On(ForRevertSiteTitleText);
                                    Thread.Sleep(KortextGlobals.s);
                                    ForRevertSiteTitleText.SendKeys(prev_SiteTitleText);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Reverting Site Title Successful." + prev_SiteTitleText);
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while Reverting Site Title." + prev_SiteTitleText);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while reverting Site Title." + prev_SiteTitleText + "." + statusreturntext);
                                    }
                                    Console.WriteLine("Site Title Completed.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void CSLSettings()
        {
            CitationButton();
        }

        public void CitationButton()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "CSL settings")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Display citation button for list items")
                                {
                                    List<NgWebElement> YesNoButtons = new List<NgWebElement>(Widget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                    foreach (IWebElement YesNoButton in YesNoButtons)
                                    {
                                        if (YesNoButton.GetAttribute("class") == "btn btn-default")
                                        {
                                            Klick.On(YesNoButton);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext != "Preference updated")
                                            {
                                                Console.WriteLine("Error while updating Display Citation Button to " + YesNoButton.Text + "." + statusreturntext);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Display Citation Button update Successful to " + YesNoButton.Text);
                                            }
                                            if (YesNoButton.Text == "NO")
                                            {
                                                if (verifyCitationsdisplayed() == false)
                                                {
                                                    Console.WriteLine("Citation Button update to NO is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Citation Button update to NO is not working as expected.");
                                                }
                                            }
                                            else if (YesNoButton.Text == "YES")
                                            {
                                                if (verifyCitationsdisplayed() == true)
                                                {
                                                    Console.WriteLine("Citation Button update to YES is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Citation Button update to YES is not working as expected.");
                                                }
                                            }
                                            break;
                                        }
                                    }

                                    Pages.LandingPage.ClickOnMenu_SettingsBtn();
                                    List<NgWebElement> SecondRoundSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
                                    if (SecondRoundSettingTypes.Count > 0)
                                    {
                                        foreach (IWebElement SecondRoundSettingType in SecondRoundSettingTypes)
                                        {
                                            Driver.HighlightElement(SecondRoundSettingType);
                                            IWebElement SecondRoundSettingName = SecondRoundSettingType.FindElement(SettingName_locator);
                                            if (SecondRoundSettingName.Text == "CSL settings")
                                            {
                                                Klick.On(SecondRoundSettingName);
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SecondRoundWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                                                if (SecondRoundWidgets.Count > 0)
                                                {
                                                    foreach (NgWebElement SecondRoundWidget in SecondRoundWidgets)
                                                    {
                                                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SecondRoundWidget);
                                                        Driver.HighlightElement(SecondRoundWidget);
                                                        IWebElement SecondRoundControlName = SecondRoundWidget.FindElement(ControlName_locator);
                                                        if (SecondRoundControlName.Text == "Display citation button for list items")
                                                        {
                                                            //Clicking Yes/No button again to bring to previous picture
                                                            List<NgWebElement> NoYesButtons = new List<NgWebElement>(SecondRoundWidget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                                            foreach (IWebElement NoYesButton in NoYesButtons)
                                                            {
                                                                if (NoYesButton.GetAttribute("class") == "btn btn-default")
                                                                {
                                                                    Klick.On(NoYesButton);
                                                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                                    if (statusreturntext != "Preference updated")
                                                                    {
                                                                        Console.WriteLine("Error while updating Display Citation Button to " + NoYesButton.Text + "." + statusreturntext);
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("Display Citation Button update Successful to " + NoYesButton.Text);
                                                                    }
                                                                    if (NoYesButton.Text == "NO")
                                                                    {
                                                                        if (verifyCitationsdisplayed() == false)
                                                                        {
                                                                            Console.WriteLine("Citation Button revert to NO is working good.");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Citation Button revert to NO is not working as expected.");
                                                                        }
                                                                    }
                                                                    else if (NoYesButton.Text == "YES")
                                                                    {
                                                                        if (verifyCitationsdisplayed() == true)
                                                                        {
                                                                            Console.WriteLine("Citation Button revert to YES is working good.");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Citation Button revert to YES is not working as expected.");
                                                                        }
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    Console.WriteLine("Display Citation Button Completed");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void RequestSettings()
        {
            DigReqNotEmail();
        }

        public void DigReqNotEmail()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "Requests")
                    {
                        Klick.On(SettingName);
                        Thread.Sleep(KortextGlobals.s);
                        List<NgWebElement> Widgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (Widgets.Count > 0)
                        {
                            foreach (NgWebElement Widget in Widgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", Widget);
                                Driver.HighlightElement(Widget);
                                IWebElement ControlName = Widget.FindElement(ControlName_locator);
                                if (ControlName.Text == "Digitisation request notification email address")
                                {
                                    string prev_emailtext = null;
                                    IWebElement EmailText = Widget.FindElement(MiscTextField_locator);
                                    prev_emailtext = EmailText.GetAttribute("value");
                                    Console.WriteLine("prev_emailtext = " + prev_emailtext);
                                    WaitFind.FindElem(EmailText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Digitisation request notification email address to Blank.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Updating Digitisation request notification email address to Blank Successful.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Digitisation request notification email address to Blank." + statusreturntext);
                                    }
                                    Klick.On(EmailText);
                                    Thread.Sleep(KortextGlobals.s);
                                    EmailText.SendKeys("admin@kortext.com");
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Digitisation request notification email address Successful." + "admin@kortext.com");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Digitisation request notification email address." + "admin@kortext.com");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Digitisation request notification email address." + "admin@kortext.com" + "." + statusreturntext);
                                    }

                                    //Reverting the value to previous value
                                    WaitFind.FindElem(EmailText, 10).Clear();
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Incorrect Status Message while Updating Digitisation request notification email address to Blank.");
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Updating Digitisation request notification email address to Blank Successful.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Digitisation request notification email address to Blank." + statusreturntext);
                                    }
                                    Klick.On(EmailText);
                                    Thread.Sleep(KortextGlobals.s);
                                    EmailText.SendKeys(prev_emailtext);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext == "Preference updated")
                                    {
                                        Console.WriteLine("Updating Digitisation request notification email address Successful." + prev_emailtext);
                                    }
                                    else if (statusreturntext == "Could not update preference")
                                    {
                                        Console.WriteLine("Error while updating Digitisation request notification email address." + prev_emailtext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while updating Digitisation request notification email address." + prev_emailtext + "." + statusreturntext);
                                    }
                                    Console.WriteLine("Digitisation request notification email address Completed.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Widgets loaded. Error loading the page.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Error loading Settings Page");
            }
        }

        public void ListTemplateSettings()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            string TemplateCheckListURL = null;
            
            List<NgWebElement> SettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SettingTypes.Count > 0)
            {
                foreach (IWebElement SettingType in SettingTypes)
                {
                    Driver.HighlightElement(SettingType);
                    IWebElement SettingName = SettingType.FindElement(SettingName_locator);
                    if (SettingName.Text == "List templates")
                    {
                        Klick.On(SettingName);
                        TemplateName = SearchAndReturnNewTemplateName("Template");

                        List<NgWebElement> tabsets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in tabset.tabs")));
                        if (tabsets.Count > 0)
                        {
                            foreach (NgWebElement tabset in tabsets)
                            {
                                if (tabset.Displayed == true)
                                {
                                    IWebElement TemplateNameTextField = tabset.FindElement(TemplateNameTextField_locator);
                                    WaitFind.FindElem(TemplateNameTextField, 10).Clear();
                                    Klick.On(TemplateNameTextField);
                                    Thread.Sleep(KortextGlobals.s);
                                    TemplateNameTextField.SendKeys(TemplateName);
                                    Thread.Sleep(KortextGlobals.s);
                                    IWebElement AddTemplateButton = tabset.FindElement(AddTemplateButton_locator);
                                    Klick.On(AddTemplateButton);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext != "Template created")
                                    {
                                        Console.WriteLine("Error while creating Template. " + TemplateName + "." + statusreturntext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Template Creation Successful. " + TemplateName);
                                    }

                                    List<NgWebElement> TemplatesSearched = new List<NgWebElement>(tabset.FindElements(NgBy.Repeater("node in templateCtrl.TemplateService.templateList")));
                                    if (TemplatesSearched.Count > 0)
                                    {
                                        foreach (IWebElement TemplateSearched in TemplatesSearched)
                                        {
                                            Driver.HighlightElement(TemplateSearched);
                                            IWebElement TemplateTitle = TemplateSearched.FindElement(TemplateTitle_locator);
                                            if (TemplateTitle.Text == TemplateName)
                                            {
                                                Console.WriteLine("Newly Created Template found");
                                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", TemplateSearched);
                                                break;
                                            }
                                        }
                                    }

                                    //Working on Newly created template
                                    List<NgWebElement> AddingSectionsTemplatesSearched = new List<NgWebElement>(tabset.FindElements(NgBy.Repeater("node in templateCtrl.TemplateService.templateList")));
                                    if (AddingSectionsTemplatesSearched.Count > 0)
                                    {
                                        foreach (IWebElement AddingSectionsTemplateSearched in AddingSectionsTemplatesSearched)
                                        {
                                            Driver.HighlightElement(AddingSectionsTemplateSearched);
                                            IWebElement AddingSectionsTemplateTitle = AddingSectionsTemplateSearched.FindElement(TemplateTitle_locator);
                                            if (AddingSectionsTemplateTitle.Text == TemplateName)
                                            {
                                                IWebElement AddingSectionsEditTemplateButton = AddingSectionsTemplateSearched.FindElement(TemplateEditButton_locator);
                                                Klick.On(AddingSectionsEditTemplateButton);
                                                Thread.Sleep(KortextGlobals.s);
                                                IWebElement AddingSectionsAddSectionButton = AddingSectionsTemplateSearched.FindElement(SectionAddButton_locator);
                                                Klick.On(AddingSectionsAddSectionButton);
                                                Thread.Sleep(KortextGlobals.s);
                                                IWebElement AddingSectionsSectionClickButon = AddingSectionsTemplateSearched.FindElement(SectionClickButton_locator);
                                                Klick.On(AddingSectionsSectionClickButon);
                                                Thread.Sleep(KortextGlobals.s);

                                                WaitFind.FindElem(SectionTitleTextField, 10).Clear();
                                                Klick.On(SectionTitleTextField);
                                                Thread.Sleep(KortextGlobals.s);
                                                SectionTitleTextField.SendKeys("Section1");
                                                Thread.Sleep(KortextGlobals.s);
                                                Klick.On(AddingSectionFinishButton);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext != "Section created")
                                                {
                                                    Console.WriteLine("Error while creating Section. " + "Section1" + "." + statusreturntext);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Section Creation Successful. " + "Section1");
                                                }

                                                List<NgWebElement> AddingSectionsSearched = new List<NgWebElement>(tabset.FindElements(NgBy.Repeater("node in node.model")));
                                                if (AddingSectionsSearched.Count > 0)
                                                {
                                                    foreach (NgWebElement AddingSectionSearched in AddingSectionsSearched)
                                                    {
                                                        Driver.HighlightElement(AddingSectionSearched);
                                                        IWebElement AddingSubSectionSectionTitle = AddingSectionSearched.FindElement(TemplateTitle_locator);
                                                        if (AddingSubSectionSectionTitle.Text == "Section1")
                                                        {
                                                            IWebElement AddingSubSectionsEditSectionButton = AddingSectionSearched.FindElement(TemplateEditButton_locator);
                                                            Klick.On(AddingSubSectionsEditSectionButton);
                                                            Thread.Sleep(KortextGlobals.s);
                                                            IWebElement AddingSubSectionsAddSubSectionButton = AddingSectionSearched.FindElement(SubSectionAddButton_locator);
                                                            Klick.On(AddingSubSectionsAddSubSectionButton);
                                                            Thread.Sleep(KortextGlobals.s);
                                                            IWebElement AddingSubSectionsSubSectionClickButon = AddingSectionSearched.FindElement(SectionClickButton_locator);
                                                            Klick.On(AddingSubSectionsSubSectionClickButon);
                                                            Thread.Sleep(KortextGlobals.s);

                                                            WaitFind.FindElem(SubSectionTitleTextField, 10).Clear();
                                                            Klick.On(SubSectionTitleTextField);
                                                            Thread.Sleep(KortextGlobals.s);
                                                            SubSectionTitleTextField.SendKeys("SubSection1");
                                                            Thread.Sleep(KortextGlobals.s);
                                                            Klick.On(AddingSubSectionFinishButton);
                                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                            if (statusreturntext != "Template updated")
                                                            {
                                                                Console.WriteLine("Error while adding SubSection. " + "SubSection1" + "." + statusreturntext);
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("SubSection added Successful. " + "SubSection1");
                                                            }

                                                            IWebElement ExpandArrow = AddingSectionSearched.FindElement(By.CssSelector("a[uib-tooltip = 'Expand / collapse']"));
                                                            if (ExpandArrow.Text != "keyboard_arrow_down")
                                                            {
                                                                Klick.On(ExpandArrow);
                                                                Thread.Sleep(KortextGlobals.s);
                                                            }

                                                            List<NgWebElement> Subsections = new List<NgWebElement>(AddingSectionSearched.FindElements(NgBy.Repeater("node in node.model")));
                                                            if (Subsections.Count > 0)
                                                            {
                                                                foreach (IWebElement Subsection in Subsections)
                                                                {
                                                                    Driver.HighlightElement(Subsection);
                                                                    IWebElement SubsectionName = Subsection.FindElement(TemplateTitle_locator);
                                                                    if (SubsectionName.Text == "SubSection1")
                                                                    {
                                                                        Console.WriteLine("SubSection Completed. " + "SubSection1");
                                                                        break;
                                                                    }
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            try
            {
                verifyCourseCodeEnabled();
                Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
                TemplateCheckListURL = Driver.Instance.Url;
                //Driver.Instance.Url = "https://kortext.rebuslist.com/#/list/457";
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                Pages.TraverseBufferPage.EditThisList();
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(EditBufferNewTemplateList).SelectByText(TemplateName);
                Klick.On(EditBufferUndoButton);
                Thread.Sleep(KortextGlobals.s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while verifying the New Template Created in a List." + e.Message);
            }

            //Deleting the Subsection,Section and Template
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> SecondRoundSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (SecondRoundSettingTypes.Count > 0)
            {
                foreach (IWebElement SecondRoundSettingType in SecondRoundSettingTypes)
                {
                    Driver.HighlightElement(SecondRoundSettingType);
                    IWebElement SecondRoundSettingName = SecondRoundSettingType.FindElement(SettingName_locator);
                    if (SecondRoundSettingName.Text == "List templates")
                    {
                        Klick.On(SecondRoundSettingName);

                        List<NgWebElement> SecondRoundtabsets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in tabset.tabs")));
                        if (SecondRoundtabsets.Count > 0)
                        {
                            foreach (NgWebElement SecondRoundtabset in SecondRoundtabsets)
                            {
                                if (SecondRoundtabset.Displayed == true)
                                {
                                    List<NgWebElement> SecondRoundTemplatesSearched = new List<NgWebElement>(SecondRoundtabset.FindElements(NgBy.Repeater("node in templateCtrl.TemplateService.templateList")));
                                    if (SecondRoundTemplatesSearched.Count > 0)
                                    {
                                        foreach (NgWebElement SecondRoundTemplateSearched in SecondRoundTemplatesSearched)
                                        {
                                            Driver.HighlightElement(SecondRoundTemplateSearched);
                                            IWebElement SecondRoundTemplateTitle = SecondRoundTemplateSearched.FindElement(TemplateTitle_locator);
                                            string SecondRoundTemplateName = SecondRoundTemplateTitle.Text;
                                            if (SecondRoundTemplateTitle.Text == TemplateName)
                                            {
                                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SecondRoundTemplateSearched);
                                                IWebElement TemplateExpandButton = SecondRoundTemplateSearched.FindElement(By.CssSelector("a[uib-tooltip = 'Expand / collapse']"));
                                                if (TemplateExpandButton.Text != "keyboard_arrow_down")
                                                {
                                                    Klick.On(TemplateExpandButton);
                                                    Thread.Sleep(KortextGlobals.s);
                                                }
                                                List<NgWebElement> SecondRoundSections = new List<NgWebElement>(SecondRoundTemplateSearched.FindElements(NgBy.Repeater("node in node.model")));
                                                if (SecondRoundSections.Count > 0)
                                                {
                                                    foreach (NgWebElement SecondRoundSection in SecondRoundSections)
                                                    {
                                                        Driver.HighlightElement(SecondRoundSection);
                                                        IWebElement SectionTitle = SecondRoundSection.FindElement(TemplateTitle_locator);
                                                        string SectionTitleName = SectionTitle.Text;
                                                        IWebElement SectionExpandButton = SecondRoundSection.FindElement(By.CssSelector("a[uib-tooltip = 'Expand / collapse']"));
                                                        if (SectionExpandButton.Text != "keyboard_arrow_down")
                                                        {
                                                            Klick.On(SectionExpandButton);
                                                            Thread.Sleep(KortextGlobals.s);
                                                        }
                                                        //Deleting Subsections
                                                        List<NgWebElement> SecondRoundSubSections = new List<NgWebElement>(SecondRoundSection.FindElements(NgBy.Repeater("node in node.model")));
                                                        if (SecondRoundSubSections.Count > 0)
                                                        {
                                                            foreach (IWebElement SecondRoundSubSection in SecondRoundSubSections)
                                                            {
                                                                Driver.HighlightElement(SecondRoundSubSection);
                                                                IWebElement SubsectionTitle = SecondRoundSubSection.FindElement(TemplateTitle_locator);
                                                                string SubsectionTitleName = SubsectionTitle.Text;
                                                                IWebElement SubSectionDeleteButton = SecondRoundSubSection.FindElement(DeleteButton_locator);
                                                                Klick.On(SubSectionDeleteButton);
                                                                IWebElement ConfirmDeleteButton = SecondRoundSubSection.FindElement(ConfirmDeleteButton_locator);
                                                                Klick.On(ConfirmDeleteButton);
                                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                                if (statusreturntext != "Subsection deleted")
                                                                {
                                                                    Console.WriteLine("Error while Deleting SubSection." + SubsectionTitleName + "." + statusreturntext);
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("SubSection Deleted Successful." + SubsectionTitleName);
                                                                }
                                                            }
                                                        }

                                                        //Deleting Sections
                                                        IWebElement SectionDeleteButton = SecondRoundSection.FindElement(DeleteButton_locator);
                                                        Klick.On(SectionDeleteButton);
                                                        IWebElement SectionConfirmDeleteButton = SecondRoundSection.FindElement(ConfirmDeleteButton_locator);
                                                        Klick.On(SectionConfirmDeleteButton);
                                                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                        if (statusreturntext != "Section deleted")
                                                        {
                                                            Console.WriteLine("Error while Deleting Section." + SectionTitleName + "." + statusreturntext);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Section Deleted Successful." + SectionTitleName);
                                                        }
                                                    }
                                                }

                                                //Deleting Template
                                                IWebElement TemplateDeleteButton = SecondRoundTemplateSearched.FindElement(DeleteButton_locator);
                                                Klick.On(TemplateDeleteButton);
                                                IWebElement TemplateConfirmDeleteButton = SecondRoundTemplateSearched.FindElement(ConfirmDeleteButton_locator);
                                                Klick.On(TemplateConfirmDeleteButton);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext != "Template deleted")
                                                {
                                                    Console.WriteLine("Error while Deleting Template." + SecondRoundTemplateName + "." + statusreturntext);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Template Deleted Successful." + SecondRoundTemplateName);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            //Checking if the Template is shown in the List after deleting it
            try
            {
                verifyCourseCodeEnabled();
                //Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
                Driver.Instance.Url = TemplateCheckListURL;
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                Pages.TraverseBufferPage.EditThisList();
                Thread.Sleep(KortextGlobals.s);
                new SelectElement(EditBufferNewTemplateList).SelectByText(TemplateName);
                Klick.On(EditBufferUndoButton);
                Console.WriteLine("Template shown in the List after Delete. Not expected.");
                Thread.Sleep(KortextGlobals.s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Template Not shown after Delete. As expected." + e.Message);
            }
        }

        private string SearchAndReturnNewTemplateName(string username)
        {
            //search for TestUser and increment suffix until you find one that hasn't been created yet.
            //Return that user name to be added.
            usernameappend = 1;
            for (int i = 0; i < i + 1; i++)
            {
                int found_flag = 0;
                UserNameText = username + usernameappend;
                try
                {
                    List<NgWebElement> tabsets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in tabset.tabs")));
                    if (tabsets.Count > 0)
                    {
                        foreach (NgWebElement tabset in tabsets)
                        {
                            if (tabset.Displayed == true)
                            {
                                List<NgWebElement> TemplatesSearched = new List<NgWebElement>(tabset.FindElements(NgBy.Repeater("node in templateCtrl.TemplateService.templateList")));
                                if (TemplatesSearched.Count > 0)
                                {
                                    foreach (IWebElement TemplateSearched in TemplatesSearched)
                                    {
                                        Driver.HighlightElement(TemplateSearched);
                                        IWebElement TemplateTitle = TemplateSearched.FindElement(TemplateTitle_locator);
                                        if (TemplateTitle.Text == UserNameText)
                                        {
                                            usernameappend = usernameappend + 1;
                                            found_flag = 1;
                                            break;
                                        }
                                    }
                                    if (found_flag == 0)
                                    {
                                        Console.WriteLine("Template found." + UserNameText);
                                        return UserNameText;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Template found." + UserNameText);
                                    return UserNameText;
                                }
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Search -" + UserNameText + " Template Not Found; Using this User Type." + e.Message);
                    return UserNameText;
                }
            }
            return UserNameText;
        }
        
        public bool verifyLikesDislikes()
        {
            if(ListURL == null)
            {
                verifyCourseCodeEnabled();
                Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
                ListURL = Driver.Instance.Url;
                Assert.IsTrue(Pages.PearlNewListAddDocs.AddDocs(), "Error while Adding Initial Documents to the Reading List.");
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);

                /*
                Driver.Instance.Url = "https://kortext.rebuslist.com/#/list/563";
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                ListURL = Driver.Instance.Url;
                */
            }
            else
            {
                Driver.Instance.Url = ListURL;
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
            }
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 600)");
            IList<IWebElement> LikesIcons = Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip='Rate up item']"));
            if(LikesIcons.Count > 0)
            {
                if(LikesIcons[0].Displayed == true)
                {
                    Console.WriteLine("Likes/Dislikes Buttons appear in the List Page.");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return true;
                }
                else
                {
                    Console.WriteLine("Likes/Dislikes Buttons do not appear in the List Page.");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Likes/Dislikes Buttons do not appear in the List Page.");
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                return false;
            }
        }

        public bool verifyCourseCodeDisplay()
        {
            if (ListURL == null)
            {
                verifyCourseCodeEnabled();
                Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
                ListURL = Driver.Instance.Url;
                Assert.IsTrue(Pages.PearlNewListAddDocs.AddDocs(), "Error while Adding Initial Documents to the Reading List.");
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);

                /*
                Driver.Instance.Url = "https://kortext.rebuslist.com/#/list/563";
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                ListURL = Driver.Instance.Url;
                */
            }
            else
            {
                Driver.Instance.Url = ListURL;
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
            }
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 600)");
            IList<IWebElement> CourseCodeDisplayed = Driver.Instance.FindElements(By.CssSelector("span[list = '::ctrl.model']"));
            if (CourseCodeDisplayed.Count > 0)
            {
                if(CourseCodeDisplayed[0].Displayed == true)
                {
                    Console.WriteLine("Course Code displayed in the List Page.");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return true;
                }
                else
                {
                    Console.WriteLine("Course Code not displayed in the List Page.");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Course Code not displayed in the List Page.");
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                return false;
            }
        }
        
        public bool verifyListYearDisplay()
        {
            if (ListURL == null)
            {
                verifyCourseCodeEnabled();
                Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
                ListURL = Driver.Instance.Url;
                Assert.IsTrue(Pages.PearlNewListAddDocs.AddDocs(), "Error while Adding Initial Documents to the Reading List.");
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);

                /*                        
                Driver.Instance.Url = "https://kortext.rebuslist.com/#/list/563";
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                ListURL = Driver.Instance.Url;
                */
            }
            else
            {
                Driver.Instance.Url = ListURL;
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
            }
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 600)");
            IList<IWebElement> ListYearDisplayed = Driver.Instance.FindElements(By.CssSelector("list-year[year = 'ctrl.model.year']"));
            if (ListYearDisplayed.Count > 0)
            {
                if(ListYearDisplayed[0].Displayed == true)
                {
                    Console.WriteLine("List Year displayed in the List Page.");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return true;
                }
                else
                {
                    Console.WriteLine("List Year not displayed in the List Page.");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("List Year not displayed in the List Page.");
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                return false;
            }
        }

        public string convertYearFormatList(string tobeconverted)
        {
            string convertedstring = tobeconverted;
            for(int i=0; i < 11;i++)
            {
                if(i==0)
                {
                    convertedstring = convertedstring.Replace("0", "y");
                }
                else if (i == 1)
                {
                    convertedstring = convertedstring.Replace("1", "y");
                }
                else if (i == 2)
                {
                    convertedstring = convertedstring.Replace("2", "y");
                }
                else if (i == 3)
                {
                    convertedstring = convertedstring.Replace("3", "y");
                }
                else if (i == 4)
                {
                    convertedstring = convertedstring.Replace("4", "y");
                }
                else if (i == 5)
                {
                    convertedstring = convertedstring.Replace("5", "y");
                }
                else if (i == 6)
                {
                    convertedstring = convertedstring.Replace("6", "y");
                }
                else if (i == 7)
                {
                    convertedstring = convertedstring.Replace("7", "y");
                }
                else if (i == 8)
                {
                    convertedstring = convertedstring.Replace("8", "y");
                }
                else if (i == 9)
                {
                    convertedstring = convertedstring.Replace("9", "y");
                }
                else if(i==10)
                {
                    convertedstring = convertedstring.Replace("/", "-");
                }
            }
            return convertedstring;
        }

        public bool verifyFormatYearDisplay(string stringtocompare)
        {
            if (ListURL == null)
            {
                verifyCourseCodeEnabled();
                Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
                ListURL = Driver.Instance.Url;
                Assert.IsTrue(Pages.PearlNewListAddDocs.AddDocs(), "Error while Adding Initial Documents to the Reading List.");
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);

                /*
                Driver.Instance.Url = "https://kortext.rebuslist.com/#/list/563";
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                ListURL = Driver.Instance.Url;
                */
            }
            else
            {
                Driver.Instance.Url = ListURL;
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
            }
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 600)");
            IList<IWebElement> YearformatDisplayed = Driver.Instance.FindElements(By.CssSelector("list-year[year = 'ctrl.model.year']"));
            string Yearformatstring = null;
            if (YearformatDisplayed.Count > 0)
            {
                foreach(IWebElement YearformatDisplay in YearformatDisplayed)
                {
                    Yearformatstring = YearformatDisplay.Text.Replace("(", "");
                    Yearformatstring = Yearformatstring.Replace(")", "");
                    string ActualYearFormat = convertYearFormatList(Yearformatstring);

                    if(ActualYearFormat == stringtocompare)
                    {
                        Console.WriteLine("Year Display Format is good in the List Page.");
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Year Display Format is not good in the List Page.");
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                        return false;
                    }
                }
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                return false;
            }
            else
            {
                Console.WriteLine("Year is not displayed in the List Page.");
                return false;
            }
        }

        public bool verifyActualAllowedMaterials(string typetocheck)
        {
            if (ListURL == null)
            {
                verifyCourseCodeEnabled();
                Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
                ListURL = Driver.Instance.Url;
                Assert.IsTrue(Pages.PearlNewListAddDocs.AddDocs(), "Error while Adding Initial Documents to the Reading List.");
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);

                /*
                Driver.Instance.Url = "https://kortext.rebuslist.com/#/list/563";
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                ListURL = Driver.Instance.Url;
                */
            }
            else
            {
                Driver.Instance.Url = ListURL;
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
            }
            Pages.TraverseBufferPage.EditThisList();
            Thread.Sleep(KortextGlobals.l);

            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 500)");

            IList<IWebElement> AddMaterialsButton = Driver.Instance.FindElements(By.CssSelector("button[ng-click = 'actionsCtrl.addNewItem()']"));
            if(AddMaterialsButton.Count > 0)
            {
                Klick.On(AddMaterialsButton[0]);
                Thread.Sleep(KortextGlobals.l);
                IWebElement ManualAddButton = Driver.Instance.FindElement(By.CssSelector("button[ng-click = 'addModal.addEmpty()']"));
                Klick.On(ManualAddButton);

                IWebElement MaterialType = Driver.Instance.FindElement(By.CssSelector("select[ng-model = 'adminMaterialController.currentMaterial.metadata.type']"));
                try
                {
                    new SelectElement(MaterialType).SelectByValue("string:"+typetocheck);
                    Console.WriteLine("Material Type shown for a list.");
                    IWebElement CancelButton = Driver.Instance.FindElement(By.CssSelector("button[ng-click = 'adminMaterialController.cancel()']"));
                    Klick.On(CancelButton);
                    IWebElement CloseButton = Driver.Instance.FindElement(By.CssSelector("button[ng-click = 'addModal.cancel()']"));
                    Klick.On(CloseButton);
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception while verifying the Material Type shown for a List." + e.Message);
                    IWebElement CancelButton = Driver.Instance.FindElement(By.CssSelector("button[ng-click = 'adminMaterialController.cancel()']"));
                    Klick.On(CancelButton);
                    IWebElement CloseButton = Driver.Instance.FindElement(By.CssSelector("button[ng-click = 'addModal.cancel()']"));
                    Klick.On(CloseButton);
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Add Citation Button not found in Buffer List Page.");
            }
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
            return false;
        }

        public void verifyCourseCodeEnabled()
        {
            Pages.LandingPage.ClickOnMenu_SettingsBtn();
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> CourseCodeSettingTypes = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("tab in adminGlobal.formTabs")));
            if (CourseCodeSettingTypes.Count > 0)
            {
                foreach (IWebElement CourseCodeSettingType in CourseCodeSettingTypes)
                {
                    Driver.HighlightElement(CourseCodeSettingType);
                    IWebElement CourseCodeSettingName = CourseCodeSettingType.FindElement(SettingName_locator);
                    if (CourseCodeSettingName.Text == "Miscellaneous settings")
                    {
                        Klick.On(CourseCodeSettingName);
                        Thread.Sleep(KortextGlobals.s);

                        //To check whether the Display List Year with List Title is switched to YES
                        List<NgWebElement> CourseCodeWidgets = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("widget in adminGlobal.GlobalService.config[tab.group] track by $index")));
                        if (CourseCodeWidgets.Count > 0)
                        {
                            foreach (NgWebElement CourseCodeWidget in CourseCodeWidgets)
                            {
                                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", CourseCodeWidget);
                                Driver.HighlightElement(CourseCodeWidget);
                                IWebElement CourseCodeControlName = CourseCodeWidget.FindElement(ControlName_locator);
                                if (CourseCodeControlName.Text == "Display course code with list title")
                                {
                                    List<NgWebElement> CourseCodeYesNoButtons = new List<NgWebElement>(CourseCodeWidget.FindElements(NgBy.Repeater("item in form.titleMap")));
                                    foreach (IWebElement CourseCodeYesNoButton in CourseCodeYesNoButtons)
                                    {
                                        if ((CourseCodeYesNoButton.GetAttribute("class") == "btn btn-default") && CourseCodeYesNoButton.Text == "YES")
                                        {
                                            Klick.On(CourseCodeYesNoButton);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext != "Preference updated")
                                            {
                                                Console.WriteLine("Error while updating Course Code with list title. " + CourseCodeYesNoButton.Text + "." + statusreturntext);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Course Code with list title update Successful to " + CourseCodeYesNoButton.Text);
                                            }
                                            if (CourseCodeYesNoButton.Text == "NO")
                                            {
                                                if (verifyCourseCodeDisplay() == false)
                                                {
                                                    Console.WriteLine("Course Code update to NO is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Course Code update to NO is not working as expected.");
                                                }
                                            }
                                            else if (CourseCodeYesNoButton.Text == "YES")
                                            {
                                                if (verifyCourseCodeDisplay() == true)
                                                {
                                                    Console.WriteLine("Course Code update to YES is working good.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Course Code update to YES is not working as expected.");
                                                }
                                            }
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }
        public bool verifyCitationsdisplayed()
        {
            if (ListURL == null)
            {
                verifyCourseCodeEnabled();
                Assert.IsTrue(Pages.PearlCreateReadingList.CreateList(), "Error while Creating Reading List");
                ListURL = Driver.Instance.Url;
                Assert.IsTrue(Pages.PearlNewListAddDocs.AddDocs(), "Error while Adding Initial Documents to the Reading List.");
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                
                /*
                Driver.Instance.Url = "https://kortext.rebuslist.com/#/list/563";
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
                ListURL = Driver.Instance.Url;
                */
            }
            else
            {
                Driver.Instance.Url = ListURL;
                Thread.Sleep(KortextGlobals.l);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.xl);
            }
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 600)");
            IList<IWebElement> ViewCitationButton = Driver.Instance.FindElements(By.CssSelector("button[uib-tooltip='View citation']"));
            if (ViewCitationButton.Count > 0)
            {
                if(ViewCitationButton[0].Displayed == true)
                {
                    Console.WriteLine("View Citation Buttons appear in the List Page.");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return true;
                }
                else
                {
                    Console.WriteLine("View Citation Buttons do not appear in the List Page.");
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("View Citation Buttons do not appear in the List Page.");
                ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
                return false;
            }
        }
    }
}



