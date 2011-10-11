using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace FlyFetch
{
    class PageInterceptor<T,TColl> : IPageHit where T : new() where TColl:IList<T>,new()
    {

        IPageProvider<T, TColl> pageFiller;
        int pageSize;
        TColl collection;
        HashSet<int> pagesHit = new HashSet<int>();

        public PageInterceptor(IPageProvider<T,TColl> pageFiller
                                , int pageSize
                                , TColl collection
                               
                                )
        {
            this.pageFiller = pageFiller;
            this.pageSize = pageSize;
            this.collection = collection;
        }
        #region IPageHit Members
        public void Hit(int npage)
        {
            bool fetch = false;
            lock (pagesHit)
            {
                if (!pagesHit.Contains(npage))
                {
                    pagesHit.Add(npage);
                    fetch = true;
                }
            }
            if (fetch)
            {
                pageFiller.Completed += (s, e) =>
                    {
                        lock (pagesHit)
                        {
                            pagesHit.Remove(npage);
                        }
                        foreach (var pe in collection.Skip(npage * pageSize).Take(pageSize).OfType<IPageableElement>())
                            pe.Loaded = true;

                    };
                //prepare single item data
                int first = npage * pageSize;
                for (int i = first; i < first + pageSize; ++i)
                {
                    collection[i] = new T();
                }
                pageFiller.GetAPage(collection, npage * pageSize, pageSize);
            }

        }
        #endregion
    }
}
