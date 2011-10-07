using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Threading;

namespace FlyFetch.Test
{
    [TestClass]
    public class FetchingCollection
    {
        readonly int pageSize = 100;
        readonly int totalLen = 1000000;
        [TestMethod]
        public void BasicFactory()
        {
            ManualResetEvent created = new ManualResetEvent(false);
            ObservableCollection<SampleViewItem> coll=null;
            CollectionFactory.Create<SampleViewItem>(pageSize,
                                                    (c, first, count) => { },
                                                    () => totalLen,
                                                    (c) => { coll = c; created.Set(); }
                                                    );
            Assert.IsTrue(created.WaitOne(maxWait));
            Assert.IsNotNull(coll);
            Assert.AreEqual(totalLen, coll.Count);
            Assert.IsTrue(coll.All(k => k is IPageableElement));
            for (int i = 0; i < totalLen; i += pageSize)
            {
                Assert.AreEqual(i / pageSize, (coll[i] as IPageableElement).PageIndex);
                Assert.IsFalse((coll[i] as IPageableElement).Loaded);
                if (i < totalLen)
                    Assert.AreSame(coll[i], coll[i + 1]); // same object inside same page
            }
        }
        readonly TimeSpan maxWait = TimeSpan.FromMilliseconds(5000);
        [TestMethod]
        public void PageHitShouldLoadPagesFromDatasource()
        {
            ManualResetEvent created = new ManualResetEvent(false);
            ManualResetEvent fetched = new ManualResetEvent(false);
            ObservableCollection<SampleViewItem> coll = null;
            CollectionFactory.Create<SampleViewItem>(pageSize,
                                                    (c, first, count) => { GetAPage(c,first,count); fetched.Set(); },
                                                    () => totalLen,
                                                    (c) => { coll = c; created.Set(); }
                                                    );
            Assert.IsTrue(created.WaitOne(maxWait));
            var itemInpage0 = coll[pageSize / 2];
            var xxx = itemInpage0.S1; // force to load
            Assert.IsTrue(fetched.WaitOne(maxWait));
            Assert.IsTrue((itemInpage0 as IPageableElement).Loaded);
            Assert.AreEqual("S1=" + (pageSize / 2).ToString(), itemInpage0.S1);
        }

        private void GetAPage(Collection<SampleViewItem> c, int first, int count)
        {
            for (int i = first; i < i + count; ++i)
            {
                c[i].S1 = "S1=" + i.ToString();
            }
        }
    }
}
