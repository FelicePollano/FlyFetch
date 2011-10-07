using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace FlyFetch
{
    class PageInterceptor<T> : IPageHit where T : new()
    {
          
        Action<Collection<T>, int, int> pageFiller;
        int pageSize;
        Collection<T> collection;
        HashSet<int> pagesHit = new HashSet<int>();

        public PageInterceptor(Action<Collection<T>, int, int> pageFiller
                                , int pageSize
                                , Collection<T> collection
                                ,Action<Exception> error
                                )
        {
            this.pageFiller = pageFiller;
            this.pageSize = pageSize;
            this.collection = collection;
        }
        #region IPageHit Members
        public void Hit(int npage)
        {
            if (pagesHit.Contains(npage))
            {
                pagesHit.Add(npage);
            }

        }
        #endregion
    }
}
