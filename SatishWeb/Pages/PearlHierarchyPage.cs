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
using OpenQA.Selenium.Support;

namespace PearlFramework
{
    public class PearlHierarchyPage
    {
        //Page factory calls won't work (That I can figure!!!) on calls to IWebElements containers.  So this is the next best thing - declaring the locator strategies once at the top of the page.
        // called by using this format in the functions on a IWebElement thisunit already found: thisunit.FindElements(edit_icon_locator)


        // internal nodes of the tree locators - single point of maintenance
        By unit_title = By.CssSelector("a[ng-show = 'nameCtrl.display']"); //unit locator - looks for a unit on the hierarchy page.
        By edit_icon_locator = By.Id("hierarchy-edit-element");//pencil on right hand side for each unit
        
        By expand_icon_locator = By.CssSelector("a[uib-tooltip = 'Expand / collapse']"); //chevron on left side of unit/list that is either right or downwards facing.
        By children_locator = By.CssSelector("li[class='ng-scope angular-ui-tree-node']");
        By ng_children_locator = NgBy.Repeater("node in node.descendants");
        By coursename_locator = By.ClassName("course-code");
        By year_locator = By.CssSelector("list-year");

        //Once edit button is clicked the following action buttons are available.
        By delete_button_locator = By.CssSelector("button[uib-tooltip = 'Delete']"); //delete button (garbage pail) locator for a give containter.
        By add_child_button_locator = By.CssSelector("button[uib-tooltip = 'Add child Unit or List']");
        By edit_metadata_button_locator = By.CssSelector("button[uib-tooltip = 'Edit metadata']");
        By edit_userroles_button_locator = By.CssSelector("button[uib-tooltip = 'Edit user roles']");
        By suppress_indicator_locator = By.CssSelector("button[ng-class='getState().colour'] ");
        //    By suppressed_button_locator = By.CssSelector("button[uib-tooltip = 'Unsuppress [Hidden, branch suppressed]']");
        //     By unsuppressed_button_locator = By.CssSelector("button[uib-tooltip = 'Suppress [Currently visible]']");

        By uniticon_locator = By.CssSelector("span[uib-tooltip = 'Unit']");
        By listicon_locator = By.CssSelector("span[uib-tooltip = 'List']");
        By Dragicon_locator = By.CssSelector("a[uib-tooltip = 'Move this list']");

        //SubMenus of action buttons:
        By unit_button_locator = By.CssSelector("button[uib-tooltip = 'Add a child Unit']");
        By list_button_locator = By.CssSelector("button[uib-tooltip = 'Add a child List']");
        By cancel_delete_button_locator = By.CssSelector("button[uib-tooltip = 'Cancel delete']");
        By confirm_delete_button_locator = By.CssSelector("button[uib-tooltip = 'Confirm delete']");

        //ManageUsersModal - rows of existing users and found users.
        By add_user_button_locator = By.ClassName("add-user-to-list");
        By user_details_name = By.ClassName("user-details");
        By user_row = By.ClassName("list-group-item");
        By orange_role_buttons_locator = By.CssSelector("label[uib-tooltip-html ='getTooltip(permissive.id)']"); //returns the orange role labels.
        By blue_role_buttons_locator = By.CssSelector("label[uib-tooltip-html ='getTooltip(nonPermissive.id)']"); //returns the blue role labels.

