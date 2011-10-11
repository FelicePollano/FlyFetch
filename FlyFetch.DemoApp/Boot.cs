using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;


using System.Reflection;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;




 
namespace FlyFetch.DemoApp
{
    class Boot:Bootstrapper<MainWindowModel>
    {
        
        static Boot()
        {
            LogManager.GetLog=(t)=>new TraceLogger(t);
        }
        public Boot()
        {
           
        }
        protected override void Configure()
        {
            base.Configure();
        }
       

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
        }

      
        
        
    }
}
