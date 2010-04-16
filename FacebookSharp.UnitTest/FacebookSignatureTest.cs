using FacebookSharp.Signature;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
namespace FacebookSharp.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for FacebookSignatureTest and is intended
    ///to contain all FacebookSignatureTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FacebookSignatureTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Verify
        ///</summary>
        [TestMethod()]
        public void VerifyTest()
        {
            FacebookSignature target = new FacebookSignature("3221a15c4e2804c04da31670a7b64516", new Dictionary<string, string>()
{
    { "in_canvas", "1" },
    { "request_method", "GET" },
    { "friends", "4,6,..." },
    { "position_fix", "1" },
    { "locale", "en_US" },
    { "in_new_facebook", "1" },
    { "time", "1221071115.1896" },
    { "added", "1" },
    { "profile_update_time", "1220998418" },
    { "user", "2901279" },
    { "session_key", "9a7e04226b1a3c85823bfafd-2901279" },
    { "expires", "0" },
    { "api_key", "650503b8455d7ae1cd4524da50d88129" },
});
            bool expected = true; 
            bool actual;
            actual = target.Verify("86cd871c996910064ab9884459c58bab");
            Assert.AreEqual(expected, actual);
        }
    }
}
