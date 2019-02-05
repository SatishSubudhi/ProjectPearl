using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using Protractor;




namespace PearlFramework.Utilities
{
    public class DoInitialize
    {

        public static TPage PageElementsIn<TPage>() where TPage : new()
        {
            var page = new TPage();
            //Console.WriteLine("in doinitialize");

            PageFactory.InitElements(Driver.Instance, page);
           // Console.WriteLine("Just before waitforAngular in doInitialize.PageElementsIn ");
       //     Driver.ngDriver.WaitForAngular();

            return page;
        }


    }
}
