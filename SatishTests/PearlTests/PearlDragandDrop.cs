using PearlFramework;
using NUnit.Framework;
using System.Threading;
using PearlFramework.Utilities;
using SatishTests.Utilities;


/// <summary>
/// UPDATED FOR THE STORE REDESIGN PROJECT
/// </summary>
namespace Tests
{

    [TestFixture("Chrome")]
   // [TestFixture("Edge")]
     // [TestFixture("IE11")]
    //[TestFixture("Chrome_NL")]
    //[TestFixture("Edge_NL")]
    //[TestFixture("IE11_NL")]
    //[TestFixture("Android_Chrome")]
    public class PearlDragandDrop : SetupTearDown
    {
        
        public PearlDragandDrop(string b)
        {
            KortextGlobals.platformtype = b;
        }

        [Category("Utility")]
        //can login with correct email and password
        [Test]
        public void Pearl_Drag_and_Drop()
        {
            //Pages.PearlLandingPage.EuCookie_PopupWindow_ClickOK();
            Thread.Sleep(KortextGlobals.s);
            //Pages.HeaderPage.SelectLogin();
            Pages.PearlLoginPage.LoginAs(KortextGlobals.Pearl_username, KortextGlobals.Pearl_password);
            Pages.PearlDragandDrop.GoTo();
            Pages.PearlDragandDrop.Dragit(0,6);
        }
    }
}
