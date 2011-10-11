using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.ComponentModel;

namespace FlyFetch.DemoApp
{
    public class SalesOrderHeaderViewItem:PropertyChangedBase
    {
        public SalesOrderHeaderViewItem()
        {
            SalesOrderNumber =
                Contact =
                Agent =
                Comment = "Loading ...";
        }
        private string salesOrderNumber;

        public virtual string SalesOrderNumber
        {
            get { return salesOrderNumber; }
            set { salesOrderNumber = value; NotifyOfPropertyChange(() => SalesOrderNumber); }
        }

        private DateTime dueDate;

        public virtual DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; NotifyOfPropertyChange(() => DueDate); }
        }

        private DateTime orderDate;

        public virtual DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; NotifyOfPropertyChange(() => OrderDate); }
        }
        private DateTime? shipDate;

        public virtual DateTime? ShipDate
        {
            get { return shipDate; }
            set { shipDate = value; NotifyOfPropertyChange(() => ShipDate); }
        }

        private string contact;

        public virtual string   Contact
        {
            get { return contact; }
            set { contact = value; NotifyOfPropertyChange(() => Contact); }
        }
        private string agent;

        public virtual string Agent
        {
            get { return agent; }
            set { agent = value; NotifyOfPropertyChange(() => Agent); }
        }


        private string comment;

        public virtual string Comment
        {
            get { return comment; }
            set { comment = value; NotifyOfPropertyChange(() => Comment); }
        }
      
    }
}
