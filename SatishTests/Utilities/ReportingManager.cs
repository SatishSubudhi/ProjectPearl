using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Remote;
using PearlFramework;
using System;
using PearlFramework.Utilities;

namespace SatishTests.Utilities
{
    class ReportingManager
    {

        /// Create new instance of Extent report
        private static readonly ExtentReports _instance = new ExtentReports(KortextGlobals.reportnameChrome, true);
        static ReportingManager() {
            Console.WriteLine("platformtype " + KortextGlobals.platformtype);
        }
        private ReportingManager() {
            Console.WriteLine("report " + KortextGlobals.reportnameChrome);
        }

        public static ExtentReports Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}

