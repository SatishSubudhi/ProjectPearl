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
using Protractor;

namespace PearlFramework
{
    public class PearlUserPage
    {
        //  By originalEmal = By.td

        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip ='Add a new user']")]

        //   [FindsBy(How = How.XPath, Using = "//div/div/div/div/div/button")]
        protected IWebElement rnd_add_user_btn
        {
            get; set;
        }

        [FindsBy(How = How.Id, Using = "user-search")]
        protected IWebElement user_search_input_field
        {
            get; set;
        }


        [FindsBy(How = How.ClassName, Using = "user-search-results")]
        protected IWebElement user_table
        { get; set; }
        /**********************************************
         *   User ADd and edit popup modal variables.
         *   
         *   *******************************************/
        [FindsBy(How = How.Id, Using = "name")]
        // [FindsBy(How = How.XPath, Using = "//form[@id='userForm']/div/input")]
        protected IWebElement full_name_input
        {
            get; set;
        }
        [FindsBy(How = How.Id, Using = "login")]
        //   [FindsBy(How = How.XPath, Using = "//form[@id='userForm']/div[2]/input")]
        protected IWebElement login_name_input
        {
            get; set;
        }
        [FindsBy(How = How.Id, Using = "email")]
        //        [FindsBy(How = How.XPath, Using = "//form[@id='userForm']/div[3]/input")]
        protected IWebElement email_input
        {
            get; set;
        }


        //   [FindsBy(How = How.XPath, Using = "//form[@id='userForm']/div[4]/select")]

