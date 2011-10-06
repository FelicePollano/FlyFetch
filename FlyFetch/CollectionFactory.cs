using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;


namespace FlyFetch
{
    /*
    public static class CollectionFactory
    {
        public interface IHasRowNum
        {
            int RowNum { get; set; }
            bool Loaded { get; set; }
        }
        class PageInterceptor<T>:IInvokeWrapper where T:new()
        {
            class ProxyData
            {
                public int RowNum { get; set; }
                public bool Loaded { get; set; }
            }

            Dictionary<object, ProxyData> proxyData=new Dictionary<object,ProxyData>();
            HashSet<string> targetProperties ;
            HashSet<int> fetchingPages ;
            Action<Collection<T>, int, int> pageFiller;
            Collection<T> collection;
            int pageSize;
            T target;
            public PageInterceptor(Action<Collection<T>,int,int > pageFiller
                                    ,int pageSize
                                    ,T target
                                    ,Collection<T> collection
                                    ,HashSet<string> targetProperties
                                    ,HashSet<int> fetchingPages
                                    )
            {
                
                this.pageFiller = pageFiller;
                this.target = target;
                this.pageSize = pageSize;
                this.collection = collection;
                this.targetProperties = targetProperties;
                this.fetchingPages = fetchingPages;
            }
            #region IInvokeWrapper Members
            public void AfterInvoke(InvocationInfo info, object returnValue)
            {
            }
            public void BeforeInvoke(InvocationInfo info)
            {
            }
            private void GetPage(int page)
            {
                
                BackgroundWorker bkw = new BackgroundWorker();
                bkw.WorkerReportsProgress = true;
                bkw.ProgressChanged += (s, e) =>
                    {
                        var data = new { Page = 0 };
                        fetchingPages.Remove(Cast(data,e.UserState).Page);
                    };
                bkw.DoWork += (s, e) =>
                {
                    int p = (int)e.Argument;
                    pageFiller(collection, p * pageSize, pageSize);
                    for (int i = p * pageSize; i < p * pageSize + pageSize; ++i)
                    {
                        var hasRowNum = collection[i] as IHasRowNum;
                        if (null != hasRowNum)
                            hasRowNum.Loaded = true;
                    }
                    (s as BackgroundWorker).ReportProgress(100, new { Page = p });
                };
                bkw.RunWorkerAsync(page);
            }

            private void EnsureProxyData(object target)
            {
                if (!proxyData.ContainsKey(target))
                    proxyData[target] = new ProxyData();
            }
            public object DoInvoke(InvocationInfo info)
            {
                EnsureProxyData(target);
               
                if (info.TargetMethod.Name == "set_RowNum")
                {
                    proxyData[target].RowNum = Convert.ToInt32(info.Arguments[0]) ;
                    return null;
                }
                if (info.TargetMethod.Name == "get_RowNum")
                {
                    return proxyData[target].RowNum;
                }
                if (info.TargetMethod.Name == "set_Loaded")
                {
                    proxyData[target].Loaded = Convert.ToBoolean(info.Arguments[0]);
                    return null;
                }
                if (info.TargetMethod.Name == "get_Loaded")
                {
                    return proxyData[target].Loaded;
                }

                if (info.TargetMethod.Name.StartsWith("get_"))
                {
                    var pname = info.TargetMethod.Name.Substring(4);
                    if (targetProperties.Contains(pname))
                    {
                        if (!proxyData[target].Loaded)
                        {
                            int page = proxyData[target].RowNum / pageSize;
                            if (!fetchingPages.Contains(page))
                            {
                                fetchingPages.Add(page);
                                GetPage(page);
                            }
                        }
                    }
                }
                
                return  info.TargetMethod.Invoke(target, info.Arguments);
            }

            #endregion
        }
        static T Cast<T>(T typeHolder, Object x)
        {
            return (T)x;
        }

        //static LinFu.DynamicProxy.ProxyFactory factory = new LinFu.DynamicProxy.ProxyFactory();
        public static void Create<T>(int pageSize
                                                    , Action<Collection<T>, int, int> pageFiller
                                                     ,Func<int> getCount
                                                     ,Action<ObservableCollection<T>> created
                                                    ) where T:new()
        {

            BackgroundWorker wrk = new BackgroundWorker();
            wrk.WorkerReportsProgress = true;
            wrk.ProgressChanged += (s, e) =>
                {
                    var collection = new ObservableCollection<T>();
                    HashSet<int> fetchingPages = new HashSet<int>();
                    HashSet<string> targetProperties = new HashSet<string>();
                    foreach (var pi in typeof(T).GetProperties()
                    .Where(k => k.CanRead))
                    {
                        targetProperties.Add(pi.Name);
                    }


                    var data = new { Count = 0 };
                    for (int i = 0; i < Cast(data,e.UserState).Count; ++i)
                    {
                        
                        var t = factory.CreateProxy<T>(
                                                new PageInterceptor<T>(
                                                pageFiller
                                                , pageSize
                                                , new T()
                                                ,collection
                                                ,targetProperties
                                                ,fetchingPages
                                                )
                                            ,new[] { typeof(IHasRowNum) });
                        (t as IHasRowNum).RowNum = i;
                        
                        collection.Add(t);
                    }
                    created(collection);
                };
                 wrk.DoWork += (s, e) =>
                {
                    var fnc = e.Argument as Func<int>;
                    (s as BackgroundWorker).ReportProgress(100, new { Count = fnc() });
                };
            wrk.RunWorkerAsync(getCount);
        }
    }*/
}
