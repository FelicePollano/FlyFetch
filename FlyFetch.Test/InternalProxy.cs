using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.ComponentModel;

namespace FlyFetch.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class InternalProxy
    {
        public InternalProxy()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        class DummyHitChecker : IPageHit
        {
            #region IPageHit Members

            public void Hit(int npage)
            {
               
            }

            #endregion
        }

        [TestMethod]
        public void UseInternalProxyFactory()
        {
            var item = ProxyFactory.CreateProxy<SampleViewItem>(new DummyHitChecker());
            (item as IPageableElement).Loaded = true;
            item.S1 = "CIAO";
            Assert.AreEqual("CIAO", item.S1);
            Assert.IsTrue(typeof(SampleViewItem).IsAssignableFrom(item.GetType()));
            var other = ProxyFactory.CreateProxy<OtherViewItem>(new DummyHitChecker());
            Assert.IsTrue(typeof(INotifyPropertyChanged).IsAssignableFrom(other.GetType()));
            IPageableElement pageable = item as IPageableElement;
            Assert.IsNotNull(pageable);
            pageable.Loaded = true;
            pageable.PageIndex = 1;
            Assert.IsTrue(pageable.Loaded);
            Assert.AreEqual(1, pageable.PageIndex);
        }
    }
}