        //find all unit names on hierarchy page 
        //  [FindsBy(How = How.CssSelector, Using = "li[class='ng-scope angular-ui-tree-node']")]  using protractor-net call instead
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByRepeater), Using = "node in adminHierarchy.hS.hierarchy")]
        public IList<IWebElement> initiallistofunits
        { get; set; }
        [FindsBy(How = How.Custom, CustomFinderType = typeof(NgByRepeater), Using = "node in node.descendants")]
        public IList<IWebElement> initiallistofdecendants
        { get; set; }
        [FindsBy(How = How.Id, Using = "tree-root-1")]
        public IWebElement initiallistsofunits_movefrom
        { get; set; }

        [FindsBy(How = How.Id, Using = "tree-root-2")]
        private IWebElement initiallistsofunits_moveto
        { get; set; }

        /*****************************************************************************
         *                       Buttons at the top of the hierarchy page
         *****************************************************************************/
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip ='Add new unit']")]
        protected IWebElement addnewunitbtn
        {
            get;
            set;
        }

        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip ='Modify list properties']")]
        protected IWebElement mainmodifylistpropertiesbtn
        {
            get;
            set;
        }

        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip ='Move lists']")]
        protected IWebElement mainmovelistsbtn
        {
            get;
            set;
        }


        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip ='Clone lists']")]
        protected IWebElement mainclonelistsbtn
        {
            get;
            set;
        }
        [FindsBy(How = How.CssSelector, Using = "button[uib-tooltip ='Rollover lists']")]
        protected IWebElement mainrolloverlistsbtn
        {
            get;
            set;
        }
        /*********************************************************************
         *     Manage Users MOdal
         *******************************************************************/

        [FindsBy(How = How.Id, Using = "list-user-roles-modal")]
        protected IWebElement listuserroles_modal
        {
            get;
            set;
        }


        [FindsBy(How = How.ClassName, Using = "modal-title")]
        protected IWebElement listuserroles_modaltitle
        {
            get;
            set;
        }

        [FindsBy(How = How.Id, Using = "user-search")]
        protected IWebElement listuserinput
        {
            get;
            set;
        }

        [FindsBy(How = How.Id, Using = "new-users-panel")]
        protected IWebElement newusersfoundpanel
        { get; set; }

        [FindsBy(How = How.Id, Using = "existing-users-panel")]
        protected IWebElement associateduserspanel
        { get; set; }

        [FindsBy(How = How.ClassName, Using = "finish-button")]
        protected IWebElement finishbutton
        { get; set; }



        /*****************************************************************************
*                     FOR FILLING IN NEW UNIT FORM and NEW LIST FORM
*****************************************************************************/
        [FindsBy(How = How.CssSelector, Using = "input[name='listname']")] //same one used for creating a new list as well.
        protected IWebElement UnitNameInput
        {
            get;
            set;
        }

        [FindsBy(How = How.Id, Using = "list-course-identifier")]
        protected IWebElement UnitCourseIdentifierInput
        {
            get;
            set;
        }
        //       [FindsBy(How = How.XPath, Using = "//form[@id='metaForm']/div[3]/input")]

        [FindsBy(How = How.CssSelector, Using = "input[ng-model='priority.ratio.books']")]
        protected IWebElement UnitRatioBooksInput
        {
            get;
            set;
        }
        //  [FindsBy(How = How.XPath, Using = "//form[@id='metaForm']/div[3]/input[2]")]
        [FindsBy(How = How.CssSelector, Using = "input[ng-model='priority.ratio.students']")]

        protected IWebElement UnitRatioStudentsInput
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "list-ratio-students")]
        // [FindsBy(How = How.XPath, Using = "//form[@id='metaForm']/div[4]/input")]
        protected IWebElement UnitTotalStudentsInput
        {
            get;
            set;
        }
        [FindsBy(How = How.Id, Using = "listyear")]
        // [FindsBy(How = How.XPath, Using = "//form[@id='metaForm']/div[7]/input")]
        protected IWebElement UnitYearInput
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

        /*******************************************************************
         *  Move lists locators
         * ****************************************************************/
        By listmoveicon = By.CssSelector("a[uib-tooltip ='Move this list']");

        By cloneicon = By.CssSelector("a[uib-tooltip ='Clone this list']");

        By rolllovericon = By.CssSelector("button[ng-click ='btnSlideout.toggle()']");
        By rolloverentirehierarchy = By.CssSelector("div[uib-tooltip = 'Rollover entire hierarchy']");

        By rolloverarchiveicon = By.CssSelector("button[uib-tooltip ='Archive rolled-over lists']");
        By rollovernoarchiveicon = By.CssSelector("button[uib-tooltip ='Do not archive rolled-over lists']");

        string statusreturntext;
        string searchresult;
        string FirstParent;
        string SecondParent;
        string ThirdParent;
        int MoveFromPosition = 0;
        //  int MoveToNewPosition = 0 ;
        int MoveToPosition = 0;
        int TotalChildren = 0;
        int listlength = 0;
        public bool CreateNewUnit(string unitname, string coursetextstring)
        {
            /* ********************************
             * Dont need this as FindUnit will take you to the hierarchy page
             * ***************************************

             ****************************************/


            if (FindUnit(unitname, coursetextstring) == null)
            {
                // string searchresult = SearchandReturnNewUnitName(unitname);
                // Thread.Sleep(KortextGlobals.s);
                Klick.On(addnewunitbtn);
                //  Console.WriteLine("clicking on add new unitbutn");
                Thread.Sleep(KortextGlobals.s);
                return FillInNew_UnitorList_Form(unitname, coursetextstring);
            }
            Console.WriteLine("Unit of that name already exists. Please remove and try again");
            return false;
        }

        private bool FillInNew_UnitorList_Form(string unitname, string coursetextstring)
        {
            //should put some error handling if not on Add Unit or Add List modal

            //  Thread.Sleep(KortextGlobals.s);
            WaitFind.FindElem(UnitNameInput, 10);
            UnitNameInput.SendKeys(unitname);
            WaitFind.FindElem(UnitCourseIdentifierInput, 10);
            UnitCourseIdentifierInput.SendKeys(coursetextstring);
            WaitFind.FindElem(UnitTotalStudentsInput, 10);
            UnitTotalStudentsInput.SendKeys(KortextGlobals.UnitTotalStudentsText);
            WaitFind.FindElem(UnitYearInput, 10);
            UnitYearInput.SendKeys(KortextGlobals.UnitYearText);
            Thread.Sleep(KortextGlobals.s);

            Klick.On(UnitFinishBtn);
            return true;
        }
        /*****************************************************************************
         * 
         *  FindUnit resets the hierarchy page then goes to the hierarchy and looks for the unit specified.
         *  It returns Null if not found or a container with the <unitname>
         *   ***********************************************************************/
        public IWebElement FindUnit(string unitname, string unitCourseIdentifierText)
        {

            // Thread.Sleep(KortextGlobals.s);
            if (!Pages.IsAt(PageName.PearlHierarchyPage))
            {
                Console.WriteLine("Not at Hierarchy Page-Trying again and Ignore the exception");
                //  Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();  Click on Menue handles getting to the hierarchy menu.
                Pages.LandingPage.ClickOnMenu_HierarchyBtn();
                //      Console.WriteLine("After trying again");

            }


            //   Thread.Sleep(KortextGlobals.s);
            foreach (IWebElement unitelement in initiallistofunits)
            {
                //get the units title text
                IList<IWebElement> unittitle = new List<IWebElement>(unitelement.FindElements(unit_title));

                //      Console.WriteLine("Number of unittitles found" + unittitle.Count);
                //  Driver.HighlightElement(unitelement);
                if (unittitle.Count > 0)
                {
                    //                 Console.WriteLine("This Unit = " + unittitle[0].Text);
                    if (unittitle[0].Text == unitname) //return the first one with the matching title
                    {
                        //  Console.WriteLine("Found unit:" + unittitle[0].Text);
                        return unitelement;
                    }
                }
            }
            //       Driver.DumpCurrentDOM();
            Console.WriteLine("Unable to find Unit " + unitname);
            return null;
        }
        public IWebElement FindSubChildUnit(string unitname, string childunitname, string unitcourseidentifiertext)
        {

            //returns each of the existing units on the page and looks for a match to the UnitName.
            //units can be many layers deep.
            //don't need to go to hierarchy start page as FindUnit will do that for us.

            //First - find the specified unit by name
            //    Thread.Sleep(KortextGlobals.s);
            IWebElement thisunit = FindUnit(unitname, unitcourseidentifiertext);
            //  Driver.HighlightElement(thisunit);
            //  Thread.Sleep(KortextGlobals.s);
            //   Console.WriteLine("About to enter expand");
            //expand the unit
            ExpandUnit(thisunit);
            IList<IWebElement> childrencontainers = new List<IWebElement>(thisunit.FindElements(children_locator));
            // Console.WriteLine("Children found " + childrencontainers.Count);
            if (childrencontainers.Count > 0)
            {
                foreach (IWebElement child in childrencontainers)
                {
                    //   Driver.HighlightElement(child);
                    //find the title of child
                    IWebElement unitchildrentitle = child.FindElement(unit_title);
                    //    Driver.HighlightElement(unitchildrentitle);
                    //    Console.WriteLine("Child title: " + unitchildrentitle.Text);
                    if (unitchildrentitle.Text == childunitname)
                    {
                        Console.WriteLine("Found unit:" + unitchildrentitle.Text);
                        return child;
                    }
                }
            }
            //       Driver.DumpCurrentDOM();
            Console.WriteLine("Unable to find Unit " + childunitname);
            return null;
        }

        public bool DeleteChild(string unitname, string childname, string unitcourseidentifiertext, string cancel_or_confirm)
        {


            //look for the unit name and return the container.
            IWebElement UnittoDelete = FindSubChildUnit(unitname, childname, unitcourseidentifiertext);

            //find the unit to be deleted
            if (UnittoDelete == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            //  Console.WriteLine("Justbeforedeleteelement" + UnittoDelete.Text);
            //its there and now find the delete button.
            //  Thread.Sleep(KortextGlobals.s);
            return DeleteThisElement(UnittoDelete, cancel_or_confirm);


        }


        public bool DeleteUnit(string unitname, string unitcourseidentifiertext, string cancel_or_confirm)
        {

            //   Console.WriteLine("In Delete Unit");
            //look for the unit name and return the container.
            IWebElement UnittoDelete = FindUnit(unitname, unitcourseidentifiertext);
            //find the unit to be deleted
            if (UnittoDelete == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            //its there and now find the delete button.
            return DeleteThisElement(UnittoDelete, cancel_or_confirm);



        }

        private bool DeleteThisElement(IWebElement UnittoDelete, string cancel_or_confirm)
        {
            Thread.Sleep(KortextGlobals.s);
            //click on the edit pencil to bring up the submenu
            //   Driver.DumpCurrentDOM();

            IWebElement trythis = UnittoDelete.FindElement(edit_icon_locator);
            //   Driver.HighlightElement(trythis);
            Klick.On(trythis);
            //IList<IWebElement> editElement = new List<IWebElement>(UnittoDelete.FindElements(edit_icon_locator));
            //should be the first one in the list.
            //   Console.WriteLine("Edit buttons count:" + editElement.Count);

            //Klick.On(editElement[0]); // this should bring up a delete icon and submenu.


            IWebElement deletebutton = UnittoDelete.FindElement(delete_button_locator);
            //If delete button is not disabled
            string disabledvalue = deletebutton.GetAttribute("Disabled");
            //     Console.WriteLine("Disabled attribute value: " + disabledvalue);
            if (disabledvalue == "true")
            {
                Console.WriteLine("Cannot delete this one - has lists attached ");
                return false;
            }
            //click on delete button
            Klick.On(deletebutton);
            //click on confirm
            switch (cancel_or_confirm)
            {

                case "Cancel":
                    IWebElement cancelbutton = UnittoDelete.FindElement(cancel_delete_button_locator);
                    Klick.On(cancelbutton);
                    return true;

                case "Confirm":
                    IWebElement confirmbutton = UnittoDelete.FindElement(confirm_delete_button_locator);
                    Klick.On(confirmbutton);
                    return true;
                default:
                    Console.WriteLine("Invalid cancel/confirm value: " + cancel_or_confirm);
                    return false;
            }


        }

        public bool DeleteWholeTree(string parentnode)
        {
            IWebElement TreetoDelete = FindUnit(parentnode, " ");
            if (TreetoDelete == null)
            {
                Console.WriteLine("Parent Unit " + parentnode + " does not exist");
                return false;
            }
            //found the parent - for each child, recursively expand it.
            ExpandUnit(TreetoDelete);
            IList<IWebElement> childrencontainers = new List<IWebElement>(TreetoDelete.FindElements(children_locator));
            // Console.WriteLine("here" + childrencontainers.Count);
            foreach (IWebElement node in childrencontainers)
            {
                //Console.WriteLine("InDeleteWholeTree****************************" + node.Text);
                ExpandWholeTreeAndDelete(node);
            }

            return DeleteThisElement(TreetoDelete, "Confirm");
        }
        private void ExpandWholeTreeAndDelete(IWebElement childnode)  //recursive function that returns when a node is not expandable
        {
            //Function assumes that you have found the parent already and expanded it.
            if (ExpandUnit(childnode))
            {            //For each of the children of this expanded node, call the recursive function ExpandWholeTree with the child nodes name.
                IList<IWebElement> childrencontainers = new List<IWebElement>(childnode.FindElements(children_locator));
                //   Console.WriteLine("Children found " + childrencontainers.Count);
                if (childrencontainers.Count > 0)
                {
                    foreach (IWebElement child in childrencontainers)
                    { //find the title of child
                        IWebElement unitchildrentitle = child.FindElement(unit_title);
                        //   Console.WriteLine("Children: " + child.Text);
                        //For each of the children of this expanded node, call the recursive function ExpandWholeTree with the child nodes name.
                        ExpandWholeTreeAndDelete(child);
                    }
                }
                else

                {
                    DeleteThisElement(childnode, "Confirm");
                    return;
                }

            }
            DeleteThisElement(childnode, "Confirm");
            return;
        }
        private bool ExpandUnit(IWebElement thisunit)
        {//assumes you are passing in the IwebElement that you want expanded.
            //Console.WriteLine("Inside expand" + thisunit.Text);
            //if li collapsed="false"  - expand it.
            string collapsedvalue = thisunit.GetAttribute("collapsed");
            Console.WriteLine("collapsed value = " + collapsedvalue);
            //    Driver.DumpCurrentDOM();
            if (collapsedvalue == "true")
            {// Expand the unit (left side arrow a[uib - tooltip = 'Expand / collapse']
                IWebElement expandicon = thisunit.FindElement(expand_icon_locator);
                //   Driver.HighlightElement(expandicon);
                //check if it went from right to down icon.
                Console.WriteLine("expandicon text " + expandicon.Text);


                //   HTML DOM code shows Collapsed = TRUE even when the Unit is expanded.  This is a workaround.
                if (expandicon.Text == "chevron_right")
                {
                    Klick.On(expandicon);
                }
                //  Console.WriteLine("Exiting Expand");
                return true;
            }
            else if (collapsedvalue == "false")
            {
                Console.WriteLine("Unit was already expanded");
                return true;
            }
            else
            {
                Console.WriteLine("Error in expanding unit");
                return false;
            }

        }
        private void ExpandWholeTree(IWebElement childnode)  //recursive function that returns when a node is not expandable
        {
            //Assumes you have found the parent node and have expanded it first before calling this function

            //Check if it is expandable or not    if expandable, expand node.
            //if not expandable, return 
            if (ExpandUnit(childnode))
            {            //For each of the children of this expanded node, call the recursive function ExpandWholeTree with the child nodes name.
                IList<IWebElement> childrencontainers = new List<IWebElement>(childnode.FindElements(children_locator));
                //   Console.WriteLine("Children found " + childrencontainers.Count);
                if (childrencontainers.Count > 0)
                {
                    foreach (IWebElement child in childrencontainers)
                    { //find the title of child
                        IWebElement unitchildrentitle = child.FindElement(unit_title);
                        //   Console.WriteLine("Children: " + child.Text);
                        //For each of the children of this expanded node, call the recursive function ExpandWholeTree with the child nodes name.
                        ExpandWholeTree(child);
                    }
                }
                else

                {
                    // DeleteThisElement(childnode, "Confirm");
                    return;
                }



            }
            // DeleteThisElement(childnode, "Confirm");
            return;
        }

        public bool AddChildUnitList(string unitname, string childunitname, string unitcourseidentifiertext, string unit_or_list)
        {

            // IWebElement thisunit = FindUnitandExposeActionBtns(unitname, unitcourseidentifiertext);
            IWebElement thisunit = FindUnit(unitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            //Found the unit
            //click on the edit pencil to bring up the submenu
            IList<IWebElement> edituniticon = new List<IWebElement>(thisunit.FindElements(edit_icon_locator));

            //   Console.WriteLine("Edit buttons count:" + addnewiteminsectionicon.Count);

            Klick.On(edituniticon[0]);

            IWebElement addchildbutton = thisunit.FindElement(add_child_button_locator);
            //Pick either unit or List.
            Klick.On(addchildbutton);
            //click on confirm
            switch (unit_or_list)
            {
                case "Unit":
                    IWebElement unitbutton = thisunit.FindElement(unit_button_locator);
                    Klick.On(unitbutton);
                    return FillInNew_UnitorList_Form(childunitname, unitcourseidentifiertext);

                case "List":
                    IWebElement listbutton = thisunit.FindElement(list_button_locator);

                    Klick.On(listbutton);
                    Thread.Sleep(KortextGlobals.s);
                    return FillInNew_UnitorList_Form(childunitname, unitcourseidentifiertext);
                default:
                    Console.WriteLine("Invalid unit/list value: " + unit_or_list);
                    return false;
            }

        }

        public bool EditUnitMetadata(string unitname, string unitcourseidentifiertext, FieldToChange field, string newvalue)
        {
            //go to the hierarchy page
            //find unitname
            IWebElement thisunit = FindUnit(unitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            //Found the unit
            //click on the edit pencil to bring up the submenu
            IList<IWebElement> edituniticon = new List<IWebElement>(thisunit.FindElements(edit_icon_locator));
            Klick.On(edituniticon[0]);
            //Click on Edit Unit Metadata button
            IWebElement editmetadatabtn = thisunit.FindElement(edit_metadata_button_locator);
            Driver.HighlightElement(editmetadatabtn);
            Klick.On(editmetadatabtn);
            //figure out which field to change.

            switch (field)
            {
                case FieldToChange.Name:
                    Thread.Sleep(KortextGlobals.s);
                    WaitFind.FindElem(UnitNameInput, 10);
                    UnitNameInput.Clear();
                    UnitNameInput.SendKeys(newvalue);
                    WaitFind.FindElem(UnitCourseIdentifierInput, 10);
                    Thread.Sleep(KortextGlobals.s);
                    Klick.On(UnitFinishBtn);
                    return true;

                case FieldToChange.CourseIdentifier:
                    Thread.Sleep(KortextGlobals.s);
                    WaitFind.FindElem(UnitCourseIdentifierInput, 10);
                    UnitCourseIdentifierInput.Clear();
                    UnitCourseIdentifierInput.SendKeys(newvalue);
                    Thread.Sleep(KortextGlobals.s);
                    Klick.On(UnitFinishBtn);
                    return true;
                case FieldToChange.Year:
                    Thread.Sleep(KortextGlobals.s);
                    WaitFind.FindElem(UnitYearInput, 10);
                    UnitYearInput.Clear();
                    UnitYearInput.SendKeys(newvalue);
                    Thread.Sleep(KortextGlobals.s);
                    Klick.On(UnitFinishBtn);
                    return true;
                default:
                    Console.WriteLine("Invalid field name used");
                    return false;
            }

        }

        public bool ValidateMetadataChange(string unitname, string unitcourseidentifiertext, FieldToChange field, string newvalue)
        {
            //go to the hierarchy page
            //find unitname

            IWebElement thisunit = FindUnit(unitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }

            //Click on Edit Unit Metadata button and open up edit modal
            //   IWebElement editmetadatabtn = thisunit.FindElement(edit_metadata_button_locator);
            //   Driver.HighlightElement(editmetadatabtn);
            //    Klick.On(editmetadatabtn);
            //   Driver.DumpCurrentDOM();
            switch (field)
            {
                case FieldToChange.Name:
                    Console.WriteLine("Please use Find functions to validate Unit Name has changed");
                    return false;
                case FieldToChange.CourseIdentifier:
                    Thread.Sleep(KortextGlobals.s);
                    //  Console.WriteLine("Just before locating course");
                    string coursename = thisunit.FindElement(coursename_locator).Text;
                    //Console.WriteLine("course name found: " + coursename);
                    Driver.HighlightElement(thisunit.FindElement(coursename_locator));
                    //   Console.WriteLine(coursename);
                    return coursename == "[" + newvalue + "]";

                case FieldToChange.Year:
                    Thread.Sleep(KortextGlobals.s);
                    string yearvalue = thisunit.FindElement(year_locator).Text;
                    Console.WriteLine("Year value found:" + yearvalue);


                    //manipulate newvalue into 2 digit year.
                    string newyearshort = newvalue.Substring(2);
                    //manipulate the year value on screen into 2 digit year ie 18/19 -> 18
                    string[] splits = yearvalue.Split(new char[] { '/' });
                    string splityear = splits[0].Substring(1);
                    //  int newyear = 
                    return splityear == newyearshort;
                default:
                    Console.WriteLine("Invalid field name used");
                    return false;
            }


        }

        public bool EditChildMetadata(string unitname, string childunitname, string unitcourseidentifiertext, FieldToChange field, string newvalue)
        {
            //look for the unit name and return the container.
            IWebElement thisunit = FindSubChildUnit(unitname, childunitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Child name " + unitname + " does not exist");
                return false;
            }
            //found the child now edit the fields.
            //click on the edit pencil to bring up the submenu
            IList<IWebElement> edituniticon = new List<IWebElement>(thisunit.FindElements(edit_icon_locator));
            Klick.On(edituniticon[0]);
            //Click on Edit Unit Metadata button
            IWebElement editmetadatabtn = thisunit.FindElement(edit_metadata_button_locator);
            // Driver.HighlightElement(editmetadatabtn);
            Klick.On(editmetadatabtn);
            switch (field)
            {
                case FieldToChange.Name:
                    Thread.Sleep(KortextGlobals.s);
                    WaitFind.FindElem(UnitNameInput, 10);
                    UnitNameInput.Clear();
                    UnitNameInput.SendKeys(newvalue);
                    WaitFind.FindElem(UnitCourseIdentifierInput, 10);
                    Thread.Sleep(KortextGlobals.s);
                    Klick.On(UnitFinishBtn);

                    return true;

                case FieldToChange.CourseIdentifier:
                    Thread.Sleep(KortextGlobals.s);
                    WaitFind.FindElem(UnitCourseIdentifierInput, 10);
                    UnitCourseIdentifierInput.Clear();
                    UnitCourseIdentifierInput.SendKeys(newvalue);
                    Thread.Sleep(KortextGlobals.s);
                    Klick.On(UnitFinishBtn);
                    Console.WriteLine("Edited child course name");
                    return true;
                case FieldToChange.Year:
                    Thread.Sleep(KortextGlobals.s);
                    WaitFind.FindElem(UnitYearInput, 10);
                    UnitYearInput.Clear();
                    UnitYearInput.SendKeys(newvalue);
                    Thread.Sleep(KortextGlobals.s);
                    Klick.On(UnitFinishBtn);
                    return true;
                default:
                    Console.WriteLine("Invalid field name used");
                    return false;
            }
        }

        public bool ValidateChildMetadataChange(string unitname, string childunitname, string unitcourseidentifiertext, FieldToChange field, string newvalue)
        {
            IWebElement thisunit = FindSubChildUnit(unitname, childunitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }

            //Click on Edit Unit Metadata button and open up edit modal
            //   IWebElement editmetadatabtn = thisunit.FindElement(edit_metadata_button_locator);
            //   Driver.HighlightElement(editmetadatabtn);
            //    Klick.On(editmetadatabtn);
            //   Driver.DumpCurrentDOM();
            switch (field)
            {
                case FieldToChange.Name:
                    Console.WriteLine("Please use Find functions to validate Unit Name has changed");
                    return false;
                case FieldToChange.CourseIdentifier:
                    Thread.Sleep(KortextGlobals.s);
                    //  Console.WriteLine("Just before locating course");
                    string coursename = thisunit.FindElement(coursename_locator).Text;
                    //Console.WriteLine("course name found: " + coursename);
                    Driver.HighlightElement(thisunit.FindElement(coursename_locator));
                    //   Console.WriteLine(coursename);
                    return coursename == "[" + newvalue + "]";

                case FieldToChange.Year:
                    Thread.Sleep(KortextGlobals.s);
                    string yearvalue = thisunit.FindElement(year_locator).Text;
                    Console.WriteLine("Year value found:" + yearvalue);


                    //manipulate newvalue into 2 digit year.
                    string newyearshort = newvalue.Substring(2);
                    //manipulate the year value on screen into 2 digit year ie 18/19 -> 18
                    string[] splits = yearvalue.Split(new char[] { '/' });
                    string splityear = splits[0].Substring(1);
                    //  int newyear = 
                    return splityear == newyearshort;
                default:
                    Console.WriteLine("Invalid field name used");
                    return false;
            }
        }

        public bool Suppressunit(string unitname, string unitcourseidentifiertext)
        {
            IWebElement thisunit = FindUnit(unitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            //Found the unit so click on the edit pencil to bring up the submenu
            return SuppressThis(thisunit);
        }

        private bool SuppressThis(IWebElement thisunit)
        {
            IList<IWebElement> edituniticon = new List<IWebElement>(thisunit.FindElements(edit_icon_locator));
            Klick.On(edituniticon[0]);
            //Click on Suppress button
            IWebElement suppressbtn = thisunit.FindElement(suppress_indicator_locator);
            //      Driver.HighlightElement(suppressbtn);
            Console.WriteLine(suppressbtn.GetAttribute("uib-tooltip"));
            String currentstate = suppressbtn.GetAttribute("uib-tooltip");
            if (currentstate == "Suppress [Currently visible]")
            {
                Klick.On(suppressbtn);

            }
            else
            {
                Console.WriteLine("Suppress button already engaged.  Please reset and try again");
                return false;
            }
            //Make sure it actually changed
            String newstate = suppressbtn.GetAttribute("uib-tooltip");
            if (newstate == "Suppress [Currently visible]")
            {
                Console.WriteLine("Tried but unable to suppress unit");

                return false;
            }
            return true;
        }
        public bool UnSuppressunit(string unitname, string unitcourseidentifiertext)
        {
            IWebElement thisunit = FindUnit(unitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            return UnSuppressThis(thisunit);

        }
        private bool UnSuppressThis(IWebElement thisunit)
        {  //Found the unit so click on the edit pencil to bring up the submenu
            IList<IWebElement> edituniticon = new List<IWebElement>(thisunit.FindElements(edit_icon_locator));
            Klick.On(edituniticon[0]);
            //Click on Suppress button
            IWebElement suppressbtn = thisunit.FindElement(suppress_indicator_locator);
            //      Driver.HighlightElement(suppressbtn);
            Console.WriteLine(suppressbtn.GetAttribute("uib-tooltip"));
            String currentstate = suppressbtn.GetAttribute("uib-tooltip");
            //  Console.WriteLine("Unsuppress[Hidden, branch suppressed]:" + currentstate + ":");
            if (currentstate == "Unsuppress [Hidden, branch suppressed]")
            {
                Klick.On(suppressbtn);

            }
            else
            {
                Console.WriteLine("UnSuppress button already engaged.  Please reset and try again");
                return false;
            }
            //Make sure it actually changed
            String newstate = suppressbtn.GetAttribute("uib-tooltip");
            Console.WriteLine("New state: " + newstate);
            if (newstate == "Unsuppress [Hidden, branch suppressed]")
            {
                Console.WriteLine("Tried but unable to suppress unit");

                return false;
            }
            return true;

        }
        public bool Suppresschild(string unitname, string childunitname, string unitcourseidentifiertext)
        {
            IWebElement thisunit = FindSubChildUnit(unitname, childunitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            return SuppressThis(thisunit);
        }

        public bool UnSuppresschild(string unitname, string childunitname, string unitcourseidentifiertext)
        {
            IWebElement thisunit = FindSubChildUnit(unitname, childunitname, unitcourseidentifiertext);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            return UnSuppressThis(thisunit);
        }
        public bool PickUserRoles(string usertoaddtounit, string bluerole, string orangerole)
        {
            //check name added to top panel
            IWebElement thisassociateduser = FindUserinModal("Associated", usertoaddtounit);

            if (thisassociateduser == null)
            {
                Console.WriteLine("User was not added to the associated list");
                return false;
            }

            //select orange and blue roles
            IList<IWebElement> orangerolesbtn = new List<IWebElement>(thisassociateduser.FindElements(orange_role_buttons_locator));
            IList<IWebElement> bluerolesbtn = new List<IWebElement>(thisassociateduser.FindElements(blue_role_buttons_locator));

            switch (bluerole)
            {
                case "Leader":
                    Klick.On(bluerolesbtn[0]);
                    break;
                case "Owner":
                    Klick.On(bluerolesbtn[1]);
                    break;
                default:
                    Console.WriteLine("Invalid blue role passed in: " + bluerole);
                    return false;
            }

            switch (orangerole)
            {
                case "Author":
                    Klick.On(orangerolesbtn[0]);
                    break;
                case "Moderator":
                    Klick.On(orangerolesbtn[1]);
                    break;
                case "Editor":
                    Klick.On(orangerolesbtn[2]);
                    break;
                default:
                    Console.WriteLine("Invalid orange role passed in: " + orangerole);
                    return false;
            }
            return true;
        }

        public bool AddUserToParentUnit(string usertoaddtounit, string unitname, string bluerole, string orangerole)
        {
            FindUnitAndOpenManageUsers(unitname);

            //Find the user from a list of new users to add to list.
            WaitFind.FindElem(listuserinput, 10).Clear();
            Klick.On(listuserinput);
            listuserinput.SendKeys(usertoaddtounit);
            Thread.Sleep(KortextGlobals.s);


            IWebElement thisone = FindUserinModal("New", usertoaddtounit);
            //Found the first user that contains the user name passed in. Now click the add to list button.
            IWebElement thisbutton = thisone.FindElement(add_user_button_locator);
            Driver.HighlightElement(thisbutton);
            Klick.On(thisbutton);
            if (!PickUserRoles(usertoaddtounit, bluerole, orangerole))
            {
                Console.WriteLine("Unable to set user roles");
                return false;
            }


            //click on the finish button
            Driver.HighlightElement(finishbutton);
            Klick.On(finishbutton);

            return true;
        }

        private IWebElement FindUserinModal(string panel, string usertoaddtounit)
        {
            IWebElement whichpanel = null;


            if (panel == "New")
            {
                whichpanel = newusersfoundpanel;
            }
            else if (panel == "Associated")
            {
                whichpanel = associateduserspanel;
            }
            else
            {
                Console.WriteLine("Incorrect panel sent in: " + panel + ". Should be either Associated or New");
                return null;
            }
            Thread.Sleep(KortextGlobals.s);
            IList<IWebElement> listofusers = new List<IWebElement>(whichpanel.FindElements(user_row));

            //   Console.WriteLine("Number of users found" + listofusersavail.Count);
            //Add the first matching user to top portion of modal
            if (listofusers.Count > 0)
            {
                foreach (IWebElement userrow in listofusers)
                {

                    // Console.WriteLine("Found user:" + userrow.Text);
                    //find the \name of the user in this row
                    IWebElement thisone = userrow.FindElement(user_details_name);
                    //    Driver.HighlightElement(unitchildrentitle);
                    //   Console.WriteLine("this user is: " + thisone.Text);
                    if (thisone.Text.Contains(usertoaddtounit))  // will match multiple ones with contains. alternative is to pass in the user login as well as the user name.
                    {
                        //         Console.WriteLine("Found user:" + thisone.Text);
                        //Take the first one we find that contains the user name and click on the add users button.

                        return userrow;
                    }
                }
            }
            Console.WriteLine("Unable to find user: " + usertoaddtounit + "In panel: " + panel);
            return null;
        }

        private bool FindUnitAndOpenManageUsers(string unitname)
        {
            //find the unit
            //find unitname
            Thread.Sleep(KortextGlobals.s);
            IWebElement thisunit = FindUnit(unitname, KortextGlobals.UnitCourseIdentifierText);
            if (thisunit == null)
            {
                Console.WriteLine("Unit name " + unitname + " does not exist");
                return false;
            }
            //Found the unit      
            //open for editing - click on the edit pencil to bring up the submenu
            IList<IWebElement> edituniticon = new List<IWebElement>(thisunit.FindElements(edit_icon_locator));
            Klick.On(edituniticon[0]);

            //click on userroles button
            IWebElement editlistrolesbtn = thisunit.FindElement(edit_userroles_button_locator);
            //manage user window pops up.
            Thread.Sleep(KortextGlobals.s);
            Klick.On(editlistrolesbtn);
            if (!listuserroles_modal.Displayed) //probably will crash is it doesn't exist
            {
                Console.WriteLine("Trouble finding Manage User modal");
                return false;
            }
            //    Driver.HighlightElement(listuserroles_modaltitle);
            //  Console.WriteLine("Title:" + listuserroles_modaltitle.Text + "End");
            if (!listuserroles_modaltitle.Text.Equals("Manage users"))
            {
                Console.WriteLine("Problem with Manage Users Modal title");
                return false;
            }
            return true;

        }
        private bool FindChildAndOpenManageUsers(string unitname, string childname)
        {
            IWebElement thisunit = FindSubChildUnit(unitname, childname, KortextGlobals.UnitCourseIdentifierText);
            if (thisunit == null)
            {
                Console.WriteLine("Child name " + unitname + " does not exist");
                return false;
            }
            //found the child now edit the fields.
            //click on the edit pencil to bring up the submenu
            IList<IWebElement> edituniticon = new List<IWebElement>(thisunit.FindElements(edit_icon_locator));
            Klick.On(edituniticon[0]);
            //click on userroles button
            IWebElement editlistrolesbtn = thisunit.FindElement(edit_userroles_button_locator);
            //manage user window pops up.
            Thread.Sleep(KortextGlobals.s);
            Klick.On(editlistrolesbtn);
            if (!listuserroles_modal.Displayed) //probably will crash is it doesn't exist
            {
                Console.WriteLine("Trouble finding Manage User modal");
                return false;
            }
            //    Driver.HighlightElement(listuserroles_modaltitle);
            //  Console.WriteLine("Title:" + listuserroles_modaltitle.Text + "End");
            if (!listuserroles_modaltitle.Text.Equals("Manage users"))
            {
                Console.WriteLine("Problem with Manage Users Modal title");
                return false;
            }
            return true;

        }

        public bool ValidateUserRoles(string usertoaddtounit, string unitname, string bluerole, string orangerole)
        {
            bool RoleOK = false;
            FindUnitAndOpenManageUsers(unitname);

            //check name added to top panel
            IWebElement thisassociateduser = FindUserinModal("Associated", usertoaddtounit);

            if (thisassociateduser == null)
            {
                Console.WriteLine("User was not added to this Unit/List");
                return false;
            }

            //select orange and blue roles
            IList<IWebElement> orangerolesbtn = new List<IWebElement>(thisassociateduser.FindElements(orange_role_buttons_locator));
            IList<IWebElement> bluerolesbtn = new List<IWebElement>(thisassociateduser.FindElements(blue_role_buttons_locator));

            switch (bluerole)
            {
                case "Leader":
                    if (bluerolesbtn[0].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Leader was supposed to be selected but it is not");
                    return false;

                case "Owner":
                    if (bluerolesbtn[1].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Owner was supposed to be selected but it is not");
                    return false;
                default:
                    Console.WriteLine("Invalid blue role passed in: " + bluerole);
                    return false;
            }
            if (!RoleOK) return false;

            switch (orangerole)
            {
                case "Author":
                    if (orangerolesbtn[0].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Author was supposed to be selected but it is not");
                    return false;
                case "Moderator":
                    if (orangerolesbtn[1].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Moderator was supposed to be selected but it is not");
                    return false;
                case "Editor":
                    if (orangerolesbtn[2].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Editor was supposed to be selected but it is not");
                    return false;
                default:
                    Console.WriteLine("Invalid orange role passed in: " + orangerole);
                    return false;
            }
            //click on the finish button
            Driver.HighlightElement(finishbutton);
            Klick.On(finishbutton);
            return RoleOK;
        }

        public bool EditUserRolesUnit(string usertoaddtounit, string unitname, string bluerole, string orangerole)
        {
            if (!FindUnitAndOpenManageUsers(unitname))
            {
                Console.WriteLine("Unable to find Unit: " + unitname);
                return false;
            }
            if (!PickUserRoles(usertoaddtounit, bluerole, orangerole))
            {
                Console.WriteLine("Unable to set user roles");
                return false;
            }

            //click on the finish button
            Thread.Sleep(KortextGlobals.s);
            Driver.HighlightElement(finishbutton);
            Klick.On(finishbutton);
            return true;
        }
        public bool ValidateChildUserRoles(string usertoaddtounit, string unitname, string childname, string bluerole, string orangerole)
        {
            bool RoleOK = false;
            if (!FindChildAndOpenManageUsers(unitname, childname))
            {
                Console.WriteLine("Unable to find: " + unitname + "->" + childname);
                return false;
            }

            //check name added to top panel
            IWebElement thisassociateduser = FindUserinModal("Associated", usertoaddtounit);

            if (thisassociateduser == null)
            {
                Console.WriteLine("User was not added to the associated list");
                return false;
            }

            //select orange and blue roles
            IList<IWebElement> orangerolesbtn = new List<IWebElement>(thisassociateduser.FindElements(orange_role_buttons_locator));
            IList<IWebElement> bluerolesbtn = new List<IWebElement>(thisassociateduser.FindElements(blue_role_buttons_locator));

            switch (bluerole)
            {
                case "Leader":
                    if (bluerolesbtn[0].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Leader was supposed to be selected but it is not");
                    return false;

                case "Owner":
                    if (bluerolesbtn[1].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Owner was supposed to be selected but it is not");
                    return false;
                default:
                    Console.WriteLine("Invalid blue role passed in: " + bluerole);
                    return false;
            }
            if (!RoleOK) return false;

            switch (orangerole)
            {
                case "Author":
                    if (orangerolesbtn[0].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Author was supposed to be selected but it is not");
                    return false;
                case "Moderator":
                    if (orangerolesbtn[1].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Moderator was supposed to be selected but it is not");
                    return false;
                case "Editor":
                    if (orangerolesbtn[2].GetAttribute("class").Contains("active"))
                    { //then the button is selected
                        RoleOK = true;
                        break;
                    }
                    Console.WriteLine("Role button Editor was supposed to be selected but it is not");
                    return false;
                default:
                    Console.WriteLine("Invalid orange role passed in: " + orangerole);
                    return false;
            }
            //click on the finish button
            Driver.HighlightElement(finishbutton);
            Klick.On(finishbutton);
            return RoleOK;
        }

        public bool AddUserToChild(string usertoaddtounit, string unitname, string childname, string bluerole, string orangerole)
        {


            if (!FindChildAndOpenManageUsers(unitname, childname))
            {
                Console.WriteLine("Unable to find: " + unitname + "->" + childname);
                return false;
            }
            Thread.Sleep(KortextGlobals.s);
            //Find the user from a list of new users to add to list.
            WaitFind.FindElem(listuserinput, 10).Clear();
            Klick.On(listuserinput);
            listuserinput.SendKeys(usertoaddtounit);
            Thread.Sleep(KortextGlobals.s);


            IWebElement thisone = FindUserinModal("New", usertoaddtounit);
            //Found the first user that contains the user name passed in. Now click the add to list button.
            IWebElement thisbutton = thisone.FindElement(add_user_button_locator);
            Driver.HighlightElement(thisbutton);
            Klick.On(thisbutton);
            if (!PickUserRoles(usertoaddtounit, bluerole, orangerole))
            {
                Console.WriteLine("Unable to set user roles");
                return false;
            }


            //click on the finish button
            Driver.HighlightElement(finishbutton);
            Klick.On(finishbutton);

            return true;
        }



        public bool EditUserRolesChild(string usertoaddtounit, string unitname, string childname, string bluerole, string orangerole)
        {
            if (!FindChildAndOpenManageUsers(unitname, childname))
            {
                Console.WriteLine("Unable to find: " + unitname + "->" + childname);
                return false;
            }
            if (!PickUserRoles(usertoaddtounit, bluerole, orangerole))
            {
                Console.WriteLine("Unable to set user roles");
                return false;
            }

            //click on the finish button
            Thread.Sleep(KortextGlobals.s);
            Driver.HighlightElement(finishbutton);
            Klick.On(finishbutton);
            return true;
        }

        private IWebElement FindMoveFromParent(string unitname)
        {
            // 
            List<IWebElement> thisblock = new List<IWebElement>(initiallistsofunits_movefrom.FindElements(NgBy.Repeater("node in adminHierarchy.hS.hierarchy")));
                  //  Console.WriteLine("Number of units found" + thisblock.Count);
            listlength = thisblock.Count;
            foreach (IWebElement unitelement in thisblock)
            {
                IList<IWebElement> unittitle = new List<IWebElement>(unitelement.FindElements(unit_title));

                //get the units title text
              //  Thread.Sleep(KortextGlobals.s);
                {
                    //                 Console.WriteLine("This Unit = " + unittitle[0].Text);
                    if (unittitle[0].Text == unitname) //return the first one with the matching title
                    {
                        Console.WriteLine("Found unit:" + unittitle[0].Text);
                        return unitelement;
                    }
                }
            }
            Console.WriteLine("Unable to find from Parent " + unitname);
            return null;
        }
        private IWebElement FindMoveToParent(string unitname)
        {
            // 
            List<IWebElement> thisblock = new List<IWebElement>(initiallistsofunits_moveto.FindElements(By.CssSelector("li[ng-repeat = 'node in adminHierarchy.hS.hierarchy']")));
            Console.WriteLine("Number of units found more to" + thisblock.Count);
            foreach (IWebElement unitelement in thisblock)
            {
                IList<IWebElement> unittitle = new List<IWebElement>(unitelement.FindElements(unit_title));

                //get the units title text
                Thread.Sleep(KortextGlobals.s);
                {
                    //                 Console.WriteLine("This Unit = " + unittitle[0].Text);
                    if (unittitle[0].Text == unitname) //return the first one with the matching title
                    {
                        Console.WriteLine("Found unit:" + unittitle[0].Text);
                        return unitelement;
                    }
                }
            }
            //       Driver.DumpCurrentDOM();
            Console.WriteLine("Unable to find To Unit " + unitname);
            return null;
        }
        private IWebElement FindMoveToParentPosition(int positionnumber)
        {
            List<IWebElement> thisblock = new List<IWebElement>(initiallistsofunits_moveto.FindElements(By.CssSelector("li[ng-repeat = 'node in adminHierarchy.hS.hierarchy']")));

            Console.WriteLine("Number of units found move to" + thisblock.Count);

            if(positionnumber > thisblock.Count)
            {
                Console.WriteLine("Invalid move to position used.  Position was: " + positionnumber + " but there are only " + thisblock.Count + " units");
                return null;
            }
            for (int i=0; i<thisblock.Count; i++)
            {
                if (i == positionnumber)
                {
                    return thisblock[i];
                }
            }
            Console.WriteLine("Unable to find To position " + positionnumber);
            return null;
        }
        private IWebElement FindElementInMove(string unitname, string childname, string to_or_from)
        {
            //returns each of the existing units on the page and looks for a match to the UnitName.
            IWebElement thisunit = null;
            //  IWebElement thisunit = FindUnit(unitname, "Do not Touch");

            switch (to_or_from)
            {
                case "From":
                    thisunit = FindMoveFromParent(unitname);
                    break;

                case "To":
                    thisunit = FindMoveToParent(unitname);
                    break;

                default:
                    Console.WriteLine("Invalid value used for to_or_From");
                    return null;
            }
            // Thread.Sleep(KortextGlobals.s);

            //  Driver.HighlightElement(thisunit);
            // Thread.Sleep(KortextGlobals.s);
            //   Console.WriteLine("About to enter expand");
            Thread.Sleep(KortextGlobals.s);
            //expand the unit
            ExpandUnit(thisunit);
            IList<IWebElement> childrencontainers = new List<IWebElement>(thisunit.FindElements(children_locator));
            TotalChildren = childrencontainers.Count;
            Console.WriteLine("Children found " + childrencontainers.Count);

            if (childrencontainers.Count > 0)
            {
                foreach (IWebElement child in childrencontainers)
                {
                    if (to_or_from == "To")
                        MoveFromPosition = MoveFromPosition + 1;
                    else if (to_or_from == "From")
                        MoveToPosition = MoveToPosition + 1;
                    else
                    {
                        Console.WriteLine("Invalid value used for to_or_from");
                        return null;
                    }
                    //    Console.WriteLine("From: " + MoveFromPosition + " To: " + MoveToPosition);

                    //    Driver.HighlightElement(child);
                    //find the title of child
                    IWebElement unitchildrentitle = child.FindElement(unit_title);
                    //    Driver.HighlightElement(unitchildrentitle);
                    //    Console.WriteLine("Child title: " + unitchildrentitle.Text);
                    if (unitchildrentitle.Text == childname)
                    {
                        Console.WriteLine("Found unit:" + unitchildrentitle.Text);
                        return child;
                    }
                }
            }
            //       Driver.DumpCurrentDOM();
            Console.WriteLine("Unable to find Unit " + childname);
            return null;
        }
        public bool MoveListToSubUnit(string unitname, string childname, string movetochild)
        {
            // Console.WriteLine("Inside move to Top");

            //Go to hierarchy page if not already there.
            if (!Pages.IsAt(PageName.PearlHierarchyPage))
            {
                Console.WriteLine("Not at Hierarchy Page-Trying again and Ignore the exception");
                //  Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
                Pages.LandingPage.ClickOnMenu_HierarchyBtn();
                //      Console.WriteLine("After trying again");

            }
            //click on the move icon top left
            try
            {
                // Thread.Sleep(KortextGlobals.ll);
                WaitFind.FindElem(mainmovelistsbtn, 60);
                Klick.On(mainmovelistsbtn);
               // Driver.ngDriver.WaitForAngular();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in MoveListToSubUnit(): " + e.Message);
            }
            //find the parent and child and expand

            IWebElement movethisunit = FindElementInMove(unitname, childname, "From");
            //  Driver.HighlightElement(movethisunit);
            IWebElement movetounit = FindElementInMove(unitname, movetochild, "To");
            //   Driver.HighlightElement(movetounit);
            //record it's current location - FindElementToMove populates MoveFromPosition
            //find count of children on that level
            //move it to the top.
            HierarchyDragAndDrop(movethisunit, movetounit);
            //Thread.Sleep(KortextGlobals.ll);
            var statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            // Console.WriteLine(statusreturntext);
            if (statusreturntext != "List updated")
            {
                Console.WriteLine("Error while moving list to unit." + statusreturntext);
                return false;
            }
            else
            {
                Console.WriteLine("Moving list successful");
                //Reset hierarchy back to main page ie out of move mode
                mainmodifylistpropertiesbtn.Click();
                // Driver.Instance.FindElement(edit_icon_locator).Click();
                return true;
            }
        }
        public bool MoveWholeTreeToAPosition(string unitname, String MoveTo)
        {
            int MoveToPosition = 0;
            //Go to hierarchy page if not already there.
            if (!Pages.IsAt(PageName.PearlHierarchyPage))
            {
                Console.WriteLine("Not at Hierarchy Page-Trying again and Ignore the exception");
                //  Pages.PearlRedMenuPage.ClickOnRebusLabelBtn();
                Pages.LandingPage.ClickOnMenu_HierarchyBtn();
                //      Console.WriteLine("After trying again");

            }
            //click on the move icon top left
            try
            {
                // Thread.Sleep(KortextGlobals.ll);
                WaitFind.FindElem(mainmovelistsbtn, 60);
                Klick.On(mainmovelistsbtn);
              //  Driver.ngDriver.WaitForAngular();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in MoveListToSubUnit(): " + e.Message);
            }


            //Find Tree to move on lefthand side.
            IWebElement movethisunit = FindMoveFromParent(unitname);
            Driver.HighlightElement(movethisunit);
            
            //translate the move to position to an integer based upon how long the list is.
            if (MoveTo == "Top")
            {
                MoveToPosition = 0;
            }
            else if (MoveTo == "Middle")
            {
                MoveToPosition = (listlength / 4) -2 ;
                Console.WriteLine(MoveToPosition);
            }
            else if (MoveTo == "End")
            {
                MoveToPosition = listlength/2;

            }
            else
            {
                Console.WriteLine("Unknown Move To Position used. Please use Top, Middle, End only");
                return false;
            }

            //Find the correct position on the righthand side
            IWebElement movetounit = FindMoveToParentPosition(MoveToPosition);
            HierarchyDragAndDrop(movethisunit, movetounit);
            //Thread.Sleep(KortextGlobals.ll);
            var statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            // Console.WriteLine(statusreturntext);
            if (statusreturntext != "List updated")
            {
                Console.WriteLine("Error while moving list to unit." + statusreturntext);
                return false;
            }
            else
            {
                Console.WriteLine("Moving list successful");
                //Reset hierarchy back to main page ie out of move mode
                mainmodifylistpropertiesbtn.Click();
                // Driver.Instance.FindElement(edit_icon_locator).Click();
                return true;
            }

        }

        public void HierarchyDragAndDrop(IWebElement sourcelocation, IWebElement destlocation)
        {
            // Thread.Sleep(KortextGlobals.ll); //Need to wait for all the blue boxed in bottom right to finish.
            //  ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, 700)");
            //  IList<IWebElement> source = Driver.Instance.FindElements(By.ClassName("angular-ui-tree-handle"));
            //   IList<IWebElement> destination = Driver.Instance.FindElements(By.ClassName("angular-ui-tree-handle"));
            Driver.HighlightElement(sourcelocation);
            Driver.HighlightElement(destlocation);
            //  Driver.DumpCurrentDOM();
            IWebElement moveme = sourcelocation.FindElement(listmoveicon);

            // Driver.HighlightElement(moveme);
            //Drag and Drop things
            Actions actions = new Actions(Driver.Instance);
            actions.MoveToElement(moveme).Build().Perform();
            actions.ClickAndHold(moveme).Build().Perform();
            //   actions.DragAndDrop(sourcelocation, destlocation).Build().Perform();
            actions.DragAndDrop(moveme, destlocation).Build().Perform();

        }

        public void HierarchyCloning(IWebElement sourcelocation, IWebElement destlocation)
        {
            Driver.HighlightElement(sourcelocation);
            Driver.HighlightElement(destlocation);
            
            IWebElement moveme = sourcelocation.FindElement(cloneicon);

            Driver.HighlightElement(moveme);
            //Drag and Drop things
            Actions actions = new Actions(Driver.Instance);
            actions.MoveToElement(moveme).Build().Perform();
            actions.ClickAndHold(moveme).Build().Perform();
            //   actions.DragAndDrop(sourcelocation, destlocation).Build().Perform();
            actions.DragAndDrop(moveme, destlocation).Build().Perform();

        }

        public IWebElement FindNodeInTree(IWebElement parentnode, string childnode)
        {
            //  Console.WriteLine("In Findnodeintree with " + childnode);
            foreach (IWebElement unitelement in parentnode.FindElements(NgBy.Repeater("node in node.descendants")))
            {
                //get the units title text
                IList<IWebElement> unittitle = new List<IWebElement>(unitelement.FindElements(unit_title));

                //      Console.WriteLine("Number of unittitles found" + unittitle.Count);
                Driver.HighlightElement(unitelement);
                if (unittitle.Count > 0)
                {
                    Console.WriteLine("This Unit = " + unittitle[0].Text);
                    if (unittitle[0].Text == childnode) //return the first one with the matching title
                    {
                        Console.WriteLine("Found unit:" + unittitle[0].Text);
                        return unitelement;
                    }
                }

            }
            Console.WriteLine(parentnode + " was not found or does not exist");

            return null;

        }
        public bool MoveListUnit()
        {
            try
            {
                Thread.Sleep(KortextGlobals.l);

                if (!createunitliststructure())
                {
                    Console.WriteLine("Error while Creating first Unit Structure");
                    return false;
                }
                else
                {
                    Console.WriteLine("Creating first Unit Structure Successful");
                }
                FirstParent = searchresult;

                if (!createunitliststructure())
                {
                    Console.WriteLine("Error while Creating second Unit Structure");
                    return false;
                }
                else
                {
                    Console.WriteLine("Creating second Unit Structure Successful");
                }
                SecondParent = searchresult;
                
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);
                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);
                
                //Move Lists - Moving to Same Unit - Move 2nd level list to 3rd level
                if (!MoveSourceToDest("List", FirstParent, FirstParent + "ChildList1", FirstParent, FirstParent + "ChildUnit2"))
                {
                    Console.WriteLine("Error while Moving 2nd level list to 3rd level.Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "ChildList1"))
                    {
                        Console.WriteLine("List not present while verification after Moving 2nd level list to 3rd level.Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving 2nd level list to 3rd level Successful.Same Unit");
                    }
                }

                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move list from 3rd level to 2nd level (directly under parent unit).
                if (!MoveSourceToDest("List", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildList2", "", FirstParent))
                {
                    Console.WriteLine("Error while Moving list from 3rd level to 2nd level.Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent, FirstParent + "GrandChildList2"))
                    {
                        Console.WriteLine("List not present while verification after Moving list from 3rd level to 2nd level.Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving list from 3rd level to 2nd level Successful.Same Unit");
                    }
                }
                
                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);
                
                //Move list to new sub unit
                if (!MoveSourceToDest("List", FirstParent, FirstParent + "ChildList2", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit2", FirstParent + "GrandGrandChildUnit2"))
                {
                    Console.WriteLine("Error while Moving list to new sub unit.Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit2," + FirstParent + "GrandGrandChildUnit2", FirstParent + "ChildList2"))
                    {
                        Console.WriteLine("List not present while verification after Moving list to new sub unit.Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving list to new sub unit Successful.Same Unit");
                    }
                }
                
                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Lists - Moving to Same Unit
                if (!MoveSourceToDest("List", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildList1", FirstParent + "," + FirstParent + "ChildUnit1", FirstParent + "GrandChildUnit1"))
                {
                    Console.WriteLine("Error while Moving List to Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildList1"))
                    {
                        Console.WriteLine("List not present while verification after Moving List to Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving List to Same Unit Successful");
                    }
                }

                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);
                
                //Move Lists - Moving to Different Unit
                if (!MoveSourceToDest("List", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildList1", SecondParent, SecondParent + "ChildUnit1"))
                {
                    Console.WriteLine("Error while Moving List to Different Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", SecondParent + "," + SecondParent + "ChildUnit1", FirstParent + "GrandGrandChildList1"))
                    {
                        Console.WriteLine("List not present while verification after Moving List to Different Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving List to Different Unit Successful");
                    }
                }
                
                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Lists - Moving to Different Unit - Move 2nd level list to 3rd level
                if (!MoveSourceToDest("List", SecondParent, SecondParent + "ChildList1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit1"))
                {
                    Console.WriteLine("Error while Moving 2nd level list to 3rd level.Different Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", SecondParent + "ChildList1"))
                    {
                        Console.WriteLine("List not present while verification after Moving 2nd level list to 3rd level.Different Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving 2nd level list to 3rd level Successful.Different Unit");
                    }
                }
                
                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Lists - Moving to Different Unit - Move list to new sub unit
                if (!MoveSourceToDest("List", SecondParent, SecondParent + "ChildList2", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit2", FirstParent + "GrandGrandChildUnit2"))
                {
                    Console.WriteLine("Error while Moving list to new sub unit.Different Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit2," + FirstParent + "GrandGrandChildUnit2", SecondParent + "ChildList2"))
                    {
                        Console.WriteLine("List not present while verification after Moving list to new sub unit.Different Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving list to new sub unit Successful.Different Unit");
                    }
                }

                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Lists - Moving to Different Unit - Move list from 3rd level to 2nd level (directly under parent unit).
                if (!MoveSourceToDest("List", SecondParent + "," + SecondParent + "ChildUnit2", SecondParent + "GrandChildList2", FirstParent, FirstParent + "ChildUnit1"))
                {
                    Console.WriteLine("Error while Moving list from 3rd level to 2nd level.Different Unit.");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit1", SecondParent + "GrandChildList2"))
                    {
                        Console.WriteLine("List not present while verification after Moving list from 3rd level to 2nd level.Different Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving list from 3rd level to 2nd level Successful.Different Unit.");
                    }
                }

                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Units - Moving to Same Unit
                if (!MoveSourceToDest("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildUnit1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit1"))
                {
                    Console.WriteLine("Error while Moving Unit to Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Moving Unit to Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Moving Unit to Same Unit Successful");
                    }
                }

                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Units - Move Units in same unit to various locations with different sized hierarchies
                if (!MoveSourceToDest("Unit", FirstParent + "," + FirstParent + "ChildUnit1", FirstParent + "GrandChildUnit1", FirstParent + "," + FirstParent + "ChildUnit1", FirstParent + "GrandChildUnit2"))
                {
                    Console.WriteLine("Error while Move Units in same unit to various locations with different sized hierarchies");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit2", FirstParent + "GrandChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Move Units in same unit to various locations with different sized hierarchies");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Move Units in same unit to various locations with different sized hierarchies Successful");
                    }                    
                }

                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Units - Move Units in same unit - Move 2nd level unit to 3rd level
                if (!MoveSourceToDest("Unit", FirstParent, FirstParent + "ChildUnit1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit1"))
                {
                    Console.WriteLine("Error while Move Units in same unit - Move 2nd level unit to 3rd level");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", FirstParent + "ChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Move Units in same unit - Move 2nd level unit to 3rd level");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Move Units in same unit - Move 2nd level unit to 3rd level Successful");
                    }                    
                }

                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Units - Move Units in different unit to various locations with different sized hierarchies
                if (!MoveSourceToDest("Unit", SecondParent + "," + SecondParent + "ChildUnit2", SecondParent + "GrandChildUnit1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit2"))
                {
                    Console.WriteLine("Error while Move Units in different unit to various locations with different sized hierarchies");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit2", SecondParent + "GrandChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Move Units in different unit to various locations with different sized hierarchies");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Move Units in different unit to various locations with different sized hierarchies Successful");
                    }                    
                }

                Klick.On(mainmovelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Move Units - Move Units in different  unit - Move 2nd level unit to 3rd level
                if (!MoveSourceToDest("Unit", SecondParent, SecondParent + "ChildUnit1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit2"))
                {
                    Console.WriteLine("Error while Move Units in different  unit - Move 2nd level unit to 3rd level");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit2", SecondParent + "ChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Move Units in different  unit - Move 2nd level unit to 3rd level");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Move Units in different  unit - Move 2nd level unit to 3rd level Successful");
                    }
                }

                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                if (!DeleteWholeTree(FirstParent))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + FirstParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Deleting Unit Structure Successful." + FirstParent);
                }
                
                if (!DeleteWholeTree(SecondParent))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + SecondParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Deleting Unit Structure Successful." + SecondParent);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in Hierarchy Move List/Unit: " + e.Message);
                return false;
            }            
        }

        public bool createunitliststructure()
        {
            Pages.LandingPage.ClickOnMenu_HierarchyBtn();
            Thread.Sleep(KortextGlobals.l);
            if (!Pages.IsAt(PageName.PearlHierarchyPage))
            {
                Console.WriteLine("Unable to reach Hierarchy Page");
                return false;
            }

            searchresult = Pages.PearlCreateReadingList.SearchandReturnNewUnitName("ParentUnit");

            if (!CreateNewUnit(searchresult, KortextGlobals.UnitCourseIdentifierText))
            {
                Console.WriteLine("Error while Creating Root Unit." + searchresult);
                return false;
            }

            List<NgWebElement> ParentUnitsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Repeater("node in adminHierarchy.hS.hierarchy")));
            if(ParentUnitsSearched.Count > 0)
            {
                foreach(NgWebElement ParentUnitSearched in ParentUnitsSearched)
                {
                    ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", ParentUnitSearched);
                    Driver.HighlightElement(ParentUnitSearched);
                    IWebElement ParentUnitName = ParentUnitSearched.FindElement(unit_title);
                    if(ParentUnitName.Text == searchresult)
                    {
                        if(!CreateList(ParentUnitSearched, searchresult + "ChildList1"))
                        {
                            Console.WriteLine("Error Creating List." + searchresult + "ChildList1");
                            return false;
                        }
                        if(!CreateList(ParentUnitSearched, searchresult + "ChildList2"))
                        { 
                            Console.WriteLine("Error Creating List." + searchresult + "ChildList2");
                            return false;
                        }
                        if (!CreateUnit(ParentUnitSearched, searchresult + "ChildUnit1"))
                        {
                            Console.WriteLine("Error Creating Unit." + searchresult + "ChildUnit1");
                            return false;
                        }
                        if(!CreateUnit(ParentUnitSearched, searchresult + "ChildUnit2"))
                        {
                            Console.WriteLine("Error Creating Unit." + searchresult + "ChildUnit2");
                            return false;
                        }

                        List<NgWebElement> ChildUnitsSearched = new List<NgWebElement>(ParentUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                        if(ChildUnitsSearched.Count > 0)
                        {
                            foreach(NgWebElement ChildUnitSearched in ChildUnitsSearched)
                            {
                                IWebElement ChildUnitName = ChildUnitSearched.FindElement(unit_title);
                                if (ChildUnitName.Text == (searchresult + "ChildUnit1"))
                                {
                                    if(!CreateList(ChildUnitSearched, searchresult + "GrandChildList1"))
                                    {
                                        Console.WriteLine("Error Creating List." + searchresult + "GrandChildList1");
                                        return false;
                                    }
                                    if(!CreateUnit(ChildUnitSearched, searchresult + "GrandChildUnit1"))
                                    {
                                        Console.WriteLine("Error Creating Unit." + searchresult + "GrandChildUnit1");
                                        return false;
                                    }
                                    if(!CreateUnit(ChildUnitSearched, searchresult + "GrandChildUnit2"))
                                    {
                                        Console.WriteLine("Error Creating Unit." + searchresult + "GrandChildUnit2");
                                        return false;
                                    }

                                    List<NgWebElement> GrandChildUnitsSearched = new List<NgWebElement>(ChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                    if(GrandChildUnitsSearched.Count > 0)
                                    {
                                        foreach(NgWebElement GrandChildUnitSearched in GrandChildUnitsSearched)
                                        {
                                            IWebElement GrandChildUnitName = GrandChildUnitSearched.FindElement(unit_title);
                                            if (GrandChildUnitName.Text == (searchresult + "GrandChildUnit1"))
                                            {
                                                if(!CreateList(GrandChildUnitSearched, searchresult + "GrandGrandChildList1"))
                                                {
                                                    Console.WriteLine("Error Creating List." + searchresult + "GrandGrandChildList1");
                                                    return false;
                                                }
                                                if (!CreateUnit(GrandChildUnitSearched, searchresult + "GrandGrandChildUnit1"))
                                                {
                                                    Console.WriteLine("Error Creating List." + searchresult + "GrandGrandChildUnit1");
                                                    return false;
                                                }
                                            }
                                            if (GrandChildUnitName.Text == (searchresult + "GrandChildUnit2"))
                                            {
                                                if(!CreateList(GrandChildUnitSearched, searchresult + "GrandGrandChildList2"))
                                                {
                                                    Console.WriteLine("Error Creating List." + searchresult + "GrandGrandChildList2");
                                                    return false;
                                                }
                                                if (!CreateUnit(GrandChildUnitSearched, searchresult + "GrandGrandChildUnit2"))
                                                {
                                                    Console.WriteLine("Error Creating List." + searchresult + "GrandGrandChildUnit2");
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (ChildUnitName.Text == (searchresult + "ChildUnit2"))
                                {
                                    if(!CreateList(ChildUnitSearched, searchresult + "GrandChildList2"))
                                    {
                                        Console.WriteLine("Error Creating List." + searchresult + "GrandChildList2");
                                        return false;
                                    }
                                    if(!CreateUnit(ChildUnitSearched, searchresult + "GrandChildUnit1"))
                                    {
                                        Console.WriteLine("Error Creating Unit." + searchresult + "GrandChildUnit1");
                                        return false;
                                    }
                                    if(!CreateUnit(ChildUnitSearched, searchresult + "GrandChildUnit2"))
                                    {
                                        Console.WriteLine("Error Creating Unit." + searchresult + "GrandChildUnit2");
                                        return false;
                                    }

                                    List<NgWebElement> GrandChildUnitsSearched = new List<NgWebElement>(ChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                    if (GrandChildUnitsSearched.Count > 0)
                                    {
                                        foreach (NgWebElement GrandChildUnitSearched in GrandChildUnitsSearched)
                                        {
                                            IWebElement GrandChildUnitName = GrandChildUnitSearched.FindElement(unit_title);
                                            if (GrandChildUnitName.Text == (searchresult + "GrandChildUnit1"))
                                            {
                                                if(!CreateList(GrandChildUnitSearched, searchresult + "GrandGrandChildList1"))
                                                {
                                                    Console.WriteLine("Error Creating List." + searchresult + "GrandGrandChildList1");
                                                    return false;
                                                }
                                                if (!CreateUnit(GrandChildUnitSearched, searchresult + "GrandGrandChildUnit1"))
                                                {
                                                    Console.WriteLine("Error Creating List." + searchresult + "GrandGrandChildUnit1");
                                                    return false;
                                                }
                                            }
                                            if (GrandChildUnitName.Text == (searchresult + "GrandChildUnit2"))
                                            {
                                                if(!CreateList(GrandChildUnitSearched, searchresult + "GrandGrandChildList2"))
                                                {
                                                    Console.WriteLine("Error Creating List." + searchresult + "GrandGrandChildList2");
                                                    return false;
                                                }
                                                if (!CreateUnit(GrandChildUnitSearched, searchresult + "GrandGrandChildUnit2"))
                                                {
                                                    Console.WriteLine("Error Creating List." + searchresult + "GrandGrandChildUnit2");
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
            return true;
        }

        public bool CreateList(NgWebElement ListCreatedfor, string ListNameString)
        {
            IWebElement EditIcon = ListCreatedfor.FindElement(edit_icon_locator);
            Klick.On(EditIcon);
            IWebElement PlusIcon = ListCreatedfor.FindElement(add_child_button_locator);
            Klick.On(PlusIcon);
            IWebElement ListButton = ListCreatedfor.FindElement(list_button_locator);
            Klick.On(ListButton);
            FillInNew_UnitorList_Form(ListNameString, KortextGlobals.UnitCourseIdentifierText);
            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "List added")
            {
                Console.WriteLine("Error while Creating a List." + ListNameString + "." + statusreturntext);
                return false;
            }
            else
            {
                Console.WriteLine("Creating List Successful." + ListNameString);
                Thread.Sleep(KortextGlobals.s);
                return true;
            }
        }

        public bool CreateUnit(NgWebElement UnitCreatedfor, string UnitNameString)
        {
            IWebElement EditIcon = UnitCreatedfor.FindElement(edit_icon_locator);
            Klick.On(EditIcon);
            IWebElement PlusIcon = UnitCreatedfor.FindElement(add_child_button_locator);
            Klick.On(PlusIcon);
            IWebElement UnitButton = UnitCreatedfor.FindElement(unit_button_locator);
            Klick.On(UnitButton);
            FillInNew_UnitorList_Form(UnitNameString, KortextGlobals.UnitCourseIdentifierText);
            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext != "List added")
            {
                Console.WriteLine("Error while Creating a Unit." + UnitNameString + "." + statusreturntext);
                return false;
            }
            else
            {
                Console.WriteLine("Creating Unit Successful." + UnitNameString);
                Thread.Sleep(KortextGlobals.s);
                return true;
            }
        }

        public bool MoveSourceToDest(string unitlist, string sourceunitpath, string sourcetitle, string destunitpath, string desttitle)
        {
            IWebElement sourceelement;
            IWebElement destelement;
       
            sourceelement = SourceElementIdentification(unitlist, sourceunitpath, sourcetitle);
            //sourceelement = SourceElementIdentification("List", "ParentUnit1,ParentUnit1ChildUnit1,ParentUnit1GrandChildUnit1", 4, "ParentUnit1GrandGrandChildList1");
            destelement = DestElementIdentification(destunitpath, desttitle);
            //destelement = DestElementIdentification("ParentUnit2", 2, "ChildUnit1");

            if (sourceelement == null || destelement == null)
            {
                Console.WriteLine("Could not find either the Source or Destination Path for Moving.");
                return false;
            }
            else
            {
                Thread.Sleep(KortextGlobals.l);
                HierarchyDragAndDrop(sourceelement, destelement);
                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext != "List updated")
                {
                    Console.WriteLine("Error while Moving List." + statusreturntext);
                    return false;
                }
                else
                {
                    Console.WriteLine("Moving List Successful");
                    return true;
                }
            }                        
        }

        public IWebElement SourceElementIdentification(string unitlist, string sourceunitpath, string sourcetitle)
        {
            int currentlevel = 0;
            int sourcelevel = 1;
                        
            var sourceunitshierarchy = sourceunitpath.Split(',');

            foreach (var sourceunithierarchy in sourceunitshierarchy)
            {
                sourcelevel = sourcelevel + 1;
            }

            if (sourceunitpath == "")
            {
                sourcelevel = 1;
            }

            IList<NgWebElement> SourceParentsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Model("adminHierarchy.hS.hierarchy")));
            if(SourceParentsSearched.Count > 0)
            {
                IList<NgWebElement> SourceParentUnitsSearched = new List<NgWebElement>(SourceParentsSearched[0].FindElements(NgBy.Repeater("node in adminHierarchy.hS.hierarchy")));
                if (SourceParentUnitsSearched.Count > 0)
                {
                    currentlevel = currentlevel + 1;
                    foreach (NgWebElement SourceParentUnitSearched in SourceParentUnitsSearched)
                    {
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SourceParentUnitSearched);
                        Driver.HighlightElement(SourceParentUnitSearched);
                        IWebElement EachParentTitle = SourceParentUnitSearched.FindElement(unit_title);
                        IList<IWebElement> SourceParentUniticon = new List<IWebElement>(SourceParentUnitSearched.FindElements(uniticon_locator));
                        if (currentlevel < sourcelevel)
                        {
                            if ((EachParentTitle.Text == sourceunitshierarchy[0]) && (SourceParentUniticon.Count > 0))
                            {
                                IList<IWebElement> sourceparentexpandicon = new List<IWebElement> (SourceParentUnitSearched.FindElements(expand_icon_locator));
                                if(sourceparentexpandicon.Count > 0)
                                {
                                    if (sourceparentexpandicon[0].Text == "chevron_right")
                                    {
                                        Klick.On(sourceparentexpandicon[0]);
                                    }
                                }
                                Thread.Sleep(KortextGlobals.s);
                                List<NgWebElement> SourceChildUnitsSearched = new List<NgWebElement>(SourceParentUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                if (SourceChildUnitsSearched.Count > 0)
                                {
                                    currentlevel = currentlevel + 1;
                                    foreach (NgWebElement SourceChildUnitSearched in SourceChildUnitsSearched)
                                    {
                                        IList<IWebElement> SourceChildUniticon = new List<IWebElement>(SourceChildUnitSearched.FindElements(uniticon_locator));
                                        IWebElement EachChildTitle = SourceChildUnitSearched.FindElement(unit_title);
                                        if (currentlevel < sourcelevel)
                                        {
                                            if ((EachChildTitle.Text == sourceunitshierarchy[1]) && (SourceChildUniticon.Count > 0))
                                            {
                                                IList<IWebElement> sourcechildexpandicon = new List<IWebElement> (SourceChildUnitSearched.FindElements(expand_icon_locator));
                                                if (sourcechildexpandicon.Count > 0)
                                                {
                                                    if (sourcechildexpandicon[0].Text == "chevron_right")
                                                    {
                                                        Klick.On(sourcechildexpandicon[0]);
                                                    }
                                                }
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SourceGrandChildUnitsSearched = new List<NgWebElement>(SourceChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                if (SourceChildUnitsSearched.Count > 0)
                                                {
                                                    currentlevel = currentlevel + 1;
                                                    foreach (NgWebElement SourceGrandChildUnitSearched in SourceGrandChildUnitsSearched)
                                                    {
                                                        IList<IWebElement> SourceGrandChildUniticon = new List<IWebElement>(SourceGrandChildUnitSearched.FindElements(uniticon_locator));
                                                        IWebElement EachGrandChildTitle = SourceGrandChildUnitSearched.FindElement(unit_title);
                                                        if (currentlevel < sourcelevel)
                                                        {
                                                            if ((EachGrandChildTitle.Text == sourceunitshierarchy[2]) && (SourceGrandChildUniticon.Count > 0))
                                                            {
                                                                IList<IWebElement> sourcegrandchildexpandicon = new List<IWebElement> (SourceGrandChildUnitSearched.FindElements(expand_icon_locator));
                                                                if (sourcegrandchildexpandicon.Count > 0)
                                                                {
                                                                    if (sourcegrandchildexpandicon[0].Text == "chevron_right")
                                                                    {
                                                                        Klick.On(sourcegrandchildexpandicon[0]);
                                                                    }
                                                                }
                                                                Thread.Sleep(KortextGlobals.s);
                                                                List<NgWebElement> SourceGrandGrandChildUnitsSearched = new List<NgWebElement>(SourceGrandChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                                if (SourceGrandGrandChildUnitsSearched.Count > 0)
                                                                {
                                                                    foreach (NgWebElement SourceGrandGrandChildUnitSearched in SourceGrandGrandChildUnitsSearched)
                                                                    {
                                                                        Thread.Sleep(KortextGlobals.s);
                                                                        if (unitlist == "List")
                                                                        {
                                                                            IList<IWebElement> SourceGrandGrandChildListicon = new List<IWebElement>(SourceGrandGrandChildUnitSearched.FindElements(listicon_locator));
                                                                            IWebElement EachGrandGrandChildTitle = SourceGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                            if ((SourceGrandGrandChildListicon.Count > 0) && (EachGrandGrandChildTitle.Text == sourcetitle))
                                                                            {
                                                                                return SourceGrandGrandChildUnitSearched;
                                                                            }
                                                                        }
                                                                        else if (unitlist == "Unit")
                                                                        {
                                                                            IList<IWebElement> SourceGrandGrandChildUniticon = new List<IWebElement>(SourceGrandGrandChildUnitSearched.FindElements(uniticon_locator));
                                                                            IWebElement EachGrandGrandChildTitle = SourceGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                            if ((SourceGrandGrandChildUniticon.Count > 0) && (EachGrandGrandChildTitle.Text == sourcetitle))
                                                                            {
                                                                                return SourceGrandGrandChildUnitSearched;
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                                        }
                                                                    }
                                                                    //Console.WriteLine("Could not find the Source List/Unit in Level 4.");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Thread.Sleep(KortextGlobals.s);
                                                            if (unitlist == "List")
                                                            {
                                                                IList<IWebElement> SourceGrandChildListicon = new List<IWebElement>(SourceGrandChildUnitSearched.FindElements(listicon_locator));
                                                                if ((SourceGrandChildListicon.Count > 0) && (EachGrandChildTitle.Text == sourcetitle))
                                                                {
                                                                    return SourceGrandChildUnitSearched;
                                                                }
                                                            }
                                                            else if (unitlist == "Unit")
                                                            {
                                                                if ((SourceGrandChildUniticon.Count > 0) && (EachGrandChildTitle.Text == sourcetitle))
                                                                {
                                                                    return SourceGrandChildUnitSearched;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                            }
                                                        }
                                                    }
                                                    //Console.WriteLine("Could not find the Source List/Unit in Level 3.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Thread.Sleep(KortextGlobals.s);
                                            if (unitlist == "List")
                                            {
                                                IList<IWebElement> SourceChildListicon = new List<IWebElement>(SourceChildUnitSearched.FindElements(listicon_locator));
                                                if ((SourceChildListicon.Count > 0) && (EachChildTitle.Text == sourcetitle))
                                                {
                                                    return SourceChildUnitSearched;
                                                }
                                            }
                                            else if (unitlist == "Unit")
                                            {
                                                if ((SourceChildUniticon.Count > 0) && (EachChildTitle.Text == sourcetitle))
                                                {
                                                    return SourceChildUnitSearched;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                            }
                                        }
                                    }
                                    //Console.WriteLine("Could not find the Source List/Unit in Level 2.");
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(KortextGlobals.s);
                            if (unitlist == "List")
                            {
                                IList<IWebElement> SourceListicon = new List<IWebElement>(SourceParentUnitSearched.FindElements(listicon_locator));
                                if ((SourceListicon.Count > 0) && (EachParentTitle.Text == sourcetitle))
                                {
                                    return SourceParentUnitSearched;
                                }
                            }
                            else if (unitlist == "Unit")
                            {
                                if ((SourceParentUniticon.Count > 0) && (EachParentTitle.Text == sourcetitle))
                                {
                                    return SourceParentUnitSearched;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                            }
                        }
                    }
                    //Console.WriteLine("Could not find the Source List/Unit in Level 1.");
                }
            }
            return null;
        }
        
        public IWebElement DestElementIdentification(string destunitpath, string desttitle)
        {
            int currentlevel = 0;
            int destlevel = 1;

            var destunitshierarchy = destunitpath.Split(',');

            foreach (var destunithierarchy in destunitshierarchy)
            {
                destlevel = destlevel + 1;
            }

            if (destunitpath == "")
            {
                destlevel = 1;
            }

                IList<NgWebElement> SourceParentsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Model("adminHierarchy.hS.hierarchy")));
            if (SourceParentsSearched.Count > 0)
            {
                List<NgWebElement> DestParentUnitsSearched = new List<NgWebElement>(SourceParentsSearched[1].FindElements(NgBy.Repeater("node in adminHierarchy.hS.hierarchy")));
                if (DestParentUnitsSearched.Count > 0)
                {
                    currentlevel = 1;
                    foreach (NgWebElement DestParentUnitSearched in DestParentUnitsSearched)
                    {
                        IWebElement EachParentTitle = DestParentUnitSearched.FindElement(unit_title);
                        IList<IWebElement> DestParentUniticon = new List<IWebElement>(DestParentUnitSearched.FindElements(uniticon_locator));
                        if (currentlevel < destlevel)
                        {
                            if ((EachParentTitle.Text == destunitshierarchy[0]) && (DestParentUniticon.Count > 0))
                            {
                                IList<IWebElement> destparentexpandicon = new List<IWebElement> (DestParentUnitSearched.FindElements(expand_icon_locator));
                                if (destparentexpandicon.Count > 0)
                                {
                                    if (destparentexpandicon[0].Text == "chevron_right")
                                    {
                                        Klick.On(destparentexpandicon[0]);
                                    }
                                }
                                Thread.Sleep(KortextGlobals.s);
                                List<NgWebElement> DestChildUnitsSearched = new List<NgWebElement>(DestParentUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                if (DestChildUnitsSearched.Count > 0)
                                {
                                    currentlevel = currentlevel + 1;
                                    foreach (NgWebElement DestChildUnitSearched in DestChildUnitsSearched)
                                    {
                                        IList<IWebElement> DestChildUniticon = new List<IWebElement>(DestChildUnitSearched.FindElements(uniticon_locator));
                                        IWebElement EachChildTitle = DestChildUnitSearched.FindElement(unit_title);
                                        if (currentlevel < destlevel)
                                        {
                                            if ((EachChildTitle.Text == destunitshierarchy[1]) && (DestChildUniticon.Count > 0))
                                            {
                                                IList<IWebElement> destchildexpandicon = new List<IWebElement> (DestChildUnitSearched.FindElements(expand_icon_locator));
                                                if (destchildexpandicon.Count > 0)
                                                {
                                                    if (destchildexpandicon[0].Text == "chevron_right")
                                                    {
                                                        Klick.On(destchildexpandicon[0]);
                                                    }
                                                }
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> DestGrandChildUnitsSearched = new List<NgWebElement>(DestChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                if (DestChildUnitsSearched.Count > 0)
                                                {
                                                    currentlevel = currentlevel + 1;
                                                    foreach (NgWebElement DestGrandChildUnitSearched in DestGrandChildUnitsSearched)
                                                    {
                                                        IList<IWebElement> DestGrandChildUniticon = new List<IWebElement>(DestGrandChildUnitSearched.FindElements(uniticon_locator));
                                                        IWebElement EachGrandChildTitle = DestGrandChildUnitSearched.FindElement(unit_title);
                                                        if (currentlevel < destlevel)
                                                        {
                                                            if ((EachGrandChildTitle.Text == destunitshierarchy[2]) && (DestGrandChildUniticon.Count > 0))
                                                            {
                                                                IList<IWebElement> destgrandchildexpandicon = new List<IWebElement> (DestGrandChildUnitSearched.FindElements(expand_icon_locator));
                                                                if (destgrandchildexpandicon.Count > 0)
                                                                {
                                                                    if (destgrandchildexpandicon[0].Text == "chevron_right")
                                                                    {
                                                                        Klick.On(destgrandchildexpandicon[0]);
                                                                    }
                                                                }
                                                                Thread.Sleep(KortextGlobals.s);
                                                                List<NgWebElement> DestGrandGrandChildUnitsSearched = new List<NgWebElement>(DestGrandChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                                if (DestGrandGrandChildUnitsSearched.Count > 0)
                                                                {
                                                                    foreach (NgWebElement DestGrandGrandChildUnitSearched in DestGrandGrandChildUnitsSearched)
                                                                    {
                                                                        Thread.Sleep(KortextGlobals.s);
                                                                        IList<IWebElement> DestGrandGrandChildUniticon = new List<IWebElement>(DestGrandGrandChildUnitSearched.FindElements(uniticon_locator));
                                                                        IWebElement EachGrandGrandChildTitle = DestGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                        if ((DestGrandGrandChildUniticon.Count > 0) && (EachGrandGrandChildTitle.Text == desttitle))
                                                                        {
                                                                            return DestGrandGrandChildUnitSearched;
                                                                        }
                                                                    }
                                                                    //Console.WriteLine("Could not find the Destination Unit in Level 4.");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Thread.Sleep(KortextGlobals.s);
                                                            if ((DestGrandChildUniticon.Count > 0) && (EachGrandChildTitle.Text == desttitle))
                                                            {
                                                                return DestGrandChildUnitSearched;
                                                            }
                                                        }
                                                    }
                                                    //Console.WriteLine("Could not find the Destination Unit in Level 3.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Thread.Sleep(KortextGlobals.s);
                                            if ((DestChildUniticon.Count > 0) && (EachChildTitle.Text == desttitle))
                                            {
                                                return DestChildUnitSearched;
                                            }
                                        }
                                    }
                                    //Console.WriteLine("Could not find the Destination Unit in Level 2.");
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(KortextGlobals.s);
                            if ((DestParentUniticon.Count > 0) && (EachParentTitle.Text == desttitle))
                            {
                                return DestParentUnitSearched;
                            }
                        }
                    }
                    //Console.WriteLine("Could not find the Destination Unit in Level 1.");
                }
            }
            return null;
        }

        
        public bool verifylistunitpresent(string unitlist, string sourceunitpath, string sourcetitle)
        {
            int currentlevel = 0;
            int sourcelevel = 1;

            var sourceunitshierarchy = sourceunitpath.Split(',');

            foreach (var sourceunithierarchy in sourceunitshierarchy)
            {
                sourcelevel = sourcelevel + 1;
            }

            if (sourceunitpath == "")
            {
                sourcelevel = 1;
            }

            IList<NgWebElement> SourceParentsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Model("adminHierarchy.hS.hierarchy")));
            if (SourceParentsSearched.Count > 0)
            {
                IList<NgWebElement> SourceParentUnitsSearched = new List<NgWebElement>(SourceParentsSearched[0].FindElements(NgBy.Repeater("node in adminHierarchy.hS.hierarchy")));
                if (SourceParentUnitsSearched.Count > 0)
                {
                    currentlevel = currentlevel + 1;
                    foreach (NgWebElement SourceParentUnitSearched in SourceParentUnitsSearched)
                    {
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SourceParentUnitSearched);
                        Driver.HighlightElement(SourceParentUnitSearched);
                        IWebElement EachParentTitle = SourceParentUnitSearched.FindElement(unit_title);
                        IList<IWebElement> SourceParentUniticon = new List<IWebElement>(SourceParentUnitSearched.FindElements(uniticon_locator));
                        if (currentlevel < sourcelevel)
                        {
                            if ((EachParentTitle.Text == sourceunitshierarchy[0]) && (SourceParentUniticon.Count > 0))
                            {
                                IList<IWebElement> sourceparentexpandicon = new List<IWebElement>(SourceParentUnitSearched.FindElements(expand_icon_locator));
                                if (sourceparentexpandicon.Count > 0)
                                {
                                    if (sourceparentexpandicon[0].Text == "chevron_right")
                                    {
                                        Klick.On(sourceparentexpandicon[0]);
                                    }
                                }
                                Thread.Sleep(KortextGlobals.s);
                                List<NgWebElement> SourceChildUnitsSearched = new List<NgWebElement>(SourceParentUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                if (SourceChildUnitsSearched.Count > 0)
                                {
                                    currentlevel = currentlevel + 1;
                                    foreach (NgWebElement SourceChildUnitSearched in SourceChildUnitsSearched)
                                    {
                                        IList<IWebElement> SourceChildUniticon = new List<IWebElement>(SourceChildUnitSearched.FindElements(uniticon_locator));
                                        IWebElement EachChildTitle = SourceChildUnitSearched.FindElement(unit_title);
                                        if (currentlevel < sourcelevel)
                                        {
                                            if ((EachChildTitle.Text == sourceunitshierarchy[1]) && (SourceChildUniticon.Count > 0))
                                            {
                                                IList<IWebElement> sourcechildexpandicon = new List<IWebElement>(SourceChildUnitSearched.FindElements(expand_icon_locator));
                                                if (sourcechildexpandicon.Count > 0)
                                                {
                                                    if (sourcechildexpandicon[0].Text == "chevron_right")
                                                    {
                                                        Klick.On(sourcechildexpandicon[0]);
                                                    }
                                                }
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SourceGrandChildUnitsSearched = new List<NgWebElement>(SourceChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                if (SourceChildUnitsSearched.Count > 0)
                                                {
                                                    currentlevel = currentlevel + 1;
                                                    foreach (NgWebElement SourceGrandChildUnitSearched in SourceGrandChildUnitsSearched)
                                                    {
                                                        IList<IWebElement> SourceGrandChildUniticon = new List<IWebElement>(SourceGrandChildUnitSearched.FindElements(uniticon_locator));
                                                        IWebElement EachGrandChildTitle = SourceGrandChildUnitSearched.FindElement(unit_title);
                                                        if (currentlevel < sourcelevel)
                                                        {
                                                            if ((EachGrandChildTitle.Text == sourceunitshierarchy[2]) && (SourceGrandChildUniticon.Count > 0))
                                                            {
                                                                IList<IWebElement> sourcegrandchildexpandicon = new List<IWebElement>(SourceGrandChildUnitSearched.FindElements(expand_icon_locator));
                                                                if (sourcegrandchildexpandicon.Count > 0)
                                                                {
                                                                    if (sourcegrandchildexpandicon[0].Text == "chevron_right")
                                                                    {
                                                                        Klick.On(sourcegrandchildexpandicon[0]);
                                                                    }
                                                                }
                                                                Thread.Sleep(KortextGlobals.s);
                                                                List<NgWebElement> SourceGrandGrandChildUnitsSearched = new List<NgWebElement>(SourceGrandChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                                if (SourceGrandGrandChildUnitsSearched.Count > 0)
                                                                {
                                                                    currentlevel = currentlevel + 1;
                                                                    foreach (NgWebElement SourceGrandGrandChildUnitSearched in SourceGrandGrandChildUnitsSearched)
                                                                    {
                                                                        IList<IWebElement> SourceGrandGrandChildUniticon = new List<IWebElement>(SourceGrandGrandChildUnitSearched.FindElements(uniticon_locator));
                                                                        IWebElement EachGrandGrandChildTitle = SourceGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                        if (currentlevel < sourcelevel)
                                                                        {
                                                                            if ((EachGrandGrandChildTitle.Text == sourceunitshierarchy[3]) && (SourceGrandGrandChildUniticon.Count > 0))
                                                                            {
                                                                                IList<IWebElement> sourcegrandgrandchildexpandicon = new List<IWebElement>(SourceGrandGrandChildUnitSearched.FindElements(expand_icon_locator));
                                                                                if (sourcegrandgrandchildexpandicon.Count > 0)
                                                                                {
                                                                                    if (sourcegrandgrandchildexpandicon[0].Text == "chevron_right")
                                                                                    {
                                                                                        Klick.On(sourcegrandgrandchildexpandicon[0]);
                                                                                    }
                                                                                }
                                                                                Thread.Sleep(KortextGlobals.s);
                                                                                List<NgWebElement> SourceGrandGrandGrandChildUnitsSearched = new List<NgWebElement>(SourceGrandGrandChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                                                if (SourceGrandGrandGrandChildUnitsSearched.Count > 0)
                                                                                {
                                                                                    foreach (NgWebElement SourceGrandGrandGrandChildUnitSearched in SourceGrandGrandGrandChildUnitsSearched)
                                                                                    {
                                                                                        if (unitlist == "List")
                                                                                        {
                                                                                            Thread.Sleep(KortextGlobals.s);
                                                                                            IList<IWebElement> SourceGrandGrandGrandChildListicon = new List<IWebElement>(SourceGrandGrandGrandChildUnitSearched.FindElements(listicon_locator));
                                                                                            IWebElement EachGrandGrandGrandChildTitle = SourceGrandGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                                            if ((SourceGrandGrandGrandChildListicon.Count > 0) && (EachGrandGrandGrandChildTitle.Text == sourcetitle))
                                                                                            {
                                                                                                return true;
                                                                                            }
                                                                                        }
                                                                                        else if (unitlist == "Unit")
                                                                                        {
                                                                                            IList<IWebElement> SourceGrandGrandGrandChildUniticon = new List<IWebElement>(SourceGrandGrandGrandChildUnitSearched.FindElements(uniticon_locator));
                                                                                            IWebElement EachGrandGrandGrandChildTitle = SourceGrandGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                                            if ((SourceGrandGrandGrandChildUniticon.Count > 0) && (EachGrandGrandGrandChildTitle.Text == sourcetitle))
                                                                                            {
                                                                                                return true;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                                                            return false;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Thread.Sleep(KortextGlobals.s);
                                                                            if (unitlist == "List")
                                                                            {
                                                                                IList<IWebElement> SourceGrandGrandChildListicon = new List<IWebElement>(SourceGrandGrandChildUnitSearched.FindElements(listicon_locator));
                                                                                if ((SourceGrandGrandChildListicon.Count > 0) && (EachGrandGrandChildTitle.Text == sourcetitle))
                                                                                {
                                                                                    return true;
                                                                                }
                                                                            }
                                                                            else if (unitlist == "Unit")
                                                                            {
                                                                                if ((SourceGrandGrandChildUniticon.Count > 0) && (EachGrandGrandChildTitle.Text == sourcetitle))
                                                                                {
                                                                                    return true;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                                                return false;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Thread.Sleep(KortextGlobals.s);
                                                            if (unitlist == "List")
                                                            {
                                                                IList<IWebElement> SourceGrandChildListicon = new List<IWebElement>(SourceGrandChildUnitSearched.FindElements(listicon_locator));
                                                                if ((SourceGrandChildListicon.Count > 0) && (EachGrandChildTitle.Text == sourcetitle))
                                                                {
                                                                    return true;
                                                                }
                                                            }
                                                            else if (unitlist == "Unit")
                                                            {
                                                                if ((SourceGrandChildUniticon.Count > 0) && (EachGrandChildTitle.Text == sourcetitle))
                                                                {
                                                                    return true;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                    //Console.WriteLine("Could not find the Source List/Unit in Level 3.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Thread.Sleep(KortextGlobals.s);
                                            if (unitlist == "List")
                                            {
                                                IList<IWebElement> SourceChildListicon = new List<IWebElement>(SourceChildUnitSearched.FindElements(listicon_locator));
                                                if ((SourceChildListicon.Count > 0) && (EachChildTitle.Text == sourcetitle))
                                                {
                                                    return true;
                                                }
                                            }
                                            else if (unitlist == "Unit")
                                            {
                                                if ((SourceChildUniticon.Count > 0) && (EachChildTitle.Text == sourcetitle))
                                                {
                                                    return true;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                return false;
                                            }
                                        }
                                    }
                                    //Console.WriteLine("Could not find the Source List/Unit in Level 2.");
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(KortextGlobals.s);
                            if (unitlist == "List")
                            {
                                IList<IWebElement> SourceListicon = new List<IWebElement>(SourceParentUnitSearched.FindElements(listicon_locator));
                                if ((SourceListicon.Count > 0) && (EachParentTitle.Text == sourcetitle))
                                {
                                    return true;
                                }
                            }
                            else if (unitlist == "Unit")
                            {
                                if ((SourceParentUniticon.Count > 0) && (EachParentTitle.Text == sourcetitle))
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                return false;
                            }
                        }
                    }
                    //Console.WriteLine("Could not find the Source List/Unit in Level 1.");
                }
            }
            return false;
        }

        public bool CloneListUnit()
        {
            try
            {
                Thread.Sleep(KortextGlobals.l);
                if (!createunitliststructure())
                {
                    Console.WriteLine("Error while Creating first Unit Structure");
                    return false;
                }
                else
                {
                    Console.WriteLine("Creating first Unit Structure Successful");
                }
                FirstParent = searchresult;

                if (!createunitliststructure())
                {
                    Console.WriteLine("Error while Creating second Unit Structure");
                    return false;
                }
                else
                {
                    Console.WriteLine("Creating second Unit Structure Successful");
                }
                SecondParent = searchresult;
                
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);
                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Lists - Cloning to Same Unit - Clone 2nd level list to 3rd level
                if (!CloneSourceToDest("List", FirstParent, FirstParent + "ChildList1", FirstParent, FirstParent + "ChildUnit2"))
                {
                    Console.WriteLine("Error while Cloning 2nd level list to 3rd level.Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "ChildList1"))
                    {
                        Console.WriteLine("List not present while verification after Cloning 2nd level list to 3rd level.Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning 2nd level list to 3rd level Successful.Same Unit");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone list from 3rd level to 2nd level (directly under parent unit).
                if (!CloneSourceToDest("List", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildList2", "", FirstParent))
                {
                    Console.WriteLine("Error while Cloning list from 3rd level to 2nd level.Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent, FirstParent + "GrandChildList2"))
                    {
                        Console.WriteLine("List not present while verification after Cloning list from 3rd level to 2nd level.Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning list from 3rd level to 2nd level Successful.Same Unit");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone list to new sub unit
                if (!CloneSourceToDest("List", FirstParent, FirstParent + "ChildList2", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit2", FirstParent + "GrandGrandChildUnit2"))
                {
                    Console.WriteLine("Error while Cloning list to new sub unit.Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit2," + FirstParent + "GrandGrandChildUnit2", FirstParent + "ChildList2"))
                    {
                        Console.WriteLine("List not present while verification after Cloning list to new sub unit.Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning list to new sub unit Successful.Same Unit");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Lists - Cloning to Same Unit
                if (!CloneSourceToDest("List", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildList1", FirstParent + "," + FirstParent + "ChildUnit1", FirstParent + "GrandChildUnit1"))
                {
                    Console.WriteLine("Error while Cloning List to Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildList1"))
                    {
                        Console.WriteLine("List not present while verification after Cloning List to Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning List to Same Unit Successful");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Lists - Cloning to Different Unit
                if (!CloneSourceToDest("List", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildList1", SecondParent, SecondParent + "ChildUnit1"))
                {
                    Console.WriteLine("Error while Cloning List to Different Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", SecondParent + "," + SecondParent + "ChildUnit1", FirstParent + "GrandGrandChildList1"))
                    {
                        Console.WriteLine("List not present while verification after Cloning List to Different Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning List to Different Unit Successful");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Lists - Cloning to Different Unit - Clone 2nd level list to 3rd level
                if (!CloneSourceToDest("List", SecondParent, SecondParent + "ChildList1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit1"))
                {
                    Console.WriteLine("Error while Cloning 2nd level list to 3rd level.Different Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", SecondParent + "ChildList1"))
                    {
                        Console.WriteLine("List not present while verification after Cloning 2nd level list to 3rd level.Different Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning 2nd level list to 3rd level Successful.Different Unit");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Lists - Cloning to Different Unit - Clone list to new sub unit
                if (!CloneSourceToDest("List", SecondParent, SecondParent + "ChildList2", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit2", FirstParent + "GrandGrandChildUnit2"))
                {
                    Console.WriteLine("Error while Cloning list to new sub unit.Different Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit2," + FirstParent + "GrandGrandChildUnit2", SecondParent + "ChildList2"))
                    {
                        Console.WriteLine("List not present while verification after Cloning list to new sub unit.Different Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning list to new sub unit Successful.Different Unit");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Lists - Cloning to Different Unit - Clone list from 3rd level to 2nd level (directly under parent unit).
                if (!CloneSourceToDest("List", SecondParent + "," + SecondParent + "ChildUnit2", SecondParent + "GrandChildList2", FirstParent, FirstParent + "ChildUnit1"))
                {
                    Console.WriteLine("Error while Cloning list from 3rd level to 2nd level.Different Unit.");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("List", FirstParent + "," + FirstParent + "ChildUnit1", SecondParent + "GrandChildList2"))
                    {
                        Console.WriteLine("List not present while verification after Cloning list from 3rd level to 2nd level.Different Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning list from 3rd level to 2nd level Successful.Different Unit.");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Units - Cloning to Same Unit
                if (!CloneSourceToDest("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildUnit1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit1"))
                {
                    Console.WriteLine("Error while Cloning Unit to Same Unit");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", FirstParent + "GrandGrandChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Cloning Unit to Same Unit");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Cloning Unit to Same Unit Successful");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Units - Clone Units in same unit to various locations with different sized hierarchies
                if (!CloneSourceToDest("Unit", FirstParent + "," + FirstParent + "ChildUnit1", FirstParent + "GrandChildUnit1", FirstParent + "," + FirstParent + "ChildUnit1", FirstParent + "GrandChildUnit2"))
                {
                    Console.WriteLine("Error while Clone Units in same unit to various locations with different sized hierarchies");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit1," + FirstParent + "GrandChildUnit2", FirstParent + "GrandChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Clone Units in same unit to various locations with different sized hierarchies");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Clone Units in same unit to various locations with different sized hierarchies Successful");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Units - Clone Units in same unit - Clone 2nd level unit to 3rd level
                if (!CloneSourceToDest("Unit", FirstParent, FirstParent + "ChildUnit1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit1"))
                {
                    Console.WriteLine("Error while Clone Units in same unit - Clone 2nd level unit to 3rd level");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit1", FirstParent + "ChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Clone Units in same unit - Clone 2nd level unit to 3rd level");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Clone Units in same unit - Clone 2nd level unit to 3rd level Successful");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Units - Clone Units in different unit to various locations with different sized hierarchies
                if (!CloneSourceToDest("Unit", SecondParent + "," + SecondParent + "ChildUnit2", SecondParent + "GrandChildUnit1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit2"))
                {
                    Console.WriteLine("Error while Clone Units in different unit to various locations with different sized hierarchies");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit2", SecondParent + "GrandChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Clone Units in different unit to various locations with different sized hierarchies");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Clone Units in different unit to various locations with different sized hierarchies Successful");
                    }
                }

                Klick.On(mainclonelistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Clone Units - Clone Units in different  unit - Clone 2nd level unit to 3rd level
                if (!CloneSourceToDest("Unit", SecondParent, SecondParent + "ChildUnit1", FirstParent + "," + FirstParent + "ChildUnit2", FirstParent + "GrandChildUnit2"))
                {
                    Console.WriteLine("Error while Clone Units in different  unit - Clone 2nd level unit to 3rd level");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    Driver.Instance.Navigate().Refresh();
                    Thread.Sleep(KortextGlobals.l);
                    if (!verifylistunitpresent("Unit", FirstParent + "," + FirstParent + "ChildUnit2," + FirstParent + "GrandChildUnit2", SecondParent + "ChildUnit1"))
                    {
                        Console.WriteLine("Unit not present while verification after Clone Units in different  unit - Clone 2nd level unit to 3rd level");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Clone Units in different  unit - Clone 2nd level unit to 3rd level Successful");
                    }
                }

                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                if (!DeleteWholeTree(FirstParent))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + FirstParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Deleting Unit Structure Successful." + FirstParent);
                }

                if (!DeleteWholeTree(SecondParent))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + SecondParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Deleting Unit Structure Successful." + SecondParent);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in Hierarchy Clone List/Unit: " + e.Message);
                return false;
            }
        }

        public bool CloneSourceToDest(string unitlist, string sourceunitpath, string sourcetitle, string destunitpath, string desttitle)
        {
            IWebElement sourceelement;
            IWebElement destelement;

            sourceelement = SourceElementIdentification(unitlist, sourceunitpath, sourcetitle);
            //sourceelement = SourceElementIdentification("List", "ParentUnit1,ParentUnit1ChildUnit1,ParentUnit1GrandChildUnit1", 4, "ParentUnit1GrandGrandChildList1");
            destelement = DestElementIdentification(destunitpath, desttitle);
            //destelement = DestElementIdentification("ParentUnit2", 2, "ChildUnit1");

            if (sourceelement == null || destelement == null)
            {
                Console.WriteLine("Could not find either the Source or Destination Path for Cloning.");
                return false;
            }
            else
            {
                IWebElement UnitIcon = destelement.FindElement(uniticon_locator);
                HierarchyCloning(sourceelement, UnitIcon);
                statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                if (statusreturntext == "Clone request is processing")
                {
                    for (int i = 0; i < 60; i++)
                    {
                        statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                        if (statusreturntext == "Clone request has been completed")
                        {
                            Console.WriteLine("Cloning List Successful");
                            return true;
                        }
                        Thread.Sleep(1000);
                    }
                }
                else if (statusreturntext == "Clone request has been completed")
                {
                    Console.WriteLine("Cloning List Successful");
                    return true;
                }
                else
                {
                    Console.WriteLine("Error while Cloning List");
                    return false;
                }
                return true;
            }
        }

        public bool RolloverListUnit()
        {
            try
            {
                Thread.Sleep(KortextGlobals.l);
                
                if (!createunitliststructure())
                {
                    Console.WriteLine("Error while Creating first Unit Structure");
                    return false;
                }
                else
                {
                    Console.WriteLine("Creating first Unit Structure Successful");
                }
                FirstParent = searchresult;

                if (!createunitliststructure())
                {
                    Console.WriteLine("Error while Creating second Unit Structure");
                    return false;
                }
                else
                {
                    Console.WriteLine("Creating second Unit Structure Successful");
                }
                SecondParent = searchresult;

                if (!createunitliststructure())
                {
                    Console.WriteLine("Error while Creating third Unit Structure");
                    return false;
                }
                else
                {
                    Console.WriteLine("Creating third Unit Structure Successful");
                }
                ThirdParent = searchresult;
                /*
                FirstParent = "ParentUnit1";
                SecondParent = "ParentUnit2";
                ThirdParent = "ParentUnit3";
                Pages.LandingPage.ClickOnMenu_HierarchyBtn();
                */
                Driver.Instance.Navigate().Refresh();
                Thread.Sleep(KortextGlobals.l);

                //Rollover Parent Unit - No Archive
                if (!RollingOver("Unit", "", FirstParent, "No Archive"))
                {
                    Console.WriteLine("Error while Rollover Parent Unit, No Archive");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    if (!verifyafterrollover("Unit", "", FirstParent, "No Archive"))
                    {
                        Console.WriteLine("Rolling Over NOT successful for Parent Unit, No Archive");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Rolling Over successful for Parent Unit, No Archive");
                    }
                }

                //Rollover Parent Unit - Archive
                if (!RollingOver("Unit", "",SecondParent, "Archive"))
                {
                    Console.WriteLine("Error while Rollover Parent Unit, Archive");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    if (!verifyafterrollover("Unit", "", SecondParent, "Archive"))
                    {
                        Console.WriteLine("Rolling Over NOT successful for Parent Unit, Archive");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Rolling Over successful for Parent Unit, Archive");
                    }
                }

                //Rollover List - No Archive
                if (!RollingOver("List", ThirdParent, ThirdParent + "ChildList2", "No Archive"))
                {
                    Console.WriteLine("Error while Rollover List, No Archive");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    if (!verifyafterrollover("List", ThirdParent, ThirdParent + "ChildList2", "No Archive"))
                    {
                        Console.WriteLine("Rolling Over NOT successful for List, No Archive");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Rolling Over successful for List, No Archive");
                    }
                }

                //Rollover List - Archive
                if (!RollingOver("List", ThirdParent, ThirdParent + "ChildList1", "Archive"))
                {
                    Console.WriteLine("Error while Rollover List, Archive");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    if (!verifyafterrollover("List", ThirdParent, ThirdParent + "ChildList1", "Archive"))
                    {
                        Console.WriteLine("Rolling Over NOT successful for List, Archive");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Rolling Over successful for List, Archive");
                    }
                }

                //Rollover - Hierarchy - Archive
                if (!RollingOver("Hierarchy", "","", "Archive"))
                {
                    Console.WriteLine("Error while Rollover Hierarchy, Archive");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    if (!verifyafterrollover("Hierarchy", "", "", "Archive"))
                    {
                        Console.WriteLine("Rolling Over NOT successful for Hierarchy, Archive");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Rolling Over successful for Hierarchy, Archive");
                    }
                }

                Klick.On(mainrolloverlistsbtn);
                Thread.Sleep(KortextGlobals.l);

                //Rollover - Hierarchy - No Archive
                if (!RollingOver("Hierarchy", "", "", "No Archive"))
                {
                    Console.WriteLine("Error while Rollover Hierarchy, No Archive");
                    return false;
                }
                else
                {
                    Thread.Sleep(KortextGlobals.s);
                    if (!verifyafterrollover("Hierarchy", "", "", "No Archive"))
                    {
                        Console.WriteLine("Rolling Over NOT successful for Hierarchy, No Archive");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Rolling Over successful for Hierarchy, No Archive");
                    }
                }

                if (!DeleteWholeTree(FirstParent))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + FirstParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Deleting Unit Structure Successful." + FirstParent);
                }

                if (!DeleteWholeTree(SecondParent))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + SecondParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Deleting Unit Structure Successful." + SecondParent);
                }

                if (!DeleteWholeTree(ThirdParent))
                {
                    Console.WriteLine("Error while Deleting Unit Structure." + ThirdParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Deleting Unit Structure Successful." + ThirdParent);
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in Rollover: " + e.Message);
                return false;
            }
        }

        public bool RollingOver(string unitlist, string sourceunitpath, string sourcetitle, string ArchiveOption)
        {

            IWebElement RolloverElement;
            IWebElement HierarchyElement;
            IWebElement Rolloverbutton;

            if (sourceunitpath == "" && sourcetitle == "")
            {
                Rolloverbutton = Driver.Instance.FindElement(rolloverentirehierarchy);

                Klick.On(Rolloverbutton);
                Thread.Sleep(KortextGlobals.s);

                if (ArchiveOption == "Archive")
                {
                    IWebElement RolloverArchiveButton = Driver.Instance.FindElement(rolloverarchiveicon);
                    Klick.On(RolloverArchiveButton);

                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                    if (statusreturntext == "Rollover has been completed")
                    {
                        Console.WriteLine("Rollover Hierarchy Archive Successful");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Error while Rollover Hierarchy Archive." + statusreturntext);
                        return false;
                    }
                }
                else if (ArchiveOption == "No Archive")
                {
                    IWebElement RolloverNoArchiveButton = Driver.Instance.FindElement(rollovernoarchiveicon);
                    Klick.On(RolloverNoArchiveButton);

                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                    if (statusreturntext == "Rollover has been completed")
                    {
                        Console.WriteLine("Rollover Hierarchy No Archive Successful");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Error while Rollover Hierarchy No Archive." + statusreturntext);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect Archive Option provided");
                    return false;
                }
            }
            else
            {
                HierarchyElement = SourceElementIdentification(unitlist, sourceunitpath, sourcetitle);

                if (!UnsuppressAndMetadataYear(HierarchyElement))
                {
                    Console.WriteLine("Error while Unsuppressing Unit or List");
                    return false;
                }

                Klick.On(mainrolloverlistsbtn);
                Thread.Sleep(KortextGlobals.l);

                RolloverElement = SourceElementIdentification(unitlist, sourceunitpath, sourcetitle);

                Rolloverbutton = RolloverElement.FindElement(rolllovericon);

                Klick.On(Rolloverbutton);
                Thread.Sleep(KortextGlobals.s);

                if (ArchiveOption == "Archive")
                {
                    IWebElement RolloverArchiveButton = RolloverElement.FindElement(rolloverarchiveicon);
                    Klick.On(RolloverArchiveButton);

                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                    if (statusreturntext == "Rollover has been completed")
                    {
                        Console.WriteLine("Rollover Archive Successful");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Error while Rollover Archive." + statusreturntext);
                        return false;
                    }
                }
                else if (ArchiveOption == "No Archive")
                {
                    IWebElement RolloverNoArchiveButton = RolloverElement.FindElement(rollovernoarchiveicon);
                    Klick.On(RolloverNoArchiveButton);

                    statusreturntext = Pages.PearlEditBuffer.StatusMessage();
                    if (statusreturntext == "Rollover has been completed")
                    {
                        Console.WriteLine("Rollover No Archive Successful");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Error while Rollover No Archive." + statusreturntext);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect Archive Option provided");
                    return false;
                }
            }
        }

        public bool verifyafterrollover(string unitlist, string sourceunitpath, string sourcetitle, string ArchiveOption)
        {
            Driver.Instance.Navigate().Refresh();
            Thread.Sleep(KortextGlobals.l);

            if(ArchiveOption == "No Archive")
            {
                if (!verifyafterrolloverlistunit(unitlist, sourceunitpath, sourcetitle, KortextGlobals.RolledOvedUnitYearText))
                {
                    Console.WriteLine("Error during Rollover. The RolledOver Unit verification failed.");
                    return false;
                }
            }
            else if(ArchiveOption == "Archive")
            {
                if (!verifyafterrolloverlistunit(unitlist, sourceunitpath, sourcetitle, KortextGlobals.RolledOvedUnitYearText))
                {
                    Console.WriteLine("Error during Rollover. The New RolledOver Unit verification failed.");
                    return false;
                }
                if (!verifyafterrolloverlistunit(unitlist, sourceunitpath, sourcetitle, KortextGlobals.PriortoRollOverUnitYear))
                {
                    Console.WriteLine("Error during Rollover. The Old RolledOver Unit verification failed.");
                    return false;
                }
            }
            return true;
        }

        public bool verifyafterrolloverlistunit(string unitlist, string sourceunitpath, string sourcetitle, string sourceyear)
        {
            int currentlevel = 0;
            int sourcelevel = 1;

            var sourceunitshierarchy = sourceunitpath.Split(',');

            foreach (var sourceunithierarchy in sourceunitshierarchy)
            {
                sourcelevel = sourcelevel + 1;
            }

            if (sourceunitpath == "")
            {
                sourcelevel = 1;
            }

            if (sourceunitpath == "" && sourcetitle == "")
            {
                sourcelevel = 1;
            }

            IList<NgWebElement> SourceParentsSearched = new List<NgWebElement>(Driver.ngDriver.FindElements(NgBy.Model("adminHierarchy.hS.hierarchy")));
            if (SourceParentsSearched.Count > 0)
            {
                IList<NgWebElement> SourceParentUnitsSearched = new List<NgWebElement>(SourceParentsSearched[0].FindElements(NgBy.Repeater("node in adminHierarchy.hS.hierarchy")));
                if (SourceParentUnitsSearched.Count > 0)
                {
                    currentlevel = currentlevel + 1;
                    foreach (NgWebElement SourceParentUnitSearched in SourceParentUnitsSearched)
                    {
                        ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", SourceParentUnitSearched);
                        Driver.HighlightElement(SourceParentUnitSearched);
                        IWebElement EachParentTitle = SourceParentUnitSearched.FindElement(unit_title);
                        IList<IWebElement> SourceParentUniticon = new List<IWebElement>(SourceParentUnitSearched.FindElements(uniticon_locator));
                        if (currentlevel < sourcelevel)
                        {
                            if ((EachParentTitle.Text == sourceunitshierarchy[0]) && (SourceParentUniticon.Count > 0))
                            {
                                IList<IWebElement> sourceparentexpandicon = new List<IWebElement>(SourceParentUnitSearched.FindElements(expand_icon_locator));
                                if (sourceparentexpandicon.Count > 0)
                                {
                                    if (sourceparentexpandicon[0].Text == "chevron_right")
                                    {
                                        Klick.On(sourceparentexpandicon[0]);
                                    }
                                }
                                Thread.Sleep(KortextGlobals.s);
                                List<NgWebElement> SourceChildUnitsSearched = new List<NgWebElement>(SourceParentUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                if (SourceChildUnitsSearched.Count > 0)
                                {
                                    currentlevel = currentlevel + 1;
                                    foreach (NgWebElement SourceChildUnitSearched in SourceChildUnitsSearched)
                                    {
                                        IList<IWebElement> SourceChildUniticon = new List<IWebElement>(SourceChildUnitSearched.FindElements(uniticon_locator));
                                        IWebElement EachChildTitle = SourceChildUnitSearched.FindElement(unit_title);
                                        if (currentlevel < sourcelevel)
                                        {
                                            if ((EachChildTitle.Text == sourceunitshierarchy[1]) && (SourceChildUniticon.Count > 0))
                                            {
                                                IList<IWebElement> sourcechildexpandicon = new List<IWebElement>(SourceChildUnitSearched.FindElements(expand_icon_locator));
                                                if (sourcechildexpandicon.Count > 0)
                                                {
                                                    if (sourcechildexpandicon[0].Text == "chevron_right")
                                                    {
                                                        Klick.On(sourcechildexpandicon[0]);
                                                    }
                                                }
                                                Thread.Sleep(KortextGlobals.s);
                                                List<NgWebElement> SourceGrandChildUnitsSearched = new List<NgWebElement>(SourceChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                if (SourceChildUnitsSearched.Count > 0)
                                                {
                                                    currentlevel = currentlevel + 1;
                                                    foreach (NgWebElement SourceGrandChildUnitSearched in SourceGrandChildUnitsSearched)
                                                    {
                                                        IList<IWebElement> SourceGrandChildUniticon = new List<IWebElement>(SourceGrandChildUnitSearched.FindElements(uniticon_locator));
                                                        IWebElement EachGrandChildTitle = SourceGrandChildUnitSearched.FindElement(unit_title);
                                                        if (currentlevel < sourcelevel)
                                                        {
                                                            if ((EachGrandChildTitle.Text == sourceunitshierarchy[2]) && (SourceGrandChildUniticon.Count > 0))
                                                            {
                                                                IList<IWebElement> sourcegrandchildexpandicon = new List<IWebElement>(SourceGrandChildUnitSearched.FindElements(expand_icon_locator));
                                                                if (sourcegrandchildexpandicon.Count > 0)
                                                                {
                                                                    if (sourcegrandchildexpandicon[0].Text == "chevron_right")
                                                                    {
                                                                        Klick.On(sourcegrandchildexpandicon[0]);
                                                                    }
                                                                }
                                                                Thread.Sleep(KortextGlobals.s);
                                                                List<NgWebElement> SourceGrandGrandChildUnitsSearched = new List<NgWebElement>(SourceGrandChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                                if (SourceGrandGrandChildUnitsSearched.Count > 0)
                                                                {
                                                                    currentlevel = currentlevel + 1;
                                                                    foreach (NgWebElement SourceGrandGrandChildUnitSearched in SourceGrandGrandChildUnitsSearched)
                                                                    {
                                                                        IList<IWebElement> SourceGrandGrandChildUniticon = new List<IWebElement>(SourceGrandGrandChildUnitSearched.FindElements(uniticon_locator));
                                                                        IWebElement EachGrandGrandChildTitle = SourceGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                        if (currentlevel < sourcelevel)
                                                                        {
                                                                            if ((EachGrandGrandChildTitle.Text == sourceunitshierarchy[3]) && (SourceGrandGrandChildUniticon.Count > 0))
                                                                            {
                                                                                IList<IWebElement> sourcegrandgrandchildexpandicon = new List<IWebElement>(SourceGrandGrandChildUnitSearched.FindElements(expand_icon_locator));
                                                                                if (sourcegrandgrandchildexpandicon.Count > 0)
                                                                                {
                                                                                    if (sourcegrandgrandchildexpandicon[0].Text == "chevron_right")
                                                                                    {
                                                                                        Klick.On(sourcegrandgrandchildexpandicon[0]);
                                                                                    }
                                                                                }
                                                                                Thread.Sleep(KortextGlobals.s);
                                                                                List<NgWebElement> SourceGrandGrandGrandChildUnitsSearched = new List<NgWebElement>(SourceGrandGrandChildUnitSearched.FindElements(NgBy.Repeater("node in node.descendants")));
                                                                                if (SourceGrandGrandGrandChildUnitsSearched.Count > 0)
                                                                                {
                                                                                    foreach (NgWebElement SourceGrandGrandGrandChildUnitSearched in SourceGrandGrandGrandChildUnitsSearched)
                                                                                    {
                                                                                        if (unitlist == "List")
                                                                                        {
                                                                                            Thread.Sleep(KortextGlobals.s);
                                                                                            IList<IWebElement> SourceGrandGrandGrandChildListicon = new List<IWebElement>(SourceGrandGrandGrandChildUnitSearched.FindElements(listicon_locator));
                                                                                            IWebElement EachGrandGrandGrandChildTitle = SourceGrandGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                                            IWebElement EachGrandGrandGrandChildYear = SourceGrandGrandGrandChildUnitSearched.FindElement(year_locator);
                                                                                            if ((SourceGrandGrandGrandChildListicon.Count > 0) && (EachGrandGrandGrandChildTitle.Text == sourcetitle) && (EachGrandGrandGrandChildYear.Text == sourceyear))
                                                                                            {
                                                                                                return true;
                                                                                            }
                                                                                        }
                                                                                        else if (unitlist == "Unit")
                                                                                        {
                                                                                            IList<IWebElement> SourceGrandGrandGrandChildUniticon = new List<IWebElement>(SourceGrandGrandGrandChildUnitSearched.FindElements(uniticon_locator));
                                                                                            IWebElement EachGrandGrandGrandChildTitle = SourceGrandGrandGrandChildUnitSearched.FindElement(unit_title);
                                                                                            IWebElement EachGrandGrandGrandChildYear = SourceGrandGrandGrandChildUnitSearched.FindElement(year_locator);
                                                                                            if ((SourceGrandGrandGrandChildUniticon.Count > 0) && (EachGrandGrandGrandChildTitle.Text == sourcetitle) && (EachGrandGrandGrandChildYear.Text == sourceyear))
                                                                                            {
                                                                                                return true;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                                                            return false;
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            Thread.Sleep(KortextGlobals.s);
                                                                            if (unitlist == "List")
                                                                            {
                                                                                IList<IWebElement> SourceGrandGrandChildListicon = new List<IWebElement>(SourceGrandGrandChildUnitSearched.FindElements(listicon_locator));
                                                                                IWebElement EachGrandGrandChildYear = SourceGrandGrandChildUnitSearched.FindElement(year_locator);
                                                                                if ((SourceGrandGrandChildListicon.Count > 0) && (EachGrandGrandChildTitle.Text == sourcetitle) && (EachGrandGrandChildYear.Text == sourceyear))
                                                                                {
                                                                                    return true;
                                                                                }
                                                                            }
                                                                            else if (unitlist == "Unit")
                                                                            {
                                                                                IWebElement EachGrandGrandChildYear = SourceGrandGrandChildUnitSearched.FindElement(year_locator);
                                                                                if ((SourceGrandGrandChildUniticon.Count > 0) && (EachGrandGrandChildTitle.Text == sourcetitle) && (EachGrandGrandChildYear.Text == sourceyear))
                                                                                {
                                                                                    return true;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                                                return false;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Thread.Sleep(KortextGlobals.s);
                                                            if (unitlist == "List")
                                                            {
                                                                IList<IWebElement> SourceGrandChildListicon = new List<IWebElement>(SourceGrandChildUnitSearched.FindElements(listicon_locator));
                                                                IWebElement EachGrandChildYear = SourceGrandChildUnitSearched.FindElement(year_locator);
                                                                if ((SourceGrandChildListicon.Count > 0) && (EachGrandChildTitle.Text == sourcetitle) && (EachGrandChildYear.Text == sourceyear))
                                                                {
                                                                    return true;
                                                                }
                                                            }
                                                            else if (unitlist == "Unit")
                                                            {
                                                                IWebElement EachGrandChildYear = SourceGrandChildUnitSearched.FindElement(year_locator);
                                                                if ((SourceGrandChildUniticon.Count > 0) && (EachGrandChildTitle.Text == sourcetitle) && (EachGrandChildYear.Text == sourceyear))
                                                                {
                                                                    return true;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                    //Console.WriteLine("Could not find the Source List/Unit in Level 3.");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Thread.Sleep(KortextGlobals.s);
                                            if (unitlist == "List")
                                            {
                                                IList<IWebElement> SourceChildListicon = new List<IWebElement>(SourceChildUnitSearched.FindElements(listicon_locator));
                                                IWebElement EachChildYear = SourceChildUnitSearched.FindElement(year_locator);
                                                if ((SourceChildListicon.Count > 0) && (EachChildTitle.Text == sourcetitle) && (EachChildYear.Text == sourceyear))
                                                {
                                                    return true;
                                                }
                                            }
                                            else if (unitlist == "Unit")
                                            {
                                                IWebElement EachChildYear = SourceChildUnitSearched.FindElement(year_locator);
                                                if ((SourceChildUniticon.Count > 0) && (EachChildTitle.Text == sourcetitle) && (EachChildYear.Text == sourceyear))
                                                {
                                                    return true;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                                return false;
                                            }
                                        }
                                    }
                                    //Console.WriteLine("Could not find the Source List/Unit in Level 2.");
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(KortextGlobals.s);
                            if (unitlist == "List")
                            {
                                IList<IWebElement> SourceListicon = new List<IWebElement>(SourceParentUnitSearched.FindElements(listicon_locator));
                                IWebElement EachParentYear = SourceParentUnitSearched.FindElement(year_locator);
                                if ((SourceListicon.Count > 0) && (sourcetitle == "") && (EachParentYear.Text == sourceyear))
                                {
                                    return true;
                                }
                                if ((SourceListicon.Count > 0) && (EachParentTitle.Text == sourcetitle) && (EachParentYear.Text == sourceyear))
                                {
                                    return true;
                                }
                            }
                            else if (unitlist == "Unit")
                            {
                                IWebElement EachParentYear = SourceParentUnitSearched.FindElement(year_locator);
                                if ((SourceParentUniticon.Count > 0) && (sourcetitle == "") && (EachParentYear.Text == sourceyear))
                                {
                                    return true;
                                }
                                if ((SourceParentUniticon.Count > 0) && (EachParentTitle.Text == sourcetitle) && (EachParentYear.Text == sourceyear))
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error. Whether Unit or List - not passed properly.");
                                return false;
                            }
                        }
                    }
                    //Console.WriteLine("Could not find the Source List/Unit in Level 1.");
                }
            }
            Console.WriteLine("Couldn't find the Rolled Over List/Unit.");
            return false;
        }

        public bool UnsuppressAndMetadataYear(IWebElement ElementtobeRolledover)
        {
            IWebElement EditButton = ElementtobeRolledover.FindElement(edit_icon_locator);
            Klick.On(EditButton);
            Thread.Sleep(KortextGlobals.s);
            IWebElement MetadataButton = ElementtobeRolledover.FindElement(edit_metadata_button_locator);
            Klick.On(MetadataButton);
            Thread.Sleep(KortextGlobals.s);
            WaitFind.FindElem(UnitYearInput, 10).Clear();
            UnitYearInput.SendKeys(KortextGlobals.UnitYearText);
            Klick.On(UnitFinishBtn);

            statusreturntext = Pages.PearlEditBuffer.StatusMessage();
            if (statusreturntext == "List updated")
            {
                Console.WriteLine("Updating List/Unit Year Successful");
            }
            else
            {
                Console.WriteLine("Error while updating List/Unit Year." + statusreturntext);
                return false;
            }

            IWebElement SuppressButton = ElementtobeRolledover.FindElement(suppress_indicator_locator);
            string SuppressButtonTooltip = SuppressButton.GetAttribute("uib-tooltip");
            if(SuppressButtonTooltip == "Unsuppress [Hidden, branch suppressed]")
            {
                Klick.On(SuppressButton);
                Thread.Sleep(KortextGlobals.s);
            }

            return true;
        }

        public bool SuppressUnsuppress()
        {
            try
            {
                IWebElement HierarchyElement;

                Thread.Sleep(KortextGlobals.l);
                if (!createunitliststructure())
                {
                    Console.WriteLine("Error while Creating first Unit Structure");
                    return false;
                }
                else
                {
                    Console.WriteLine("Creating first Unit Structure Successful");
                }
                FirstParent = searchresult;

                if(!UnsuppressWholeTree(FirstParent))
                {
                    Console.WriteLine("Error while Unsuppressing Whole Unit.");
                    return false;
                }

                HierarchyElement = SourceElementIdentification("Unit", "", FirstParent);

                IWebElement EditButton = HierarchyElement.FindElement(edit_icon_locator);
                Klick.On(EditButton);
                Thread.Sleep(KortextGlobals.s);
                IWebElement SuppressButton = HierarchyElement.FindElement(suppress_indicator_locator);

                //Clicking the Suppress Button twice before we actually check the state it is into. Clicking the Suppress/Unsuppress twice makes the Unit as a whole Suppress/Unsuppress, if any discrepencies are present in the child Units/Lists, they are uniformed.
                Klick.On(SuppressButton);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(SuppressButton);
                Thread.Sleep(KortextGlobals.s);

                string SuppressButtonTooltip = SuppressButton.GetAttribute("uib-tooltip");
                if (SuppressButtonTooltip == "Unsuppress [Hidden, branch suppressed]")
                {
                    Klick.On(SuppressButton);
                    Thread.Sleep(KortextGlobals.s);
                }

                Pages.LandingPage.Do_Logout();

                if(Pages.PearlRedMenuPage.SearchRebus(FirstParent) == "Displaying 0 of 0 results found\r\nclose")
                {
                    Console.WriteLine("Search showed No Results." + FirstParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Search Successful for Unsuppress." + FirstParent);
                }

                //Validate terms displayed on page match the original search term
                if(!Pages.PearlRedMenuPage.ValidateSearchResults(FirstParent))
                {
                    Console.WriteLine("Search Results displayed do not match search term");
                    return false;
                }
                else
                {
                    Console.WriteLine("Search Results validation successful." + FirstParent);
                }

                Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
                Pages.LandingPage.ClickOnMenu_HierarchyBtn();

                HierarchyElement = SourceElementIdentification("Unit", "", FirstParent);

                EditButton = HierarchyElement.FindElement(edit_icon_locator);
                Klick.On(EditButton);
                Thread.Sleep(KortextGlobals.s);
                SuppressButton = HierarchyElement.FindElement(suppress_indicator_locator);

                //Clicking the Suppress Button twice before we actually check the state it is into. Clicking the Suppress/Unsuppress twice makes the Unit as a whole Suppress/Unsuppress, if any discrepencies are present in the child Units/Lists, they are uniformed.
                Klick.On(SuppressButton);
                Thread.Sleep(KortextGlobals.s);
                Klick.On(SuppressButton);
                Thread.Sleep(KortextGlobals.s);

                SuppressButtonTooltip = SuppressButton.GetAttribute("uib-tooltip");
                if (SuppressButtonTooltip == "Suppress [Currently visible]")
                {
                    Klick.On(SuppressButton);
                    Thread.Sleep(KortextGlobals.s);
                }

                Pages.LandingPage.Do_Logout();

                if (Pages.PearlRedMenuPage.SearchRebus(FirstParent) != "Displaying 0 of 0 results found\r\nclose")
                {
                    Console.WriteLine("Search showed Results, which shouldn't be." + FirstParent);
                    return false;
                }
                else
                {
                    Console.WriteLine("Search Successful for Suppress." + FirstParent);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in Hierarchy Suppress/Unsuppress List/Unit: " + e.Message);
                return false;
            }

        }
        public bool UnsuppressWholeTree(string parentnode)
        {
            IWebElement TreetoSuppress = FindUnit(parentnode, " ");
            if (TreetoSuppress == null)
            {
                Console.WriteLine("Parent Unit " + parentnode + " does not exist");
                return false;
            }
            //found the parent - for each child, recursively expand it.
            ExpandUnit(TreetoSuppress);

            UnsuppressThisElement(TreetoSuppress);

            IList<IWebElement> childrencontainers = new List<IWebElement>(TreetoSuppress.FindElements(children_locator));
            // Console.WriteLine("here" + childrencontainers.Count);
            foreach (IWebElement node in childrencontainers)
            {
                //Console.WriteLine("InDeleteWholeTree****************************" + node.Text);
                ExpandWholeTreeAndUnsuppress(node);
            }
            return true;
        }
        private void ExpandWholeTreeAndUnsuppress(IWebElement childnode)  //recursive function that returns when a node is not expandable
        {
            //Function assumes that you have found the parent already and expanded it.
            if (ExpandUnit(childnode))
            {            //For each of the children of this expanded node, call the recursive function ExpandWholeTree with the child nodes name.
                IList<IWebElement> childrencontainers = new List<IWebElement>(childnode.FindElements(children_locator));
                //   Console.WriteLine("Children found " + childrencontainers.Count);
                if (childrencontainers.Count > 0)
                {
                    foreach (IWebElement child in childrencontainers)
                    { //find the title of child
                        IWebElement unitchildrentitle = child.FindElement(unit_title);
                        //   Console.WriteLine("Children: " + child.Text);
                        //For each of the children of this expanded node, call the recursive function ExpandWholeTree with the child nodes name.
                        ExpandWholeTreeAndUnsuppress(child);
                    }
                }
                else
                {
                    UnsuppressThisElement(childnode);
                    return;
                }
            }
            UnsuppressThisElement(childnode);
            return;
        }
        private bool UnsuppressThisElement(IWebElement UnittoSuppress)
        {
            Thread.Sleep(KortextGlobals.s);
            //click on the edit pencil to bring up the submenu
            //   Driver.DumpCurrentDOM();

            IWebElement trythis = UnittoSuppress.FindElement(edit_icon_locator);
            Klick.On(trythis);
            
            IWebElement suppressbutton = UnittoSuppress.FindElement(suppress_indicator_locator);

            //Clicking the Suppress Button twice before we actually check the state it is into. Clicking the Suppress/Unsuppress twice makes the Unit as a whole Suppress/Unsuppress, if any discrepencies are present in the child Units/Lists, they are uniformed.
            Klick.On(suppressbutton);
            Thread.Sleep(KortextGlobals.s);
            Klick.On(suppressbutton);
            Thread.Sleep(KortextGlobals.s);

            string SuppressButtonTooltip = suppressbutton.GetAttribute("uib-tooltip");
            if (SuppressButtonTooltip == "Unsuppress [Hidden, branch suppressed]")
            {
                Klick.On(suppressbutton);
                Thread.Sleep(KortextGlobals.s);
            }
            return true;
        }
    }
    public enum FieldToChange
    {
        Name,
        CourseIdentifier,
        ReadingPriorityTag1,
        ReadingPriorityTag2,
        NumberOfStudents,
        VisibleFrom,
        VisibleTo,
        CourseStart,
        CourseEnd,
        Year
    }
}


