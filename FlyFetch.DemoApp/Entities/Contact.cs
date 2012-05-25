using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFetch.DemoApp.Entities
{
    public class Contact
    {
        public virtual int ContactID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
    }
}
