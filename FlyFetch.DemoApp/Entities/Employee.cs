using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFetch.DemoApp.Entities
{
    public class Employee
    {
        public virtual int EmployeeID { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
