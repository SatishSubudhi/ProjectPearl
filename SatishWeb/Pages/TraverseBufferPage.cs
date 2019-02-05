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

namespace PearlFramework
{
    public class TraverseBufferPage
    {
   [FindsBy(How = How.ClassName, Using = "panel-group")]
        protected IWebElement sectiontable
        {
            get;
            set;
        }


        //       [FindsBy(How = How.XPath, Using = "//div[@id='list']/div[2]/div/div/div/button[2]")]
        [FindsBy(How = How.CssSelector, Using ="button[ng-click='actionCtrl.enterEditMode()']")]

        protected IWebElement EditListData
        {
            get; set;
        }

        public IWebElement FindSection(string sectionname)
        {//assumes you are already at buffer page.
            //Add Material to SubSection

        
            Thread.Sleep(KortextGlobals.l);
            //          IList<IWebElement> sections = new List<IWebElement>(sectiontable.FindElements(By.CssSelector("div[section='section']")));
            IList<IWebElement> sections = new List<IWebElement>(sectiontable.FindElements(By.CssSelector("div[section='section']")));

            Console.WriteLine("number of sections found" + sections.Count);

            //Go through each of the sections and look for a match to sectionname
            foreach (var sectionelement in sections)
            {
                //     Find each of the titles currently in this section.  First one should be the section name.
           IList<IWebElement> sectiontitle = new List<IWebElement>(sectionelement.FindElements(By.TagName("h2")));
                Console.WriteLine("Number of sectiontitles found"+sectiontitle.Count);
                if (sectiontitle.Count > 0)
                {
                    Driver.HighlightElement(sectiontitle[0]);
                    //      Console.WriteLine("section title:" + sectiontitle.Text);
                    if (sectiontitle[0].Text == sectionname)
                    {
                        //  Console.WriteLine("2Number of sectiontitles found" + sectiontitle.Count);
                        Console.WriteLine("Found section:" + sectiontitle[0].Text);
                        return sectionelement;

                    }
              
                }
               
            }
            return null;
        }


        public IWebElement FindSubSectionofSection(string sectionname, string subsectionname)
        {//assumes you are already at buffer page.
            IWebElement thissection = FindSection(sectionname);
            //a list of titles of the section and subsections have h2 tags.
            IList<IWebElement> subsection = new List<IWebElement>(thissection.FindElements(By.CssSelector("div[subsection='subsection']")));
            Console.WriteLine("SubSections found:" + subsection.Count);
            //Go through each of the sections and look for a match to sectionname
            foreach (var sectionelement in subsection)
            {
                //  Console.WriteLine(sectionelement.Text);

                //look for the element with the name Week 3  
                // Thread.Sleep(KortextGlobals.l);
                //     Find each of the titles currently in this section.  First one should be the section name.
                //     Find each of the titles currently in this section.  First one should be the section name.
                IList<IWebElement> sectiontitle = new List<IWebElement>(sectionelement.FindElements(By.TagName("h2")));
                if (sectiontitle.Count > 0)
                {
                    Driver.HighlightElement(sectiontitle[0]);

                    Console.WriteLine("subsection title:" + sectiontitle[0].Text);
                    if (sectiontitle[0].Text == subsectionname)
                    {
                        //  Console.WriteLine("2Number of sectiontitles found" + sectiontitle.Count);
                        Console.WriteLine("Found section:" + sectiontitle[0].Text);
                        return sectionelement;

                    }
                }
                

            }
            return null;
        }
        //must supply  a sectionname and subsection name
        public IWebElement FindMaterialIteminSubsection(string sectionname, string subsectionname, string materialname)
        {//assumes you are already at buffer page.
            //find the matching subsection:
            IWebElement thissubsection = FindSubSectionofSection(sectionname, subsectionname);
            //a list of all of the subsections in the section <sectionname>
          //  IList <IWebElement> materials = new List<IWebElement>(thissubsection.FindElements(By.CssSelector("div[item='item']")));
            IList<IWebElement> materials = new List<IWebElement>(thissubsection.FindElements(By.TagName("li")));

            Console.WriteLine("Materials found:" + materials.Count);
            //Go through each of the sections and look for a match to sectionname
            foreach (var materialelement in materials)
            {
                //  Console.WriteLine(sectionelement.Text);
                //look for the title of the material (h4 html)
                IList<IWebElement> materialtitle = new List<IWebElement>(materialelement.FindElements(By.TagName("h4")));
                if (materialtitle.Count > 0)
                {
                    Driver.HighlightElement(materialtitle[0]);

                    Console.WriteLine("material title:" + materialtitle[0].Text);
                    if (materialtitle[0].Text == materialname)
                    {
                        //  Console.WriteLine("2Number of sectiontitles found" + sectiontitle.Count);
                        Console.WriteLine("Found material:" + materialtitle[0].Text);
                        return materialelement;

                    }
                }


            }
            return null;
        }
        public IWebElement FindMaterialIteminSection(string sectionname,  string materialname)
        {//assumes you are already at buffer page.
            //find the matching subsection:
            IWebElement thissection = FindSection(sectionname);
        
            //a list of all of the subsections in the section <sectionname>
            //  IList <IWebElement> materials = new List<IWebElement>(thissubsection.FindElements(By.CssSelector("div[item='item']")));
            IList<IWebElement> materials = new List<IWebElement>(thissection.FindElements(By.TagName("li")));

            Console.WriteLine("Materials found:" + materials.Count);
            //Go through each of the sections and look for a match to sectionname
            foreach (var materialelement in materials)
            {
                //  Console.WriteLine(sectionelement.Text);
                //look for the title of the material (h4 html)
                IList<IWebElement> materialtitle = new List<IWebElement>(materialelement.FindElements(By.TagName("h4")));
                if (materialtitle.Count > 0)
                {
                    Driver.HighlightElement(materialtitle[0]);

                    Console.WriteLine("material title:" + materialtitle[0].Text);
                    if (materialtitle[0].Text == materialname)
                    {
                        //  Console.WriteLine("2Number of sectiontitles found" + sectiontitle.Count);
                        Console.WriteLine("Found material:" + materialtitle[0].Text);
                        return materialelement;

                    }
                }


            }
            return null;
        }
        public void EditSection(string sectionname)
        {
            //clicks on the edit button for the list (TestList1)
            EditThisList();

            IWebElement thissection = FindSection(sectionname);
            //finds add new material button for a section
            IWebElement addnewiteminsectionicon = thissection.FindElement(By.CssSelector("button[uib-tooltip='Add new item in section']"));

            //finds add new subsection button for a section
            IWebElement addnewsubsectioninsectionicon = thissection.FindElement(By.CssSelector("button[uib-tooltip='Add new subsection']"));
            Console.WriteLine("after finding icons");

            //Finds the Delete button for a section
            IWebElement deletesectionicon = thissection.FindElement(By.CssSelector("button[uib-tooltip='Delete']"));

            //Get the buttons to edit it.
            Driver.HighlightElement(addnewiteminsectionicon);
            Driver.HighlightElement(addnewsubsectioninsectionicon);
            Driver.HighlightElement(deletesectionicon);

        }
        public void EditSubSection(string sectionname, string subsectionname)
        {   //clicks on the edit button for the list (TestList1) .Assumes you are at TestList1 Page.
            EditThisList();

            IWebElement thissubsection = FindSubSectionofSection(sectionname, subsectionname);
            //finds add new material button for a section
            IWebElement addnewiteminsubsectionicon = thissubsection.FindElement(By.CssSelector("button[uib-tooltip='Add new item in subsection']"));

            //finds add new subsection button for a section
            IWebElement movethissubsectionicon = thissubsection.FindElement(By.CssSelector("div[uib-tooltip='Move this section']"));
            

            //Finds the Delete button for a section
            IWebElement deletesectionicon = thissubsection.FindElement(By.CssSelector("button[uib-tooltip='Delete']"));

            //Get the buttons to edit it.
            Driver.HighlightElement(addnewiteminsubsectionicon);
            Driver.HighlightElement(movethissubsectionicon);
            Driver.HighlightElement(deletesectionicon);

        }


