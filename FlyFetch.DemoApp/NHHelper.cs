using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;

using NHibernate;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using FlyFetch.DemoApp.Entities;

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
            LoggerProvider.SetLoggersFactory(new QueryLoggerFactory());
            ModelMapper mapper = new ModelMapper(new SimpleModelInspector());

            mapper.Class<Contact>(
                k => { 
                    k.Id(i => i.ContactID, m => m.Generator(Generators.Native));
                    k.Schema("Person");
                } 
                );
            mapper.Class<Employee>(
                k => 
                {
                    k.Id(i => i.EmployeeID, m => m.Generator(Generators.Native));
                    k.Schema("HumanResources");
                    k.ManyToOne(c => c.Contact, m => { m.Column("ContactID"); m.Lazy(LazyRelation.NoLazy); m.Fetch(FetchKind.Join); });
                }
                );
            mapper.Class<SalesOrderHeader>(
                k =>
                {
                    k.Id(i => i.SalesOrderID,m=>m.Generator(Generators.Native));
                    k.Schema("Sales");
                    k.ManyToOne(c => c.SalesPerson, m => m.Column("SalesPersonID"));
                    k.ManyToOne(c => c.Contact, m => m.Column("ContactID"));
                }
                );

            var map = mapper.CompileMappingForAllExplicitlyAddedEntities();
            cfg.AddDeserializedMapping(map,string.Empty);
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
