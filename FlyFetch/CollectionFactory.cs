using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;


namespace FlyFetch
{
    public static class CollectionFactory
    {
        public static void Create<T,TColl>(int pageSize
                                            , IPageProvider<T,TColl> pageProvider
                                                ,ICountProvider countProvider
                                                ,Action<TColl> created
                                            ) where T:new() where TColl:IList<T>,new()
        {
            if (null == countProvider)
                throw new ArgumentNullException("countProvider");
            if (null == pageProvider)
                throw new ArgumentNullException("pageProvider");
           countProvider.CountAvailable+=(s,e)
             =>
                {
                    var collection = new TColl();
                    HashSet<int> fetchingPages = new HashSet<int>();

                    var data = new { Count = 0 };
                    for (int i = 0; i < e.Count;i+=pageSize)
                    {
                        var t = ProxyFactory.CreateProxy<T>(new PageInterceptor<T,TColl>(pageProvider,pageSize,collection));
                        (t as IPageableElement).PageIndex = i / pageSize;
                        for (int j = i; j < i + pageSize && j<e.Count;++j )
                            collection.Add(t);
                    }
                    created(collection);
                };
           countProvider.GetCount();
        }
    }
}
