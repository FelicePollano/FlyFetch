using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace FlyFetch.DemoApp
{
    class MainWindowModel
    {
        public IEnumerable<IResult> Loaded()
        {
            using (var uov = NHHelper.Instance.OpenUnitOfWork())
            { 
            }
            yield break;
        }
    }
}
