using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Diagnostics;


namespace FlyFetch.DemoApp
{
    class TraceLogger:ILog
    {
        Type t;
        public TraceLogger(Type t)
        {
            this.t = t;
        }
        #region ILog Members

        public void Info(string format, params object[] args)
        {
            Trace.WriteLine( t.Name+"-INFO:"+string.Format(format,args) );
        }

        public void Warn(string format, params object[] args)
        {
            Trace.WriteLine(t.Name + "-WARN:" + string.Format(format, args));
        }

        public void Error(Exception exception)
        {
            Trace.WriteLine(t.Name + "-ERROR:" +exception.Message);
        }

        #endregion
    }
}