        [FindsBy(How = How.CssSelector, Using = "select[ng-model='userModal.user.usertype']")]
        protected IWebElement UserTypeDropDown
        {
            get; set;
        }
        //    For both edit and Add popup user modals.
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'userModal.ok()']")]
        protected IWebElement UserFinishBtn
        {
            get; set;
        }

        //for Edit user popup modal only.
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'userModal.resetPassword()']")]
        protected IWebElement UserResetPasswordBtn
        {
            get; set;
        }
        //    For both edit and Add popup user modals.
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'userModal.close()']")]
        protected IWebElement CloseUserModalBtn
        {
            get; set;
        }



        /*******************************************
*              User Search Variables
*              
* *********************************************/
        [FindsBy(How = How.ClassName, Using = "form-control-feedback")]
        protected IWebElement SearchResultsCount
        {
            get; set;
        }


        //Don't use these any more. You can now find the exact match of a username.
        //  [FindsBy(How = How.XPath, Using = "//td[4]/button[2]")]
        //  protected IWebElement FirstUserSearchResultEdit
        //  {
        //     get; set;
        //   }
        // [FindsBy(How = How.XPath, Using = "//td[4]/button")]
        //   protected IWebElement FirstUserSearchResultRoles
        //   {
        //       get; set;
        //   }

        //input field to add new lists to a user
        [FindsBy(How = How.Id, Using = "list-search")]
        protected IWebElement UserRolesListSearch
        {
            get; set;
        }

        By ListSearchResultOwner_locator = By.CssSelector("label[ng-repeat = 'nonPermissive in roleButtons.nonPermissive.buttons']");

        [FindsBy(How = How.XPath, Using = "//ul[@id='list-search-results']/li/div/div[2]/div/div/label")]
        protected IWebElement ListSearchResultOwner
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "//ul[@id='list-search-results']/li/div/div[2]/div/div/label[2]")]
        protected IWebElement ListSearchResultLeader
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "//ul[@id='list-search-results']/li/div/div[2]/div/div[2]/label")]
        protected IWebElement ListSearchResultAuthor
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "//ul[@id='list-search-results']/li/div/div[2]/div/div[2]/label[2]")]
        protected IWebElement ListSearchResultModerator
        {
            get; set;
        }
        [FindsBy(How = How.XPath, Using = "//ul[@id='list-search-results']/li/div/div[2]/div/div[2]/label[3]")]
        protected IWebElement ListSearchResultEditor
        {
            get; set;
        }
        [FindsBy(How = How.CssSelector, Using = "button.btn.btn-success")]

        protected IWebElement ListAssignFinishButton
        {
            get; set;
        }


        //table containing the lists assigned to the current user being edited.
        [FindsBy(How = How.Id, Using = "explicit-body")]
        protected IWebElement ListsAssignedToUser
        {
            get; set;
        }

        string UserNameText;
        string currentURL;
        string statusreturntext;
        int usernameappend = 1;
        string origuserFullName;
        string origuserLoginName;
        string origuseremail;
        string origusertype = "";//place holder to store the original role of the user for validation. 

        //go to user page and validate you are there. Returns true if Users page appears
        public bool GoToUserPage()
        {
            return Pages.LandingPage.ClickOnMenu_UserBtn();

        }
        private void ClickOnPasswordResetBtn()
        {
            WaitFind.FindElem(UserResetPasswordBtn, 20);
            Klick.On(UserResetPasswordBtn);
        }
        private void ClickOnCloseBtn()
        {
            WaitFind.FindElem(CloseUserModalBtn, 20);
            Klick.On(CloseUserModalBtn);
        }

        private int CountOfUsersReturned()
        {
            WaitFind.FindElem(SearchResultsCount, 20);
            //   Console.WriteLine("count found " + Convert.ToInt16(SearchResultsCount.Text));
            return Convert.ToInt16(SearchResultsCount.Text);

        }

        private bool SelectUserType(String usertype)
        {
            SelectElement type = new SelectElement(UserTypeDropDown);

            switch (usertype)
            {
                case "LIBRARIAN":
                    type.SelectByText("LIBRARIAN");
                    break;
                case "ADMIN":
                    //  Klick.On(UserTypeDropDown);
                    //   Thread.Sleep(KortextGlobals.s);
                    type.SelectByText("ADMIN");
                    //   UserTypeDropDown.SendKeys(Keys.Enter);
                    break;

                case "PUBLIC":
                    //   Klick.On(UserTypeDropDown);
                    /// Thread.Sleep(KortextGlobals.s);
                    type.SelectByText("PUBLIC");
                    //  UserTypeDropDown.SendKeys("P");
                    //  Thread.Sleep(KortextGlobals.s);
                    //  UserTypeDropDown.SendKeys(Keys.Enter);
                    break;
                case "STAFF":
                    type.SelectByText("STAFF");
                    break;
             //   case "STUDENT":
               //     type.SelectByText("STUDENT");
                //    break;
                default:
                    Console.WriteLine("Invalid user type");
                    return false;

            }
            return true;
        }
        //Add a new user with username = TestUser#  If called without usertype or email, defaults to Librarian, admin@kortext.com
        public bool AddUserDefault(String usertype = "Librarian", String username = "ASusanUser", String email = "admin@kortext.com")
        {
            try
            {
                //check if at user page. If not, try going there again. If that fails - return false.
                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }
                //search for existing TestUser<#>  
                //If found, check to see if the next number higher exists.  Returns a new TestUser<#>
                string searchuserresult = SearchAndReturnNewUserName(username);
                Klick.On(rnd_add_user_btn);

                if (!FillInUserForm(searchuserresult, searchuserresult, email, usertype))
                {
                    Console.WriteLine("Error entering user info in form");
                    return false;
                }



                Thread.Sleep(KortextGlobals.s);


                //  Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
                //in Function validation
                if (UserExists(searchuserresult) == null)
                    return false;
                else
                {
                    Console.WriteLine("Successfully added user: " + searchuserresult);
                    return true;
                }

            }

            catch (Exception e)
            {
                Console.WriteLine("Exception in AddUser: " + e.Message);
                return false;
            }
        }
        private bool FillInUserForm(string userfullname, string userloginname, string email, string usertype)
        {
            WaitFind.FindElem(full_name_input, 10);
            full_name_input.SendKeys(userfullname);
            WaitFind.FindElem(login_name_input, 10);
            login_name_input.SendKeys(userloginname);
            WaitFind.FindElem(email_input, 10);
            email_input.SendKeys(email);
            if (!SelectUserType(usertype.ToUpper()))
            {
                Console.WriteLine("Unable to select usertype: " + usertype);
                return false;
            }
            Thread.Sleep(KortextGlobals.s);
            //   Driver.DumpCurrentDOM();
            Klick.On(UserFinishBtn);
         //   Console.WriteLine("After click on finish");
            return true;
        }

        public IWebElement UserExists(string username)
        {
            string foundname;
            //   Console.WriteLine("Inside UserExists");
            //check if at user page. If not, try going there again. If that fails - return false.
            Thread.Sleep(KortextGlobals.s);
            if (!Pages.LandingPage.ClickOnMenu_UserBtn())
            {
                Console.WriteLine("Unable to get to Users Page");
                return null;
            
            }
            //    Console.WriteLine("after gotouserpage check");
            WaitFind.FindElem(user_search_input_field, 10).Clear();
            Klick.On(user_search_input_field);
            //  Console.WriteLine("after search field clear and click");
            Thread.Sleep(KortextGlobals.s);
            user_search_input_field.SendKeys(username);
            //Console.WriteLine("after sendkeys");
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> rowsofusers = new List<IWebElement>(user_table.FindElements(By.TagName("tr")));
            //   Console.WriteLine("number of rows found" + rowsofusers.Count);

            //Traverse each row looking for the username.
            foreach (var elemTr in rowsofusers)
            {
                //                Console.WriteLine("In loop: ", elemTr);
                IList<IWebElement> lstTdElem = new List<IWebElement>(elemTr.FindElements(By.TagName("td")));
                //              Console.WriteLine("td count: " + lstTdElem.Count);
                if (lstTdElem.Count > 0)
                {
                    //                   Console.WriteLine("number of users found" + lstTdElem.Count);
                    foundname = lstTdElem[0].Text;

                    if (foundname == username)
                    {
                        /* OrigName = lstTdElem[0].Text;
                         OrigLogin = lstTdElem[1].Text;
                         useremail = lstTdElem[2].Text;
                         Console.WriteLine("Username found: " + userFullName);
                         Console.WriteLine("Login name found: " + userLoginName);
                         Console.WriteLine("email found: " + useremail);*/
                        return elemTr;
                    }
                }

            }
            Console.WriteLine("User: " + username + " is not found");
            return null;
        }


        private string SearchAndReturnNewUserName(string username)
        {
            //search for TestUser and increment suffix until you find one that hasn't been created yet.
            //Return that user name to be added.
            usernameappend = 1;
            for (int i = 0; i < i + 1; i++)
            {
                UserNameText = username + usernameappend;
                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(UserNameText);
                Thread.Sleep(KortextGlobals.s);
                try
                {
                    int action_flag = 0;
                    List<NgWebElement> verifyUsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                    if (verifyUsersSearched.Count > 0)
                    {
                        foreach (IWebElement verifyUserSearched in verifyUsersSearched)
                        {
                            IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                            if (verifyUserTitles[1].Text == UserNameText)
                            {
                                usernameappend = usernameappend + 1;
                                action_flag = 1;
                                break;
                            }
                        }
                        if (action_flag == 0)
                        {
                            Console.WriteLine("Search -" + UserNameText + " User Not Found; Using this username " + UserNameText);
                            return UserNameText;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Search -" + UserNameText + " User Not Found; Using this username " + UserNameText);
                        return UserNameText;
                    }

                    /*
                    var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
                    var Userispresent = wait.Until(x => x.FindElement(By.XPath("//td")));
                    usernameappend = usernameappend + 1;
                    CountOfUsersReturned();
                    */
                }
                catch (Exception e)
                {
                    Console.WriteLine("Search -" + UserNameText + " User Not Found; Using this username " + e.Message);
                    return UserNameText;

                }
            }
            Console.WriteLine("Username found" + UserNameText);
            return UserNameText;
        }

        //holding place for rest of tests.
        public bool EditUserListRoles(string username, string listrolestring)
        {
            try
            {
                Thread.Sleep(KortextGlobals.s);

                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }

                //UserExists returns a row container matching the user name.
                IWebElement thisuser = UserExists(username);
                if (thisuser == null)
                {
                    Console.WriteLine("User " + username + " does not currently exist");
                    return false;
                }


                // Driver.HighlightElement(FirstUserSearchResultRoles);

                //click on the edit button for row that contains the user name.
                ClickOnUserActionBtns(thisuser, UserActionBtn.editListRoleBtn);


                Thread.Sleep(KortextGlobals.s);
                // Klick.On(FirstUserSearchResultRoles);
                Thread.Sleep(KortextGlobals.s);
                WaitFind.FindElem(UserRolesListSearch, 10).Clear();
                Klick.On(UserRolesListSearch);
                Thread.Sleep(KortextGlobals.s);
                UserRolesListSearch.SendKeys(listrolestring);
                Thread.Sleep(KortextGlobals.s);
                if (WaitFind.FindElem(ListSearchResultOwner, 10) == null)
                {
                    //  WaitFind.FindElem(UserRolesListSearch, 10).Clear();
                    //  UserRolesListSearch.SendKeys("ics");
                    Console.WriteLine("Unable to find User List Role " + listrolestring);
                    return false;
                }
                Klick.On(ListSearchResultOwner);
                Klick.On(ListSearchResultAuthor);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(ListAssignFinishButton);
                Thread.Sleep(KortextGlobals.s);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in EditUser: " + e.Message);
                return false;
            }

        }

        public bool EditUser_Type(string username, string userrole)
        {
            try
            {
                Thread.Sleep(KortextGlobals.s);

                //check if at user page. If not, try going there again. If that fails - return false.
                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }


                // Console.WriteLine("Just about to go to UserExists");
                //See if passed in user exists. If not fail

                //UserExists returns a row container matching the user name.
                IWebElement thisuser = UserExists(username);
                if (thisuser == null)
                {
                    Console.WriteLine("User " + username + " does not currently exist");
                    return false;
                }
                //click on the edit button for row that contains the user name.
                ClickOnUserActionBtns(thisuser, UserActionBtn.editUserBtn);
                if (!ChangeUserInfoField(username, "User Type", userrole))
                {
                    Console.WriteLine("Unable to change user role");
                    return false;
                }



                Thread.Sleep(KortextGlobals.s);
                // return true;
                bool rolematched = ValidateUserInfo(username, userrole, origusertype, "User Type");
                return rolematched;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in EditUser_Type: " + e.Message);
                return false;
            }
        }
        public bool EditUser_Email(string username, string useremailtext)
        {
            try
            {
                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }


                // Console.WriteLine("Just about to go to UserExists");
                //See if passed in user exists. If not fail

                //UserExists returns a row container matching the user name.
                IWebElement thisuser = UserExists(username);
                if (thisuser == null)
                {
                    Console.WriteLine("User " + username + " does not currently exist");
                    return false;
                }

                //get original values: 
                IList<IWebElement> lstTdElem = new List<IWebElement>(thisuser.FindElements(By.TagName("td")));
                origuserFullName = lstTdElem[0].Text;
                origuserLoginName = lstTdElem[1].Text;
                origuseremail = lstTdElem[2].Text;


                //      Console.WriteLine("td count: " + lstTdElem.Count);
                if (lstTdElem.Count > 0)
                {

                }
                //click on the edit button for row that contains the user name.
                ClickOnUserActionBtns(thisuser, UserActionBtn.editUserBtn);
                if (!ChangeUserInfoField(username, "Email", useremailtext))
                {
                    Console.WriteLine("Unable to change user email");
                    return false;
                }



                Thread.Sleep(KortextGlobals.s);
                // return true;
                //   bool emailmatched = ValidateUserEmail(username, useremailtext, origuseremail);
                bool matched = ValidateUserInfo(username, useremailtext, origuseremail, "Email");
                return matched;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in EditUser_Email: " + e.Message);
                return false;
            }
        }

        public bool EditUser_LoginName(string username, string loginnametext)
        {
            try
            {
                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }


                // Console.WriteLine("Just about to go to UserExists");
                //See if passed in user exists. If not fail

                //UserExists returns a row container matching the user name.
                IWebElement thisuser = UserExists(username);
                if (thisuser == null)
                {
                    Console.WriteLine("User " + username + " does not currently exist");
                    return false;
                }

                //get original values: 
                IList<IWebElement> lstTdElem = new List<IWebElement>(thisuser.FindElements(By.TagName("td")));
                origuserFullName = lstTdElem[0].Text;
                origuserLoginName = lstTdElem[1].Text;
                origuseremail = lstTdElem[2].Text;


                //      Console.WriteLine("td count: " + lstTdElem.Count);
                if (lstTdElem.Count > 0)
                {

                }
                //click on the edit button for row that contains the user name.
                ClickOnUserActionBtns(thisuser, UserActionBtn.editUserBtn);
                //change the field specified.
                if (!ChangeUserInfoField(username, "LoginName", loginnametext))
                {
                    Console.WriteLine("Unable to change user loginName");
                    return false;
                }


                Thread.Sleep(KortextGlobals.s);
                // return true;
                bool matched = ValidateUserInfo(username, loginnametext, origuserLoginName, "LoginName");
                return matched;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in EditUser_LoginName: " + e.Message);
                return false;
            }
        }

        public bool EditUser_FullName(string username, string fullnametext)
        {
            try
            {
                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }


                // Console.WriteLine("Just about to go to UserExists");
                //See if passed in user exists. If not fail

                //UserExists returns a row container matching the user name.
                IWebElement thisuser = UserExists(username);
                if (thisuser == null)
                {
                    Console.WriteLine("User " + username + " does not currently exist");
                    return false;
                }

                //get original values: 
                IList<IWebElement> lstTdElem = new List<IWebElement>(thisuser.FindElements(By.TagName("td")));
                origuserFullName = lstTdElem[0].Text;
                origuserLoginName = lstTdElem[1].Text;
                origuseremail = lstTdElem[2].Text;


                //      Console.WriteLine("td count: " + lstTdElem.Count);
                if (lstTdElem.Count > 0)
                {

                }
                //click on the edit button for row that contains the user name.
                ClickOnUserActionBtns(thisuser, UserActionBtn.editUserBtn);
                //change the field specified.
                if (!ChangeUserInfoField(username, "Name", fullnametext))
                {
                    Console.WriteLine("Unable to change user loginName");
                    return false;
                }


                Thread.Sleep(KortextGlobals.s);
                // return true;
                bool matched = ValidateUserInfo(fullnametext, fullnametext, origuserFullName, "Name");
                return matched;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in EditUser_FullName: " + e.Message);
                return false;
            }
        }

     
        private bool ValidateUserInfo(string username, string newtext, string origtext, string field)
        {
            string foundvalue = "";
            try
            {
                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }
                Thread.Sleep(KortextGlobals.s);
                //UserExists returns a row container matching the user name.
                IWebElement thisuser = UserExists(username);
                if (thisuser == null)
                {
                    Console.WriteLine("User " + username + " does not currently exist");
                    return false;
                }

                //pull the user email from the user search screen.
                IList<IWebElement> usercolumns = new List<IWebElement>(thisuser.FindElements(By.TagName("td")));

                switch (field)
                {
                    case "Name":
                        foundvalue = usercolumns[0].Text;
                        Console.WriteLine("Username found: " + foundvalue);
                        break;
                    case "LoginName":

                        foundvalue = usercolumns[1].Text;
                        Console.WriteLine("Login name found: " + foundvalue);
                        break;
                    case "Email":
                        foundvalue = usercolumns[2].Text;
                        Console.WriteLine("email found: " + foundvalue);
                        break;

                    case "User Type":
                        //click on the edit button for row that contains the user name.
                        ClickOnUserActionBtns(thisuser, UserActionBtn.editUserBtn);
                        SelectElement type = new SelectElement(UserTypeDropDown);
                        foundvalue = type.SelectedOption.Text;
                        break;
                    default:
                        Console.WriteLine("Invalid info to change string used");
                        return false;

                }
                if (foundvalue.ToUpper() == newtext.ToUpper())
                {
                    Console.WriteLine("Original " + field + ": " + origtext + " changed to " + foundvalue + " and matches");
                    return true;
                }

                Console.WriteLine("Original Email: " + origtext + " changed to " + foundvalue + " but doesn't match");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in ValidateUserFieldInfo: " + e.Message);
                return false;
            }

        }
        private bool ChangeUserInfoField(string username, string infotochange, string newvalue)
        {

            //assumes that user has already been found using calling function and edit screen is already opened

            switch (infotochange)
            {
                case "Name":
                    //Change User Full Name to new value. 
                    Thread.Sleep(KortextGlobals.s);
                    full_name_input.Clear();
                    full_name_input.SendKeys(newvalue);
                    break;
                case "LoginName":
                    Thread.Sleep(KortextGlobals.s);
                    login_name_input.Clear();
                    login_name_input.SendKeys(newvalue);
                    break;
                case "Email":
                    Thread.Sleep(KortextGlobals.s);
                    email_input.Clear();
                    email_input.SendKeys(newvalue);
                    break;
                //add in new email.

                case "User Type":
                    Thread.Sleep(KortextGlobals.s);
                    SelectElement type = new SelectElement(UserTypeDropDown);
                    origusertype = type.SelectedOption.Text;
                    // Console.WriteLine("origrole: " + origrole);
                    // 
                    SelectUserType(newvalue.ToUpper());
                    break;


                default:
                    Console.WriteLine("Invalid info to change string used");
                    return false;
            }
            Thread.Sleep(KortextGlobals.s);
            //save the changes.
            Driver.HighlightElement(UserFinishBtn);
            Klick.On(UserFinishBtn);
            return true;
        }

        private bool ClickOnUserActionBtns(IWebElement username, UserActionBtn which_button)
        {//this function finds a specified user name and then clicks the proper button (edit,edit lists, delete user, restore)
         //assumes that the user has already been searched for and it's container is being passed into this function.
            IList<IWebElement> lstTdElem = new List<IWebElement>(username.FindElements(By.TagName("td")));
            //      Console.WriteLine("td count: " + lstTdElem.Count);
            if (lstTdElem.Count > 0)
            {

                switch (which_button)
                {
                    case UserActionBtn.editListRoleBtn:
                        IWebElement ListRoleBtn = lstTdElem[3].FindElement(By.CssSelector("button[ng-click = 'userSearchController.editRoles(user)'"));
                        Driver.HighlightElement(ListRoleBtn);
                        Klick.On(ListRoleBtn);
                        return true;
                    case UserActionBtn.editUserBtn:
                        IWebElement editBtn = lstTdElem[3].FindElement(By.CssSelector("button[ng-click = 'userSearchController.editUser(user)'"));
                        Driver.HighlightElement(editBtn);
                        Klick.On(editBtn);

                        return true;
                    case UserActionBtn.deleteUser:
                        IWebElement deleteBtn = lstTdElem[3].FindElement(By.CssSelector("button[ng-click = 'userSearchController.toggleUserActive(user)'"));
                        Driver.HighlightElement(deleteBtn);
                        Klick.On(deleteBtn);

                        return true;
                    case UserActionBtn.restoreUser:
                        IWebElement restoreBtn = lstTdElem[3].FindElement(By.CssSelector("button[uib-tooltip = 'Restore user'"));
                        Driver.HighlightElement(restoreBtn);
                        Klick.On(restoreBtn);

                        return true;
                    default:
                        Console.WriteLine("Invalid user action button");
                        return false;
                }
            }

            //  }
            Console.WriteLine("User: " + username + " cannot click on Action Buttons");
            return false;
        }
        //  
        //  }

        public bool AddUsertoList(string nonperm_role, string perm_role)
        {
            string List_details_name;
            string AttachedList_details_name;
            string verifyAttachedList_details_name;
            int action_flag = 0;

            try
            {
                if (nonperm_role == "" && perm_role == "")
                {
                    Console.WriteLine("List cannot be attached to a User without any Roles.");
                    return false;
                }
                Thread.Sleep(KortextGlobals.s);

                if(!Pages.PearlCreateReadingList.CreateList())
                {
                    Console.WriteLine("Error while Creating a List.");
                    return false;
                }

                currentURL = Driver.Instance.Url;

                string List_Title = Driver.Instance.FindElement(By.Id("list-title")).Text;
                if (List_Title.Contains("["))
                {
                    List_Title = List_Title.Substring(0, List_Title.IndexOf("["));
                    List_Title = List_Title.Trim();
                }
                else
                {
                    List_Title = List_Title.Trim();
                }

                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");
                    return false;
                }

                string username = SearchAndReturnNewUserName("ASusanUser");

                if(!AddUserDefault("Librarian"))
                {
                    Console.WriteLine("Error while creating a New User.");
                    return false;
                }

                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> UsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if(UsersSearched.Count > 0)
                {
                    foreach(IWebElement UserSearched in UsersSearched)
                    {
                        //IList<IWebElement> UserTitles = UserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> UserTitles = UserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        IWebElement ManageRoles_icon = UserSearched.FindElement(By.CssSelector("button[ng-click = 'userSearchController.editRoles(user)']"));
                        if ((UserTitles[1].Text == username) && (ManageRoles_icon.Displayed == true))
                        {
                            Klick.On(ManageRoles_icon);
                            Thread.Sleep(KortextGlobals.l);
                            WaitFind.FindElem(UserRolesListSearch, 10).Clear();
                            Klick.On(UserRolesListSearch);
                            Thread.Sleep(KortextGlobals.s);
                            UserRolesListSearch.SendKeys(List_Title);
                            Thread.Sleep(KortextGlobals.s);

                            List<NgWebElement> ListsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("list in rolesModal.listsResultsFiltered")));
                            if(ListsSearched.Count > 0)
                            {
                                foreach (NgWebElement ListSearched in ListsSearched)
                                {
                                    IWebElement List_details = ListSearched.FindElement(By.ClassName("list-details"));
                                    if (List_details.Text.Contains("["))
                                    {
                                        List_details_name = List_details.Text.Substring(List_details.Text.IndexOf(" "));
                                        List_details_name = List_details_name.Substring(0, List_details_name.IndexOf(" ["));
                                        List_details_name = List_details_name.Trim();
                                    }
                                    else
                                    {
                                        List_details_name = List_details.Text.Trim();
                                    }
                                    
                                    if (List_details_name == List_Title)
                                    {
                                        List<NgWebElement> NonPermRolesSearched = new List<NgWebElement>(ListSearched.FindElements(NgBy.Repeater("nonPermissive in roleButtons.nonPermissive.buttons")));
                                        switch(nonperm_role)
                                        {
                                            case "Leader":
                                                Klick.On(NonPermRolesSearched[0]);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext == "Role updated")
                                                {
                                                    Console.WriteLine("Non Permissive Role Update Successful to." + nonperm_role);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error while updating Non Permissive Role to." + nonperm_role);
                                                    return false;
                                                }
                                                break;
                                            case "Owner":
                                                Klick.On(NonPermRolesSearched[1]);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext == "Role updated")
                                                {
                                                    Console.WriteLine("Non Permissive Role Update Successful to." + nonperm_role);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error while updating Non Permissive Role to." + nonperm_role);
                                                    return false;
                                                }
                                                break;
                                            default:
                                                Console.WriteLine("Blank/Incorrect Non-Permissive Role Passed. Not updated the Non-Permissive Roles."+ nonperm_role);
                                                List<NgWebElement> PermRolesSearched = new List<NgWebElement>(ListSearched.FindElements(NgBy.Repeater("permissive in roleButtons.permissive.buttons")));
                                                switch (perm_role)
                                                {
                                                    case "Author":
                                                        Klick.On(PermRolesSearched[0]);
                                                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                        if (statusreturntext == "Role updated")
                                                        {
                                                            Console.WriteLine("Permissive Role Update Successful to." + perm_role);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Error while updating Permissive Role to." + perm_role);
                                                            return false;
                                                        }
                                                        break;
                                                    case "Moderator":
                                                        Klick.On(PermRolesSearched[1]);
                                                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                        if (statusreturntext == "Role updated")
                                                        {
                                                            Console.WriteLine("Permissive Role Update Successful to." + perm_role);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Error while updating Permissive Role to." + perm_role);
                                                            return false;
                                                        }
                                                        break;
                                                    case "Editor":
                                                        Klick.On(PermRolesSearched[2]);
                                                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                        if (statusreturntext == "Role updated")
                                                        {
                                                            Console.WriteLine("Permissive Role Update Successful to." + perm_role);
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Error while updating Permissive Role to." + perm_role);
                                                            return false;
                                                        }
                                                        break;
                                                    default:
                                                        Console.WriteLine("Blank/Incorrect Permissive Role Passed. Not updated the Permissive Roles." + perm_role);
                                                        break;
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                }
                            }

                            //Assigning Permissive Roles if NonPermissive Role is valid. If NonPermissive Role is invalid, then the Action for Permissive Role is taken care above.
                            if (nonperm_role == "Leader" || nonperm_role == "Owner")
                            {
                                List<NgWebElement> AttachedLists = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("list in rolesModal.rolesModalService.lists | orderBy: 'name'")));
                                if (AttachedLists.Count > 0)
                                {
                                    foreach (NgWebElement AttachedList in AttachedLists)
                                    {
                                        IWebElement AttachedList_details = AttachedList.FindElement(By.ClassName("user-details"));
                                        if (AttachedList_details.Text.Contains("["))
                                        {
                                            AttachedList_details_name = AttachedList_details.Text.Substring(AttachedList_details.Text.IndexOf(" "));
                                            AttachedList_details_name = AttachedList_details_name.Substring(0, AttachedList_details_name.IndexOf(" ["));
                                            AttachedList_details_name = AttachedList_details_name.Trim();
                                        }
                                        else
                                        {
                                            AttachedList_details_name = AttachedList_details.Text.Trim();
                                        }
                                        
                                        if (AttachedList_details_name == List_Title)
                                        {
                                            List<NgWebElement> AttahedPermRolesSearched = new List<NgWebElement>(AttachedList.FindElements(NgBy.Repeater("permissive in roleButtons.permissive.buttons")));
                                            switch (perm_role)
                                            {
                                                case "Author":
                                                    Klick.On(AttahedPermRolesSearched[0]);
                                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                    if (statusreturntext == "Role updated")
                                                    {
                                                        Console.WriteLine("Permissive Role Update Successful to." + perm_role);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Error while updating Permissive Role to." + perm_role);
                                                        return false;
                                                    }
                                                    break;
                                                case "Moderator":
                                                    Klick.On(AttahedPermRolesSearched[1]);
                                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                    if (statusreturntext == "Role updated")
                                                    {
                                                        Console.WriteLine("Permissive Role Update Successful to." + perm_role);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Error while updating Permissive Role to." + perm_role);
                                                        return false;
                                                    }
                                                    break;
                                                case "Editor":
                                                    Klick.On(AttahedPermRolesSearched[2]);
                                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                    if (statusreturntext == "Role updated")
                                                    {
                                                        Console.WriteLine("Permissive Role Update Successful to." + perm_role);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Error while updating Permissive Role to." + perm_role);
                                                        return false;
                                                    }
                                                    break;
                                                default:
                                                    Console.WriteLine("Blank/Incorrect Permissive Role Passed. Not updated the Permissive Roles." + perm_role);
                                                    break;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            Klick.On(ListAssignFinishButton);
                            Thread.Sleep(KortextGlobals.l);

                            //Verify the above action is done successfully
                            Driver.Instance.Navigate().Refresh();
                            Thread.Sleep(KortextGlobals.l);
                            WaitFind.FindElem(user_search_input_field, 10).Clear();
                            Klick.On(user_search_input_field);
                            Thread.Sleep(KortextGlobals.s);
                            user_search_input_field.SendKeys(username);
                            Thread.Sleep(KortextGlobals.s);

                            List<NgWebElement> verifyUsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                            if (verifyUsersSearched.Count > 0)
                            {
                                foreach (IWebElement verifyUserSearched in verifyUsersSearched)
                                {
                                    //IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.ClassName("user-active"));
                                    IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                                    IWebElement verifyManageRoles_icon = verifyUserSearched.FindElement(By.CssSelector("button[ng-click = 'userSearchController.editRoles(user)']"));
                                    if ((verifyUserTitles[1].Text == username) && (verifyManageRoles_icon.Displayed == true))
                                    {
                                        Klick.On(verifyManageRoles_icon);
                                        Thread.Sleep(KortextGlobals.l);
                                        
                                        List<NgWebElement> verifyAttachedLists = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("list in rolesModal.rolesModalService.lists | orderBy: 'name'")));
                                        if (verifyAttachedLists.Count > 0)
                                        {
                                            foreach (NgWebElement verifyAttachedList in verifyAttachedLists)
                                            {
                                                IWebElement verifyAttachedList_details = verifyAttachedList.FindElement(By.ClassName("user-details"));
                                                if (verifyAttachedList_details.Text.Contains("["))
                                                {
                                                    verifyAttachedList_details_name = verifyAttachedList_details.Text.Substring(verifyAttachedList_details.Text.IndexOf(" "));
                                                    verifyAttachedList_details_name = verifyAttachedList_details_name.Substring(0, verifyAttachedList_details_name.IndexOf(" ["));
                                                    verifyAttachedList_details_name = verifyAttachedList_details_name.Trim();
                                                }
                                                else
                                                {
                                                    verifyAttachedList_details_name = verifyAttachedList_details.Text.Trim();
                                                }

                                                if (verifyAttachedList_details_name == List_Title)
                                                {
                                                    List<IWebElement> actual_nonperm = new List<IWebElement>(verifyAttachedList.FindElements(By.CssSelector("label[ng-model = 'roleButtons.nonPermissive.held']")));
                                                    //Console.WriteLine("Classname for actual_nonperm is: " + actual_nonperm[0].GetAttribute("class"));
                                                    //Console.WriteLine("Classname for actual_nonperm is: " + actual_nonperm[1].GetAttribute("class"));
                                                    if ((actual_nonperm[0].GetAttribute("class").Contains("active")) && (nonperm_role == "Leader"))
                                                    {
                                                        Console.WriteLine("Non Permissive Role Successful");
                                                    }
                                                    else if ((actual_nonperm[1].GetAttribute("class").Contains("active")) && (nonperm_role == "Owner"))
                                                    {
                                                        Console.WriteLine("Non Permissive Role Successful");
                                                    }
                                                    else if ((nonperm_role == "") && !(actual_nonperm[0].GetAttribute("class").Contains("active")) && !(actual_nonperm[1].GetAttribute("class").Contains("active")))
                                                    {
                                                        Console.WriteLine("Non Permissive Role Successful");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Non Permissive Role not updated as expected");
                                                        return false;
                                                    }

                                                    List<IWebElement> actual_perm = new List<IWebElement>(verifyAttachedList.FindElements(By.CssSelector("label[ng-model = 'roleButtons.permissive.held']")));
                                                    //Console.WriteLine("Classname for actual_perm is: " + actual_perm[0].GetAttribute("class"));
                                                    //Console.WriteLine("Classname for actual_perm is: " + actual_perm[1].GetAttribute("class"));
                                                    //Console.WriteLine("Classname for actual_perm is: " + actual_perm[2].GetAttribute("class"));
                                                    if ((actual_perm[0].GetAttribute("class").Contains("active")) && (perm_role == "Author"))
                                                    {
                                                        Console.WriteLine("Permissive Role Successful");
                                                    }
                                                    else if ((actual_perm[1].GetAttribute("class").Contains("active")) && (perm_role == "Moderator"))
                                                    {
                                                        Console.WriteLine("Permissive Role Successful");
                                                    }
                                                    else if ((actual_perm[2].GetAttribute("class").Contains("active")) && (perm_role == "Editor"))
                                                    {
                                                        Console.WriteLine("Permissive Role Successful");
                                                    }
                                                    else if ((perm_role == "") && !(actual_perm[0].GetAttribute("class").Contains("active")) && !(actual_perm[1].GetAttribute("class").Contains("active")) && !(actual_perm[2].GetAttribute("class").Contains("active")))
                                                    {
                                                        Console.WriteLine("Permissive Role Successful");
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Permissive Role not updated as expected");
                                                        return false;
                                                    }

                                                    Klick.On(ListAssignFinishButton);
                                                    Thread.Sleep(KortextGlobals.l);
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("User not found." + username);
                                return false;
                            }
                            action_flag = 1;
                            break;
                        }
                    }
                    if (action_flag == 0)
                    {
                        Console.WriteLine("User not found." + username);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("User not found." + username);
                    return false;
                }

                /*if(!DeleteUser(username))
                {
                    Console.WriteLine("Error while deleting user." + username);
                    return false;
                }
                if (!Pages.PearlHierarchyPage.DeleteWholeTree("ABCDUniversity" + List_Title.Replace("TestList", "")))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + "ABCDUniversity" + List_Title.Replace("TestList", ""));
                    return false;
                }
                */
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in AddUsertoList: " + e.Message);
                return false;
            }
        }

        public bool UpdateUserListRoles(string nonperm_role, string nonperm_role_new, string perm_role, string perm_role_new)
        {
            string AttachedList_details_name;
            string verifyAttachedList_details_name;
            int action_flag = 0;

            try
            {
                if (!Pages.LandingPage.ClickOnMenu_HierarchyBtn())
                {
                    Console.WriteLine("Unable to get to Hierarchy Page");
                    return false;
                }
                Thread.Sleep(KortextGlobals.l);
                string Unit_Title = Pages.PearlCreateReadingList.SearchandReturnNewUnitName("ABCDUniversity");
                string List_Title = "TestList" + Unit_Title.Replace("ABCDUniversity","");
                Thread.Sleep(KortextGlobals.s);

                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");
                    return false;
                }
                Thread.Sleep(KortextGlobals.l);
                string username = SearchAndReturnNewUserName("ASusanUser");
                Thread.Sleep(KortextGlobals.s);

                if (!AddUsertoList(nonperm_role, perm_role))
                {
                    Console.WriteLine("Error while Adding New User");
                    return false;
                }

                Thread.Sleep(KortextGlobals.s);
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);
                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> UsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if (UsersSearched.Count > 0)
                {
                    foreach (IWebElement UserSearched in UsersSearched)
                    {
                        //IList<IWebElement> UserTitles = UserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> UserTitles = UserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        IWebElement ManageRoles_icon = UserSearched.FindElement(By.CssSelector("button[ng-click = 'userSearchController.editRoles(user)']"));
                        if ((UserTitles[1].Text == username) && (ManageRoles_icon.Displayed == true))
                        {
                            Klick.On(ManageRoles_icon);
                            Thread.Sleep(KortextGlobals.l);

                            List<NgWebElement> AttachedLists = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("list in rolesModal.rolesModalService.lists | orderBy: 'name'")));
                            if (AttachedLists.Count > 0)
                            {
                                foreach (NgWebElement AttachedList in AttachedLists)
                                {
                                    IWebElement AttachedList_details = AttachedList.FindElement(By.ClassName("user-details"));
                                    if (AttachedList_details.Text.Contains("["))
                                    {
                                        AttachedList_details_name = AttachedList_details.Text.Substring(AttachedList_details.Text.IndexOf(" "));
                                        AttachedList_details_name = AttachedList_details_name.Substring(0, AttachedList_details_name.IndexOf(" ["));
                                        AttachedList_details_name = AttachedList_details_name.Trim();
                                    }
                                    else
                                    {
                                        AttachedList_details_name = AttachedList_details.Text.Trim();
                                    }

                                    if (AttachedList_details_name == List_Title)
                                    {
                                        List<IWebElement> actual_nonperm = new List<IWebElement>(AttachedList.FindElements(By.CssSelector("label[ng-model = 'roleButtons.nonPermissive.held']")));
                                        if (nonperm_role_new == nonperm_role)
                                        {
                                            Console.WriteLine("Old and New Non-Permissive Role are same. No updates done here.");
                                        }
                                        else if ((nonperm_role_new == "Leader") && (nonperm_role_new != nonperm_role))
                                        {
                                            Klick.On(actual_nonperm[0]);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext == "Role updated")
                                            {
                                                Console.WriteLine("Non Permissive Role Update Successful to " + nonperm_role_new);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error while updating Non Permissive Role to " + nonperm_role_new);
                                                return false;
                                            }
                                        }
                                        else if ((nonperm_role_new == "Owner") && (nonperm_role_new != nonperm_role))
                                        {
                                            Klick.On(actual_nonperm[1]);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext == "Role updated")
                                            {
                                                Console.WriteLine("Non Permissive Role Update Successful to " + nonperm_role_new);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error while updating Non Permissive Role to " + nonperm_role_new);
                                                return false;
                                            }
                                        }
                                        else if ((nonperm_role_new == "") && (nonperm_role_new != nonperm_role))
                                        {
                                            if (nonperm_role == "Leader")
                                            {
                                                Klick.On(actual_nonperm[0]);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext == "Role updated")
                                                {
                                                    Console.WriteLine("Non Permissive Role Update Successful to Blank." + nonperm_role_new);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error while updating Non Permissive Role to Blank." + nonperm_role_new);
                                                    return false;
                                                }
                                            }
                                            else if (nonperm_role == "Owner")
                                            {
                                                Klick.On(actual_nonperm[1]);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext == "Role updated")
                                                {
                                                    Console.WriteLine("Non Permissive Role Update Successful to Blank." + nonperm_role_new);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error while updating Non Permissive Role to Blank." + nonperm_role_new);
                                                    return false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect New Non-Permissive Role provided." + nonperm_role_new);
                                            return false;
                                        }

                                        List<IWebElement> actual_perm = new List<IWebElement>(AttachedList.FindElements(By.CssSelector("label[ng-model = 'roleButtons.permissive.held']")));
                                        if (perm_role_new == perm_role)
                                        {
                                            Console.WriteLine("Old and New Permissive Role are same. No updates done here.");
                                        }
                                        else if ((perm_role_new == "Author") && (perm_role_new != perm_role))
                                        {
                                            Klick.On(actual_perm[0]);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext == "Role updated")
                                            {
                                                Console.WriteLine("Permissive Role Update Successful to " + perm_role_new);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error while updating Permissive Role to " + perm_role_new);
                                                return false;
                                            }
                                        }
                                        else if ((perm_role_new == "Moderator") && (perm_role_new != perm_role))
                                        {
                                            Klick.On(actual_perm[1]);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext == "Role updated")
                                            {
                                                Console.WriteLine("Permissive Role Update Successful to " + perm_role_new);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error while updating Permissive Role to " + perm_role_new);
                                                return false;
                                            }
                                        }
                                        else if ((perm_role_new == "Editor") && (perm_role_new != perm_role))
                                        {
                                            Klick.On(actual_perm[2]);
                                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                            if (statusreturntext == "Role updated")
                                            {
                                                Console.WriteLine("Permissive Role Update Successful to " + perm_role_new);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error while updating Permissive Role to " + perm_role_new);
                                                return false;
                                            }
                                        }
                                        else if ((perm_role_new == "") && (perm_role_new != perm_role))
                                        {
                                            if (perm_role == "Author")
                                            {
                                                Klick.On(actual_perm[0]);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext == "Role updated")
                                                {
                                                    Console.WriteLine("Permissive Role Update Successful to Blank." + perm_role_new);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error while updating Permissive Role to Blank." + perm_role_new);
                                                    return false;
                                                }
                                            }
                                            else if (perm_role == "Moderator")
                                            {
                                                Klick.On(actual_perm[1]);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext == "Role updated")
                                                {
                                                    Console.WriteLine("Permissive Role Update Successful to Blank." + perm_role_new);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error while updating Permissive Role to Blank." + perm_role_new);
                                                    return false;
                                                }
                                            }
                                            else if (perm_role == "Editor")
                                            {
                                                Klick.On(actual_perm[2]);
                                                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                                if (statusreturntext == "Role updated")
                                                {
                                                    Console.WriteLine("Permissive Role Update Successful to Blank." + perm_role_new);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error while updating Permissive Role to Blank." + perm_role_new);
                                                    return false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Incorrect New Permissive Role provided." + perm_role_new);
                                            return false;
                                        }

                                        Klick.On(ListAssignFinishButton);
                                        Thread.Sleep(KortextGlobals.l);

                                        //verify the above action is done successfully
                                        Driver.Instance.Navigate().Refresh();
                                        Thread.Sleep(KortextGlobals.l);
                                        WaitFind.FindElem(user_search_input_field, 10).Clear();
                                        Klick.On(user_search_input_field);
                                        Thread.Sleep(KortextGlobals.s);
                                        user_search_input_field.SendKeys(username);
                                        Thread.Sleep(KortextGlobals.s);

                                        List<NgWebElement> verifyUsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                                        if (verifyUsersSearched.Count > 0)
                                        {
                                            foreach (IWebElement verifyUserSearched in verifyUsersSearched)
                                            {
                                                //IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.ClassName("user-active"));
                                                IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                                                IWebElement verifyManageRoles_icon = verifyUserSearched.FindElement(By.CssSelector("button[ng-click = 'userSearchController.editRoles(user)']"));
                                                if ((verifyUserTitles[1].Text == username) && (verifyManageRoles_icon.Displayed == true))
                                                {
                                                    Klick.On(verifyManageRoles_icon);
                                                    Thread.Sleep(KortextGlobals.l);

                                                    List<NgWebElement> verifyAttachedLists = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("list in rolesModal.rolesModalService.lists | orderBy: 'name'")));
                                                    if (verifyAttachedLists.Count > 0)
                                                    {
                                                        foreach (NgWebElement verifyAttachedList in verifyAttachedLists)
                                                        {
                                                            IWebElement verifyAttachedList_details = verifyAttachedList.FindElement(By.ClassName("user-details"));
                                                            if (verifyAttachedList_details.Text.Contains("["))
                                                            {
                                                                verifyAttachedList_details_name = verifyAttachedList_details.Text.Substring(verifyAttachedList_details.Text.IndexOf(" "));
                                                                verifyAttachedList_details_name = verifyAttachedList_details_name.Substring(0, verifyAttachedList_details_name.IndexOf(" ["));
                                                                verifyAttachedList_details_name = verifyAttachedList_details_name.Trim();
                                                            }
                                                            else
                                                            {
                                                                verifyAttachedList_details_name = verifyAttachedList_details.Text.Trim();
                                                            }

                                                            if(nonperm_role_new == "" && perm_role_new == "")
                                                            {
                                                                if (verifyAttachedList_details_name == List_Title)
                                                                {
                                                                    Console.WriteLine("The List is still attached to the User. Update Failed.");
                                                                    return false;
                                                                }
                                                            }
                                                            if (verifyAttachedList_details_name == List_Title)
                                                            {
                                                                List<IWebElement> verifyactual_nonperm = new List<IWebElement>(verifyAttachedList.FindElements(By.CssSelector("label[ng-model = 'roleButtons.nonPermissive.held']")));
                                                                if ((verifyactual_nonperm[0].GetAttribute("class").Contains("active")) && (nonperm_role_new == "Leader"))
                                                                {
                                                                    Console.WriteLine("Non Permissive Role Successful");
                                                                }
                                                                else if ((verifyactual_nonperm[1].GetAttribute("class").Contains("active")) && (nonperm_role_new == "Owner"))
                                                                {
                                                                    Console.WriteLine("Non Permissive Role Successful");
                                                                }
                                                                else if ((nonperm_role_new == "") && !(verifyactual_nonperm[0].GetAttribute("class").Contains("active")) && !(verifyactual_nonperm[1].GetAttribute("class").Contains("active")))
                                                                {
                                                                    Console.WriteLine("Non Permissive Role Successful");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Non Permissive Role not updated as expected");
                                                                    return false;
                                                                }

                                                                List<IWebElement> verifyactual_perm = new List<IWebElement>(verifyAttachedList.FindElements(By.CssSelector("label[ng-model = 'roleButtons.permissive.held']")));
                                                                if ((verifyactual_perm[0].GetAttribute("class").Contains("active")) && (perm_role_new == "Author"))
                                                                {
                                                                    Console.WriteLine("Permissive Role Successful");
                                                                }
                                                                else if ((verifyactual_perm[1].GetAttribute("class").Contains("active")) && (perm_role_new == "Moderator"))
                                                                {
                                                                    Console.WriteLine("Permissive Role Successful");
                                                                }
                                                                else if ((verifyactual_perm[2].GetAttribute("class").Contains("active")) && (perm_role_new == "Editor"))
                                                                {
                                                                    Console.WriteLine("Permissive Role Successful");
                                                                }
                                                                else if ((perm_role_new == "") && !(verifyactual_perm[0].GetAttribute("class").Contains("active")) && !(verifyactual_perm[1].GetAttribute("class").Contains("active")) && !(verifyactual_perm[2].GetAttribute("class").Contains("active")))
                                                                {
                                                                    Console.WriteLine("Permissive Role Successful");
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Permissive Role not updated as expected");
                                                                    return false;
                                                                }

                                                                Klick.On(ListAssignFinishButton);
                                                                Thread.Sleep(KortextGlobals.l);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("User not found." + username);
                                            return false;
                                        }
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("User not found." + username);
                                return false;
                            }
                            action_flag = 1;
                            break;
                        }
                    }
                    if (action_flag == 0)
                    {
                        Console.WriteLine("User not found." + username);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("User not found." + username);
                    return false;
                }

                /*if (!DeleteUser(username))
                {
                    Console.WriteLine("Error while deleting user." + username);
                    return false;
                }
                if (!Pages.PearlHierarchyPage.DeleteWholeTree(Unit_Title))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + Unit_Title);
                    return false;
                }
                */
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in UpdateUserListRoles: " + e.Message);
                return false;
            }
        }

        public bool DeleteUser(string username = "No User Provided")
        {
            int action_flag = 0;

            try
            {
                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }
                if (username == "No User Provided")
                {
                    username = SearchAndReturnNewUserName("ASusanUser");
                    AddUserDefault();
                    Thread.Sleep(KortextGlobals.s);
                }

                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> UsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if (UsersSearched.Count > 0)
                {
                    foreach (IWebElement UserSearched in UsersSearched)
                    {
                        //IList<IWebElement> UserTitles = UserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> UserTitles = UserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        IWebElement Delete_icon = UserSearched.FindElement(By.CssSelector("button[ng-click = 'btnSlideout.toggle()']"));
                        if ((UserTitles[1].Text == username) && (Delete_icon.Displayed == true))
                        {
                            Klick.On(Delete_icon);
                            Thread.Sleep(KortextGlobals.l);

                            IWebElement ConfirmDelete_icon = UserSearched.FindElement(By.CssSelector("button[ng-click = 'btnSlideout.doAction(thisBtn, $event)']"));
                            Klick.On(ConfirmDelete_icon);
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext == "User deleted successfully")
                            {
                                Console.WriteLine("User Deleted Successfully." + username);
                            }
                            else
                            {
                                Console.WriteLine("Error while Deleting User." + username);
                                return false;
                            }
                            action_flag = 1;
                            break;
                        }
                    }
                    if (action_flag == 0)
                    {
                        Console.WriteLine("User not found." + username);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("User not found." + username);
                    return false;
                }

                //verify the action done above is successful
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> verifyUsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if (verifyUsersSearched.Count > 0)
                {
                    foreach (IWebElement verifyUserSearched in verifyUsersSearched)
                    {
                        //IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        if (verifyUserTitles[1].Text == username)
                        {
                            Console.WriteLine("Error while User Delete. User found after delete." + username);
                            return false;
                        }
                    }
                    Console.WriteLine("User Delete Completed. User not found after delete." + username);
                }
                else
                {
                    Console.WriteLine("User Delete Completed. User not found after delete." + username);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in DeleteUser: " + e.Message);
                return false;
            }
        }

        public bool ArchiveUser(string username = "No User Provided")
        {
            int action_flag = 0;

            try
            {
                Thread.Sleep(KortextGlobals.s);
                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");

                    return false;
                }
                if (username == "No User Provided")
                {
                    username = SearchAndReturnNewUserName("ASusanUser");
                    AddUserDefault();
                    Thread.Sleep(KortextGlobals.s);
                }

                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> UsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if (UsersSearched.Count > 0)
                {
                    foreach (IWebElement UserSearched in UsersSearched)
                    {
                        //IList<IWebElement> UserTitles = UserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> UserTitles = UserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        IWebElement Archive_icon = UserSearched.FindElement(By.CssSelector("button[uib-tooltip = 'Archive user']"));
                        if ((UserTitles[1].Text == username) && (Archive_icon.Displayed == true))
                        {
                            Klick.On(Archive_icon);
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext == "User updated")
                            {
                                Console.WriteLine("User Archived Successfully." + username);
                            }
                            else
                            {
                                Console.WriteLine("Error while Archiving User." + username);
                                return false;
                            }
                            action_flag = 1;
                            break;
                        }
                    }
                    if (action_flag == 0)
                    {
                        Console.WriteLine("User not found." + username);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("User not found." + username);
                    return false;
                }

                //verify the action done above is successful
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);
                action_flag = 0;
                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> verifyUsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if (verifyUsersSearched.Count > 0)
                {
                    foreach (IWebElement verifyUserSearched in verifyUsersSearched)
                    {
                        //IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        IWebElement Restore_icon = verifyUserSearched.FindElement(By.CssSelector("button[uib-tooltip = 'Restore user']"));
                        if ((verifyUserTitles[1].Text == username) && (Restore_icon.Displayed == true))
                        {
                            Console.WriteLine("User Archived Completed." + username);
                            action_flag = 1;
                            break;
                        }
                    }
                    if (action_flag == 0)
                    {
                        Console.WriteLine("User not found." + username);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("User not found." + username);
                    return false;
                }
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception in ArchiveUser: " + e.Message);
                return false;
            }
        }

        public bool RestoreUser(string username = "No User Provided")
        {
            int action_flag = 0;

            try
            {
                Thread.Sleep(KortextGlobals.s);

                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");
                    return false;
                }
                if (username == "No User Provided")
                {
                    username = SearchAndReturnNewUserName("ASusanUser");
                    Thread.Sleep(KortextGlobals.s);
                }

                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                if (!ArchiveUser())
                {
                    Console.WriteLine("Error while Archiving User.");
                    return false;
                }

                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> UsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if (UsersSearched.Count > 0)
                {
                    foreach (IWebElement UserSearched in UsersSearched)
                    {
                        //IList<IWebElement> UserTitles = UserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> UserTitles = UserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        IWebElement Restore_icon = UserSearched.FindElement(By.CssSelector("button[uib-tooltip = 'Restore user']"));
                        if ((UserTitles[1].Text == username) && (Restore_icon.Displayed == true))
                        {
                            Klick.On(Restore_icon);
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext == "User updated")
                            {
                                Console.WriteLine("User Restored Successfully." + username);
                            }
                            else
                            {
                                Console.WriteLine("Error while Restoring User." + username);
                                return false;
                            }
                            action_flag = 1;
                            break;
                        }
                    }
                    if (action_flag == 0)
                    {
                        Console.WriteLine("User not found." + username);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("User not found." + username);
                    return false;
                }

                //verify the action done above is successful
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);
                action_flag = 0;
                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> verifyUsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if (verifyUsersSearched.Count > 0)
                {
                    foreach (IWebElement verifyUserSearched in verifyUsersSearched)
                    {
                        //IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> verifyUserTitles = verifyUserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        IWebElement Archive_icon = verifyUserSearched.FindElement(By.CssSelector("button[uib-tooltip = 'Archive user']"));
                        if ((verifyUserTitles[1].Text == username) && (Archive_icon.Displayed == true))
                        {
                            Console.WriteLine("User Restored Completed." + username);
                            action_flag = 1;
                            break;
                        }
                    }
                    if (action_flag == 0)
                    {
                        Console.WriteLine("User not found." + username);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("User not found." + username);
                    return false;
                }
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception in ArchiveUser: " + e.Message);
                return false;
            }
        }

        public bool UserPwdReset(string username = "No User Provided")
        {
            int action_flag = 0;

            try
            {
                Thread.Sleep(KortextGlobals.s);

                if (!Pages.LandingPage.ClickOnMenu_UserBtn())
                {
                    Console.WriteLine("Unable to get to Users Page");
                    return false;
                }
                if (username == "No User Provided")
                {
                    username = SearchAndReturnNewUserName("ASusanUser");
                    AddUserDefault();
                    Thread.Sleep(KortextGlobals.s);
                }

                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                WaitFind.FindElem(user_search_input_field, 10).Clear();
                Klick.On(user_search_input_field);
                Thread.Sleep(KortextGlobals.s);
                user_search_input_field.SendKeys(username);
                Thread.Sleep(KortextGlobals.s);

                List<NgWebElement> UsersSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("user in userSearchController.results")));
                if (UsersSearched.Count > 0)
                {
                    foreach (IWebElement UserSearched in UsersSearched)
                    {
                        //IList<IWebElement> UserTitles = UserSearched.FindElements(By.ClassName("user-active"));
                        IList<IWebElement> UserTitles = UserSearched.FindElements(By.CssSelector("td[ng-class = \"user.active ? 'user-active' : 'user-inactive'\"]"));
                        IWebElement EditUser_icon = UserSearched.FindElement(By.CssSelector("button[uib-tooltip = 'Edit user']"));
                        if ((UserTitles[1].Text == username) && (EditUser_icon.Displayed == true))
                        {
                            Klick.On(EditUser_icon);
                            Thread.Sleep(KortextGlobals.l);

                            IWebElement ResetPwd_icon = Driver.Instance.FindElement(By.CssSelector("button[ng-click = 'userModal.resetPassword()']"));
                            Klick.On(ResetPwd_icon);
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext == "Password reset email sent")
                            {
                                Console.WriteLine("User Password Reset Successful." + username);
                            }
                            else
                            {
                                Console.WriteLine("Error while User Password Reset." + username);
                                return false;
                            }
                            action_flag = 1;
                            break;
                        }
                    }
                    if (action_flag == 0)
                    {
                        Console.WriteLine("User not found." + username);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("User not found." + username);
                    return false;
                }

                IWebElement EditClose_buton = Driver.Instance.FindElement(By.CssSelector("button[ng-click = 'userModal.close()']"));
                Klick.On(EditClose_buton);
                Thread.Sleep(KortextGlobals.s);
                return true;
            }

            catch (Exception e)
            {
                Console.WriteLine("Exception in ArchiveUser: " + e.Message);
                return false;
            }
        }
    }
    public enum UserActionBtn
    {
        editUserBtn,
        editListRoleBtn,
        deleteUser,
        restoreUser

    }
}



