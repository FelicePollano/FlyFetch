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
        
       

        public static void Create<T>(int pageSize
                                                    , Action<Collection<T>, int, int> pageFiller
                                                     , Func<int> getCount
                                                     , Action<ObservableCollection<T>> created
                                                    ) where T : new()
        {
            Create<T>(pageSize,
                pageFiller,
                getCount,
                created,
                delegate { }
                );
        }
       
        public static void Create<T>(int pageSize
                                                    , Action<Collection<T>, int, int> pageFiller
                                                     ,Func<int> getCount
                                                     ,Action<ObservableCollection<T>> created
                                                     ,Action<Exception> Error
                                                    ) where T:new()
        {

            BackgroundWorker wrk = new BackgroundWorker();
            wrk.WorkerReportsProgress = true;
            wrk.ProgressChanged += (s, e) =>
                {
                    var collection = new ObservableCollection<T>();
                    HashSet<int> fetchingPages = new HashSet<int>();

                    var data = new { Count = 0 };
                    for (int i = 0; i < Helpers.Cast(data,e.UserState).Count;i+=pageSize)
                    {
                        var t = ProxyFactory.CreateProxy<T>(new PageInterceptor<T>(pageFiller,pageSize,collection,Error));
                        (t as IPageableElement).PageIndex = i / pageSize;
                        for (int j = i; j < i + pageSize;++j )
                            collection.Add(t);
                    }
                    created(collection);
                };
                 wrk.DoWork += (s, e) =>
                {
                    var arg = new { F = new Func<int>(() => 0), E = new Action<Exception>(k => { }) };
                    arg = Helpers.Cast(arg, e.Argument);
                    try
                    {
                        (s as BackgroundWorker).ReportProgress(100, new { Count = arg.F() });
                    }
                    catch (Exception exc)
                    {
                        arg.E(exc);
                        (s as BackgroundWorker).ReportProgress(100, new { Count = 0 });
                    }
                };
                 wrk.RunWorkerAsync(new { F=getCount,E=Error });
        }
    }
}
