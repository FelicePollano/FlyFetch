using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;

using NHibernate;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;

namespace FlyFetch.DemoApp
{
    public class UnitOfWork : IDisposable
    {
        ISession session;
        ISessionFactory factory;
        public UnitOfWork(ISessionFactory factory)
        {
            this.factory = factory;
            session = factory.OpenSession();
            CurrentSessionContext.Bind(session);
        }
        #region IDisposable Members

        public void Dispose()
        {
            session.Close();
            CurrentSessionContext.Unbind(factory);
        }

        #endregion
    }
    public class NHHelper
    {
        public UnitOfWork OpenUnitOfWork()
        {
            return new UnitOfWork(factory);
        }

        public void EvictAll<T>()
        {
            factory.Evict(typeof(T));
        }

        static NHHelper instance;
        static readonly object padlock = new object();
        static public NHHelper Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (null == instance)
                        instance = new NHHelper();
                }
                return instance;
            } 
        }

        Configuration cfg;
        ISessionFactory factory;
        private NHHelper()
        {
            //NH
            cfg = new Configuration();
            cfg.Configure();
            
            //ModelMapper
           
            factory = cfg.BuildSessionFactory();
            
        }
        public Configuration GetConfig()
        {
            return cfg;
        }
       
        public ISession CurrentSession
        {
            get { return factory.GetCurrentSession(); }
        }
    }
}
