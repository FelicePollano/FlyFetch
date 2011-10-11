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
            ObservableCollection<SampleViewItem> coll=null;
            CollectionFactory.Create<SampleViewItem, ObservableCollection<SampleViewItem>>(pageSize,
                                                    new FakePageProvider<ObservableCollection<SampleViewItem>>(),
                                                    new FakeCountProvider(totalLen),
                                                    (c) => { coll = c;  }
                                                    );
            
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
          
            ObservableCollection<SampleViewItem> coll = null;
            CollectionFactory.Create<SampleViewItem,ObservableCollection<SampleViewItem>>(pageSize,
                                                    new FakePageProvider<ObservableCollection<SampleViewItem>>(),
                                                    new FakeCountProvider(totalLen),
                                                    (c) => { coll = c;  }
                                                    );
            
            var itemInpage0 = coll[pageSize / 2];
            var xxx = itemInpage0.S1; // force to load

            int index = pageSize / 2;
            Assert.AreEqual("S1=" + index.ToString(), coll[index].S1);
        }

        private void GetAPage(Collection<SampleViewItem> c, int first, int count)
        {
            for (int i = first; i < i + count; ++i)
            {
                c[i].S1 = "S1=" + i.ToString();
            }
        }

        class FakeCountProvider : ICountProvider
        {
            int len;
            public FakeCountProvider(int len)
            {
                this.len = len;
            }

            #region ICountProvider Members

            public void GetCount()
            {
                CountAvailable(this, new CountEventArgs(len));
            }

            public event EventHandler<CountEventArgs> CountAvailable;

            #endregion
        }

        class FakePageProvider<TColl> : IPageProvider<SampleViewItem, TColl> where TColl : IList<SampleViewItem>
        {
            #region IPageProvider<T,TColl> Members

            public void GetAPage(TColl collection, int first, int count)
            {
                for (int i = first; i < first + count; ++i)
                {
                    collection[i].S1 = "S1=" + i.ToString();
                    collection[i].S2 = "S2=" + i.ToString();
                    collection[i].S3 = "S3=" + i.ToString();
                    collection[i].S4 = "S4=" + i.ToString();
                    collection[i].S5 = "S5=" + i.ToString();
                }
                Completed(this, EventArgs.Empty);
            }

            public event EventHandler Completed;

            #endregion
        }
    }
}
