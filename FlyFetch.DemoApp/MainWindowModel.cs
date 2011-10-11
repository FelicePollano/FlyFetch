using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using Microsoft.Windows.Controls;


namespace FlyFetch.DemoApp
{
    class MainWindowModel:PropertyChangedBase,INotifyDataSourceHit
    {

        private bool dataSourceHit;

        public bool DataSourceHit
        {
            get { return dataSourceHit; }
            set { dataSourceHit = value; NotifyOfPropertyChange(() => DataSourceHit); }
        }

        private bool busy;

        public bool Busy
        {
            get { return busy; }
            set { busy = value; NotifyOfPropertyChange(() => Busy); }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; NotifyOfPropertyChange(() => Count); }
        }

        private ObservableCollection<SalesOrderHeaderViewItem> items;

        public ObservableCollection<SalesOrderHeaderViewItem> Items
        {
            get { return items; }
            set { items = value; NotifyOfPropertyChange(() => Items); }
        }
        

        public IEnumerable<IResult> Loaded()
        {
            Busy = true;
            var qo = new SalesOrderHeaderQueryObject(this,

                (exc) =>
                {
                    Execute.OnUIThread(()=>
                        MessageBox.Show(GetMessage(exc),"ERROR!",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Error)
                        );
                }
                );
            CollectionFactory.Create<SalesOrderHeaderViewItem, ObservableCollection<SalesOrderHeaderViewItem>>
                (
                  20
                  , qo
                  , qo
                  , created => { Items = created; Busy = false; Count = Items.Count; }
                );
            
            yield break;
        }

        private string GetMessage(Exception exc)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Error occured:");
            do
            {
                sb.AppendLine(exc.Message);
                exc = exc.InnerException;
            } while (exc != null);
            sb.AppendLine();
            sb.AppendLine("Please note that AdventureWorks sample database is required in order to run this sample.");
            sb.AppendLine("If it is installed, please set the correct connection string in file hibernate.cfg.xml");
            return sb.ToString();
        }
        

        #region INotifyDataSourceHit Members

        public void QueryInProgress(bool inProgress)
        {
            DataSourceHit = inProgress;
        }

        #endregion
    }
}
