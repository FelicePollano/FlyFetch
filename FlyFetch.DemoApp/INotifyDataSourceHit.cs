using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFetch.DemoApp
{
    interface INotifyDataSourceHit
    {
        void QueryInProgress(bool inProgress);
    }
}
