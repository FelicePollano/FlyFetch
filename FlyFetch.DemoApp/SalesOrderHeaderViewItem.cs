using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.ComponentModel;

namespace FlyFetch.DemoApp
{
    public class SalesOrderHeaderViewItem:INotifyPropertyChanged
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
            set { salesOrderNumber = value; PropertyChanged(this, new PropertyChangedEventArgs("SalesOrderNumber")); }
        }

        private DateTime dueDate;

        public virtual DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; PropertyChanged(this, new PropertyChangedEventArgs("DueDate")); }
        }

        private DateTime orderDate;

        public virtual DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; PropertyChanged(this, new PropertyChangedEventArgs("OrderDate")); }
        }
        private DateTime? shipDate;

        public virtual DateTime? ShipDate
        {
            get { return shipDate; }
            set { shipDate = value; PropertyChanged(this, new PropertyChangedEventArgs("ShipDate")); }
        }

        private string contact;

        public virtual string   Contact
        {
            get { return contact; }
            set { contact = value; PropertyChanged(this, new PropertyChangedEventArgs("Contact")); }
        }
        private string agent;

        public virtual string Agent
        {
            get { return agent; }
            set { agent = value; PropertyChanged(this, new PropertyChangedEventArgs("Agent")); }
        }


        private string comment;

        public virtual string Comment
        {
            get { return comment; }
            set { comment = value; PropertyChanged(this, new PropertyChangedEventArgs("Comment")); }
        }



        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #endregion
    }
}
