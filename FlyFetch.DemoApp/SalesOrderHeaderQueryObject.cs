using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using NHibernate.Linq;
using FlyFetch.DemoApp.Entities;
using System.ComponentModel;

namespace FlyFetch.DemoApp
{
    class SalesOrderHeaderQueryObject:ICountProvider,IPageProvider<SalesOrderHeaderViewItem,ObservableCollection<SalesOrderHeaderViewItem>>
    {
        INotifyDataSourceHit notifyHit;
        Action<Exception> reportError;
        public SalesOrderHeaderQueryObject(INotifyDataSourceHit notifyHit, Action<Exception> reportError)
        {
            this.notifyHit = notifyHit;
            this.reportError = reportError;
        }
        #region ICountProvider Members

        public void GetCount()
        {
            BackgroundWorker wrk = new BackgroundWorker();
           
            wrk.DoWork += (s, e) =>
                {
                    try
                    {
                        notifyHit.QueryInProgress(true);
                        using (NHHelper.Instance.OpenUnitOfWork())
                        {
                            e.Result = NHHelper.Instance.CurrentSession.Query<SalesOrderHeader>()
                                .Count();
                        }
                    }
                    catch (Exception exc)
                    {
                        e.Result = 0;
                        reportError(exc);
                    }
                };
            wrk.RunWorkerCompleted+=(s,e)=>
                {
                    CountAvailable(this, new CountEventArgs((int)e.Result));
                    notifyHit.QueryInProgress(false);
                };
            wrk.RunWorkerAsync();
        }

        public event EventHandler<CountEventArgs> CountAvailable = delegate { };

        #endregion

        #region IPageProvider<SalesOrderHeaderViewItem,ObservableCollection<SalesOrderHeaderViewItem>> Members

        public void GetAPage(ObservableCollection<SalesOrderHeaderViewItem> collection, int first, int count)
        {
            BackgroundWorker wrk = new BackgroundWorker();

            wrk.DoWork += (s, e) =>
            {
                notifyHit.QueryInProgress(true);
                try
                {
                    using (NHHelper.Instance.OpenUnitOfWork())
                    {
                        var list = NHHelper.Instance.CurrentSession.Query<SalesOrderHeader>()
                            .Fetch(k => k.Contact)
                            .Fetch(k => k.SalesPerson).ThenFetch(k=>k.Contact)
                            .Skip(first)
                            .Take(count)
                            .ToList();


                        for (int i = 0; i < list.Count; ++i)
                        {
                            var r = list[i];
                            if (r.SalesPerson != null)
                                collection[first + i].Agent = string.Join(" ", new[] { r.SalesPerson.Contact.FirstName, r.SalesPerson.Contact.MiddleName ?? "", r.SalesPerson.Contact.LastName });
                            else
                                collection[first + i].Agent = "-";
                            collection[first + i].Comment = r.Comment ?? "-";
                            collection[first + i].Contact = string.Join(" ", new[] { r.Contact.FirstName, r.Contact.MiddleName ?? "", r.Contact.LastName });
                            collection[first + i].DueDate = r.DueDate;
                            collection[first + i].OrderDate = r.OrderDate;
                            collection[first + i].ShipDate = r.ShipDate;
                            collection[first + i].SalesOrderNumber = r.SalesOrderNumber;
                        }
                    }
                }
                catch (Exception exc)
                {
                    reportError(exc);
                }
            };
            wrk.RunWorkerCompleted += (s, e) =>
            {
                Completed(this, EventArgs.Empty);
                notifyHit.QueryInProgress(false);
            };
            wrk.RunWorkerAsync();
        }

        public event EventHandler Completed = delegate { };

        #endregion
    }
}