        public void EditMaterialInSubsection(string sectionname, string subsectionname, string materialname)
        {//clicks on the edit button for the list (TestList1) .Assumes you are at TestList1 Page.
            EditThisList();
            //returns the container of hte material on the web page.
            IWebElement thismaterial = FindMaterialIteminSubsection(sectionname, subsectionname,materialname);

            //there are two request changes icon in this container.  
           IList<IWebElement> requestchangesicon =new List<IWebElement>(thismaterial.FindElements(By.CssSelector("button[uib-tooltip='Request changes']")));
            //finds add new subsection button for a section
            IWebElement addnewmaterialafterthisone_icon = thismaterial.FindElement(By.CssSelector("button[uib-tooltip='Add new item after this one']"));


            //Finds the Delete button for a section
            IWebElement deletematerialicon = thismaterial.FindElement(By.CssSelector("button[uib-tooltip='Delete this item']"));

            //Get the buttons to edit it.
            Driver.HighlightElement(requestchangesicon[0]);
            Driver.HighlightElement(addnewmaterialafterthisone_icon);
            Driver.HighlightElement(deletematerialicon);
        }
        public void EditMaterialInSection(string sectionname, string materialname)
        {//clicks on the edit button for the list (TestList1) .Assumes you are at TestList1 Page.
            EditThisList();
            //returns the container of hte material on the web page.
            IWebElement thismaterial = FindMaterialIteminSection(sectionname,  materialname);

            //there are two request changes icon in this container.  
            IList<IWebElement> requestchangesicon = new List<IWebElement>(thismaterial.FindElements(By.CssSelector("button[uib-tooltip='Request changes']")));
            //finds add new subsection button for a section
            IWebElement addnewmaterialafterthisone_icon = thismaterial.FindElement(By.CssSelector("button[uib-tooltip='Add new item after this one']"));


            //Finds the Delete button for a section
            IWebElement deletematerialicon = thismaterial.FindElement(By.CssSelector("button[uib-tooltip='Delete this item']"));

            //Get the buttons to edit it.
            Driver.HighlightElement(requestchangesicon[0]);
            Driver.HighlightElement(addnewmaterialafterthisone_icon);
            Driver.HighlightElement(deletematerialicon);
        }
        public void EditThisList()
        {
            Thread.Sleep(KortextGlobals.ll);
            WaitFind.FindElem(EditListData, 60);
            Klick.On(EditListData);
        }
    }
}
