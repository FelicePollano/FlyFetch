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

        

        

        class DummyHitChecker : IPageHit
        {
            #region IPageHit Members

            public void Hit(int npage)
            {
               
            }

            #endregion
        }

        class TrackHitChecker : IPageHit
        {
            public TrackHitChecker()
            {
                Hits = new List<int>();
            }
            public List<int> Hits { get; private set; }
            #region IPageHit Members

            public void Hit(int npage)
            {
                Hits.Add(npage);
            }

            #endregion
        }
        readonly string CIAO = "CIAO";
        [TestMethod]
        public void InternalProxyFactoryCreateAWorkingType()
        {
            var item = ProxyFactory.CreateProxy<SampleViewItem>(new DummyHitChecker());
            (item as IPageableElement).Loaded = true;
            item.S1 = CIAO;
            Assert.AreEqual(CIAO, item.S1);
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
        [TestMethod]
        public void CheckCountOfDirtyPageHit()
        {
            TrackHitChecker hitTracker = new TrackHitChecker();
            var item1 = ProxyFactory.CreateProxy<SampleViewItem>(hitTracker);
            (item1 as IPageableElement).PageIndex=6;
            var read = item1.S1;
            read = item1.S2;
            read = item1.S3;
            item1.S4 = CIAO;
            Assert.AreEqual(3, hitTracker.Hits.Count);
            Assert.IsTrue(hitTracker.Hits.Any(k=>k==6));
            (item1 as IPageableElement).Loaded = true;
            read = item1.S4;
            Assert.AreEqual(3, hitTracker.Hits.Count);
            Assert.AreEqual(CIAO, read);
        }
    }
}
