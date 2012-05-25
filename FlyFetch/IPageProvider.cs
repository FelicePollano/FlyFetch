using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFetch
{
    public interface IPageProvider<T,TColl> where TColl:IList<T>
    {
        void GetAPage(TColl collection, int first, int count);
        event EventHandler Completed;
    }
}
