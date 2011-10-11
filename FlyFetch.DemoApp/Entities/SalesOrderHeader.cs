using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFetch.DemoApp.Entities
{
    public class SalesOrderHeader
    {
        public virtual int SalesOrderID { get; set; }
        public virtual string SalesOrderNumber { get; set; }
        public virtual string Comment { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Employee SalesPerson { get; set; }
        public virtual DateTime OrderDate { get; set; }
        public virtual DateTime? ShipDate { get; set; }
        public virtual DateTime DueDate { get; set; }
    }
}
