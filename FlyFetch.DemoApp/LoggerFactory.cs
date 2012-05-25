using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Diagnostics;

namespace FlyFetch.DemoApp
{
    class QueryLoggerFactory : ILoggerFactory
    {
        #region ILoggerFactory Members

        public NHibernate.IInternalLogger LoggerFor(Type type)
        {
            return new NullLogger();
        }

        public NHibernate.IInternalLogger LoggerFor(string keyName)
        {
            if (keyName == "NHibernate.SQL")
                return new QueryTraceLogger();
            else
                return new NullLogger();
        }

        #endregion
    }

    class QueryTraceLogger : IInternalLogger
    {
        #region IInternalLogger Members

        public void Debug(object message, Exception exception)
        {
            Trace.WriteLine(message);
        }

        public void Debug(object message)
        {

            Trace.WriteLine(message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            Trace.WriteLine(string.Format(format, args));
        }

        public void Error(object message, Exception exception)
        {
            Trace.WriteLine(message);
        }

        public void Error(object message)
        {
            Trace.WriteLine(message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Trace.WriteLine(string.Format(format, args));
        }

        public void Fatal(object message, Exception exception)
        {
            Trace.WriteLine(message);
        }

        public void Fatal(object message)
        {
            Trace.WriteLine(message);
        }

        public void Info(object message, Exception exception)
        {
            Trace.WriteLine(message);
        }

        public void Info(object message)
        {
            Trace.WriteLine(message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Trace.WriteLine(string.Format(format, args));
        }

        public bool IsDebugEnabled
        {
            get { return true; }
        }

        public bool IsErrorEnabled
        {
            get { return true; }
        }

        public bool IsFatalEnabled
        {
            get { return true; }
        }

        public bool IsInfoEnabled
        {
            get { return true; }
        }

        public bool IsWarnEnabled
        {
            get { return true; }
        }

        public void Warn(object message, Exception exception)
        {
            Trace.WriteLine(message);
        }

        public void Warn(object message)
        {
            Trace.WriteLine(message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Trace.WriteLine(string.Format(format, args));
        }

        #endregion
    }

    class NullLogger : IInternalLogger
    {
        #region IInternalLogger Members

        public void Debug(object message, Exception exception)
        {
        }

        public void Debug(object message)
        {
        }

        public void DebugFormat(string format, params object[] args)
        {
        }

        public void Error(object message, Exception exception)
        {
        }

        public void Error(object message)
        {
        }

        public void ErrorFormat(string format, params object[] args)
        {
        }

        public void Fatal(object message, Exception exception)
        {
        }

        public void Fatal(object message)
        {
        }

        public void Info(object message, Exception exception)
        {
        }

        public void Info(object message)
        {
        }

        public void InfoFormat(string format, params object[] args)
        {
        }

        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public bool IsErrorEnabled
        {
            get { return false; }
        }

        public bool IsFatalEnabled
        {
            get { return false; }
        }

        public bool IsInfoEnabled
        {
            get { return false; }
        }

        public bool IsWarnEnabled
        {
            get { return false; }
        }

        public void Warn(object message, Exception exception)
        {
        }

        public void Warn(object message)
        {
        }

        public void WarnFormat(string format, params object[] args)
        {
        }

        #endregion
    }

}
