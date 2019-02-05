using PearlFramework;
using NUnit.Framework;
using System.Threading;
using PearlFramework.Utilities;
using SatishTests.Utilities;
/// <summary>
//Hierarchy view - Move Lists, Clone Lists, and Rollover Lists
/// </summary>
/// 
namespace Tests
{
    [TestFixture("Chrome")]
   // [TestFixture("Edge")]
 //    // [TestFixture("IE11")]
    class HierarchySuppressRolloverCloneTests : SetupTearDown
    {
        public HierarchySuppressRolloverCloneTests(string b)
        {
            KortextGlobals.platformtype = b;
        }

    }
}
