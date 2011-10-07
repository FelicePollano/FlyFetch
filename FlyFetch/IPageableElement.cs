using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFetch
{
    public interface IPageableElement
    {
        int PageIndex { get; set; }
        bool Loaded { get; set; }
    }
}
