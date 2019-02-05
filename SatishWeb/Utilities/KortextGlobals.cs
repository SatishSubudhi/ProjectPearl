using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using System;
using System.IO;
using System.Reflection;
using PearlFramework.Utilities;
using OpenQA.Selenium.Support.UI;

namespace PearlFramework.Utilities
{
    public static class KortextGlobals
    {

        //public static string Country = "NL"; //Netherlands
        //public static string Country = "UK"; 
        //vio
        //public static string device_name = "hammerhead";
        public static string device_name = "gt5note10wifi";
        public static string Android_ver = "6.0.1";
        public static string Country = "";
        public static Double tax_value1;
        public static Double tax_value2;
        public static bool AlreadyLockedOut = false;

        //global variable for Thread.Sleep
        public static int s = 2000;
        public static int l = 6000;
        public static int ll = 10000;
        public static int xl = 15000;
     
        public static string currency;
        //select the platformtype to run for
        public static string platformtype;
        public static string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string relativePath = "\\..\\..\\..\\ExtentConfig\\extent-config.xml";
        public static string absolutePath = Path.GetFullPath(assemblyFolder + relativePath);
        public static string reportnameChrome = KortextGlobals.assemblyFolder + "\\..\\..\\TestResults\\TestResults_Chrome.html";
        public static string reportnameIE11 = KortextGlobals.assemblyFolder + "\\..\\..\\TestResults\\TestResults_IE11.html";
        public static string reportnameEdge = KortextGlobals.assemblyFolder + "\\..\\..\\TestResults\\TestResults_Edge.html";
        //public static string ExcelVoucherBulkOrderPath = KortextGlobals.assemblyFolder + "\\..\\..\\TestResults\\TestResults_Edge.html";
        public static string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string pathDownloads = "C:\\Downloads";

        public static string LibRelativePath = "\\..\\..\\..\\Libraries";
        public static string LibAbsolutePath = Path.GetFullPath(assemblyFolder + LibRelativePath);
        public static string BaseAddress;
        public static string StoreBaseAdminAddress;
        public static string StoreBasketAddress;
        public static string StoreWishListAddress;
        public static string StoreVouchersAddress;
        public static string StoreCreateAccountAddress;
        public static string StoreBulkVouchersAddress;
        public static string StorePaymentMethod;
        public static string StoreCustomerInfo;
        public static string StoreLogin;
        public static string username;
        public static string password;
        public static string Pearl_username;
        public static string Pearl_password;
        public static string PayPal_username;
        public static string PayPal_password;
        public static string username_forlock;
        public static string password_forlock;
        public static string BookWithDiscount;
        public static string DiscountCode;
        public static string DiscountCodeEntirePurchase1;
        public static string DiscountCodeEntirePurchase2;
        public static string receiver1 = "RECEIVER1";
        public static string receiver2 = "RECEIVER2";
        public static string receiver_email = "vioung05@yahoo.com";
        public static string UnitCourseIdentifierText = "Test Course";
        public static string UnitTotalStudentsText = "1000";
        public static string UnitYearText = "2017";
        public static string RolledOvedUnitYearText = "18/19";
        public static string PriortoRollOverUnitYear = "17/18";
        public static void Addresses()
        {
            if (KortextGlobals.Country.Equals("UK"))
            {
                KortextGlobals.BaseAddress = "https://qa-auto.keylinks.org/#/";
                KortextGlobals.currency = "£";

                Pearl_username = "auto_admin";
                Pearl_password = "C00lQA4321!";

            }
       
        }

    }
}

