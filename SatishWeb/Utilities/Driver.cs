using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;
using PearlFramework.Utilities;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using Protractor;


namespace PearlFramework
{
    public class Driver
    {

        public static IWebDriver Instance { get; set; }

        public static IJavaScriptExecutor JSDriver;
        public static NgWebDriver ngDriver;

        public static string Title
        {
            get
            {
                return Instance.Title;
            }
        }


        public static void Initialize(string deviceplatform)
        {

            if (deviceplatform.Equals("Chrome") || deviceplatform.Equals("Chrome_NL"))
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--disable-extensions");
                options.AddArguments("disable-infobars");
                options.AddArgument("test-type");
                options.AddArguments("disable-popup-blocking");
                options.AddArgument("start-maximized");
                //options.AddArguments("download.default_directory", KortextGlobals.pathDownloads);
                options.AddUserProfilePreference("download.default_directory", @KortextGlobals.pathDownloads);
                Instance = new ChromeDriver(KortextGlobals.LibAbsolutePath, options, TimeSpan.FromMinutes(4));
               

            }
            if (deviceplatform.Contains("IE11"))
            {
                /************************************************************************
                 * Having trouble running ie11? Check your protected zones: 
                 * http://jimevansmusic.blogspot.ca/2012/08/youre-doing-it-wrong-protected-mode-and.html\\
                 * 
                 */
                //ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                //ieOptions.UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Accept;
                InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                ieOptions.EnablePersistentHover = true;
                //ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
              //  ieOptions.UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Accept;
                ieOptions.EnableNativeEvents = false;
           //     Console.WriteLine("Before to set instance:"+KortextGlobals.LibAbsolutePath);
                Instance = new InternetExplorerDriver(KortextGlobals.LibAbsolutePath, ieOptions, TimeSpan.FromMinutes(4));
             //   Console.WriteLine("After to set instance");

            }
            if (deviceplatform.Contains("Edge"))
            {
                //   Console.WriteLine("Before to set instance:" + KortextGlobals.LibAbsolutePath);
                EdgeOptions edgeOptions = new EdgeOptions();
                Instance = new EdgeDriver(KortextGlobals.LibAbsolutePath, edgeOptions, TimeSpan.FromMinutes(4));
                Console.WriteLine("After to set instance");
            }

            if (deviceplatform.Contains("Firefox"))
            {
                //add firefox location to your PATH and add an env variable: webdriver.gecko.driver pointing to the driver location

                //Environment.SetEnvironmentVariable("webdriver.gecko.driver", @"C:\\KORTEXT\\QA-Automation\\StoreAutomation\\Test Automation\\Libraries\\geckodriver.exe");
                //Environment.SetEnvironmentVariable("webdriver.gecko.driver", KortextGlobals.LibAbsolutePath + "\\geckodriver.exe");
                //FirefoxDriverService driverService = FirefoxDriverService.CreateDefaultService();
                //driverService.FirefoxBinaryPath = @"C:\\Program Files\\Mozilla Firefox\\firefox.exe";
                //driverService.HideCommandPromptWindow = true;
                //driverService.SuppressInitialDiagnosticInformation = true;
                //FirefoxOptions foptions = new FirefoxOptions();
                //foptions.AddAdditionalCapability("IsMarionette", true);
                //Instance = new FirefoxDriver(driverService, foptions, TimeSpan.FromSeconds(120));
                //Instance = new FirefoxDriver();

                //DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
                //capabilities.SetCapability("marionette", true);
                Instance = new FirefoxDriver();
            }
            Driver.Instance.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(140);
            JSDriver = (IJavaScriptExecutor)Driver.Instance;
           // var ngDriver = new ngdriver(Driver.Instance);
            ngDriver = new NgWebDriver(Driver.Instance);
            Driver.Instance.Manage().Window.Maximize();
        }

        public static void HighlightElement(IWebElement Element, string colour = "red", int Duration = 2)
        {
            string OriginalStyle = Element.GetAttribute("style");
            string bordercolour = "border: 2px solid " + colour + "; border-style: dashed;";

            JSDriver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                                   Element,
                                   "style",
                                   bordercolour);

            Thread.Sleep(Duration * 1000);
            JSDriver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2])",
                                   Element,
                                   "style",
                                   OriginalStyle);
        }

        public static void Navigate_BaseAddress()
        {
           // var wait = new WebDriverWait(Driver.ngDriver, TimeSpan.FromSeconds(5));
         //   WebDriverWait wait = new WebDriverWait(Driver.ngDriver.WrappedDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            //  Driver.Instance.Manage().Window.Maximize();
            try
            {
                KortextGlobals.Addresses();
                  Driver.Instance.Navigate().GoToUrl(KortextGlobals.BaseAddress);
             //   Driver.ngDriver.Navigate().GoToUrl(KortextGlobals.BaseAddress);
        //      Driver.ngDriver.WaitForAngular();
                //    Console.WriteLine("Navigating");
              
             
            }
            catch (Exception e)
            {

                Console.WriteLine("Exception going to the base url." + e.Message);
            }
        }
        //dumps the current DOMS html into the output.  Useful to see if you are missing anything not loaded.
        public static void DumpCurrentDOM()
        {
            String pagesource = Driver.Instance.PageSource;
            Console.WriteLine("Current DOM: " + pagesource);
      

        }

        public static void Close()
        {
          /*  switch (KortextGlobals.platformtype)
            {
                case "Chrome":
                case "IE11_UK":
                case "Edge_UK":
                case "Firefox_UK":
                case "Firefox_NL":
                    if (Instance != null)
                        Instance.Quit();
                    break;
            }*/
                if (Instance != null)
               Instance.Quit();
        }

    }

}