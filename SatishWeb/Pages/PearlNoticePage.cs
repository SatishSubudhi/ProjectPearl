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
    public class PearlNoticesPage
    {
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'adminSysMess.openModal()']")]
        protected IWebElement CreateNoticeButton
        {
            get;
            set;
        }
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "messageModal.message.type")]
        protected IWebElement NoticeTypeList
        {
            get;
            set;
        }
        /*[FindsBy(How = How.CssSelector, Using = "select[ng-model = 'messageModal.message.type']")]
        protected IWebElement NoticeTypeList
        {
            get;
            set;
        }*/
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "messageModal.message.subject")]
        protected IWebElement NoticeSubjectTextField
        {
            get;
            set;
        }
        /*[FindsBy(How = How.CssSelector, Using = "select[ng-model = 'messageModal.message.subject']")]
        protected IWebElement NoticeSubjectTextField
        {
            get;
            set;
        }*/
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "messageModal.message.body")]
        protected IWebElement NoticeBodyTextField
        {
            get;
            set;
        }
        /*[FindsBy(How = How.CssSelector, Using = "select[ng-model = 'messageModal.message.body']")]
        protected IWebElement NoticeBodyTextField
        {
            get;
            set;
        }*/
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'messageModal.save()']")]
        protected IWebElement NoticeSaveButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'messageModal.cancel()']")]
        protected IWebElement NoticeCancelButton
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "notification-icon")]
        protected IWebElement NotificationIcon
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "system-messages-type-filter")]
        protected IWebElement ShowOnlyFilter
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "div[uib-tooltip = 'Alerts']")]
        protected IWebElement NotificationsAlertsButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "div[uib-tooltip = 'News']")]
        protected IWebElement NotificationsNewsButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "h3[ng-if = 'messageModal.message.sent']")]
        protected IWebElement ViewNoticeHeader
        {
            get;
            set;
        }

        By SearchField_locator = By.CssSelector("input[placeholder= 'Search...']");
        By ViewNotice_locator = By.CssSelector("button[ng-if= 'adminSysMess.displayRead(row)']");
        By SendNotice_locator = By.CssSelector("button[ng-click = 'adminSysMess.sendMessage(row)']");
        By NoticeIcon_locator = By.CssSelector("span[ng-show = 'typeCtrl.shouldDisplay']");
        By NoticeEdit_locator = By.CssSelector("button[ng-if = 'adminSysMess.displayUpdate(row)']");
        By NoticeName_locator = By.ClassName("col-md-10");
       
        By NoticeSelectPage_locator = By.CssSelector("a[ng-click = 'selectPage(page)']");

        string statusreturntext;
        string currentURL;
        string AlertName;
        string NewsName;
        string UserNameText;
        int usernameappend;

        public bool NoticesPage()
        {
            try
            {
                currentURL = Driver.Instance.Url;

                AlertName = SearchAndReturnNewNoticeName("HolidayAlert");
                NewsName = SearchAndReturnNewNoticeName("UniversityNews");

                //Create New Notice
                AddNewNotice("Alert", AlertName);

                AddNewNotice("News", NewsName);

                //Search for Notices
                SearchNotices(AlertName);

                SearchNotices(NewsName);

                //Update Notice
                UpdateNotice(AlertName, "Body", "1234567890!@#$%^&*()?<>,.:;ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                UpdateNotice(AlertName, "Subject", "News" + AlertName);
                UpdateNotice("News" + AlertName, "Subject", AlertName);

                UpdateNotice(NewsName, "Body", "1234567890!@#$%^&*()?<>,.:;ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                UpdateNotice(NewsName, "Subject", "News" + NewsName);
                UpdateNotice("News" + NewsName, "Subject", NewsName);

                //Send Notice
                SendNotice(AlertName);

                SendNotice(NewsName);

                //View Notice
                ViewNotice(AlertName);

                ViewNotice(NewsName);

                //Check Notice / Notifications
                /* Notifications not working as expected. Will work on this function after the defect is fixed.
                CheckNotice(AlertName);
                CheckNotice(NewsName);
                */
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in NoticesPage.cs: " + e.Message);
                return false;
            }
        }

        private string SearchAndReturnNewNoticeName(string username)
        {
            //search for TestUser and increment suffix until you find one that hasn't been created yet.
            //Return that user name to be added.
            usernameappend = 1;
            for (int i = 0; i < i + 1; i++)
            {
                int found_flag = 0;
                UserNameText = username + usernameappend;
                Thread.Sleep(KortextGlobals.s);
                IList<IWebElement> SearchNoticeField = Driver.Instance.FindElements(SearchField_locator);
                if(SearchNoticeField.Count > 0)
                {
                    WaitFind.FindElem(SearchNoticeField[1], 10).Clear();
                    Klick.On(SearchNoticeField[1]);
                    Thread.Sleep(KortextGlobals.s);
                    SearchNoticeField[1].SendKeys(UserNameText);
                    Thread.Sleep(KortextGlobals.s);
                    try
                    {
                        List<NgWebElement> NoticesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSysMess.displayedCollection")));
                        if (NoticesSearched.Count > 0)
                        {
                            foreach (IWebElement NoticeSearched in NoticesSearched)
                            {
                                Driver.HighlightElement(NoticeSearched);
                                IWebElement NoticeName = NoticeSearched.FindElement(NoticeName_locator);
                                if (NoticeName.Text == UserNameText)
                                {
                                    found_flag = 1;
                                    usernameappend = usernameappend + 1;
                                    break;
                                }
                            }
                            if (found_flag == 0)
                            {
                                Console.WriteLine("Notice Name found." + UserNameText);
                                return UserNameText;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Notice Name found." + UserNameText);
                            return UserNameText;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Search -" + UserNameText + " Notice Not Found; Using this Notice Name " + e.Message);
                        return UserNameText;
                    }
                }
                else
                {
                    Console.WriteLine("Could not find the Search Field in SearchName function.");
                }
            }
            return UserNameText;
        }

        public void AddNewNotice(string noticetype, string searchtext)
        {
            Thread.Sleep(KortextGlobals.s);
            Klick.On(CreateNoticeButton);
            Thread.Sleep(KortextGlobals.s);
            if(noticetype == "Alert")
            {
                new SelectElement(NoticeTypeList).SelectByIndex(1);
            }
            else if(noticetype == "News")
            {
                new SelectElement(NoticeTypeList).SelectByIndex(2);
            }
            else
            {
                Console.WriteLine("Incorrect Notice Type Passed. Selecting NEWS to continue.");
                new SelectElement(NoticeTypeList).SelectByIndex(2);
            }
            
            WaitFind.FindElem(NoticeSubjectTextField, 10).Clear();
            Klick.On(NoticeSubjectTextField);
            Thread.Sleep(KortextGlobals.s);
            NoticeSubjectTextField.SendKeys(searchtext);
            Thread.Sleep(KortextGlobals.s);

            WaitFind.FindElem(NoticeBodyTextField, 10).Clear();
            Klick.On(NoticeBodyTextField);
            Thread.Sleep(KortextGlobals.s);
            NoticeBodyTextField.SendKeys("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()?<>,.:;");
            Thread.Sleep(KortextGlobals.s);

            Klick.On(NoticeSaveButton);
            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "Message added")
            {
                Console.WriteLine("Error while Creating New Notice." + statusreturntext);
            }
            else
            {
                Console.WriteLine("Creating Notice Successful");
            }
            Thread.Sleep(KortextGlobals.s);
            Console.WriteLine("Searching Notices after Creation.");
            SearchNotices(searchtext);
        }
        public void SearchNotices(string searchtext)
        {
            int i = 0;
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            IList<IWebElement> SearchNoticeField = Driver.Instance.FindElements(SearchField_locator);
            if (SearchNoticeField.Count > 0)
            {
                WaitFind.FindElem(SearchNoticeField[1], 10).Clear();
                Klick.On(SearchNoticeField[1]);
                Thread.Sleep(KortextGlobals.s);
                SearchNoticeField[1].SendKeys(searchtext);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> NoticesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSysMess.displayedCollection")));
                if (NoticesSearched.Count > 0)
                {
                    foreach (IWebElement NoticeSearched in NoticesSearched)
                    {
                        IWebElement NoticeName = NoticeSearched.FindElement(NoticeName_locator);
                        if (NoticeName.Text == searchtext)
                        {
                            Console.WriteLine(searchtext + " Notice found");
                            i = 1;
                            break;
                        }
                    }
                    if (i == 0)
                    {
                        Console.WriteLine(searchtext + " Notice not found");
                    }
                }
                else
                {
                    Console.WriteLine(searchtext + " Notice not found");
                }
            }
            else
            {
                Console.WriteLine("Could not find the Search Field in Search function.");
            }
        }
        public void UpdateNotice(string noticename, string noticefield, string newnoticetext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            IList<IWebElement> SearchNoticeField = Driver.Instance.FindElements(SearchField_locator);
            if (SearchNoticeField.Count > 0)
            {
                WaitFind.FindElem(SearchNoticeField[1], 10).Clear();
                Klick.On(SearchNoticeField[1]);
                Thread.Sleep(KortextGlobals.s);
                SearchNoticeField[1].SendKeys(noticename);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> NoticesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSysMess.displayedCollection")));
                if (NoticesSearched.Count > 0)
                {
                    foreach (IWebElement NoticeSearched in NoticesSearched)
                    {
                        IWebElement NoticeNameField = NoticeSearched.FindElement(NoticeName_locator);
                        if (NoticeNameField.Text == noticename)
                        {
                            Driver.HighlightElement(NoticeSearched);
                            IWebElement NoticeEditButton = NoticeSearched.FindElement(NoticeEdit_locator);
                            Klick.On(NoticeEditButton);
                            if (noticefield == "Subject")
                            {
                                WaitFind.FindElem(NoticeSubjectTextField, 10).Clear();
                                Klick.On(NoticeSubjectTextField);
                                Thread.Sleep(KortextGlobals.s);
                                NoticeSubjectTextField.SendKeys(newnoticetext);
                            }
                            else if (noticefield == "Body")
                            {
                                WaitFind.FindElem(NoticeBodyTextField, 10).Clear();
                                Klick.On(NoticeBodyTextField);
                                Thread.Sleep(KortextGlobals.s);
                                NoticeBodyTextField.SendKeys(newnoticetext);
                            }
                            else
                            {
                                Console.WriteLine("Incorrect Notice Field provided." + noticefield);
                                break;
                            }
                            Klick.On(NoticeSaveButton);
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext == "Message updated")
                            {
                                Console.WriteLine("Notice Update Successful for " + noticename + "," + noticefield);
                            }
                            else if (statusreturntext == "Could not update tag")
                            {
                                Console.WriteLine("Error while updating Notice for " + noticename + "," + noticefield + "." + statusreturntext);
                            }
                            if (noticefield == "Subject")
                            {
                                SearchNotices(newnoticetext);
                            }
                            else if (noticefield == "Body")
                            {
                                SearchNotices(noticename);
                            }
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(noticename + " Notice not found to be updated");
                }
            }
            else
            {
                Console.WriteLine("Could not find the Search Field in Update function.");
            }
        }
        public void SendNotice(string searchtext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            IList<IWebElement> SearchNoticeField = Driver.Instance.FindElements(SearchField_locator);
            if (SearchNoticeField.Count > 0)
            {
                WaitFind.FindElem(SearchNoticeField[1], 10).Clear();
                Klick.On(SearchNoticeField[1]);
                Thread.Sleep(KortextGlobals.s);
                SearchNoticeField[1].SendKeys(searchtext);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> NoticesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSysMess.displayedCollection")));
                if (NoticesSearched.Count > 0)
                {
                    foreach (IWebElement NoticeSearched in NoticesSearched)
                    {
                        IWebElement NoticeNameField = NoticeSearched.FindElement(NoticeName_locator);
                        if (NoticeNameField.Text == searchtext)
                        {
                            IWebElement NoticeSendButton = NoticeSearched.FindElement(SendNotice_locator);
                            Klick.On(NoticeSendButton);
                            Console.WriteLine("Sending Notice Successful." + searchtext);
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine(searchtext + " Notice not found to be Sent");
                }
                Console.WriteLine("Searching Notice after Sending");
                SearchNotices(searchtext);
            }
            else
            {
                Console.WriteLine("Could not find the Search Field in Send Notice function.");
            }
        }
        public void ViewNotice(string searchtext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            IList<IWebElement> SearchNoticeField = Driver.Instance.FindElements(SearchField_locator);
            if (SearchNoticeField.Count > 0)
            {
                WaitFind.FindElem(SearchNoticeField[1], 10).Clear();
                Klick.On(SearchNoticeField[1]);
                Thread.Sleep(KortextGlobals.s);
                SearchNoticeField[1].SendKeys(searchtext);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> NoticesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("row in adminSysMess.displayedCollection")));
                if (NoticesSearched.Count > 0)
                {
                    foreach (IWebElement NoticeSearched in NoticesSearched)
                    {
                        IWebElement NoticeNameField = NoticeSearched.FindElement(NoticeName_locator);
                        if (NoticeNameField.Text == searchtext)
                        {
                            IWebElement NoticeViewButton = NoticeSearched.FindElement(ViewNotice_locator);
                            Klick.On(NoticeViewButton);
                            if (ViewNoticeHeader.Displayed)
                            {
                                Console.WriteLine("Viewing Notice Successful." + searchtext);
                                Klick.On(NoticeCancelButton);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Error while Viewing Notice." + searchtext);
                                Driver.Instance.Navigate().Refresh();
                                Thread.Sleep(KortextGlobals.l);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(searchtext + " Notice not found to be Viewed");
                }
            }
            else
            {
                Console.WriteLine("Could not find the Search Field in View Notice function.");
            }
        }
    }
}



