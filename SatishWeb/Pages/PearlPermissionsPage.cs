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
    public class PearlPermissionsPage
    {
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = 'usertypes.addNew()']")]
        protected IWebElement NewUserTypeButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "p[editable-text= 'usertypeController.usertype.name']")]
        protected IWebElement RoleName
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "p[editable-text= 'usertypeController.usertype.description']")]
        protected IWebElement RoleDesc
        {
            get;
            set;
        }
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByModel), Using = "$parent.$data")]
        protected IWebElement TypeNameDescTextField
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[title= 'Submit']")]
        protected IWebElement NameDescSubmitButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[ng-click = '$form.$cancel()']")]
        protected IWebElement NameDescCancel
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Confirm delete']")]
        protected IWebElement DeleteConfirmButton
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip = 'Cancel delete']")]
        protected IWebElement DeleteCancelButton
        {
            get;
            set;
        }
       
        By UserTypeName_locator = By.CssSelector("p[editable-text= 'usertypeController.usertype.name']");
        By UserTypeDesc_locator = By.CssSelector("p[editable-text= 'usertypeController.usertype.description']");
        By Add_Resp_locator = By.CssSelector("span[ng-click = 'usertypeController.assignResp(thisResp)']");
        By Add_Priv_locator = By.CssSelector("span[ng-click = 'usertypeController.assignPriv(thisPriv)']");
        By Delete_Resp_locator = By.CssSelector("span[ng-click = 'usertypeController.divestResp(thisResp)']");
        By Delete_Priv_locator = By.CssSelector("span[ng-click = 'usertypeController.divestPriv(thisPriv)']");
        By Delete_UserType_locator = By.CssSelector("button[uib-tooltip= 'Delete this user type']");
        By UserTypeExpandedDetails = By.CssSelector("div[uib-collapse= '!isOpen']");
        By ExpandCollapse_Arrow = By.CssSelector("span[ng-bind-html= \'(usertypeController.isOpen) ? 'expand_less' : 'expand_more'\"]");
        By TitleUserTypeName_locator = By.CssSelector("a[ng-click = 'toggleOpen()']");
        By SectionName_locator = By.ClassName("panel-heading");
        By NameDesc_locator = By.ClassName("panel-body");
        By AvailableResp_locator = By.ClassName("available-responsibilities");
        By AvailablePriv_locator = By.ClassName("available-privileges");
        By AssignedResp_locator = By.ClassName("assigned-responsibilities");
        By AssignedPriv_locator = By.ClassName("assigned-privileges");

        string statusreturntext;
        string currentURL;
        string UserTypeName;
        string UserNameText;
        int usernameappend;

        public bool PermissionsPage()
        {
            try
            {
                currentURL = Driver.Instance.Url;
                
                UserTypeName = SearchAndReturnNewUserTypeName("UserType");

                //Create New User Types
                AddNewRole(UserTypeName, "Description for " + UserTypeName);

                //Search User Type
                SearchRoles(UserTypeName);

                //Add Responsibility
                AddResp(UserTypeName);

                //Add Privilege
                AddPriv(UserTypeName);

                //Delete Responsibility
                DeleteResp(UserTypeName);

                //Delete Privilege
                DeletePriv(UserTypeName);
                
                //Edit / Update User Type Name
                UpdateRole(UserTypeName, "Name", "New" + UserTypeName);

                UpdateRole("New" + UserTypeName, "Name", UserTypeName);

                //Edit / Update User Type Description
                UpdateRole(UserTypeName, "Description", "Nothing much. Just updating the description for" + UserTypeName);

                //Delete User Type
                DeleteRole(UserTypeName);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in PermissionsPage.cs: " + e.Message);
                return false;
            }
        }

        private string SearchAndReturnNewUserTypeName(string username)
        {
            //search for TestUser and increment suffix until you find one that hasn't been created yet.
            //Return that user name to be added.
            usernameappend = 1;
            for (int i = 0; i < i + 1; i++)
            {
                int found_flag = 0;
                UserNameText = username + usernameappend;
                Thread.Sleep(KortextGlobals.s);
                try
                {
                    List<NgWebElement> UserTypesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("usertype in usertypes.US.allUsertypes track by usertype.id")));
                    if (UserTypesSearched.Count > 0)
                    {
                        foreach (IWebElement UserTypeSearched in UserTypesSearched)
                        {
                            Driver.HighlightElement(UserTypeSearched);
                            IWebElement UserTypeTitle = UserTypeSearched.FindElement(TitleUserTypeName_locator);
                            if (UserTypeTitle.Text == (UserNameText + " expand_more"))
                            {
                                usernameappend = usernameappend + 1;
                                found_flag = 1;
                                break;
                            }
                        }
                        if(found_flag == 0)
                        {
                            Console.WriteLine("User Type found." + UserNameText);
                            return UserNameText;
                        }
                    }
                    else
                    {
                        Console.WriteLine("User Type found." + UserNameText);
                        return UserNameText;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Search -" + UserNameText + " UserType Not Found; Using this User Type." + e.Message);
                    return UserNameText;
                }
            }
            return UserNameText;
        }

        public void AddNewRole(string searchtext, string desctext)
        {
            Thread.Sleep(KortextGlobals.s);
            Klick.On(NewUserTypeButton);
            Thread.Sleep(KortextGlobals.s);

            Klick.On(RoleName);
            Thread.Sleep(KortextGlobals.s);
            WaitFind.FindElem(TypeNameDescTextField, 10).Clear();
            Klick.On(TypeNameDescTextField);
            Thread.Sleep(KortextGlobals.s);
            TypeNameDescTextField.SendKeys(searchtext);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(NameDescSubmitButton);

            Klick.On(RoleDesc);
            Thread.Sleep(KortextGlobals.s);
            WaitFind.FindElem(TypeNameDescTextField, 10).Clear();
            Klick.On(TypeNameDescTextField);
            Thread.Sleep(KortextGlobals.s);
            TypeNameDescTextField.SendKeys(desctext);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(NameDescSubmitButton);
            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "User type created")
            {
                Console.WriteLine("Error while Creating a User Role." + statusreturntext);
            }
            else
            {
                Console.WriteLine("User Role Created Successful");
            }
            SearchRoles(searchtext);
        }
        public void SearchRoles(string searchtext)
        {
            int i = 0;
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> UserTypesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("usertype in usertypes.US.allUsertypes track by usertype.id")));
            if (UserTypesSearched.Count > 0)
            {
                foreach (IWebElement UserTypeSearched in UserTypesSearched)
                {
                    Driver.HighlightElement(UserTypeSearched);
                    IWebElement UserTypeTitle = UserTypeSearched.FindElement(TitleUserTypeName_locator);
                    if (UserTypeTitle.Text == (UserNameText + " expand_more"))
                    {
                        Console.WriteLine(searchtext + " User Role found");
                        i = 1;
                        break;
                    }
                }
                if (i == 0)
                {
                    Console.WriteLine(searchtext + " User Role not found");
                }
            }
            else
            {
                Console.WriteLine(searchtext + " User Role not found");
            }
        }
        public void UpdateRole(string rolenm, string rolefield, string newtext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> UserTypesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("usertype in usertypes.US.allUsertypes track by usertype.id")));
            if (UserTypesSearched.Count > 0)
            {
                foreach (IWebElement UserTypeSearched in UserTypesSearched)
                {
                    Driver.HighlightElement(UserTypeSearched);
                    IWebElement UserTypeTitle = UserTypeSearched.FindElement(TitleUserTypeName_locator);
                    if (UserTypeTitle.Text == (rolenm + " expand_more"))
                    {
                        Klick.On(UserTypeTitle);
                        Thread.Sleep(KortextGlobals.s);
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", UserTypeTitle);
                        Thread.Sleep(KortextGlobals.s);

                        if (rolefield == "Name")
                        {
                            IWebElement UserTypeName = UserTypeSearched.FindElement(UserTypeName_locator);
                            Klick.On(UserTypeName);
                            Thread.Sleep(KortextGlobals.s);
                            WaitFind.FindElem(TypeNameDescTextField, 10).Clear();
                            Klick.On(TypeNameDescTextField);
                            Thread.Sleep(KortextGlobals.s);
                            TypeNameDescTextField.SendKeys(newtext);
                            Thread.Sleep(KortextGlobals.s);
                            Klick.On(NameDescSubmitButton);
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext != "User type updated")
                            {
                                Console.WriteLine("Error while Updating User Role Name." + statusreturntext);
                            }
                            else
                            {
                                Console.WriteLine("User Role Name Update Successful");
                            }
                        }
                        else if (rolefield == "Description")
                        {
                            IWebElement UserTypeDesc = UserTypeSearched.FindElement(UserTypeDesc_locator);
                            Klick.On(UserTypeDesc);
                            Thread.Sleep(KortextGlobals.s);
                            WaitFind.FindElem(TypeNameDescTextField, 10).Clear();
                            Klick.On(TypeNameDescTextField);
                            Thread.Sleep(KortextGlobals.s);
                            TypeNameDescTextField.SendKeys(newtext);
                            Thread.Sleep(KortextGlobals.s);
                            Klick.On(NameDescSubmitButton);
                            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                            if (statusreturntext != "User type updated")
                            {
                                Console.WriteLine("Error while Updating User Role Description." + statusreturntext);
                            }
                            else
                            {
                                Console.WriteLine("User Role Description Update Successful");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Incorrect Role Field passed to the function.");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine(rolenm + " Role not found to be updated");
            }
        }
        public void DeleteRole(string searchtext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> UserTypesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("usertype in usertypes.US.allUsertypes track by usertype.id")));
            if (UserTypesSearched.Count > 0)
            {
                foreach (IWebElement UserTypeSearched in UserTypesSearched)
                {
                    Driver.HighlightElement(UserTypeSearched);
                    IWebElement UserTypeTitle = UserTypeSearched.FindElement(TitleUserTypeName_locator);
                    if (UserTypeTitle.Text == (searchtext + " expand_more"))
                    {
                        Klick.On(UserTypeTitle);
                        Thread.Sleep(KortextGlobals.s);
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", UserTypeTitle);
                        Thread.Sleep(KortextGlobals.s);

                        IWebElement RoleDeleteButton = UserTypeSearched.FindElement(Delete_UserType_locator);
                        Klick.On(RoleDeleteButton);
                        Thread.Sleep(KortextGlobals.s);
                        Klick.On(DeleteConfirmButton);
                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                        if (statusreturntext != "User type deleted")
                        {
                            Console.WriteLine("Error while Deleting User Type Role." + statusreturntext);
                        }
                        else
                        {
                            Console.WriteLine("Deleting User Type Role Successful");
                        }
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine(searchtext + " User Type Role not found to be deleted");
            }
            Console.WriteLine("Searching User Type after Deleting");
            SearchRoles(searchtext);
        }
        public void AddResp(string searchtext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> UserTypesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("usertype in usertypes.US.allUsertypes track by usertype.id")));
            if (UserTypesSearched.Count > 0)
            {
                foreach (IWebElement UserTypeSearched in UserTypesSearched)
                {
                    Driver.HighlightElement(UserTypeSearched);
                    IWebElement UserTypeTitle = UserTypeSearched.FindElement(TitleUserTypeName_locator);
                    if (UserTypeTitle.Text == (UserNameText + " expand_more"))
                    {
                        Klick.On(UserTypeTitle);
                        Thread.Sleep(KortextGlobals.s);

                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", UserTypeTitle);
                        Thread.Sleep(KortextGlobals.l);

                        IList<IWebElement> AvailableResp = Driver.Instance.FindElements(AvailableResp_locator);
                        if (AvailableResp.Count > 0)
                        {
                            List<NgWebElement> IndividualResponsibilities = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("thisResp in usertypeController.unassignedResponsibilities() | orderBy: 'name' track by thisResp.id")));
                            if (IndividualResponsibilities.Count > 0)
                            {
                                int AvailableRespCount = IndividualResponsibilities.Count;
                                foreach (IWebElement IndividualResp in IndividualResponsibilities)
                                {
                                    Driver.HighlightElement(IndividualResp);
                                    IWebElement AddRespName = IndividualResp.FindElement(By.ClassName("ng-binding"));
                                    //Console.WriteLine("AddRespName.Text = " + AddRespName.Text);
                                    string ResponsibilityNm = AddRespName.Text;
                                    IWebElement AddResponsibility = IndividualResp.FindElement(Add_Resp_locator);
                                    Klick.On(AddResponsibility);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    // checkingstring = string.Concat();
                                    //Console.WriteLine("checkingstring = " + checkingstring);
                                    if (statusreturntext != string.Concat(ResponsibilityNm," assigned to ",searchtext)) 
                                    {
                                        Console.WriteLine("Error while Adding Responsibilities." + ResponsibilityNm + "." + statusreturntext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Adding Responsibilities Successful." + ResponsibilityNm);
                                    }
                                    List<NgWebElement> IndividualResponsibilitiesAfter = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("thisResp in usertypeController.unassignedResponsibilities() | orderBy: 'name' track by thisResp.id")));
                                    int AvailableRespCountAfter = IndividualResponsibilitiesAfter.Count;
                                    if (AvailableRespCount == (AvailableRespCountAfter + 1))
                                    {
                                        Console.WriteLine("Adding Responsibilities Completed");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while Adding Responsibilities");
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Available Responsibilties present for Addition");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Section for Available Responsibilities not found");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(searchtext + " User Type Role not found to Assign Responsibilities");
            }
        }
        public void AddPriv(string searchtext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> UserTypesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("usertype in usertypes.US.allUsertypes track by usertype.id")));
            if (UserTypesSearched.Count > 0)
            {
                foreach (IWebElement UserTypeSearched in UserTypesSearched)
                {
                    Driver.HighlightElement(UserTypeSearched);
                    IWebElement UserTypeTitle = UserTypeSearched.FindElement(TitleUserTypeName_locator);
                    if (UserTypeTitle.Text == (UserNameText + " expand_more"))
                    {
                        Klick.On(UserTypeTitle);
                        Thread.Sleep(KortextGlobals.s);
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", UserTypeTitle);
                        Thread.Sleep(KortextGlobals.l);

                        IList<IWebElement> AvailablePriv = Driver.Instance.FindElements(AvailablePriv_locator);
                        if (AvailablePriv.Count > 0)
                        {
                            List<NgWebElement> IndividualPriviledges = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("thisPriv in usertypeController.unassignedPrivileges() | orderBy: 'name' track by thisPriv.name")));
                            if (IndividualPriviledges.Count > 0)
                            {
                                int AvailablePrivCount = IndividualPriviledges.Count;
                                foreach (IWebElement IndividualPriv in IndividualPriviledges)
                                {
                                    Driver.HighlightElement(IndividualPriv);
                                    IWebElement AddPrivName = IndividualPriv.FindElement(By.ClassName("ng-binding"));
                                    string PriviledgeNm = AddPrivName.Text;
                                    IWebElement AddPriviledge = IndividualPriv.FindElement(Add_Priv_locator);
                                    Klick.On(AddPriviledge);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext != (PriviledgeNm + " assigned to " + searchtext))
                                    {
                                        Console.WriteLine("Error while Adding Priviledges." + PriviledgeNm + "." + statusreturntext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Adding Priviledges Successful." + PriviledgeNm);
                                    }
                                    List<NgWebElement> IndividualPriviledgesAfter = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("thisPriv in usertypeController.unassignedPrivileges() | orderBy: 'name' track by thisPriv.name")));
                                    int AvailablePrivCountAfter = IndividualPriviledgesAfter.Count;
                                    if (AvailablePrivCount == (AvailablePrivCountAfter + 1))
                                    {
                                        Console.WriteLine("Adding Priviledges Completed");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while Adding Priviledges");
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Available Priviledges present for Addition");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Section for Available Priviledges not found");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(searchtext + " User Type Role not found to Assign Priviledges");
            }
        }
        public void DeleteResp(string searchtext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> UserTypesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("usertype in usertypes.US.allUsertypes track by usertype.id")));
            if (UserTypesSearched.Count > 0)
            {
                foreach (IWebElement UserTypeSearched in UserTypesSearched)
                {
                    Driver.HighlightElement(UserTypeSearched);
                    IWebElement UserTypeTitle = UserTypeSearched.FindElement(TitleUserTypeName_locator);
                    if (UserTypeTitle.Text == (UserNameText + " expand_more"))
                    {
                        Klick.On(UserTypeTitle);
                        Thread.Sleep(KortextGlobals.s);
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", UserTypeTitle);
                        Thread.Sleep(KortextGlobals.l);

                        IList<IWebElement> AssignedResp = Driver.Instance.FindElements(AssignedResp_locator);
                        if (AssignedResp.Count > 0)
                        {
                            List<NgWebElement> IndividualResponsibilities = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("thisResp in usertypeController.inferred | orderBy: 'name' track by thisResp.id")));
                            if (IndividualResponsibilities.Count > 0)
                            {
                                int AvailableRespCount = IndividualResponsibilities.Count;
                                foreach (IWebElement IndividualResp in IndividualResponsibilities)
                                {
                                    Driver.HighlightElement(IndividualResp);
                                    IWebElement DeleteRespName = IndividualResp.FindElement(By.ClassName("ng-binding"));
                                    string ResponsibilityNm = DeleteRespName.Text;
                                    IWebElement DeleteResponsibility = IndividualResp.FindElement(Delete_Resp_locator);
                                    Klick.On(DeleteResponsibility);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext != (ResponsibilityNm + " removed from " + searchtext))
                                    {
                                        Console.WriteLine("Error while Deleting Responsibilities." + ResponsibilityNm + "." + statusreturntext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Deleting Responsibilities Successful." + ResponsibilityNm);
                                    }
                                    List<NgWebElement> IndividualResponsibilitiesAfter = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("thisResp in usertypeController.inferred | orderBy: 'name' track by thisResp.id")));
                                    int AvailableRespCountAfter = IndividualResponsibilitiesAfter.Count;
                                    if (AvailableRespCount == (AvailableRespCountAfter + 1))
                                    {
                                        Console.WriteLine("Deleting Responsibilities Completed");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while Deleting Responsibilities");
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Assigned Responsibilities present for Deletion");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Section for Assigned Responsibilities not found");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(searchtext + " User Type Role not found to Assign Responsibilities");
            }
        }
        public void DeletePriv(string searchtext)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            List<NgWebElement> UserTypesSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("usertype in usertypes.US.allUsertypes track by usertype.id")));
            if (UserTypesSearched.Count > 0)
            {
                foreach (IWebElement UserTypeSearched in UserTypesSearched)
                {
                    Driver.HighlightElement(UserTypeSearched);
                    IWebElement UserTypeTitle = UserTypeSearched.FindElement(TitleUserTypeName_locator);
                    if (UserTypeTitle.Text == (UserNameText + " expand_more"))
                    {
                        Klick.On(UserTypeTitle);
                        Thread.Sleep(KortextGlobals.s);
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", UserTypeTitle);
                        Thread.Sleep(KortextGlobals.l);

                        IList<IWebElement> AssignedPriv = Driver.Instance.FindElements(AssignedPriv_locator);
                        if (AssignedPriv.Count > 0)
                        {
                            List<NgWebElement> IndividualPriviledges = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("thisPriv in usertypeController.usertype.privileges | orderBy: 'name' track by thisPriv.name")));
                            if (IndividualPriviledges.Count > 0)
                            {
                                int AvailablePrivCount = IndividualPriviledges.Count;
                                foreach (IWebElement IndividualPriv in IndividualPriviledges)
                                {
                                    Driver.HighlightElement(IndividualPriv);
                                    IWebElement DeletePrivName = IndividualPriv.FindElement(By.ClassName("ng-binding"));
                                    string PriviledgeNm = DeletePrivName.Text;
                                    IWebElement DeletePriviledge = IndividualPriv.FindElement(Delete_Priv_locator);
                                    Klick.On(DeletePriviledge);
                                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                                    if (statusreturntext != (PriviledgeNm + " removed from " + searchtext))
                                    {
                                        Console.WriteLine("Error while Deleting Priviledges." + PriviledgeNm + "." + statusreturntext);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Deleting Priviledges Successful." + PriviledgeNm);
                                    }
                                    List<NgWebElement> IndividualPriviledgesAfter = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("thisPriv in usertypeController.usertype.privileges | orderBy: 'name' track by thisPriv.name")));
                                    int AvailablePrivCountAfter = IndividualPriviledgesAfter.Count;
                                    if (AvailablePrivCount == (AvailablePrivCountAfter + 1))
                                    {
                                        Console.WriteLine("Deleting Priviledges Completed");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error while Deleting Priviledges");
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("No Assigned Priviledges present for Deletion");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Section for Available Priviledges not found");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(searchtext + " User Type Role not found to Assign Priviledges");
            }
        }
    }
}



