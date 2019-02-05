using OpenQA.Selenium;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using System;
using OpenQA.Selenium.Support.UI;
using PearlFramework.Utilities;
using OpenQA.Selenium.Interactions;
using System.Threading;
using NUnit.Framework;

namespace PearlFramework
{

    public class PearlLoginPage
    {


        [FindsBy(How = How.Id, Using = "username")]
        protected IWebElement UsernameTxtBox
        {
            get; set;
        }

        [FindsBy(How = How.Id, Using = "password")]
        protected IWebElement PasswordTxtBox
        {
            get; set;
        }

        [FindsBy(How = How.Id, Using = "login-button")]
        protected IWebElement loginButton
        {
            get; set;
        }



        [FindsBy(How = How.Id, Using = "loginErrorMsg")]
        protected IWebElement LoginError
        {
            get;
            set;
        }
        
        

        //in line check to see if we are at the right page ie. log in page
        public bool IsAtPage()
        {
            return loginButton.Displayed;
        }

        public PearlLoginPage LoginAs(string password)
        {
            PasswordTxtBox.SendKeys(password);
            //loginButton.Click();
            Klick.On(loginButton);
            return this;
        }

        /// <summary>
        /// Successful login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 

        public PearlLoginPage LoginAs(string username, string password)
        {
            PerformLogin(username, password);
            return DoInitialize.PageElementsIn<PearlLoginPage>(); //returning this page allows us to capture the error messages.
        }
        
 
        /// <summary>
        /// Perform Login actions
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void PerformLogin(string username, string password)
        {
            try
            {
                Thread.Sleep(KortextGlobals.s);
             //   Console.WriteLine("Clicking main menu btn");
               // Pages.PearlRedMenuPage.ClickOnMainMenuBtn();
               // Console.WriteLine("after Clicking main menu btn");
                Pages.LandingPage.ClickOnLoginBtn();
           //  Console.WriteLine("Clicking login");
                // Klick.On(MenuLogin);

                WaitFind.FindElem(UsernameTxtBox, 10).Clear();
                UsernameTxtBox.SendKeys(username);
                PasswordTxtBox.Clear();
                PasswordTxtBox.SendKeys(password);
                WaitFind.FindElem(loginButton,10).Click();
                //  Klick.On(loginButton);
                //   Console.WriteLine(ToasterText.GetStatusMessage());
                Thread.Sleep(KortextGlobals.s);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in PerformLogin: " + e.Message);
            }
        }

        public string GetMessage()
        {
            try
            {
                WaitFind.FindElem(LoginError, 20);
                return LoginError.Text;
             }
            catch (Exception e)
            {
                Console.WriteLine("Exception in GetMessage: " + e.Message);
                return null;
            }
        }




    }


}



