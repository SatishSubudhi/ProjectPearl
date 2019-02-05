using NUnit.Framework;
using RelevantCodes.ExtentReports;
using RelevantCodes.ExtentReports.Model;
using PearlFramework;
using SatishTests.Utilities;
using PearlFramework.Utilities;
using System.IO;
using System;
using System.Reflection;
//using OpenQA.Selenium.Remote;
using System.Threading;
using NUnit.Framework.Interfaces;
//using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium;
//using OpenQA.Selenium.IE;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace SatishTests.Utilities
{
    public class SetupTearDown
    {
        protected static ReportingTasks _reportingTasks;
        protected static ExtentReports extentReports;

        /*        [OneTimeSetUp]
                public void OneTimeSetUp()
                {

                }
        */

        [SetUp]
        public void Init()
        {
            switch (KortextGlobals.platformtype)
            {
                //This needs to be fixed.  Throughout the whole application.
                case "Chrome":
                case "IE11":
                case "Edge":
                case "Firefox":
                    KortextGlobals.Country = "UK";
                    break;
            }
            //setup the test results reporting
            string TestResultsDir = KortextGlobals.assemblyFolder + "\\..\\..\\TestResults_";
            System.IO.Directory.CreateDirectory(TestResultsDir);
            string rep = TestResultsDir + "\\" + KortextGlobals.Country + "Pearl_TestResults_" + DateTime.Now.ToString("MM-dd-yyyy") + "_" + KortextGlobals.platformtype + ".html";
            extentReports = new ExtentReports(rep, false);
            extentReports.LoadConfig(KortextGlobals.absolutePath);
            Driver.Initialize(KortextGlobals.platformtype);
            _reportingTasks = new ReportingTasks(extentReports);
            extentReports.AddSystemInfo("platformtype", KortextGlobals.platformtype);
            Console.WriteLine("Browser:", KortextGlobals.platformtype);
            string v_browser = ((RemoteWebDriver)Driver.Instance).Capabilities.Version;
            extentReports.AddSystemInfo("platformtype version", v_browser);
            _reportingTasks.AddTestReport(""); //removing this line from all the tests.  Will now automatically create report as part of init.

            //go to the base address and wait for angularjs to load.
            Driver.Navigate_BaseAddress();
          //  Console.WriteLine("After navigate");
          //  Thread.Sleep(KortextGlobals.l);
        }

        [TearDown]
        public void Cleanup()
        {

            _reportingTasks.FinalizeTest();
            extentReports.Close();
    //        Pages.PearlLoginPage.Logout();
            Driver.Close();
        }
        /*
                [OneTimeTearDown]
                        public void OnetimeCleanup()
                        {
                            //e.EmailReport(KortextGlobals.reportnameIE11);
                            //e.EmailReport(KortextGlobals.reportnameEdge);

                        }
        */
    }
}
